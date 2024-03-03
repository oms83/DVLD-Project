using DVLD.Global;
using DVLD.Properties;
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

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduledTest : UserControl
    {
        clsTestAppointment _TestAppointment;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        clsTestType _TestTypeInfo;

        clsLicenseClasses LicenseClasses;
        
        int _TestAppointmentID = -1;
        int _LocalDrivingLicenseApplicationID = -1;
        int _TestID = -1;
        private clsTestType.enTestType _TestType;
        public clsTestType.enTestType TestType
        {
            get => _TestType;
            set
            {
                _TestType = value;
                switch (_TestType)
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

        public int TestID
        {
            get => _TestID;
        }

        public int TestAppointmentID
        {
            get => _TestAppointmentID;
        }

        private enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }

        private enTestType _TestTypeID = enTestType.VisionTest;

        public ctrlScheduledTest()
        {
            InitializeComponent();
        }

        private void ctrlScheduledTest_Load(object sender, EventArgs e)
        {

        }

        public void LoadData(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;

            _TestAppointment = clsTestAppointment.Find(TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No  Appointment ID = " + _TestAppointmentID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _TestAppointmentID = -1;
                return;
            }

            _TestID = _TestAppointment.TestID;

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_TestAppointment.LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;

            LicenseClasses = clsLicenseClasses.Find(_LocalDrivingLicenseApplication.LicenseClassesID);

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplicationID.ToString();
                
            lblDrivingClass.Text = LicenseClasses.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;

            lblTrial.Text = clsLocalDrivingLicenseApplication.TotalTrialsPerTest(_LocalDrivingLicenseApplicationID, _TestType).ToString();

            lblDate.Text = clsFormat.DateFormat(_TestAppointment.AppointmentDate);
            lblFees.Text = _TestAppointment.PaidFees.ToString(); 
            lblTestID.Text = _TestAppointment.TestID != -1 ? _TestAppointment.TestID.ToString() : "Not Taken Yet";

        }
    }
}
