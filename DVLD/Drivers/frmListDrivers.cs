using DVLD.Applications.International_Licenses;
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

namespace DVLD
{
    public partial class frmListDrivers : Form
    {
        DataTable _dtDrivers;
        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _LoadDriversData()
        {
            _dtDrivers = clsDriver.GetAllDrivers();
            dgvListPeople.DataSource = _dtDrivers;
        }
        private void _SetDGVColumns()
        {
            cmbFilterBy.SelectedIndex = 0;

            if (dgvListPeople.Rows.Count > 0)
            {
                dgvListPeople.Columns[0].Width = 150;
                dgvListPeople.Columns[1].Width = 150;
                dgvListPeople.Columns[2].Width = 150;

                dgvListPeople.Columns[3].Width = 400;
                dgvListPeople.Columns[4].Width = 250;
                dgvListPeople.Columns[5].Width = 150;
            }

            lblNumberOfRecord.Text = dgvListPeople.Rows.Count.ToString();
        }
        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            _LoadDriversData();
            _SetDGVColumns();
        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = cmbFilterBy.Text != "None";

            if (txtFilterBy.Visible)
            {
                txtFilterBy.Focus();
                txtFilterBy.Text = "";
            }
            _LoadDriversData();
            
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterString = "";

            /*
                None
                Driver ID
                Person ID
                National No.
                Full Name 
            */
            switch (cmbFilterBy.Text)
            {
                case "None":
                    FilterString = "";
                    break;
                case "Driver ID":
                    FilterString = "DriverID";
                    break;
                case "Person ID":
                    FilterString = "PersonID";
                    break;
                case "National No.":
                    FilterString = "NationalNo";
                    break;
                case "Full Name":
                    FilterString = "FullName";
                    break;
            }

            if (cmbFilterBy.Text.Trim() == "None" || txtFilterBy.Text.Trim() == "")
            {
                _dtDrivers.DefaultView.RowFilter = "";
                _LoadDriversData();
                lblNumberOfRecord.Text = dgvListPeople.Rows.Count.ToString();
                return;
            }
            if (FilterString == "DriverID" || FilterString == "PersonID")
            {
                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterString, txtFilterBy.Text.Trim());
            }
            else
            {
                _dtDrivers.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", FilterString, txtFilterBy.Text.Trim());
            }
            lblNumberOfRecord.Text = dgvListPeople.Rows.Count.ToString();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            frmShowPersonInfo frm = new frmShowPersonInfo((int)dgvListPeople.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void issueInternationalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvListPeople.CurrentRow.Cells[0].Value;
            
            clsDriver Driver = clsDriver.FindByDriverID(DriverID);

            int LicenseID = clsLicense.GetActiveLicenseIDByPersonID(Driver.PersonID, 3);

            if (LicenseID == -1)
            {
                MessageBox.Show("Driver with ID = " + DriverID + " does not have a Class 3 driver's licence", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(DriverID) != -1)
            {
                MessageBox.Show("Selected driver aleardy has an international license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmNewInternationalLicenseApplications frm = new frmNewInternationalLicenseApplications(LicenseID);
            frm.ShowDialog();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory((int)dgvListPeople.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
