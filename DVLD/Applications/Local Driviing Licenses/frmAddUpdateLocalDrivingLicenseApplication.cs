using DVLD.Global;
using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Local_Driviing_Licenses
{
    public partial class frmAddUpdateLocalDrivingLicenseApplication : Form
    {
        private enum enMode { AddNew, Update }
        
        private enMode Mode;

        private int _LDLAppID;
        
        clsLocalDrivingLicenseApplication _LDLApp;
        private int _PersonID;
        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();

            Mode = enMode.AddNew;
        }

        public frmAddUpdateLocalDrivingLicenseApplication(int LDLAppID)
        {
            InitializeComponent();

            Mode = enMode.Update;

            _LDLAppID = LDLAppID;
        }

        private void _RestLocalDrivingLicenseApplicationInfo()
        {
            _FillComboBoxOfLicenseClasses();

            if (Mode == enMode.AddNew)
            {
                //lblTitle.Text = "New Local Driving License Application";
                //this.Text = "New Local Driving License Application";

                ctrlUserCardWithFilter1.FilterFocus();
                tpApplicationInfo.Enabled = false;

                cmbLicenseClass.SelectedIndex = 2;

                lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
                lblApplicationDate.Text = clsFormat.DateFormat(DateTime.Now);
                lblCreatedByUser.Text = GlobalSettings.CurrentUser.UserName;
                lblLocalDrivingLicebseApplicationID.Text = "[????]";

                _LDLApp = new clsLocalDrivingLicenseApplication();
            }

            else
            {
                //lblTitle.Text = "Update Local Driving License Application";
                //this.Text = "Update Local Driving License Application";
                
                btnNext.Enabled = true;
                tpApplicationInfo.Enabled = true;
                ctrlUserCardWithFilter1.FilterEnable = false;
            }

            lblMode.Text = this.Text = ( (Mode == enMode.AddNew) ? "Add New" : "Update" ) + " Local Driving License Application";
            
        }
       
        private void _FillComboBoxOfLicenseClasses()
        {

            DataTable _dtLicenseClasses = clsLicenseClasses.GellAllLicenseClasses();

            foreach (DataRow row in _dtLicenseClasses.Rows)
            {
                cmbLicenseClass.Items.Add(row["ClassName"]);
            }

        }

        private void _LoadLocalDrivingLicenseApplicationData()
        {
            _LDLApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LDLAppID);
            if ( _LDLApp == null )
            {
                MessageBox.Show("No Local Driving License Application With Local Driving License Application ID = " +  _LDLAppID, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            cmbLicenseClass.SelectedIndex = cmbLicenseClass.FindString(_LDLApp.LicenseClassesInfo.ClassName);

            ctrlUserCardWithFilter1.LoadPersonInfo(_LDLApp.ApplicantPersonID);

            lblApplicationDate.Text = _LDLApp.ApplicationDate.ToString();

            lblCreatedByUser.Text = clsUser.FindByUserID(_LDLApp.CreatedByUserID).UserName;

            lblApplicationDate.Text = clsFormat.DateFormat(_LDLApp.ApplicationDate);
            
            lblFees.Text = _LDLApp.PaidFees.ToString();
            
            lblLocalDrivingLicebseApplicationID.Text = _LDLApp.LocalDrivingLicenseApplicationID.ToString();
        }

        private void frmAddUpdateLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _RestLocalDrivingLicenseApplicationInfo();

            if (Mode == enMode.Update)
            {
                _LoadLocalDrivingLicenseApplicationData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Feild Are Not Valid!, Please Put The Mouse Over The Red Icon(s) To See The Error", "Error",  
                    MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return;
            }

            int LicenseClassID = clsLicenseClasses.Find(cmbLicenseClass.Text).ClassID;

            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_PersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id = " + ActiveApplicationID, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                cmbLicenseClass.Focus();
                
                return;
            }


            _LDLApp.LicenseClassesID = LicenseClassID;

            _LDLApp.ApplicantPersonID = ctrlUserCardWithFilter1.PersonID;
            _LDLApp.CreatedByUserID = GlobalSettings.CurrentUser.UserID;
            _LDLApp.ApplicationTypeID = (int)clsApplication.enApplicationType.NewDrivingLicense;
            _LDLApp.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LDLApp.ApplicationDate = DateTime.Now;
            _LDLApp.LastStatusDate = DateTime.Now;

            //_LDLApp.PaidFees = Convert.ToSingle(lblFees.Text);
            
            _LDLApp.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationFees;

            if (_LDLApp.Save())
            {
                lblLocalDrivingLicebseApplicationID.Text = _LDLApp.LocalDrivingLicenseApplicationID.ToString();

                MessageBox.Show("Data Saved Successfully", (Mode == enMode.Update) ? "Confirm Update" : "Confirm Add", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Mode = enMode.Update;
            }
        }

        private void ctrlUserCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;
        }

        private void cmbLicenseClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
                return;

            }

            if (ctrlUserCardWithFilter1.PersonID != -1)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
            }

            else
            {
                tpApplicationInfo.Enabled = false;
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmAddUpdateLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlUserCardWithFilter1.FilterFocus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           this.Close();
        }
    }
}
