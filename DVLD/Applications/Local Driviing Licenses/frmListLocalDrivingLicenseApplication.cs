using DVLD.Licenses.Local_Licenses;
using DVLD.Tests;
using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Local_Driviing_Licenses
{
    public partial class frmListLocalDrivingLicenseApplication : Form
    {
        private DataTable _dtLocalDrivingLicenseApplication;
        public frmListLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        private void _SetDGVColumns()
        {
            cmbFilterBy.SelectedIndex = 0;
            txtFilterBy.Visible = false;

            if (dgvListLocalDrivingApplications.Rows.Count > 0 )
            {
                dgvListLocalDrivingApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvListLocalDrivingApplications.Columns[0].Width = 120;

                dgvListLocalDrivingApplications.Columns[1].HeaderText = "Driving Class";
                dgvListLocalDrivingApplications.Columns[1].Width = 300;
                
                dgvListLocalDrivingApplications.Columns[2].HeaderText = "National No";
                dgvListLocalDrivingApplications.Columns[2].Width = 120;

                dgvListLocalDrivingApplications.Columns[3].HeaderText = "Full Name";
                dgvListLocalDrivingApplications.Columns[3].Width = 250;

                dgvListLocalDrivingApplications.Columns[4].HeaderText = "Application Date";
                dgvListLocalDrivingApplications.Columns[4].Width = 250;

                dgvListLocalDrivingApplications.Columns[5].HeaderText = "Passed Tests";
                dgvListLocalDrivingApplications.Columns[5].Width = 110;

                dgvListLocalDrivingApplications.Columns[6].HeaderText = "Status";
                dgvListLocalDrivingApplications.Columns[6].Width = 125;
            }
        }

        private void _RefreshData()
        {
            _dtLocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvListLocalDrivingApplications.DataSource = _dtLocalDrivingLicenseApplication;
            lblNumberOfRecord.Text = dgvListLocalDrivingApplications.Rows.Count.ToString();

        }
        private void frmListLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _RefreshData();
            _SetDGVColumns();
        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = (cmbFilterBy.Text.Trim() != "None");

            if (txtFilterBy.Visible)
            {
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (cmbFilterBy.Text == "None")
            {
                _RefreshData();
                return;
            }

            string FilterString = cmbFilterBy.Text.Trim();

            switch (FilterString)
            {
                case "L.D.L.AppID":
                    FilterString = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No.":
                    FilterString = "NationalNo";
                    break;
                case "Full Name":
                    FilterString = "FullName";
                    break;
                case "Status":
                    FilterString = "Status";
                    break;
                default:
                    FilterString = "";
                    break;
            }

            if (txtFilterBy.Text.Trim() == "" || cmbFilterBy.Text == "None")
            {
                _dtLocalDrivingLicenseApplication.DefaultView.RowFilter = "";
                lblNumberOfRecord.Text = dgvListLocalDrivingApplications.Rows.Count.ToString();
                return;
            }

            if (FilterString == "LocalDrivingLicenseApplicationID")
            {
                _dtLocalDrivingLicenseApplication.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterString, txtFilterBy.Text.Trim());
            }
            else
            {
                _dtLocalDrivingLicenseApplication.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterString, txtFilterBy.Text.Trim());
            }

            lblNumberOfRecord.Text = dgvListLocalDrivingApplications.Rows.Count.ToString();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (cmbFilterBy.Text.Trim() == "L.D.L.AppID") ? !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) : false;

            //if (cmbFilterBy.Text.Trim() == "L.D.L.AppID")
            //{
            //    e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            //}
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplication_Load(null, null);
        }

        private void tsmApplicationEdit_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication(
                (int)dgvListLocalDrivingApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplication_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmApplicationDelete_Click(object sender, EventArgs e)
        {
            int LocalDrivingApplicationID = (int)dgvListLocalDrivingApplications.CurrentRow.Cells[0].Value;
            
            clsLocalDrivingLicenseApplication Obj_LDLApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingApplicationID);
            
            /*

                int ApplicationID = Obj_LDLApp.ApplicationID;
            
                if (LocalDrivingApplicationID != -1 && ApplicationID != -1)
                {
                    if (MessageBox.Show("Are You Sure You Want To Delete This Application With ID = " + LocalDrivingApplicationID,
                        "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        if (clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication(LocalDrivingApplicationID, ApplicationID))
                        {
                            MessageBox.Show("Application Deleted Successfully", "Confirm Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("You Can't Delete This Local Driving License Application With ID  = " + LocalDrivingApplicationID +
                                " Because It's Linked With Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Application Not Deleted Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
                _RefreshData();

           */
                
            if (Obj_LDLApp != null) 
            {
                if (MessageBox.Show("Are You Sure You Want To Delete This Application With ID = " + LocalDrivingApplicationID,
                    "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (Obj_LDLApp.DeleteLocalDrivingLicenseApplication())
                    {
                        MessageBox.Show("Application Deleted Successfully", "Confirm Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("You Can't Delete This Local Driving License Application With ID  = " + LocalDrivingApplicationID +
                            " Because It's Linked With Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No Local Drivnig License Application With ID = " + LocalDrivingApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _RefreshData();

        }

        private void tsmCancelApplication_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Cancel This Application ?", "Confirm Cancel", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;


            int LocalDrivingApplicationID = (int)dgvListLocalDrivingApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication Obj_LDLApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingApplicationID);

            if (Obj_LDLApp != null)
            {
                if (Obj_LDLApp.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully", "Confirm Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Application Not Cancelled Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No Local Drivnig License Application With ID = " + LocalDrivingApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _RefreshData();
        }

        private void tsmShowApplicationDetails_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplication frm = new frmLocalDrivingLicenseApplication((int)dgvListLocalDrivingApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void dgvListLocalDrivingApplications_DoubleClick(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplication frm = new frmLocalDrivingLicenseApplication((int)dgvListLocalDrivingApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvListLocalDrivingApplications.CurrentRow.Cells[0].Value;

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New)
            {
                tsmShowLicense.Enabled = false;
                tsmIssueDrivingLicenseFirstTime.Enabled = false;

                tsmApplicationDelete.Enabled = tsmApplicationEdit.Enabled = tsmCancelApplication.Enabled = tsmSechduleTest.Enabled= true;
                
                /*
                    if (_LocalDrivingLicenseApplication.DoesPassPreviousTest(clsTestType.enTestType.VisionTest))
                    {
                        tsmVisionTest.Enabled = true;
                        tsmWrittenTest.Enabled = false;
                        tsmStreetTest.Enabled = false;
                    }

                    if (_LocalDrivingLicenseApplication.DoesPassPreviousTest(clsTestType.enTestType.WrittenTest))
                    {
                        tsmVisionTest.Enabled = false;
                        tsmWrittenTest.Enabled = true;
                        tsmStreetTest.Enabled = false;
                    }
                    if (_LocalDrivingLicenseApplication.DoesPassPreviousTest(clsTestType.enTestType.StreetTest))
                    {
                        tsmVisionTest.Enabled = false;
                        tsmWrittenTest.Enabled = false;
                        tsmStreetTest.Enabled = true;
                    }
                */

                bool PassedVisionTest = _LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest); ;
                bool PassedWrittenTest = _LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest);
                bool PassedStreetTest = _LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest);

                tsmSechduleTest.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (_LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

                if (tsmSechduleTest.Enabled)
                {
                    //To Allow Schdule vision test, Person must not passed the same test before.
                    tsmVisionTest.Enabled = !PassedVisionTest;

                    //To Allow Schdule written test, Person must pass the vision test and must not passed the same test before.
                    tsmWrittenTest.Enabled = PassedVisionTest && !PassedWrittenTest;

                    //To Allow Schdule steet test, Person must pass the vision * written tests, and must not passed the same test before.
                    tsmStreetTest.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;

                }

                if (_LocalDrivingLicenseApplication.PassedAllTests())
                {
                    tsmSechduleTest.Enabled = false;
                    tsmIssueDrivingLicenseFirstTime.Enabled = true;
                }
                return;
            }
            
            tsmApplicationDelete.Enabled = tsmApplicationEdit.Enabled = tsmCancelApplication.Enabled =
            tsmIssueDrivingLicenseFirstTime.Enabled = tsmSechduleTest.Enabled = tsmShowLicense.Enabled = false;
            
            if (_LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.Completed)
            {
                tsmShowLicense.Enabled = true;
            }

        }

        private void tsmVisionTest_Click(object sender, EventArgs e)
        {
            frmListTestAppointments frm = new frmListTestAppointments((int)dgvListLocalDrivingApplications.CurrentRow.Cells[0].Value, clsTestType.enTestType.VisionTest);
            frm.ShowDialog();
            _RefreshData();

        }

        private void tsmWrittenTest_Click(object sender, EventArgs e)
        {
            frmListTestAppointments frm = new frmListTestAppointments((int)dgvListLocalDrivingApplications.CurrentRow.Cells[0].Value, clsTestType.enTestType.WrittenTest);
            frm.ShowDialog();
            _RefreshData();
        }

        private void tsmStreetTest_Click(object sender, EventArgs e)
        {
            frmListTestAppointments frm = new frmListTestAppointments((int)dgvListLocalDrivingApplications.CurrentRow.Cells[0].Value, clsTestType.enTestType.StreetTest);
            frm.ShowDialog();
            _RefreshData();
        }

        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvListLocalDrivingApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            int LicenseID = LocalDrivingLicenseApplication.GetActiveLicenseID();

            if (LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Loal Driving License App. With ID = " + LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
            _RefreshData();
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvListLocalDrivingApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Application Driving License App. With ID" + LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(LocalDrivingLicenseApplication.ApplicantPersonID);
            frm.ShowDialog();

            _RefreshData();
        }

        private void tsmIssueDrivingLicenseFirstTime_Click(object sender, EventArgs e)
        {

            int LocalDrivingLicenseApplicationID = (int)dgvListLocalDrivingApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            if (LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Application Driving License App. With ID" + LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (clsLicense.IsLicenseExistByPersonID(LocalDrivingLicenseApplication.ApplicantPersonID, LocalDrivingLicenseApplication.LicenseClassesID))
            {
                MessageBox.Show("Error: The License For This Application Already is Issued", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            frmIssueDrivingLicenseFirtsTime frm = new frmIssueDrivingLicenseFirtsTime(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
            _RefreshData();
        }
    }
}
