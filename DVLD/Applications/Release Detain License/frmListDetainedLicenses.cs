using DVLD.Licenses.Detain_Licenses;
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

namespace DVLD.Applications.Release_Detain_License
{
    public partial class frmListDetainedLicenses : Form
    {
        DataTable _dtDetaindLicense;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }
        
        private void _RefreshData()
        {
            _dtDetaindLicense = clsDetainedLicense.GetAllDetainedLicenses();
            dgvListDetainedLicenses.DataSource = _dtDetaindLicense;
        }
        private void _SetDVGColumns()
        {
            cmbFilterBy.SelectedIndex = 0;
            txtFilterBy.Visible = false;

            if (dgvListDetainedLicenses.Rows.Count > 0)
            {
                dgvListDetainedLicenses.Columns[0].Width = 100;
                dgvListDetainedLicenses.Columns[0].HeaderText = "Detained ID";

                dgvListDetainedLicenses.Columns[1].Width = 100;
                dgvListDetainedLicenses.Columns[1].HeaderText = "L. ID";

                dgvListDetainedLicenses.Columns[2].HeaderText = "Detained Date";
                dgvListDetainedLicenses.Columns[2].Width = 190;

                dgvListDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvListDetainedLicenses.Columns[3].Width = 100;

                dgvListDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvListDetainedLicenses.Columns[4].Width = 150;

                dgvListDetainedLicenses.Columns[5].HeaderText = "Released Date";
                dgvListDetainedLicenses.Columns[5].Width = 190;

                dgvListDetainedLicenses.Columns[6].HeaderText = "N. No.";
                dgvListDetainedLicenses.Columns[6].Width = 100;

                dgvListDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvListDetainedLicenses.Columns[7].Width = 200;

                dgvListDetainedLicenses.Columns[8].HeaderText = "Release App. ID";
                dgvListDetainedLicenses.Columns[8].Width = 155;
            }

            lblNumberOfRecord.Text = dgvListDetainedLicenses.Rows.Count.ToString();
        }
        
        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _RefreshData();
            _SetDVGColumns();
        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterBy.Text == "None")
            {
                txtFilterBy.Visible = false;
                cmbReleased.Visible = false;
                _RefreshData();
            }
            else if (cmbFilterBy.Text == "Is Released")
            {
                txtFilterBy.Visible = false;
                cmbReleased.Visible = true;
                cmbReleased.SelectedIndex = 0;
            }
            else
            {
                txtFilterBy.Visible = true;
                cmbReleased.Visible = false;
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {

            string FilterString = "";


            switch (cmbFilterBy.Text)
            {
                case "None":
                    FilterString = "";
                    break;
                case "Detain ID":
                    FilterString = "DetainID";
                    break;
                case "National No.":
                    FilterString = "NationalNo";
                    break;
                case "Full Name":
                    FilterString = "FullName";
                    break;
                case "Release Application ID":
                    FilterString = "ReleaseApplicationID";
                    break;
                default:
                    FilterString = "";
                    break;
            }

            if (cmbFilterBy.Text.Trim() == "None" || txtFilterBy.Text.Trim() == "")
            {
                _dtDetaindLicense.DefaultView.RowFilter = "";
                _RefreshData();
                return;
            }

            if (FilterString == "DetainID" || FilterString == "ReleaseApplicationID")
            {
                _dtDetaindLicense.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterString, txtFilterBy.Text.Trim());
            }
            else if (FilterString == "NationalNo" || FilterString == "FullName")
            {
                _dtDetaindLicense.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterString, txtFilterBy.Text.Trim());
            }

            lblNumberOfRecord.Text = dgvListDetainedLicenses.Rows.Count.ToString();
        }

        private void cmbReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterString = "";
            string FilterBy = "IsReleased";

            switch (cmbReleased.Text)
            {
                case "All":
                    FilterString = "";
                    break;
                case "Yes":
                    FilterString = "1";
                    break;
                case "No":
                    FilterString = "0";
                    break;
                default:
                    FilterString = "";
                    break;
            }

            if (cmbReleased.Text == "All")
            {
                _dtDetaindLicense.DefaultView.RowFilter = "";
                _RefreshData();
                return;
            }
            else
            {
                _dtDetaindLicense.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterBy, FilterString);
            }
            lblNumberOfRecord.Text = dgvListDetainedLicenses.Rows.Count.ToString();

        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFilterBy.Text.Trim() == "Detain ID" || cmbFilterBy.Text.Trim() == "Release Application ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
                
        }

        private void tsmShowPersonDetails_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvListDetainedLicenses.CurrentRow.Cells[1].Value;
            clsLicense License = clsLicense.Find(LicenseID);
            if (License == null)
            {
                MessageBox.Show("No license with LicenseID = " + LicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmShowPersonInfo frm = new frmShowPersonInfo(License.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void tsmShowLicenseDetails_Click(object sender, EventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo((int)dgvListDetainedLicenses.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvListDetainedLicenses.CurrentRow.Cells[1].Value;
            clsLicense License = clsLicense.Find(LicenseID);
            if (License == null)
            {
                MessageBox.Show("No license with LicenseID = " + LicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(License.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
            _RefreshData();

        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
                _RefreshData();
        }

        private void tsmReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication((int)dgvListDetainedLicenses.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
                _RefreshData();
        }

        private void cmsIApplications_Opening(object sender, CancelEventArgs e)
        {
            int DetainLicenseID = (int)dgvListDetainedLicenses.CurrentRow.Cells[0].Value;

            clsDetainedLicense detainedLicense = clsDetainedLicense.Find(DetainLicenseID);


            if (detainedLicense == null)
            {

                MessageBox.Show("No detained license with ID = " + DetainLicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tsmReleaseDetainedLicense.Enabled = detainedLicense.IsReleased ?  false : true;
        }
    }
}
