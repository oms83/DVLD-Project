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

namespace DVLD.Applications.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private clsApplication _ApplicationInfo;
        private clsLocalDrivingLicenseApplication _ApplicationLocalDrivingLicense;


        private int _ApplicationID = -1;
        private int _LocalDrivingLicenseApplicationID = -1;
        public clsLocalDrivingLicenseApplication SelectedApplicationLocalDrivingLicense
        {
            get => _ApplicationLocalDrivingLicense;
        }
        public clsApplication SelectedAppliacionInfo
        {
            get => _ApplicationInfo;
        }

        public int ApplicationID
        {
            get => _ApplicationID;
        }

        public int LocalDrivingLicenseApplicationID
        {
            get => _LocalDrivingLicenseApplicationID;
        }
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        private void ctrlApplicationBasicInfo_Load(object sender, EventArgs e)
        {
        }

        public void RestApplicationInfo()
        {
            llViewPersonInfo.Enabled = false;

            lblApplicant.Text = "[????]";
            lblApplicationID.Text = "[????]";
            lblApplicationType.Text = "[????]";
            lblFees.Text = "[$$$$]";
            lblDate.Text = "[DD/MM/YYYY]";
            lblStatusDate.Text = "[DD/MM/YYYY]";
            lblStatus.Text = "[????]";
            lblCreatedBy.Text = "[????]";
        }

        private void _LoadData()
        {
            
            clsApplicationType ApplicationType = clsApplicationType.Find(_ApplicationInfo.ApplicationTypeID);

            lblApplicant.Text = _ApplicationInfo.ApplicantPersonID.ToString();
            lblApplicationID.Text = _ApplicationInfo.ApplicationID.ToString();
            lblApplicationType.Text = ApplicationType.ApplicationTypeTitle;
            lblFees.Text = ApplicationType.ApplicationFees.ToString();
            lblDate.Text = clsFormat.DateFormat(_ApplicationInfo.ApplicationDate);
            lblStatusDate.Text = clsFormat.DateFormat(_ApplicationInfo.LastStatusDate);
            lblStatus.Text = _ApplicationInfo.StatusText.ToString();
            lblCreatedBy.Text = _ApplicationInfo.CreatedByUserINfo.UserName;

        }
        public void LoadApplicationInfoByLocalDrivingLicenseApplicationID(int ApplicationLocalDrivingLicenseID)
        {
             _ApplicationLocalDrivingLicense = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(ApplicationLocalDrivingLicenseID);

            llViewPersonInfo.Enabled = (_ApplicationLocalDrivingLicense != null);
            
            if (_ApplicationLocalDrivingLicense == null)
            {
                MessageBox.Show("Not Application With ApplicationID = " + _LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                RestApplicationInfo();
                
                return;
            }
            _ApplicationInfo = clsApplication.GetApplicationByApplicationID(_ApplicationLocalDrivingLicense.ApplicationID);

            _ApplicationID = _ApplicationInfo.ApplicationID;
            _LocalDrivingLicenseApplicationID = _ApplicationLocalDrivingLicense.LocalDrivingLicenseApplicationID;

            _LoadData();

        }
        public void LoadApplicationInfoByApplicationID(int ApplicationID)
        {
            _ApplicationInfo = clsApplication.GetApplicationByApplicationID(ApplicationID);

            llViewPersonInfo.Enabled = (_ApplicationInfo != null);

            if (_ApplicationInfo == null)
            {
                MessageBox.Show("Not Application With ApplicationID = " + _LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                RestApplicationInfo();

                return;
            }

            _ApplicationLocalDrivingLicense = clsLocalDrivingLicenseApplication.FindByApplicationID(ApplicationID);

            _ApplicationID = _ApplicationInfo.ApplicationID;
            _LocalDrivingLicenseApplicationID = _ApplicationLocalDrivingLicense.LocalDrivingLicenseApplicationID;

            _LoadData();
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(_ApplicationInfo.ApplicantPersonID);
            frm.ShowDialog();

            //LoadApplicationInfoByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
        }
    }
}
