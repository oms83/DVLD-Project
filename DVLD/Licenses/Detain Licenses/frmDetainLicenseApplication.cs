using DVLD.Global;
using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Detain_Licenses
{
    public partial class frmDetainLicenseApplication : Form
    {
        clsLicense _License;
        int _LicenseID = -1;
        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }

        private void _RestLicenseInfo()
        {
            txtFineFees.Text = string.Empty;
            lblCreatedBy.Text = GlobalSettings.CurrentUser.UserName;
            lblDetainDate.Text = clsFormat.DateFormat(DateTime.Now);
            lblDetainID.Text = "[????]";
            lblLicenseID.Text = "[????]";
            btnDetain.Enabled = false;
        }

        private bool _DetainedLicenseConstraint(int LicenseID)
        {
            _License = clsLicense.Find(LicenseID);

            if (_License == null)
            {
                MessageBox.Show("Error: No License With ID = " + LicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_License.IsLicenseExpired())
            {
                llShowLicenseHistory.Enabled = true;
                llShowLicenseInfo.Enabled = true;
                MessageBox.Show("Selected license expired, you should renew it: ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!_License.IsActive)
            {
                llShowLicenseHistory.Enabled = true;
                llShowLicenseInfo.Enabled = true;
                MessageBox.Show("Selected license should be active", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //if (clsDetainedLicense.IsLicenseDetained(_License.LicenseID))
            //{
            //    llShowLicenseHistory.Enabled = true;
            //    llShowLicenseInfo.Enabled = true;
            //    MessageBox.Show("Selected license already is detained", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}

            return true;
        }

        private void _LoadLicenseInfo()
        {
            lblLicenseID.Text = _LicenseID.ToString();
            btnDetain.Enabled = true;
        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;

            _RestLicenseInfo();
            lblLicenseID.Text = _LicenseID.ToString();

            if (_LicenseID == -1)
            {
                return;
            }

            if (!_DetainedLicenseConstraint(_LicenseID))
            {
                return;
            }

            _LoadLicenseInfo();
        }

        private void frmDetainLicenseApplication_Load(object sender, EventArgs e)
        {
            _RestLicenseInfo();
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                txtFineFees.Focus();
                errorProvider1.SetError(txtFineFees, "This field required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFineFees, null);
            }
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {

            TextBox textBox = sender as TextBox;

            if (e.KeyChar == '.' && textBox.Text.Contains('.'))
            {
                // Dot is already present in the textbox, so ignore this key press
                e.Handled = true;
                return;
            }

            // Allow digits and control characters
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_License.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_License.LicenseID);
            frm.ShowDialog();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid, Please put the mouse over red icon(s) to show the error",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            float Fees = Convert.ToSingle(txtFineFees.Text.Trim());
            int DetainedLicenseID = _License.Detain(GlobalSettings.CurrentUser.UserID, Fees);
            if (DetainedLicenseID == -1) 
            {
                MessageBox.Show("Error: License With LicenseID = " + _LicenseID + " Does Not Detained Successfully", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnDetain.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            MessageBox.Show("License With LicenseID = " + _LicenseID + " Detained Successfully", "Confirm Detain",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblDetainID.Text = DetainedLicenseID.ToString();

        }
    }
}
