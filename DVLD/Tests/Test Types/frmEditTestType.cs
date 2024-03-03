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

namespace DVLD.Tests.Test_Types
{
    public partial class frmEditTestType : Form
    {

        private int _TestTypeID;


        private clsTestType _TestType;

        public frmEditTestType(int TestTypeID)
        {
            InitializeComponent();

            _TestTypeID = TestTypeID;
        }

        private void _RestTestTypeInfo()
        {
            txtDescription.Text = "";
            txtFees.Text = "";
            txtTitle.Text = "";
            lblTestTypeID.Text = "";
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
        private void _LoadData()
        {
            _TestType = clsTestType.Find((clsTestType.enTestType)_TestTypeID);

            if (_TestType == null)
            {
                MessageBox.Show($"No Test Type With Test Type ID = {_TestTypeID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _RestTestTypeInfo();

                this.Close();

                return;
            }

            txtDescription.Text = _TestType.TestTypeDescription;
            txtFees.Text = _TestType.TestFees.ToString();
            txtTitle.Text = _TestType.TestTypeTitle;

            lblTestTypeID.Text = ((short)_TestType.ID).ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Field Is Not Valid!, Please Put The Mouse Over The Red Icon(s) To See The Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _TestType.TestTypeDescription = txtDescription.Text.Trim();
            _TestType.TestTypeTitle = txtTitle.Text.Trim();
            _TestType.TestFees = Convert.ToSingle(txtFees.Text.Trim());

            if (_TestType.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Confirm Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Not Saved Successfully", "Confirm Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                e.Cancel = true;
                txtTitle.Focus();
                errorProvider1.SetError(txtTitle, "this field is required");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                e.Cancel = true;
                txtTitle.Focus();
                errorProvider1.SetError(txtDescription, "this field is required");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtDescription, null);
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text))
            {
                e.Cancel = true;
                txtTitle.Focus();
                errorProvider1.SetError(txtFees, "this field is required");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFees, null);
            }

            if (!clsValidation.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                txtTitle.Focus();
                errorProvider1.SetError(txtFees, "Invalid Number!");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFees, null);
            }
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != (char)46);
            e.Handled = (e.KeyChar == (char)46) && (txtFees.Text.IndexOf('.') > -1);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }


    }
}
