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

namespace DVLD.Licenses.Local_Licenses
{
    public partial class frmIssueDrivingLicenseFirtsTime : Form
    {
        int _LocalDrivingLicenseApplicationID = -1;
        public frmIssueDrivingLicenseFirtsTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void frmIssueDrivingLicenseFirtsTime_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license for this application", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                this.Close();
                return;
            }
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Application Driving License App. With ID" + _LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }



            if (!LocalDrivingLicenseApplication.PassedAllTests())
            {
                MessageBox.Show("Error: Person should pass all test firt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = LocalDrivingLicenseApplication.GetActiveLicenseID();
            if (LicenseID != -1)
            {

                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }
            if (LocalDrivingLicenseApplication.IssuedLicense(GlobalSettings.CurrentUser.UserID, txtNotes.Text) != -1)
            {
                ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
                MessageBox.Show("For This Application The License Issued Successfully", "Confirm Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Error: For This Application The License Is Not Issued Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (txtNotes.Text != "")
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
