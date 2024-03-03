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

namespace DVLD.Applications.Local_Driviing_Licenses.Controls
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        clsLicenseClasses _LicenseClasses;
        int _LocalDrivingApplicationID = -1;
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void ctrlDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
        }

        private void _RestLocalDrivingLincenseApplicationData()
        {
            lblAppliedFor.Text = string.Empty;
            lblDrivingLocalApplicationID.Text = string.Empty;
            llShowLicenseInfo.Visible = false;

            lblPassedTests.Text = "3/0";
        }

        private void _LoadData()
        {
            _LicenseClasses = clsLicenseClasses.Find(_LocalDrivingLicenseApplication.LicenseClassesID);

            lblAppliedFor.Text = _LicenseClasses.ClassName;
            lblDrivingLocalApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblPassedTests.Text = "3/" + _LocalDrivingLicenseApplication.GetPassedTestCount().ToString();

            //lblPassedTests.Text = "3/0";

            llShowLicenseInfo.Enabled = _LocalDrivingLicenseApplication.IsLicenseIssued();
            llShowLicenseInfo.ForeColor = Color.Blue;
            
        }
        public void LoadApplicationInfoByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            _LocalDrivingApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;

            if ( _LocalDrivingLicenseApplication == null)
            {
                _RestLocalDrivingLincenseApplicationData();

                MessageBox.Show("No Application With ID " + LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _LoadData();
            ctrlApplicationBasicInfo1.LoadApplicationInfoByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

        }
        public void LoadApplicationInfoByApplicationID(int ApplicationID)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByApplicationID(ApplicationID);
            
            _LocalDrivingApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;

            if (_LocalDrivingLicenseApplication == null)
            {
                _RestLocalDrivingLincenseApplicationData();

                MessageBox.Show("No Application With ID " + ApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _LoadData();
            ctrlApplicationBasicInfo1.LoadApplicationInfoByApplicationID(ApplicationID);

        }
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsLicense _License = clsLicense.FindByApplicationID(_LocalDrivingLicenseApplication.ApplicationID);
            if (_License == null)
            {
                MessageBox.Show("Error: No License With ApplicationID" + _LocalDrivingLicenseApplication.ApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            frmShowLicenseInfo @new = new frmShowLicenseInfo(_License.LicenseID);
            @new.ShowDialog();
        }
    }
}
