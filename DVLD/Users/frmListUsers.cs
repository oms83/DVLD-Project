using DVLD_Business_Layer;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmListUsers : Form
    {
        private DataTable _dtUsers = clsUser.GetAllUsers();
        public frmListUsers()
        {
            InitializeComponent();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();
            _RefreshUsersData();
        }

        private void _RefreshUsersData()
        {
            _dtUsers = clsUser.GetAllUsers();
            dgvListUsers.DataSource = _dtUsers;
        }
        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersData();
            _SetGridViewColumns();
        }
        private void _SetGridViewColumns()
        {
            lblNumberOfRecord.Text = dgvListUsers.Rows.Count.ToString();
            cmbFilterBy.SelectedIndex = 0;
            if (dgvListUsers.Rows.Count > 0)
            {
                dgvListUsers.Columns[0].HeaderText = "User ID";
                dgvListUsers.Columns[0].Width = 250;

                dgvListUsers.Columns[1].HeaderText = "PersonID ID";
                dgvListUsers.Columns[1].Width = 250;

                dgvListUsers.Columns[2].HeaderText = "Full Name";
                dgvListUsers.Columns[2].Width = 300;

                dgvListUsers.Columns[4].HeaderText = "Is Active";
                dgvListUsers.Columns[3].Width = 200;
            }
        }
        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = (cmbFilterBy.Text != "None");

            cmbFilterIsActive.Visible = (cmbFilterBy.Text == "Is Active");

            if (cmbFilterIsActive.Visible) 
            {
                txtFilterBy.Visible = false; 
                cmbFilterIsActive.SelectedIndex = 0;
            }

            if (txtFilterBy.Visible) 
            {
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }

            _RefreshUsersData();


        }
        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilteringTemp = "";

            /*
                None
                User ID
                UserName
                Person ID
                Full Name
                Is Active 
            */

            switch (cmbFilterBy.Text)
            {
                case "Person ID":
                    FilteringTemp = "PersonID";
                    break;

                case "User ID":
                    FilteringTemp = "UserID";
                    break;
                case "UserName":
                    FilteringTemp = "UserName";
                    break;
                case "Full Name":
                    FilteringTemp = "FullName";
                    break;
                case "Is Active":
                    FilteringTemp = "IsActive";
                    break;
                default:
                    FilteringTemp = "None";
                    break;
            }

            // This condition should be the first condition
            if (FilteringTemp == "None" || txtFilterBy.Text == "")
            {
                _dtUsers.DefaultView.RowFilter = "";
                _RefreshUsersData();
            }
            else if (FilteringTemp == "PersonID" || FilteringTemp == "UserID")
            {
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilteringTemp, txtFilterBy.Text.Trim());
            }
            else if(FilteringTemp == "FullName" || FilteringTemp == "UserName")
            {
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilteringTemp, txtFilterBy.Text.Trim());
            }

            lblNumberOfRecord.Text = dgvListUsers.Rows.Count.ToString();
        }
        private void cmbFilterIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ColumnName = "IsActive";
            string ColumnValue = cmbFilterIsActive.Text;
            
            switch (ColumnValue)
            {
                case "Yes":
                    ColumnValue = "1";
                    break;

                case "No":
                    ColumnValue = "0";
                    break;

                default:
                    ColumnValue = "";
                    break;
            }

            if (cmbFilterIsActive.Text == "All" || ColumnValue == "")
            {
                _dtUsers.DefaultView.RowFilter = "";
            }
            else
            {
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnName, ColumnValue);
            }

            lblNumberOfRecord.Text = dgvListUsers.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvListUsers.CurrentRow.Cells[0].Value;
            frmAddEditUser frm = new frmAddEditUser(UserID);
            frm.ShowDialog();
            _RefreshUsersData();
        }

        private void tsmAddNew_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();
            _RefreshUsersData();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFilterBy.Text == "Person ID" || cmbFilterBy.Text == "User ID")
            {
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
            }
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvListUsers.CurrentRow.Cells[0].Value;

            if (clsUser.DeleteUser(UserID))
            {
                MessageBox.Show("User with UserID = " + UserID + " deleted successfully!", "Confirm Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _RefreshUsersData();
            }
            else
            {
                MessageBox.Show("User was not deleted because is has linked data to it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet!", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsmPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet!", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvListUsers.CurrentRow.Cells[0].Value); 
            frm.ShowDialog();

        }

        private void dgvListUsers_DoubleClick(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvListUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dgvListUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshUsersData();
        }
    }
}
