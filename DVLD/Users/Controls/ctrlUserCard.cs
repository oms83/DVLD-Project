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
    public partial class ctrlUserCard : UserControl
    {
        private clsUser _User;

        public clsUser User
        {
            get => User;
        }

        public ctrlUserCard()
        {
            InitializeComponent();
            
        }

        public void RestUserInfo()
        {
            lblIsActive.Text = "---";
            lblUserID.Text = "---";
            lblUserName.Text = "---";

            ctrlPersonInfo1.RestPersonInfo();

        }
        private void _FillUserInfo()
        {
            ctrlPersonInfo1.LoadPersonInfo(_User.PersonID);

            lblIsActive.Text = _User.IsActive ? "Yes" : "No";

            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;
        }
        public void LoadUserInfo(int UserID)
        {

            _User = clsUser.FindByUserID(UserID);
            if ( _User == null )
            {
                RestUserInfo();

                MessageBox.Show("No User With UserID = " + UserID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();

        }
    }
}
