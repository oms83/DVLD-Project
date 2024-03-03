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
    public partial class frmChangePassword : Form
    {
        private int _UserID;

        private clsUser _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _User = clsUser.FindByUserID(_UserID);

            if (_User == null)
            {
                MessageBox.Show("No User With UserID = " + _UserID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _RestUserPasswordInfo();
                this.Close();
                return;
            }

            ctrlUserCard1.LoadUserInfo(_UserID);
        }
        private void _RestUserPasswordInfo()
        {
            txtConfirmPassword.Text = string.Empty;
            txtCurrentPassword.Text = string.Empty;
            txtNewPassword.Text = string.Empty;

            txtCurrentPassword.Focus();

            ctrlUserCard1.RestUserInfo();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some feilds are not valid!, Please put the mouse over the red icon(s) to see the error", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.Password = txtConfirmPassword.Text.Trim();

            if (_User.ChangePassword())
            {
                MessageBox.Show("Password Updated Successfully", "Confirm Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Password Is Not Updated Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            /*
                    if (_User.Save())
                    {
                        MessageBox.Show("Password Updated Successfully", "Confirm Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error: Password Is Not Updated Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } 
            */

        }
        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                txtCurrentPassword.Focus();
                errorProvider1.SetError(txtCurrentPassword, "Current password can't be blank");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCurrentPassword, null);
            }

            if (_User.Password != txtCurrentPassword.Text.Trim())
            {
                e.Cancel = true;
                txtCurrentPassword.Focus();
                errorProvider1.SetError(txtCurrentPassword, "The entered password is not equal to the current password!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCurrentPassword, null);
            }
        }
        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                txtNewPassword.Focus();
                errorProvider1.SetError(txtNewPassword, "New password can't be blank");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNewPassword, null);
            }

        }
        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
            {
                e.Cancel = true;
                txtConfirmPassword.Focus();
                errorProvider1.SetError(txtConfirmPassword, "Current password can't be blank");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, null);
            }

            if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Passwrod Confirmation does not match New Password");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (txtConfirmPassword.Text != "" || txtNewPassword.Text != "" || txtCurrentPassword.Text != "")
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
