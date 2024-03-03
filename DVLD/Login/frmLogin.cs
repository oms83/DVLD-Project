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

namespace DVLD
{
    public partial class frmLogin : Form
    {
        private string UserName, Password;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void LoadLoginInfoData()
        {
            GlobalSettings.ReadDataFromFile(ref UserName, ref Password);

            if (UserName!="" && Password!="")
            {
                chkRememberMe.Checked = true;
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
            }
            else
            {
                txtUserName.Text = "";
                txtPassword.Text = "";
            }
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            LoadLoginInfoData();
        }

        private void SaveDataInFile()
        {
            if (chkRememberMe.Checked && UserName != "" && Password != "")
            {
                GlobalSettings.WriteDataInFile(UserName, Password, true);
            }
            else if (!chkRememberMe.Checked && Password != "" && UserName != "")
            {
                GlobalSettings.WriteDataInFile(UserName, Password, false);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            UserName = txtUserName.Text;
            Password = txtPassword.Text;

            GlobalSettings.CurrentUser = clsUser.FindByUserNameAndPassword(UserName, Password);

            if (GlobalSettings.CurrentUser == null)
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid UserName Or Password!", "Wrong Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!GlobalSettings.CurrentUser.IsActive)
            {
                txtUserName.Focus();
                MessageBox.Show("Your Account Is Not Active, Contact Your Admin", "Inactive Account", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            SaveDataInFile();

            this.Hide();
            frmMain frm = new frmMain(this);
            frm.ShowDialog();

            GlobalSettings.CurrentUser = null;

        }
    }
}
