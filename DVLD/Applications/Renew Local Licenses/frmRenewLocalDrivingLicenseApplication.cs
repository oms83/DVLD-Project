using DVLD.Global;
using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Renew_Local_Licenses
{
    public partial class frmRenewLocalDrivingLicenseApplication : Form
    {
        private clsLicense _License;
        private clsLicense _NewLicense;
        private float _TotalFees = 0;
        public frmRenewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        private void _RestNewLicenseInfo()
        {
            lblApplicationDate.Text = "[DD/MM/YYYY]";
            lblExpirationDate.Text = "[DD/MM/YYYY]";
            lblIssueDate.Text = "[DD/MM/YYYY]";

            lblLicenseFees.Text = "[$$$$]";
            lblApplicationFees.Text = "[$$$$]";
            lblTotalFees.Text = "[$$$$]";

            lblApplicationID.Text = "[????]";
            lblOldLicenseID.Text = "[????]";
            lblRenewedLicenseID.Text = "[????]";
            lblCreatedByUser.Text = GlobalSettings.CurrentUser.UserName;

            txtNotes.Text = "";

            btnRenew.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.EditPersonInfoEnabled = false;
        }

        private void frmRenewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _RestNewLicenseInfo();
        }

        private void _LoadNewLicenseInfo()
        {
            btnRenew.Enabled = true;
            ctrlDriverLicenseInfoWithFilter1.EditPersonInfoEnabled = true;
            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;

            lblApplicationDate.Text = clsFormat.DateFormat(DateTime.Now);
            lblExpirationDate.Text = clsFormat.DateFormat(DateTime.Now.AddYears(_License.LicenseClassesInfo.DefaultValidityLength));
            lblCreatedByUser.Text = GlobalSettings.CurrentUser.UserName;
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees.ToString();
            lblIssueDate.Text = clsFormat.DateFormat(DateTime.Now);
            lblLicenseFees.Text = _License.LicenseClassesInfo.ClassFees.ToString();
            lblOldLicenseID.Text = _License.LicenseID.ToString();

            _TotalFees = Convert.ToSingle(lblApplicationFees.Text.Trim()) + Convert.ToSingle(lblLicenseFees.Text.Trim());
            lblTotalFees.Text = _TotalFees.ToString();

        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int LicenseID)
        {
            _RestNewLicenseInfo();
            _License = clsLicense.Find(LicenseID);

            if (_License == null)
            {
                MessageBox.Show("Error: No License With ID = " +  LicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);  
                return;
            }
            if (!_License.IsLicenseExpired())
            {
                llShowLicenseHistory.Enabled = true;
                llShowLicenseInfo.Enabled = true;
                MessageBox.Show("Selected license is not yet expired, It will expire in: " +  _License.ExpirationDate, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!_License.IsActive)
            {
                llShowLicenseHistory.Enabled = true;
                llShowLicenseInfo.Enabled = true;
                MessageBox.Show("Selected license should be active", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadNewLicenseInfo();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to renew this license with ID = " + _License.LicenseID, "Confirm", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            _NewLicense = _License.Renew(GlobalSettings.CurrentUser.UserID, txtNotes.Text.Trim(), _TotalFees);

            if (_NewLicense != null)
            {
                lblApplicationID.Text = _NewLicense.ApplicationID.ToString();
                lblRenewedLicenseID.Text = _NewLicense.LicenseID.ToString();
                MessageBox.Show("The license was not renewed successfully with NewLicenseID = " + _NewLicense.LicenseID, "Success Renew", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: The license was not renewed successfully with NewLicenseID = " + _NewLicense.LicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmRenewLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.FocusFilter();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_License.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_NewLicense != null)
            {
                _NewLicense = clsLicense.Find(_NewLicense.LicenseID);
                frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicense.LicenseID);
                frm.ShowDialog();
            }
            else
            {
                frmShowLicenseInfo frm = new frmShowLicenseInfo(_License.LicenseID);
                frm.ShowDialog();
            }
        }
    }
}
