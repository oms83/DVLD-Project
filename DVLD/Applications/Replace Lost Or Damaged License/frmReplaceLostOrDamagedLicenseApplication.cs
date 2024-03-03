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

namespace DVLD.Applications.Replace_Lost_Or_Damaged_License
{
    public partial class frmReplaceLostOrDamagedLicenseApplication : Form
    {
        private int _LicenseID = -1;
        private clsLicense _License;
        private clsLicense _NewLicense;
        public frmReplaceLostOrDamagedLicenseApplication()
        {
            InitializeComponent();
        }
        private void _RestLicenseInfo()
        {
            lblApplicationDate.Text = "[DD/MM/YYYY]";

            lblRreplacedLicenseID.Text = "[????]";

            lblApplicationID.Text = "[????]";
            lblOldLicenseID.Text = "[????]";
            lblRreplacedLicenseID.Text = "[????]";
            lblCreatedByUser.Text = GlobalSettings.CurrentUser.UserName;



            btnIssueReplacement.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.EditPersonInfoEnabled = false;
        }

        private void _LoadLicenseInfo()
        {
            ctrlDriverLicenseInfoWithFilter1.EditPersonInfoEnabled = true;
            btnIssueReplacement.Enabled = true;
            lblOldLicenseID.Text = _License.LicenseID.ToString();
            lblCreatedByUser.Text = GlobalSettings.CurrentUser.UserName;
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense).ApplicationFees.ToString();

            //lblApplicationFees.Text = rbDamagedLicense.Checked ?
            //                        clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense).ApplicationFees.ToString() :
            //                        clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense).ApplicationFees.ToString();

            lblApplicationDate.Text = clsFormat.DateFormat(DateTime.Now);
            
            //lblApplicationID.Text = 
            //lblRreplacedLicenseID.Text = 
        }
        private void frmReplaceLostOrDamagedLicenseApplication_Load(object sender, EventArgs e)
        {
            rbLostLicense.Checked = true;
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense).ApplicationFees.ToString();

            _RestLicenseInfo();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int LicenseID)
        {
            _RestLicenseInfo();
            _License = clsLicense.Find(LicenseID);

            if (_License == null)
            {
                MessageBox.Show("Error: No Message With ID = " + LicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            llShowLicenseHistory.Enabled = true;
            
            if (!_License.IsActive)
            {
                llShowLicenseHistory.Enabled = true;
                llShowLicenseInfo.Enabled = true;
                MessageBox.Show("Selected license should be active", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadLicenseInfo();
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense).ApplicationFees.ToString();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense).ApplicationFees.ToString();
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to replace this license with ID = " + _License.LicenseID, "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            _NewLicense = _License.Replace( rbLostLicense.Checked ? clsLicense.enIssueReason.LostReplacement : 
                                                                    clsLicense.enIssueReason.DamagedReplacement,
                                                                    GlobalSettings.CurrentUser.UserID);

            if (_NewLicense != null)
            {
                ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
                gbReplacementChoices.Enabled = false;
                btnIssueReplacement.Enabled = false;

                llShowLicenseHistory.Enabled = true;
                llShowLicenseInfo.Enabled = true;

                lblApplicationID.Text = _NewLicense.ApplicationID.ToString();
                lblRreplacedLicenseID.Text = _NewLicense.LicenseID.ToString();
                MessageBox.Show("The license replaced successfully with NewLicenseID = " + _NewLicense.LicenseID, "Success Renew", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: The license was not replaced successfully with NewLicenseID = " + _NewLicense.LicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_NewLicense != null)
            {
                _NewLicense = clsLicense.Find(_NewLicense.LicenseID);
                frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicense.LicenseID);
                frm.ShowDialog();
            }
            else
            {
                frmShowLicenseInfo frm = new frmShowLicenseInfo(_License.LicenseID);
                frm.ShowDialog();
            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_License.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void frmReplaceLostOrDamagedLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.FocusFilter();
        }
    }
}
