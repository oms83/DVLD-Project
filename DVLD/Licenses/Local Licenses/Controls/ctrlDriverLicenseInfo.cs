using DVLD.Global;
using DVLD.Properties;
using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        int _LocalDrivingLicenseApplicationID = -1;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        clsLicense _License;
        int _LicenseID = -1;
        public int LicenseID
        {
            get => _LicenseID;
        }
        public clsLicense SelectedLicense
        {
            get => _License;
            set => _License = value;
        }

        int _ApplicationID = -1;
        clsApplication _Application;
        

        clsDriver _Driver;
        int _DriverID = -1;

        private bool _EditPersonInfoEnabled = true;
        public bool EditPersonInfoEnabled
        {
            get => _EditPersonInfoEnabled;
            set
            {
                _EditPersonInfoEnabled = value;
                llEditPersonInfo.Enabled = _EditPersonInfoEnabled;
            }
        }
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        public void _RestLicenseInfo()
        {
            llEditPersonInfo.Enabled = false;
            lblClass.Text = "[????]";
            lblName.Text = "[????]";
            lblLicenseID.Text = "[????]";
            lblNationalID.Text = "[????]";

            lblGender.Text = "Male";

            pbGender.Image = Resources.Man_32;

            lblIssueDate.Text = "[DD/MM/YYYY]";
            lblIssueReason.Text = "[????]";

            lblNotes.Text = "[????]";

            lblIsActive.Text = "[????]";

            lblDateOfBirth.Text = "[DD/MM/YYYY]";
            lblDriverID.Text = "[????]";
            lblExpirationDate.Text = "[DD/MM/YYYY]";
            lblIsDetained.Text = "[????]";

            pbImage.Image = Resources.Male_512;
            

        }
        private void _LoadData()
        {
            //_Application = clsApplication.GetApplicationByApplicationID(_LocalDrivingLicenseApplication.ApplicationID);

            llEditPersonInfo.Enabled = _Driver != null;

            lblClass.Text = _License.LicenseClassesInfo.ClassName;
            lblName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text = _LicenseID.ToString();
            lblNationalID.Text = _Driver.PersonInfo.NationalNo;

            lblGender.Text = _Driver.PersonInfo.Gender == 0 ? "Male" : "Female";

            pbGender.Image = _Driver.PersonInfo.Gender == 0 ? Resources.Man_32 : Resources.Woman_32;

            lblIssueDate.Text = clsFormat.DateFormat(_License.IssueDate);
            lblIssueReason.Text = _License.IssueReasonText;

            lblNotes.Text = (_License.Notes == "" || _License.Notes == null) ? "No Notes" : _License.Notes;

            lblIsActive.Text = _License.IsActive ? "Yes" : "No";

            lblDateOfBirth.Text = clsFormat.DateFormat(_Driver.PersonInfo.DateOfBirth);
            lblDriverID.Text = _Driver.DriverID.ToString();
            lblExpirationDate.Text = clsFormat.DateFormat(_License.ExpirationDate);
            lblIsDetained.Text = _License.IsActive ? "No" : "Yes";
            if (_Driver.PersonInfo.ImagePath != null || _Driver.PersonInfo.ImagePath != "")
            {
                if (File.Exists(_Driver.PersonInfo.ImagePath))
                    pbImage.ImageLocation = _Driver.PersonInfo.ImagePath;
            }
            else
            {
                pbImage.ImageLocation = "";
                pbGender.Image = _Driver.PersonInfo.Gender == 0 ? Resources.Male_512 : Resources.Female_512;
            }
        }
        public void LoadLicenseInfo(int LicenseID)
        {
            //_LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            //_LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            //if ( _LocalDrivingLicenseApplication == null )
            //{
            //    _RestLicenseInfo();
            //    MessageBox.Show("Error: No Application Driving License App. With ID" + _LocalDrivingLicenseApplicationID, "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //_ApplicationID = _LocalDrivingLicenseApplication.ApplicationID;
            //_License = clsLicense.FindByApplicationID(_ApplicationID);
            //_LicenseID = _License.LicenseID;



            _License = clsLicense.Find(LicenseID);
            if (_License == null)
            {
                MessageBox.Show("Error: No License For This Application With  LicenseID = " + LicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }
            
            _LicenseID = _License.LicenseID;
            _Driver = clsDriver.FindByDriverID(_License.DriverID);

            if (_Driver == null)
            {
                _RestLicenseInfo();
                MessageBox.Show("Error: No License With DriverID = " + _License.DriverID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _DriverID = _Driver.DriverID;

            _LoadData();


        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddNewEdit frm = new frmAddNewEdit(_Driver.PersonID);
            frm.ShowDialog();
            _LoadData();
        }
    }
}
