using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmManagePeople : Form
    {
        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();

        // Only the selected columns will be shown in the grid view
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName",
                                                    "ThirdName", "LastName", "GenderCaption", "DateOfBirth", "CountryName", 
                                                    "Phone", "Email");
        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void _RefershPeopleList()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName",
                                                    "ThirdName", "LastName", "GenderCaption", "DateOfBirth", "CountryName",
                                                    "Phone", "Email");
            dgvListPeople.DataSource = _dtPeople;

            lblNumberOfRecord.Text = dgvListPeople.Rows.Count.ToString();
        }



        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _SetGridViewColumns();
        }

        private void _SetGridViewColumns()
        {
            dgvListPeople.DataSource = _dtPeople;
            cmbFilterBy.SelectedIndex = 0;

            lblNumberOfRecord.Text = dgvListPeople.Rows.Count.ToString();

            if(_dtPeople.Rows.Count > 0)
            {
                dgvListPeople.Columns[0].HeaderText = "Person ID";
                dgvListPeople.Columns[0].Width = 100;

                dgvListPeople.Columns[1].HeaderText = "National No";
                dgvListPeople.Columns[1].Width = 100;

                dgvListPeople.Columns[2].HeaderText = "First Name";
                dgvListPeople.Columns[2].Width = 120;

                dgvListPeople.Columns[3].HeaderText = "Second Name";
                dgvListPeople.Columns[3].Width = 120;

                dgvListPeople.Columns[4].HeaderText = "Third Name";
                dgvListPeople.Columns[4].Width = 120;

                dgvListPeople.Columns[5].HeaderText = "Last Name";
                dgvListPeople.Columns[5].Width = 120;

                dgvListPeople.Columns[6].HeaderText = "Gender";
                dgvListPeople.Columns[6].Width = 60;

                dgvListPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvListPeople.Columns[7].Width = 130;

                dgvListPeople.Columns[8].HeaderText = "Country Name";
                dgvListPeople.Columns[8].Width = 100;

                dgvListPeople.Columns[9].HeaderText = "Phone";
                dgvListPeople.Columns[9].Width = 120;

                dgvListPeople.Columns[10].HeaderText = "Email";
                dgvListPeople.Columns[10].Width = 175;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterByValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            /*
                None
                Person ID
                National No.
                First Name
                Second Name
                Third Name
                Last Name
                Nationality
                Gendor
                Phone
                Email 
            */
            switch (cmbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "First Name":
                    FilterColumn = "FirstName";
                    break;
                case "Second Name":
                    FilterColumn = "SecondName";
                    break;
                case "Last Name":
                    FilterColumn = "LastName";
                    break;
                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;
                case "Nationality":
                    FilterColumn = "CountryName";
                    break;
                case "Gender":
                    FilterColumn = "Gender";
                    break;
                case "Phone":
                    FilterColumn = "Phone";
                    break;
                case "Email":
                    FilterColumn = "Email";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if(txtFilterByValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblNumberOfRecord.Text = dgvListPeople.Rows.Count.ToString();
                return;
            }
            
            if(FilterColumn == "PersonID" || cmbFilterBy.Text == "PersonID")
            {
                // In This Case We Deal With Integer Not Strring.
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterByValue.Text.Trim());

                //_dtPeople.DefaultView.RowFilter = string.Format("{0} = {1}", FilterColumn, txtFilterByValue.Text.Trim());
            }
            else
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterByValue.Text.Trim());
            }

            lblNumberOfRecord.Text = dgvListPeople.Rows.Count.ToString();
        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterByValue.Visible = (cmbFilterBy.Text != "None");

            if(txtFilterByValue.Visible)
            {
                txtFilterByValue.Text = "";
                txtFilterByValue.Focus();
            }
        }

        private void btnAddNewEdit_Click(object sender, EventArgs e)
        {
            frmAddNewEdit frm = new frmAddNewEdit();
            frm.ShowDialog();
            _RefershPeopleList();
        }

        private void tsmAddNew_Click(object sender, EventArgs e)
        {
            frmAddNewEdit frm = new frmAddNewEdit();
            frm.ShowDialog();
            _RefershPeopleList();
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvListPeople.CurrentRow.Cells[0].Value;
            frmAddNewEdit frm = new frmAddNewEdit(PersonID);
            frm.ShowDialog();
            _RefershPeopleList();
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvListPeople.CurrentRow.Cells[0].Value;
            if (clsPerson.Delete(PersonID))
            {
                MessageBox.Show("Person With ID = " + PersonID + " Deleted Successfully", "Confirm Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _RefershPeopleList();
            }
            else
            {
                MessageBox.Show("Person Was Not Deleted Because It Has Data Linked To It.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvListPeople.CurrentRow.Cells[0].Value;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
            _RefershPeopleList();
        }

        private void tsmSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsmPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Not Implemented Yet!", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void dgvListPeople_DoubleClick(object sender, EventArgs e)
        {
            int PersonID = (int)dgvListPeople.CurrentRow.Cells[0].Value;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void txtFilterByValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cmbFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
