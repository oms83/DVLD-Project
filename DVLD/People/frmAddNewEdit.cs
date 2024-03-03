using DVLD.Global;
using DVLD.Properties;
using DVLD_Business_Layer;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmAddNewEdit : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);

        public event DataBackEventHandler DataBack;
        private enum enGender { Male=0, Female=1 }
        private enum enMode { AddNew, Update }

        private enMode _Mode;

        private int _PersonID = -1;

        private clsPerson _Person;

        public frmAddNewEdit()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        public frmAddNewEdit(int PersonID)
        {
            InitializeComponent();

            _Mode = enMode.Update;

            _PersonID = PersonID;
        }

        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }

        private void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("Person With ID = " + _PersonID + " Not Found", "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            txtFirstName.Text = _Person.FirstName;
            lblPersonID.Text = _Person.PersonID.ToString();
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;

            txtAddress.Text = _Person.Address;
            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.Phone;
            txtNationalNo.Text = _Person.NationalNo;
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            if (_Person.ImagePath != null || _Person.ImagePath != "")
            {
                pbImage.ImageLocation = _Person.ImagePath;
            }
            else
            {
                pbImage.ImageLocation = "";
            }

            if (_Person.Gender == 0)
            {
                rbMale.Checked = true;
            }
            else if (_Person.Gender == 1)
            {
                rbFemale.Checked = true;
            }

            pbImage.Image = rbMale.Checked ? Resources.Male_512 : Resources.Female_512;

            cbCountry.SelectedIndex = cbCountry.FindString(_Person.CountryInfo.CountryName);

            llRemoveIamge.Visible = (pbImage.ImageLocation != "");

            _Mode = enMode.Update;
        }

        private void rbMale_Click(object sender, System.EventArgs e)
        {
            if (pbImage.ImageLocation == null || pbImage.ImageLocation == "")
                pbImage.Image = Resources.Male_512;
        }

        private void rbFemale_Click(object sender, System.EventArgs e)
        {
            if (pbImage.ImageLocation == null || pbImage.ImageLocation == "")
                pbImage.Image = Resources.Female_512;
        }

        private void _RestDefaultValues()
        {
            _FillCountriesInComboBox();

            if(_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblMode.Text = "Update Person Info";
            }

            pbImage.Image = rbMale.Checked ? Resources.Male_512 : Resources.Female_512;

            txtFirstName.Text = "";
            lblPersonID.Text = "N/A";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";

            txtAddress.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtNationalNo.Text = "";
            pbImage.ImageLocation = "";
            llRemoveIamge.Visible = (pbImage.ImageLocation != "");

            rbMale.Checked = true;

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            cbCountry.SelectedIndex = cbCountry.FindString("Syria");

        }

        private void frmAddNewEdit_Load(object sender, EventArgs e)
        {
            _RestDefaultValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _HandlePersonIamge()
        {
            if(pbImage.ImageLocation != _Person.ImagePath)
            {
                if (_Person.ImagePath != "") 
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);

                    }
                    catch (IOException iox)
                    {

                    }
                }

                if (pbImage.ImageLocation != null)
                {
                    string FileSource = pbImage.ImageLocation.ToString();
                    
                    if(clsUtility.CopyImageToProjectImagesFolder(ref FileSource))
                    {
                        pbImage.ImageLocation = FileSource;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren()) 
            {
                MessageBox.Show("Some fields are not valid!, Put the mouse over the red icon(s) to see the error.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!_HandlePersonIamge())
            {
                return;
            }

            _Person.CountryID = clsCountry.Find(cbCountry.Text).CountryID;

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.Gender = (short)(rbMale.Checked ? (short)enGender.Male : (short)enGender.Female);

            _Person.ImagePath = (pbImage.ImageLocation != null) ? pbImage.ImageLocation : "";


            _Person.Address = txtAddress.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();

            if(_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                lblMode.Text = "Update Person Info";
                _Mode = enMode.Update;

                DataBack?.Invoke(sender, _Person.PersonID);
                
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Is Not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llRemoveIamge_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbImage.ImageLocation = null;

            if (rbMale.Checked)
            {
                pbImage.Image = Resources.Male_512;
            }
            else if (rbFemale.Checked)
            {
                pbImage.Image = Resources.Female_512;
            }

            llRemoveIamge.Visible = false;
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string SelectedFilePath = openFileDialog1.FileName;
                pbImage.ImageLocation = SelectedFilePath;
                llRemoveIamge.Visible = true;
            }
        }

        private void ValidateEmptyTextBox(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var TempControl = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(TempControl.Text.Trim())) 
            {
                //btnSave.Enabled = false;
                e.Cancel = true;
                TempControl.Focus();
                errorProvider1.SetError(TempControl, "This field is required.");
            }
            else
            {
                //btnSave.Enabled = true;
                e.Cancel = false;
                errorProvider1.SetError(TempControl, null);
            }
        }

        private void txtNationalNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string NationalNoField = txtNationalNo.Text.Trim();

            if (string.IsNullOrWhiteSpace(NationalNoField))
            {
                e.Cancel = true;
                txtNationalNo.Focus();
                errorProvider1.SetError(txtNationalNo, "This field is required!");

                // If we don't user there "return" at here then we can't see the error of empty text box.
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, null);
            }


            if ((NationalNoField != _Person.NationalNo) && clsPerson.IsPersonExist(NationalNoField)) 
            {
                e.Cancel = true;
                txtNationalNo.Focus();
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void txtEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
            {
                return;
            }

            if (!clsValidation.ValidateEmail(txtEmail.Text)) 
            {
                e.Cancel = true;
                txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "Invalid email address format!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, null);
            }
        }
    }
}
