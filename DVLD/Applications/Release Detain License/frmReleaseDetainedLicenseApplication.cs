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

namespace DVLD.Applications.Release_Detain_License
{
    public partial class frmReleaseDetainedLicenseApplication : Form
    {
        private int _LicenseID = -1;
        private clsLicense _License;
        clsDetainedLicense _DetainedLicense;
        public frmReleaseDetainedLicenseApplication(int LicenseID)
        {
            InitializeComponent();

            _LicenseID = LicenseID;

            _RestData();

            lblLicenseID.Text = _LicenseID.ToString();
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            ctrlDriverLicenseInfoWithFilter1.txtFilterValue = _LicenseID;

            if (_LicenseID == -1)
            {
                return;
            }
            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_LicenseID);
            if (!_ReleaseLicenseConstraints())
            {
                return;
            }

            _LoadData();
        }

        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
        }

        private void _RestData()
        {
            lblApplicationFees.Text = "[$$$$]";
            lblTotalFees.Text = "[$$$$]";
            lblFineFees.Text = "[$$$$]"; 
            lblApplicationID.Text = "[????]";
            lblDetainID.Text = "[????]";
            lblCreatedByUser.Text = "[????]";
            lblDetainDate.Text = "[DD/MM/YYYY]";
        }
        private void _LoadData()
        {
            float ApplicationFees = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees;
            _DetainedLicense = clsDetainedLicense.FindByLicenseID(_LicenseID);

            if (_DetainedLicense == null )
            {
                MessageBox.Show("No detained license with LicenseID = " + _LicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationFees.Text = ApplicationFees.ToString();
            lblTotalFees.Text = (_DetainedLicense.FineFees + ApplicationFees).ToString();
            lblFineFees.Text = _DetainedLicense.FineFees.ToString();
            lblApplicationID.Text = "[????]";
            lblDetainID.Text  = _DetainedLicense.DetainID.ToString();
            lblCreatedByUser.Text = GlobalSettings.CurrentUser.UserName;
            lblDetainDate.Text = clsFormat.DateFormat(_DetainedLicense.DetainDate);
            lblLicenseID.Text = _LicenseID.ToString();

            btnRelease.Enabled = true;
        }
        private void frmReleaseDetainedLicenseApplication_Load(object sender, EventArgs e)
        {
            //_RestData();
        }

        private bool _ReleaseLicenseConstraints()
        {
            _License = clsLicense.Find(_LicenseID);

            if (_License == null)
            {
                llShowLicenseHistory.Enabled = false;
                llShowLicenseInfo.Enabled = false;
                MessageBox.Show("No license with LicenseID = " + _LicenseID, "Error",  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_License.IsLicenseExpired())
            {
                llShowLicenseHistory.Enabled = true;
                llShowLicenseInfo.Enabled = true;
                MessageBox.Show("Selected license expired, you should renew it: ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_License.IsActive)
            {
                llShowLicenseHistory.Enabled = true;
                llShowLicenseInfo.Enabled = true;
                MessageBox.Show("License with LicenseID = " + _LicenseID + " is not detain !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;
            btnRelease.Enabled = false;

            _RestData();

            lblLicenseID.Text = _LicenseID.ToString();

            if (_LicenseID == -1)
            {
                return;
            }

            if (!_ReleaseLicenseConstraints())
            {
                return;
            }

            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ApplicationID = -1;

            if (_License.ReleaseDetainedLicense(GlobalSettings.CurrentUser.UserID, ref ApplicationID))
            {
                MessageBox.Show("License With LicenseID = " + _LicenseID + " Released Successfully", "Confirm Release",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show("Error: License With LicenseID = " + _LicenseID + " Does Not Released Successfully", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationID.Text = ApplicationID.ToString();

            btnRelease.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_LicenseID);
            
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_License.LicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_License.DriverInfo.PersonID);
            frm.ShowDialog();
        }
    }
}
