using DVLD.Global;
using DVLD.Licenses.International_Licenses;
using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD_Business_Layer.clsApplication;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Applications.International_Licenses
{
    public partial class frmNewInternationalLicenseApplications : Form
    {
        clsLicense _License;
        clsInternationalLicense _InternationalLicense;
        int _LicenseID=-1;
        public frmNewInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        public frmNewInternationalLicenseApplications(int LicesnseID)
        {
            InitializeComponent();

            _LicenseID = LicesnseID;
            _RestInternationalLicenseInfo();
            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(LicesnseID);
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            if (!_InternationalLicenseConstraint(_LicenseID))
            {
                this.Close();
                return;
            }
            _LoadInternationalLicenseData();

        }

        private void _RestInternationalLicenseInfo()
        {
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            lblApplicationDate.Text = "[DD/MM/YYYY]";
            lblCreatedByUser.Text = "[????]";
            lblExpirationDate.Text = "[DD/MM/YYYY]";
            lblFees.Text = "[$$$$]";
            lblIssueDate.Text = "[DD/MM/YYYY]";
            lblLocalLicenseID.Text = "[????]";
            lblApplicationID.Text = "[????]";
            lblInternationalLicenseID.Text = "[????]";
            btnIssue.Enabled = false;

        }

        private void _LoadInternationalLicenseData()
        {
            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;

            lblApplicationDate.Text = clsFormat.DateFormat(DateTime.Now);
            lblCreatedByUser.Text = GlobalSettings.CurrentUser.UserName;
            lblExpirationDate.Text = clsFormat.DateFormat(DateTime.Now.AddYears(1));
            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees.ToString();
            lblIssueDate.Text = clsFormat.DateFormat(DateTime.Now);
            lblLocalLicenseID.Text = _LicenseID.ToString();

            btnIssue.Enabled = true;
            

        }

        private bool _InternationalLicenseConstraint(int LicenseID)
        {
            _License = clsLicense.Find(LicenseID);

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicense.LicenseClassID != 3)
            {
                MessageBox.Show("Selected license should be class 3, select another one", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

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

            if (clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(_License.DriverID) != -1)
            {
                llShowLicenseHistory.Enabled = true;
                llShowLicenseInfo.Enabled = true;
                MessageBox.Show("Selected driver aleardy has an international license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!_License.IsActive)
            {
                llShowLicenseHistory.Enabled = true;
                llShowLicenseInfo.Enabled = true;
                MessageBox.Show("Selected license should be active", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            return true;
        }

        public void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int LicenseID)
        {
            _LicenseID = LicenseID;
            
            btnIssue.Enabled = false;
            lblLocalLicenseID.Text = _LicenseID.ToString();

            _RestInternationalLicenseInfo();

            if (_LicenseID==-1)
            {
                return;
            }
            
            if (!_InternationalLicenseConstraint(_LicenseID))
            {
                return;
            }

            _LoadInternationalLicenseData();
        }


        private void btnIssue_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            _InternationalLicense = new clsInternationalLicense();
            _InternationalLicense.ApplicantPersonID = _License.DriverInfo.PersonID;
            _InternationalLicense.ApplicationDate = DateTime.Now;
            _InternationalLicense.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            _InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            _InternationalLicense.LastStatusDate = DateTime.Now;
            _InternationalLicense.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees;
            _InternationalLicense.CreatedByUserID = GlobalSettings.CurrentUser.UserID;

            _InternationalLicense.DriverID = _License.DriverID;
            _InternationalLicense.IssuedUsingLocalLicenseID = _License.LicenseID;
            _InternationalLicense.IssueDate = DateTime.Now;
            _InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            _InternationalLicense.IsActive = true;
            _InternationalLicense.CreatedByUserID = GlobalSettings.CurrentUser.UserID;

            if (_InternationalLicense.Save())
            {

                btnIssue.Enabled = false;
                ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
                llShowLicenseInfo.Enabled = true;

                lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
                lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();

                MessageBox.Show("International License Issued Successfully with ID=" + _InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_InternationalLicense != null)
            {

                frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(_InternationalLicense.InternationalLicenseID);
                frm.ShowDialog();
            }
            else
            {
                frmShowLicenseInfo frm = new frmShowLicenseInfo(_License.LicenseID);
                frm.ShowDialog();
            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_License.DriverInfo.PersonID);
            frm.ShowDialog();
        }
    }
}
    