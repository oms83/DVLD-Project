using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmAddEditUser : Form
    {
        public delegate void DataBackEventHandler(object sender, int UserID);
        public event DataBackEventHandler DataBack;

        private int _UserID = -1;
        private clsUser _User;

        private enum enMode { AddNew, Update }

        private enMode _Mode;

        public frmAddEditUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddEditUser(int UserID)
        {
            _UserID = UserID;
            _Mode = enMode.Update;
            InitializeComponent();
        }
        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _ResetUserInfo(); 
            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }
        private void _ResetUserInfo()
        {
            if(_Mode == enMode.AddNew)
            {
                _User = new clsUser();

                tpLoginInfo.Enabled = false;
                btnSave.Enabled = false;

                ctrlUserCardWithFilter1.FilterFocus();

                this.Text = "Add New User";
                lblMode.Text = "Add New User"; 
            }

            else if( _Mode == enMode.Update)
            {
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;

                this.Text = "Update User Info";
                lblMode.Text = "Update User Info";
            }

            txtConfirmPassword.Text = "";
            txtPassword.Text = "";
            txtUserName.Text = "";
        }
        private void _LoadData()
        {
            _User = clsUser.FindByUserID(_UserID);

            if(_User == null)
            {
                MessageBox.Show("Error: Does not found user with UserID = " + _UserID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            lblUserID.Text = _UserID.ToString();
            txtUserName.Text = _User.UserName.ToString();
            txtConfirmPassword.Text = _User.Password;
            txtPassword.Text = _User.Password;
            cbIsActive.Checked = _User.IsActive;
            ctrlUserCardWithFilter1.FilterEnable = false; 

            ctrlUserCardWithFilter1.LoadPersonInfo(_User.PersonID);

        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnNext.Enabled = true;
                tcAddEditUser.SelectedTab = tcAddEditUser.TabPages["tpLoginInfo"];
            }

            else if (_Mode == enMode.AddNew) 
            {
                if (ctrlUserCardWithFilter1.SelectedPersonInfo == null)
                {

                    MessageBox.Show("Please select or add a new person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlUserCardWithFilter1.FilterFocus();

                }
                else if (clsUser.IsUserExistForPersonID(ctrlUserCardWithFilter1.PersonID))
                {

                    MessageBox.Show("Selected person already has a user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tcAddEditUser.SelectedTab = tcAddEditUser.TabPages["tpLoginInfo"];
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid! please put the mouse over the red icon(s) to see the error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.PersonID = ctrlUserCardWithFilter1.PersonID;
            _User.Password = txtPassword.Text.Trim();
            _User.UserName = txtUserName.Text.Trim();
            //_User.IsActive = cbIsActive.Checked ? true : false; 
            _User.IsActive = cbIsActive.Checked;

            if (_User.Save())
            {
                _UserID = _User.UserID;
                lblUserID.Text = _UserID.ToString();
                lblMode.Text = "Update User Info";
                this.Text = "Update User";

                ctrlUserCardWithFilter1.FilterEnable = false;

                DataBack?.Invoke(sender, _User.UserID);

                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Does Not Saved Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {

            if(string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "This field is required!");

                // If we don't use 'return' here then we can't see the empty text box error
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, null);
            };


            if (_Mode == enMode.AddNew)
            {
                if (clsUser.IsUserExist(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "UserName is used for another user.add");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                }
            }

            else
            {
                if (txtUserName.Text.Trim() != _User.UserName)
                {
                    if (clsUser.IsUserExist(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "UserName is used for another user.up");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    }
                }
            }


            //if (txtUserName.Text.Trim() != _User.UserName && clsUser.IsUserExist(txtUserName.Text.Trim()))
            //{
            //    e.Cancel = true;
            //    txtUserName.Focus();
            //    errorProvider1.SetError(txtUserName, "UserName is used for another user.");
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(txtUserName, null);
            //}


        }

        private void ValidationPassword(Control Password, Control ConfirmPassword, string ErrorMessage1, string ErrorMessage2, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Password.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Password, ErrorMessage1);
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(Password, null);
            }

            if (!string.IsNullOrEmpty(ConfirmPassword.Text.Trim()) && Password.Text.Trim() != ConfirmPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(Password, ErrorMessage2);
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(Password, null);
            }
        }
       
        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password can't be blank");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text.Trim()))
            {
                e.Cancel = true;
                txtConfirmPassword.Focus();
                errorProvider1.SetError(txtConfirmPassword, "Password can't be blank");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, null);
            }

            if (!string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()) && txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                e.Cancel = true;
                txtConfirmPassword.Focus();
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, null);
            }

            /*
            
                ValidationPassword(txtConfirmPassword, txtPassword, "Password can't be blank",
                    "Password Confirmation does not match Password!", e);
            */
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlUserCardWithFilter1_OnPersonSelected(int obj)
        {
            //MessageBox.Show(obj.ToString(), "On Person Seleceted Event", MessageBoxButtons.OK);
        }


        // When the form loads, the focus will be on the "txtFilterValue" control
        private void frmAddEditUser_Activated(object sender, EventArgs e)
        {
            ctrlUserCardWithFilter1.FilterFocus();
        }
    }
}
