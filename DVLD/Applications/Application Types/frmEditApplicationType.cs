using DVLD.Global;
using DVLD_Business_Layer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Application_Types
{
    public partial class frmEditApplicationType : Form
    {
        clsApplicationType _ApplicationType;
        int _ApplicationTypeID;
        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();

            _ApplicationTypeID = ApplicationTypeID;
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void _LoadData()
        {
            _ApplicationType = clsApplicationType.Find(_ApplicationTypeID);
            
            if (_ApplicationType == null)
            {
                MessageBox.Show($"No Application Type With ApplicationTypeID = {_ApplicationTypeID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
                
                return;
            }

            txtFees.Text = _ApplicationType.ApplicationFees.ToString();
            txtTitle.Text = _ApplicationType.ApplicationTypeTitle;
            lblApplicationTypeID.Text = _ApplicationType.ApplicationTypeID.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some feild are not valid!, Please put the mouse over the red icon(s) to see the error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            // ToSingle ==> Float
            _ApplicationType.ApplicationFees = Convert.ToSingle(txtFees.Text);
            _ApplicationType.ApplicationTypeTitle = txtTitle.Text;

            if (_ApplicationType.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Confirm Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Is Not Saved Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                e.Cancel = true;
                txtTitle.Focus();
                errorProvider1.SetError(txtTitle, "this field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text))
            {
                e.Cancel = true;
                txtFees.Focus();
                errorProvider1.SetError(txtFees, "this field is required!");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFees, null);
            }


            if(!clsValidation.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                txtFees.Focus();
                errorProvider1.SetError(txtFees, "Invalid number!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFees, null);
            }
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {

            /*
             
                // Allowing digits, decimal point, and control characters like backspace
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true; // Ignore the character
                }
            */


            //e.Handled = true; // Ignore the character

            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.';

            // Allowing only one decimal point
            e.Handled = (e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1);
        }
    }
}
