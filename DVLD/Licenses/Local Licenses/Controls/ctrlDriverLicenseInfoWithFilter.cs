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
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
       
        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID);
            }
        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
            get
            {
                return _FilterEnabled;
            }
        }

        int _txtFilterValue = -1;
        public int txtFilterValue
        {
            set
            {
                _txtFilterValue = value;
                txtLicenseID.Text = _txtFilterValue.ToString();
            }
            get
            {
                return int.Parse(txtLicenseID.Text);
            }
        }

        public clsLicense SelectedLicense
        {
            get => ctrlDriverLicenseInfo1.SelectedLicense;
        }


        private int _LicenseID = -1;
        public int LicenseID
        {
            get => ctrlDriverLicenseInfo1.LicenseID;
        }

        public bool EditPersonInfoEnabled
        {
            set
            {
                ctrlDriverLicenseInfo1.EditPersonInfoEnabled = value;
            }
            get => ctrlDriverLicenseInfo1.EditPersonInfoEnabled;
        }
        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }
        
        private void _FindNow()
        {
            /*
                    if (txtLicenseID.Text.Trim() != "")
                    {
                        _LicenseID = int.Parse(txtLicenseID.Text.Trim());

                        _License = clsLicense.Find(_LicenseID);

                        if (_License == null)
                        {
                            if (OnLicenseSelected != null)
                            {
                                OnLicenseSelected(-1);
                            }
                            ctrlDriverLicenseInfo1._RestLicenseInfo();
                            ctrlDriverLicenseInfo1.SelectedLicense = null;
                            MessageBox.Show("No License With LicenseID = " + _LicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        _PersonID = _License.DriverInfo.PersonID;

                        ctrlDriverLicenseInfo1.LoadLicenseInfo(_License.LicenseID);


                    }
            */


            _LicenseID = int.Parse(txtLicenseID.Text.Trim());

            ctrlDriverLicenseInfo1.LoadLicenseInfo(_LicenseID);

            _LicenseID = ctrlDriverLicenseInfo1.LicenseID;

            if (OnLicenseSelected != null)
            {
                OnLicenseSelected(_LicenseID);
                //OnLicenseSelected(_LicenseID);
            }
        }

        public void LoadLicenseInfo(int LicenseID)
        {
            txtLicenseID.Text = LicenseID.ToString();
            ctrlDriverLicenseInfo1.LoadLicenseInfo(LicenseID);

        }
        private void btnFind_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!. put the mouse over the red icon(s) to see the error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FindNow();
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if(e.KeyChar == (char)13)

            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFind.PerformClick();
            }

            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void ctrlDriverLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {
            txtLicenseID.Focus();
        }

        public void FocusFilter()
        {
            txtLicenseID.Focus();
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (txtLicenseID.Text.Trim() == "")
            {
                e.Cancel = true;
                txtLicenseID.Focus();
                errorProvider1.SetError(txtLicenseID, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtLicenseID, null);
            }

        }

        
    }
}
    