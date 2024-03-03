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

namespace DVLD.Licenses.International_Licenses
{
    public partial class ctrlDrivingInternationLicenseInfo : UserControl
    {
        clsInternationalLicense _InternationalLicense;

        public ctrlDrivingInternationLicenseInfo()
        {
            InitializeComponent();
        }

        private void _LoadImage()
        {
            if (_InternationalLicense.DriverInfo.PersonInfo.ImagePath == null || _InternationalLicense.DriverInfo.PersonInfo.ImagePath == "")
            {
                pbImage.Image = _InternationalLicense.DriverInfo.PersonInfo.Gender == 0 ? Resources.Male_512 : Resources.Female_512;
                return;
            }

            if (File.Exists(_InternationalLicense.DriverInfo.PersonInfo.ImagePath))
            {
                pbImage.Load(_InternationalLicense.DriverInfo.PersonInfo.ImagePath);
            }
            else
            {
                MessageBox.Show("No Image On Your Hard With Image Path = " + _InternationalLicense.DriverInfo.PersonInfo.ImagePath,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void _LoadData()
        {
            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            lblDateOfBirth.Text = clsFormat.DateFormat(_InternationalLicense.DriverInfo.PersonInfo.DateOfBirth);
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblExpirationDate.Text = clsFormat.DateFormat(_InternationalLicense.ExpirationDate);
            lblGender.Text = _InternationalLicense.DriverInfo.PersonInfo.Gender == 0 ? "Male" : "Famle";
            pbGender.Image = lblGender.Text == "Male" ? Resources.Man_32 : Resources.Woman_32;
            lblIntLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            lblIsActive.Text = _InternationalLicense.IsActive ? "Yes" : "No";
            lblIssueDate.Text = clsFormat.DateFormat(_InternationalLicense.IssueDate);
            lblLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblName.Text = _InternationalLicense.DriverInfo.PersonInfo.FullName;
            lblNationalID.Text = _InternationalLicense.DriverInfo.PersonInfo.NationalNo;

            _LoadImage();
        }
        public void RestInternationalLicenseInfo()
        {
            lblExpirationDate.Text = "[DD/MM/YYYY]";
            lblDateOfBirth.Text = "[DD/MM/YYYY]";
            lblIssueDate.Text = "[DD/MM/YYYY]";
            lblApplicationID.Text =  "[????]";
            lblIntLicenseID.Text = "[????]";
            lblNationalID.Text = "[????]";
            lblLicenseID.Text = "[????]";
            lblDriverID.Text = "[????]";

            lblGender.Text = "[????]";
            pbGender.Image = Resources.Man_32;
            pbImage.Image = Resources.Male_512;
            lblIsActive.Text = "[????]";
            lblName.Text = "[????]";
        }
        public void LoadInternationalLicenseInfo(int InternationalLicenseID)
        {
            _InternationalLicense = clsInternationalLicense.Find(InternationalLicenseID);

            if (_InternationalLicense == null)
            {
                MessageBox.Show("No International License With ID = " + InternationalLicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadData();

        }
    }
}
