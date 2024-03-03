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

namespace DVLD.Tests
{
    public partial class frmTakeTest : Form
    {
        clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        int _TestAppointmentID = -1;
        clsTestAppointment _TestAppointment;
        clsTest _Test;
        int _TestID = -1;
        private enum enMode { AddNew, Update }
        private enMode _Mode = enMode.AddNew;
        public frmTakeTest(clsTestType.enTestType TestTypeID, int TestAppointmentID)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;

            
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlScheduledTest1.TestType = _TestTypeID;
            ctrlScheduledTest1.LoadData(_TestAppointmentID);

            if ((_TestAppointment = clsTestAppointment.Find(_TestAppointmentID)).IsLocked)
            {
                _Mode = enMode.Update;
                _LoadData();
            }
            else
            {
                _Mode = enMode.AddNew;
                lblUserMessage.Visible = false;

                _Test = new clsTest();
            }
        }

        private void _LoadData()
        {
            btnSave.Enabled = false;

            //_Test = clsTest.FindTestInfoByTestAppointmentID(_TestAppointmentID);
            _Test = clsTest.FindTestInfoByTestID(ctrlScheduledTest1.TestID);

            if ( _Test == null )
            {
                MessageBox.Show("No Test With AppointmentID = " +  _TestAppointmentID , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (_Test.TestResult)
                rbPass.Checked = true;
            else
                rbFail.Checked = true;

            rbFail.Enabled = false;
            rbPass.Enabled = false;
            lblUserMessage.Text = "You cannot change the results";
            lblUserMessage.Visible = true;
            txtNotes.Text = _Test.Notes;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save? After that you can't change the test result", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            _Test.TestResult = rbPass.Checked ? true : false;
            _Test.Notes = txtNotes.Text;
            _Test.TestAppointmentID = _TestAppointmentID;
            _Test.CreatedByUserID = GlobalSettings.CurrentUser.UserID;
            
            if (_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Confirm Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
                rbFail.Enabled = false;
                rbPass.Enabled = false;
                lblUserMessage.Text = "You cannot change the results";
                lblUserMessage.Visible = true;
                ctrlScheduledTest1.LoadData(_TestAppointmentID);
                return;
            }
            else
            {
                MessageBox.Show("Error: Data Is Not Saved Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (txtNotes.Text != "")
            {
                if (MessageBox.Show("Are you sure you want to close this form!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }
    }
}
