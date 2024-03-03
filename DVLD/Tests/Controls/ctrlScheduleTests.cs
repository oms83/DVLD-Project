using DVLD.Global;
using DVLD.Properties;
using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduleTests : UserControl
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        private int _TestAppointmentID = -1;
        private clsTestAppointment _TestAppointment;

        public enum enMode { AddNew, Update }
        private enMode _Mode = enMode.AddNew;

        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 }
        public enCreationMode _CreationMode;

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        public clsTestType.enTestType TestTypeID
        {
            get => _TestTypeID;
            set
            {
                _TestTypeID = value;
                switch (_TestTypeID)
                {
                    case clsTestType.enTestType.VisionTest:
                        gbTestType.Text = "Vision Test";
                        pbTestTypeImage.Image = Resources.Vision_512;
                        break;
                    case clsTestType.enTestType.WrittenTest:
                        gbTestType.Text = "Written Test";
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;
                    case clsTestType.enTestType.StreetTest:
                        gbTestType.Text = "Street Test";
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        break;
                }
            }
        }

        public void LoadData(int LocalDrivingLicenseApplicationID,  int TestAppointmentID = -1)
        {
            _Mode = (TestAppointmentID == -1) ? enMode.AddNew : enMode.Update;

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application With ID = " + LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnSave.Enabled = false;
                
                _LocalDrivingLicenseApplicationID = -1;

                return;
            }

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = TestAppointmentID;

            _CreationMode = (_LocalDrivingLicenseApplication.DoesAttendTestType(_TestTypeID)) ? enCreationMode.RetakeTestSchedule : enCreationMode.FirstTimeSchedule;

            

            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplicationID.ToString();

            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassesInfo.ClassName;

            //lblTrial.Text = clsLocalDrivingLicenseApplication.TotalTrialsPerTest(_LocalDrivingLicenseApplicationID, (clsTestType.enTestType)_TestTypeID).ToString();
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();


            if (_CreationMode == enCreationMode.FirstTimeSchedule)
            {
                lblTitle.Text = "Test Schedule";
                
                lblRetakeTestAppID.Text = "N/A";
                gbRetakeTestInfo.Enabled = false;
                lblRetakeAppFees.Text = "0";
                
            }
            else
            {
                lblTitle.Text = "Retake Test Schedule";
                
                lblRetakeTestAppID.Text = "N/A";
                lblRetakeAppFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees.ToString();
                gbRetakeTestInfo.Enabled = true;

            }

            lblFees.Text = clsTestType.Find(_TestTypeID).TestFees.ToString();

            lblTotalFees.Text = (Convert.ToSingle(lblRetakeAppFees.Text) + Convert.ToSingle(lblFees.Text)).ToString();

            if (_Mode == enMode.AddNew)
            {
                _TestAppointment = new clsTestAppointment();
                dtpTestDate.MinDate = DateTime.Now;
                lblFees.Text = clsTestType.Find(_TestTypeID).TestFees.ToString();
                lblUserMessage.Visible = false;
                
                if (!_HandleActiveAppointmentTest())
                {
                    return;
                }
                if (_HandleTestHasBeenPassedConstraint())
                {
                    return;
                }
                /*
                    Should Be handle when test is already passed 
                */
                return;
            }

            else
            {
                if (!_LoadTestAppointmentData())
                    return;
            }

            lblTotalFees.Text = (Convert.ToSingle(lblRetakeAppFees.Text) + Convert.ToSingle(lblFees.Text)).ToString();



            if (!_HandleAppointmentLockedConstraint())
            {
                return;
            }

            if (!_HandlePreviousTestConstriant())
            {
                return;
            }
        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Test Appointment With ID = " + _TestAppointmentID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _TestAppointmentID = -1;

                btnSave.Enabled = false;
              
                return false;
            }

            lblFees.Text = _TestAppointment.PaidFees.ToString();

            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate)<0)
            {
                dtpTestDate.MinDate = DateTime.Now;
            }
            else
            {
                dtpTestDate.MinDate = _TestAppointment.AppointmentDate;
            }

            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                gbRetakeTestInfo.Enabled = false;
                lblRetakeTestAppID.Text = "N/A";
                lblRetakeAppFees.Text = "0";
                
            }
            else
            {

                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Retake Test Schedule";
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                lblRetakeAppFees.Text = _TestAppointment.RetakeTestApplicationInfo.PaidFees.ToString();
            }

            //lblRetakeAppFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees.ToString();


            return true;

        }

        private bool _HandleTestHasBeenPassedConstraint()
        {
            if (_LocalDrivingLicenseApplication.DoesPassTestType(TestTypeID))
            {
                lblUserMessage.Text = "You Can't Add a New Test Appointment Beacuse This Test Has Already Been Passed!";
                lblUserMessage.Visible = true;
                gbRetakeTestInfo.Enabled = false;
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return true;
            }
            return false;
        }

        private bool _HandleActiveAppointmentTest()
        {
            if (_Mode == enMode.AddNew && _LocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestTypeID))
            {
                lblUserMessage.Text = "Person Already Has An Active Appointment For This Test";
                lblUserMessage.Visible = true;
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }
            else
            {
                lblUserMessage.Visible = false;
                btnSave.Enabled = true;
                dtpTestDate.Enabled = true;
            }
            return true;
        }

        private bool _HandleAppointmentLockedConstraint()
        {
            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Text = "You Can't Update The Appointment Info Because It Is Already Locked!";
                lblUserMessage.Visible = true;
                gbRetakeTestInfo.Enabled = false;
                dtpTestDate.Enabled= false;
                btnSave.Enabled= false;
                return false;
            }  
            return true;
        }

        private bool _HandlePreviousTestConstriant()
        {
            switch (_TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    lblUserMessage.Visible = false;
                    return true;


                case clsTestType.enTestType.WrittenTest:

                    if (!_LocalDrivingLicenseApplication.DoesPassPreviousTest(clsTestType.enTestType.VisionTest))
                    {
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        lblUserMessage.Text = "Can't Schedule!, Vision Test Should Be Passed";
                        lblUserMessage.Visible = true;
                        return false;
                        
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                        lblUserMessage.Visible = false;
                        return true;
                    }


                case clsTestType.enTestType.StreetTest:

                    if (!_LocalDrivingLicenseApplication.DoesPassPreviousTest(clsTestType.enTestType.WrittenTest))
                    {
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        lblUserMessage.Text = "Can't Schedule!, WrittenTest Test Should Be Passed";
                        lblUserMessage.Visible = true;
                        return false;
                        
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                        lblUserMessage.Visible = false;
                        return true;
                    }
            }


            return true;
        }
        public ctrlScheduleTests()
        {
            InitializeComponent();
        }

        private bool _HandleRetakeTest()
        {
            if (enCreationMode.RetakeTestSchedule == _CreationMode && _Mode == enMode.AddNew)
            {
                
                clsApplication _Application = new clsApplication();

                //_Application.ApplicantPersonID = clsPerson.Find(clsApplication.GetApplicationByApplicationID(_LocalDrivingLicenseApplication.ApplicationID).ApplicantPersonID).PersonID;

                _Application.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;

                //_Application.ApplicationTypeID = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationTypeID;

                _Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;

                _Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;

                _Application.ApplicationDate = DateTime.Now;

                _Application.LastStatusDate = DateTime.Now;

                _Application.CreatedByUserID = GlobalSettings.CurrentUser.UserID;

                _Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees;
            
                if (!_Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Retake Test Application Is Not Saved", "Error",  MessageBoxButtons.OK, MessageBoxIcon.Error);  
                    return false;
                }

                _TestAppointment.RetakeTestApplicationID = _Application.ApplicationID;

                return true;
            
            }



            return true;

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeTest())
            {
                return;
            }

            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.CreatedByUserID = GlobalSettings.CurrentUser.UserID;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _TestAppointment.TestTypeID = _TestTypeID;

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Data Is Not Saved Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
