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

namespace DVLD.Tests
{
    public partial class frmListTestAppointments : Form
    {
        clsTestAppointment _TestAppointment;
        DataTable _dtTestAppointments;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        int _LocalDrivingLicenseApplicationID = -1;
        clsTestType.enTestType _TestTypeID;

        public frmListTestAppointments(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
            
        }

        private void _SetDGVColumns()
        {
            if (dgvListLocalDrivingAppointments.Rows.Count > 0)
            {
                dgvListLocalDrivingAppointments.Columns[0].Width = 200;
                dgvListLocalDrivingAppointments.Columns[1].Width = 225;
                dgvListLocalDrivingAppointments.Columns[2].Width = 200;
                dgvListLocalDrivingAppointments.Columns[3].Width = 150;
            }
        }
        private void _RefreshTestAppointments()
        {

            _dtTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, _TestTypeID);
            dgvListLocalDrivingAppointments.DataSource = _dtTestAppointments;
            _SetDGVColumns();
        }
        private void _LoadTestAppointments()
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null )
            {
                MessageBox.Show("Error: No Local Driving License Application With ID = " + _LocalDrivingLicenseApplicationID, "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            _RefreshTestAppointments();

            switch (_TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    this.Text = lblTestTitle.Text = "Vision Test";
                    pbTestImage.Image = Resources.Vision_512;
                    break;

                case clsTestType.enTestType.WrittenTest:
                    this.Text = lblTestTitle.Text = "Written Test";
                    pbTestImage.Image = Resources.Written_Test_512;
                    break;

                case clsTestType.enTestType.StreetTest:
                    this.Text = lblTestTitle.Text = "Street Test";
                    pbTestImage.Image = Resources.driving_test_512;
                    break;
            }
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestAppointments();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            if (_LocalDrivingLicenseApplication.DoesPassTestType(_TestTypeID))
            {
                MessageBox.Show("You Can't Add a New Test Appointment Because This Test Has Already Been Passed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_LocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestTypeID))
            {
                MessageBox.Show("You Can't Add a New Test Appointment Because Is There An Active Schedule", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID,  -1);
            frm.ShowDialog();
            _RefreshTestAppointments();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID, (int)dgvListLocalDrivingAppointments.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshTestAppointments();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeTest frm = new frmTakeTest(_TestTypeID, (int)dgvListLocalDrivingAppointments.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshTestAppointments();
        }
    }
}
