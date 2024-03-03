using DVLD.Applications.Application_Types;
using DVLD.Applications.International_Licenses;
using DVLD.Applications.Local_Driviing_Licenses;
using DVLD.Applications.Release_Detain_License;
using DVLD.Applications.Renew_Local_Licenses;
using DVLD.Applications.Replace_Lost_Or_Damaged_License;
using DVLD.Global;
using DVLD.Licenses.Detain_Licenses;
using DVLD.Tests.Test_Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmMain : Form
    {
        //Form _frmLogin;

        frmLogin _frmLogin;
        public frmMain(frmLogin frm)
        {
            InitializeComponent();
            _frmLogin = frm;
        }

        private void mtsManagePeople_Click(object sender, EventArgs e)
        {
            frmManagePeople frm = new frmManagePeople();
            frm.ShowDialog();
        }

        private void mtsUsers_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();  
            frm.ShowDialog();
        }

        private void tsmCurrentUserInfo_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(GlobalSettings.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void tsmChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(GlobalSettings.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void tsmLogout_Click(object sender, EventArgs e)
        {
            GlobalSettings.CurrentUser = null;
            this.Close();
            _frmLogin.Show();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (GlobalSettings.CurrentUser != null)
            {
                Application.Exit();
            }
        }

        private void tmsManageApplicationTypes_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes frm = new frmListApplicationTypes();
            frm.ShowDialog();
        }

        private void tmsManageTestTypes_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes();

            frm.ShowDialog();
        }

        private void tmsLocalLicense_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void tsmLocalDrivingLicenseApplications_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicenseApplication frm = new frmListLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void tmsRetakeTest_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicenseApplication frm = new frmListLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void mtsDrivers_Click(object sender, EventArgs e)
        {
            frmListDrivers frm = new frmListDrivers();
            frm.ShowDialog();
        }

        private void tmsRenewDrivingLicense_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication frm = new frmRenewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void tmsReplacementLostDamaged_Click(object sender, EventArgs e)
        {
            frmReplaceLostOrDamagedLicenseApplication frm = new frmReplaceLostOrDamagedLicenseApplication();
            frm.ShowDialog();
        }

        private void tsmInternationalDrivingLicenseApplications_Click(object sender, EventArgs e)
        {
            frmListInternationalLicenseApplications frm = new frmListInternationalLicenseApplications();
            frm.ShowDialog();
        }

        private void tmsInternationalLicense_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplications frm = new frmNewInternationalLicenseApplications();
            frm.ShowDialog();
        }

        private void tsmManageDetained_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void tsmDetainLicenses_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();    
            frm.ShowDialog();
        }

        private void tsmReleaseDetainLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void tmsReleaseDetainDrivingLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }
    }
}
