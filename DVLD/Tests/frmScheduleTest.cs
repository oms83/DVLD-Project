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

namespace DVLD.Tests
{
    public partial class frmScheduleTest : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _TestAppointmetID;
        clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;
        public frmScheduleTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType, int TestAppointmetID)
        {
            InitializeComponent();
            _TestType = TestType;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmetID = TestAppointmetID;
        }


        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTests1.TestTypeID = _TestType;
            ctrlScheduleTests1.LoadData(_LocalDrivingLicenseApplicationID, _TestAppointmetID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
