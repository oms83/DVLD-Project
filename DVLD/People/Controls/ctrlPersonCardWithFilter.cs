using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public int PersonID
        {
            get => ucPersonInfo1.PersonID;
        }

        public clsPerson SelectedPersonInfo
        {
            get => ucPersonInfo1.SelectedPersonInfo;
        }

        private bool _FilterEnable = true;
        public bool FilterEnable
        {
            get => _FilterEnable;
            set
            {
                _FilterEnable = value;
                gbFilter.Enabled = _FilterEnable;
            }
        }

        private bool _ShowAddNewPerson = true;

        public bool ShowAddNewPerson
        {
            get => _ShowAddNewPerson;
            set
            {
                _ShowAddNewPerson = value;
                btnAddNew.Enabled = _ShowAddNewPerson;
            }
        }

        public void FilterFocus()
        {
            txtFilterBy.Focus();
        }

        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> Handler = OnPersonSelected;

            if(Handler != null)
            {
                Handler(PersonID);
            }
        }
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int PersonID)
        {
            // Load Filter Info
            cmbFilterBy.SelectedIndex = 1;
            txtFilterBy.Text = PersonID.ToString();

            // Load Person Info
            FindNow();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddNewEdit frm = new frmAddNewEdit();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }

        private void DataBackEvent(object sender, int PersonID)
        {
            // ************************************
            cmbFilterBy.SelectedIndex = 1;
            txtFilterBy.Text = PersonID.ToString();
            // ************************************

            ucPersonInfo1.LoadPersonInfo(PersonID)
;
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if pressed key is Enter 
            if(e.KeyChar == (char)13)
            {
                btnSearch.PerformClick();
            }

            if (cmbFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
            }
        }

        private void FindNow()
        {
            if (cmbFilterBy.Text == "National No.")
            {
                ucPersonInfo1.LoadPersonInfo(txtFilterBy.Text);
            }
            else if (cmbFilterBy.Text == "Person ID") 
            {
                ucPersonInfo1.LoadPersonInfo(int.Parse(txtFilterBy.Text));
            }

            if (OnPersonSelected != null && _FilterEnable)
                PersonSelected(PersonID);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // ValidataChilder return true if all of the controlls validated successfully, other than that/otherwise false.

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!. put the mouse over the red icon(s) to see the error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FindNow();
        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Text = string.Empty;
            txtFilterBy.Focus();
        }

        private void txtFilterBy_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.Trim()) && PersonID == -1) 
            {
                e.Cancel = true;
                txtFilterBy.Focus();
                errorProvider1.SetError(txtFilterBy, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFilterBy, null);
            }
        }

        private void ctrlUserCardWithFilter_Load(object sender, EventArgs e)
        {
            cmbFilterBy.SelectedIndex = 0;
            txtFilterBy.Text = "";
            txtFilterBy.Focus();
        }
    }
}
