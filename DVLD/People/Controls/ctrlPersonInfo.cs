using DVLD.Properties;
using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctrlPersonInfo : UserControl
    {
        private int _PersonID = -1;
        public int PersonID
        {
            get
            {
                return _PersonID;
            }
        }
        
        private clsPerson _Person;
        public clsPerson SelectedPersonInfo
        {
            get
            {
                return (clsPerson)_Person;
            }
        }
        public ctrlPersonInfo()
        {
            InitializeComponent();
        }
        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);

            if(_Person == null)
            {
                RestPersonInfo();
                MessageBox.Show("No person with PersonID = " +  PersonID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _FillPersonInfo();
            }
        }
        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);

            if(_Person == null )
            {
                RestPersonInfo();
                MessageBox.Show("No person with NationalNo = " + NationalNo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _FillPersonInfo();
            }
        }
        private void _FillPersonInfo()
        {
            llEditPerson.Enabled = _Person != null;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _PersonID.ToString();
            lblFullName.Text = _Person.FullName;
            lblEmail.Text = _Person.Email;
            lblGender.Text = (_Person.Gender == 0) ? "Male" : "Female";
            pbGender.Image = (_Person.Gender == 0) ? Resources.Man_32 : Resources.Woman_32;
            lblNationalNo.Text = _Person.NationalNo;
            lblPhone.Text = _Person.Phone;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblCountry.Text = clsCountry.Find(_Person.CountryID).CountryName;
            lblAddress.Text = _Person.Address;
            _LoadPersonImage();
        }
        public void RestPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "[----]";
            lblFullName.Text =  "[----]";
            lblEmail.Text = "[----]";

            lblGender.Text = "[----]";
            lblNationalNo.Text = "[----]";
            lblPhone.Text = "[----]";

            lblAddress.Text = "[----]";
            lblDateOfBirth.Text = "[----]";
            lblCountry.Text = "[----]";
            llEditPerson.Enabled = false;
            pbGender.Image = Resources.Man_32;
            pbImage.Image = Resources.Male_512;

        }
        private void _LoadPersonImage()
        {
            pbImage.Image = (_Person.Gender == 0) ? Resources.Male_512 : Resources.Female_512;

            string ImagePath = _Person.ImagePath;

            if(ImagePath != "")
            {
                if (File.Exists(ImagePath)) 
                {
                    pbImage.ImageLocation = ImagePath;
                }
                else
                {
                    MessageBox.Show("Could Not Find The Image: " +  ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void llEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddNewEdit frm = new frmAddNewEdit(_PersonID);

            frm.ShowDialog();

            LoadPersonInfo(_PersonID);
        }
    }
}
