namespace DVLD
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.msServices = new System.Windows.Forms.MenuStrip();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.mtsApplications = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsDrivingLicensesServices = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsNewDrivingLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsLocalLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsInternationalLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsRenewDrivingLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsReplacementLostDamaged = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsReleaseDetainDrivingLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsRetakeTest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmsManageApplications = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLocalDrivingLicenseApplications = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmInternationalDrivingLicenseApplications = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tmsDetainLicenses = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmManageDetained = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDetainLicenses = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmReleaseDetainLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tmsManageApplicationTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.tmsManageTestTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.mtsManagePeople = new System.Windows.Forms.ToolStripMenuItem();
            this.mtsDrivers = new System.Windows.Forms.ToolStripMenuItem();
            this.mtsUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.mtsSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCurrentUserInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.msServices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // msServices
            // 
            this.msServices.BackColor = System.Drawing.Color.White;
            this.msServices.Font = new System.Drawing.Font("Cascadia Code", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msServices.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.msServices.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.msServices.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mtsApplications,
            this.mtsManagePeople,
            this.mtsDrivers,
            this.mtsUsers,
            this.mtsSetting});
            this.msServices.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.msServices.Location = new System.Drawing.Point(0, 0);
            this.msServices.Name = "msServices";
            this.msServices.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.msServices.Size = new System.Drawing.Size(1383, 102);
            this.msServices.TabIndex = 0;
            this.msServices.Text = "menuStrip1";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.Image = global::DVLD.Properties.Resources.speed;
            this.pictureBox2.Location = new System.Drawing.Point(347, 102);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(835, 602);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 102);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1383, 596);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // mtsApplications
            // 
            this.mtsApplications.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsDrivingLicensesServices,
            this.toolStripMenuItem1,
            this.tmsManageApplications,
            this.toolStripMenuItem2,
            this.tmsDetainLicenses,
            this.toolStripMenuItem3,
            this.tmsManageApplicationTypes,
            this.toolStripMenuItem4,
            this.tmsManageTestTypes});
            this.mtsApplications.Font = new System.Drawing.Font("Cascadia Code", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtsApplications.ForeColor = System.Drawing.Color.Black;
            this.mtsApplications.Image = global::DVLD.Properties.Resources.Applications_64;
            this.mtsApplications.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mtsApplications.Name = "mtsApplications";
            this.mtsApplications.Padding = new System.Windows.Forms.Padding(5);
            this.mtsApplications.Size = new System.Drawing.Size(247, 78);
            this.mtsApplications.Text = "&Applications";
            // 
            // tmsDrivingLicensesServices
            // 
            this.tmsDrivingLicensesServices.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsNewDrivingLicense,
            this.tmsRenewDrivingLicense,
            this.tmsReplacementLostDamaged,
            this.tmsReleaseDetainDrivingLicense,
            this.tmsRetakeTest});
            this.tmsDrivingLicensesServices.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tmsDrivingLicensesServices.ForeColor = System.Drawing.Color.Black;
            this.tmsDrivingLicensesServices.Image = global::DVLD.Properties.Resources.Driver_License_48;
            this.tmsDrivingLicensesServices.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tmsDrivingLicensesServices.Name = "tmsDrivingLicensesServices";
            this.tmsDrivingLicensesServices.Size = new System.Drawing.Size(444, 70);
            this.tmsDrivingLicensesServices.Text = "Driving Licenses Services";
            // 
            // tmsNewDrivingLicense
            // 
            this.tmsNewDrivingLicense.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsLocalLicense,
            this.tmsInternationalLicense});
            this.tmsNewDrivingLicense.Font = new System.Drawing.Font("Cascadia Code", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tmsNewDrivingLicense.Image = global::DVLD.Properties.Resources.New_Driving_License_32;
            this.tmsNewDrivingLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tmsNewDrivingLicense.Name = "tmsNewDrivingLicense";
            this.tmsNewDrivingLicense.Size = new System.Drawing.Size(418, 38);
            this.tmsNewDrivingLicense.Text = "&New Driving License";
            // 
            // tmsLocalLicense
            // 
            this.tmsLocalLicense.Image = global::DVLD.Properties.Resources.Local_32;
            this.tmsLocalLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tmsLocalLicense.Name = "tmsLocalLicense";
            this.tmsLocalLicense.Size = new System.Drawing.Size(316, 38);
            this.tmsLocalLicense.Text = "&Local License";
            this.tmsLocalLicense.Click += new System.EventHandler(this.tmsLocalLicense_Click);
            // 
            // tmsInternationalLicense
            // 
            this.tmsInternationalLicense.Image = global::DVLD.Properties.Resources.International_32;
            this.tmsInternationalLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tmsInternationalLicense.Name = "tmsInternationalLicense";
            this.tmsInternationalLicense.Size = new System.Drawing.Size(316, 38);
            this.tmsInternationalLicense.Text = "&International License";
            this.tmsInternationalLicense.Click += new System.EventHandler(this.tmsInternationalLicense_Click);
            // 
            // tmsRenewDrivingLicense
            // 
            this.tmsRenewDrivingLicense.Font = new System.Drawing.Font("Cascadia Code", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tmsRenewDrivingLicense.Image = global::DVLD.Properties.Resources.Renew_Driving_License_32;
            this.tmsRenewDrivingLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tmsRenewDrivingLicense.Name = "tmsRenewDrivingLicense";
            this.tmsRenewDrivingLicense.Size = new System.Drawing.Size(418, 38);
            this.tmsRenewDrivingLicense.Text = "&Renew Driving License";
            this.tmsRenewDrivingLicense.Click += new System.EventHandler(this.tmsRenewDrivingLicense_Click);
            // 
            // tmsReplacementLostDamaged
            // 
            this.tmsReplacementLostDamaged.Font = new System.Drawing.Font("Cascadia Code", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tmsReplacementLostDamaged.Image = global::DVLD.Properties.Resources.Damaged_Driving_License_32;
            this.tmsReplacementLostDamaged.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tmsReplacementLostDamaged.Name = "tmsReplacementLostDamaged";
            this.tmsReplacementLostDamaged.Size = new System.Drawing.Size(418, 38);
            this.tmsReplacementLostDamaged.Text = "Replacement For Lost Or &Damaged";
            this.tmsReplacementLostDamaged.Click += new System.EventHandler(this.tmsReplacementLostDamaged_Click);
            // 
            // tmsReleaseDetainDrivingLicense
            // 
            this.tmsReleaseDetainDrivingLicense.Font = new System.Drawing.Font("Cascadia Code", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tmsReleaseDetainDrivingLicense.Image = global::DVLD.Properties.Resources.Detained_Driving_License_32;
            this.tmsReleaseDetainDrivingLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tmsReleaseDetainDrivingLicense.Name = "tmsReleaseDetainDrivingLicense";
            this.tmsReleaseDetainDrivingLicense.Size = new System.Drawing.Size(418, 38);
            this.tmsReleaseDetainDrivingLicense.Text = "Relea&se Detain Driving License";
            // 
            // tmsRetakeTest
            // 
            this.tmsRetakeTest.Font = new System.Drawing.Font("Cascadia Code", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tmsRetakeTest.Image = global::DVLD.Properties.Resources.Retake_Test_32;
            this.tmsRetakeTest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tmsRetakeTest.Name = "tmsRetakeTest";
            this.tmsRetakeTest.Size = new System.Drawing.Size(418, 38);
            this.tmsRetakeTest.Text = "Retake &Test";
            this.tmsRetakeTest.Click += new System.EventHandler(this.tmsRetakeTest_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(441, 6);
            // 
            // tmsManageApplications
            // 
            this.tmsManageApplications.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmLocalDrivingLicenseApplications,
            this.tsmInternationalDrivingLicenseApplications});
            this.tmsManageApplications.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tmsManageApplications.ForeColor = System.Drawing.Color.Black;
            this.tmsManageApplications.Image = global::DVLD.Properties.Resources.Manage_Applications_64;
            this.tmsManageApplications.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tmsManageApplications.Name = "tmsManageApplications";
            this.tmsManageApplications.Size = new System.Drawing.Size(444, 70);
            this.tmsManageApplications.Text = "Manage Applications";
            // 
            // tsmLocalDrivingLicenseApplications
            // 
            this.tsmLocalDrivingLicenseApplications.Font = new System.Drawing.Font("Cascadia Code", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmLocalDrivingLicenseApplications.Image = global::DVLD.Properties.Resources.LocalDriving_License;
            this.tsmLocalDrivingLicenseApplications.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmLocalDrivingLicenseApplications.Name = "tsmLocalDrivingLicenseApplications";
            this.tsmLocalDrivingLicenseApplications.Size = new System.Drawing.Size(528, 38);
            this.tsmLocalDrivingLicenseApplications.Text = "&Local Driving License Applications";
            this.tsmLocalDrivingLicenseApplications.Click += new System.EventHandler(this.tsmLocalDrivingLicenseApplications_Click);
            // 
            // tsmInternationalDrivingLicenseApplications
            // 
            this.tsmInternationalDrivingLicenseApplications.Font = new System.Drawing.Font("Cascadia Code", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmInternationalDrivingLicenseApplications.Image = global::DVLD.Properties.Resources.International_32;
            this.tsmInternationalDrivingLicenseApplications.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmInternationalDrivingLicenseApplications.Name = "tsmInternationalDrivingLicenseApplications";
            this.tsmInternationalDrivingLicenseApplications.Size = new System.Drawing.Size(528, 38);
            this.tsmInternationalDrivingLicenseApplications.Text = "&International Driving License Applications";
            this.tsmInternationalDrivingLicenseApplications.Click += new System.EventHandler(this.tsmInternationalDrivingLicenseApplications_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(441, 6);
            // 
            // tmsDetainLicenses
            // 
            this.tmsDetainLicenses.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmManageDetained,
            this.tsmDetainLicenses,
            this.tsmReleaseDetainLicense});
            this.tmsDetainLicenses.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tmsDetainLicenses.ForeColor = System.Drawing.Color.Black;
            this.tmsDetainLicenses.Image = global::DVLD.Properties.Resources.Detain_641;
            this.tmsDetainLicenses.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tmsDetainLicenses.Name = "tmsDetainLicenses";
            this.tmsDetainLicenses.Size = new System.Drawing.Size(444, 70);
            this.tmsDetainLicenses.Text = "Detain Licenses";
            // 
            // tsmManageDetained
            // 
            this.tsmManageDetained.Font = new System.Drawing.Font("Cascadia Code", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmManageDetained.Image = global::DVLD.Properties.Resources.Detain_32;
            this.tsmManageDetained.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmManageDetained.Name = "tsmManageDetained";
            this.tsmManageDetained.Size = new System.Drawing.Size(349, 38);
            this.tsmManageDetained.Text = "&Manage Detained Licenses";
            this.tsmManageDetained.Click += new System.EventHandler(this.tsmManageDetained_Click);
            // 
            // tsmDetainLicenses
            // 
            this.tsmDetainLicenses.Font = new System.Drawing.Font("Cascadia Code", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmDetainLicenses.Image = global::DVLD.Properties.Resources.Detain_32;
            this.tsmDetainLicenses.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmDetainLicenses.Name = "tsmDetainLicenses";
            this.tsmDetainLicenses.Size = new System.Drawing.Size(349, 38);
            this.tsmDetainLicenses.Text = "&Detain License";
            this.tsmDetainLicenses.Click += new System.EventHandler(this.tsmDetainLicenses_Click);
            // 
            // tsmReleaseDetainLicense
            // 
            this.tsmReleaseDetainLicense.Font = new System.Drawing.Font("Cascadia Code", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmReleaseDetainLicense.Image = global::DVLD.Properties.Resources.Release_Detained_License_321;
            this.tsmReleaseDetainLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmReleaseDetainLicense.Name = "tsmReleaseDetainLicense";
            this.tsmReleaseDetainLicense.Size = new System.Drawing.Size(349, 38);
            this.tsmReleaseDetainLicense.Text = "&Release Detain License";
            this.tsmReleaseDetainLicense.Click += new System.EventHandler(this.tsmReleaseDetainLicense_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(441, 6);
            // 
            // tmsManageApplicationTypes
            // 
            this.tmsManageApplicationTypes.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tmsManageApplicationTypes.ForeColor = System.Drawing.Color.Black;
            this.tmsManageApplicationTypes.Image = global::DVLD.Properties.Resources.Application_Types_64;
            this.tmsManageApplicationTypes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tmsManageApplicationTypes.Name = "tmsManageApplicationTypes";
            this.tmsManageApplicationTypes.Size = new System.Drawing.Size(444, 70);
            this.tmsManageApplicationTypes.Text = "Manage Application Types";
            this.tmsManageApplicationTypes.Click += new System.EventHandler(this.tmsManageApplicationTypes_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(441, 6);
            // 
            // tmsManageTestTypes
            // 
            this.tmsManageTestTypes.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tmsManageTestTypes.ForeColor = System.Drawing.Color.Black;
            this.tmsManageTestTypes.Image = global::DVLD.Properties.Resources.Test_Type_64;
            this.tmsManageTestTypes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tmsManageTestTypes.Name = "tmsManageTestTypes";
            this.tmsManageTestTypes.Size = new System.Drawing.Size(444, 70);
            this.tmsManageTestTypes.Text = "Manage Test Types";
            this.tmsManageTestTypes.Click += new System.EventHandler(this.tmsManageTestTypes_Click);
            // 
            // mtsManagePeople
            // 
            this.mtsManagePeople.ForeColor = System.Drawing.Color.Black;
            this.mtsManagePeople.Image = global::DVLD.Properties.Resources.People_64;
            this.mtsManagePeople.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mtsManagePeople.Name = "mtsManagePeople";
            this.mtsManagePeople.Padding = new System.Windows.Forms.Padding(5);
            this.mtsManagePeople.Size = new System.Drawing.Size(169, 78);
            this.mtsManagePeople.Text = "&People";
            this.mtsManagePeople.Click += new System.EventHandler(this.mtsManagePeople_Click);
            // 
            // mtsDrivers
            // 
            this.mtsDrivers.ForeColor = System.Drawing.Color.Black;
            this.mtsDrivers.Image = global::DVLD.Properties.Resources.Drivers_64;
            this.mtsDrivers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mtsDrivers.Name = "mtsDrivers";
            this.mtsDrivers.Padding = new System.Windows.Forms.Padding(5);
            this.mtsDrivers.Size = new System.Drawing.Size(182, 78);
            this.mtsDrivers.Text = "&Drivers";
            this.mtsDrivers.Click += new System.EventHandler(this.mtsDrivers_Click);
            // 
            // mtsUsers
            // 
            this.mtsUsers.ForeColor = System.Drawing.Color.Black;
            this.mtsUsers.Image = global::DVLD.Properties.Resources.Users_2_64;
            this.mtsUsers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mtsUsers.Name = "mtsUsers";
            this.mtsUsers.Padding = new System.Windows.Forms.Padding(5);
            this.mtsUsers.Size = new System.Drawing.Size(156, 78);
            this.mtsUsers.Text = "&Users";
            this.mtsUsers.Click += new System.EventHandler(this.mtsUsers_Click);
            // 
            // mtsSetting
            // 
            this.mtsSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCurrentUserInfo,
            this.tsmChangePassword,
            this.tsmLogout});
            this.mtsSetting.ForeColor = System.Drawing.Color.Black;
            this.mtsSetting.Image = global::DVLD.Properties.Resources.account_settings_64;
            this.mtsSetting.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mtsSetting.Name = "mtsSetting";
            this.mtsSetting.Padding = new System.Windows.Forms.Padding(5);
            this.mtsSetting.Size = new System.Drawing.Size(182, 78);
            this.mtsSetting.Text = "&Setting";
            // 
            // tsmCurrentUserInfo
            // 
            this.tsmCurrentUserInfo.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmCurrentUserInfo.Image = global::DVLD.Properties.Resources.PersonDetails_32;
            this.tsmCurrentUserInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmCurrentUserInfo.Name = "tsmCurrentUserInfo";
            this.tsmCurrentUserInfo.Size = new System.Drawing.Size(315, 38);
            this.tsmCurrentUserInfo.Text = "Current User Info";
            this.tsmCurrentUserInfo.Click += new System.EventHandler(this.tsmCurrentUserInfo_Click);
            // 
            // tsmChangePassword
            // 
            this.tsmChangePassword.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmChangePassword.Image = global::DVLD.Properties.Resources.Password_32;
            this.tsmChangePassword.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmChangePassword.Name = "tsmChangePassword";
            this.tsmChangePassword.Size = new System.Drawing.Size(315, 38);
            this.tsmChangePassword.Text = "Change Password";
            this.tsmChangePassword.Click += new System.EventHandler(this.tsmChangePassword_Click);
            // 
            // tsmLogout
            // 
            this.tsmLogout.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmLogout.Image = global::DVLD.Properties.Resources.sign_out_32__2;
            this.tsmLogout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmLogout.Name = "tsmLogout";
            this.tsmLogout.Size = new System.Drawing.Size(315, 38);
            this.tsmLogout.Text = "Logout";
            this.tsmLogout.Click += new System.EventHandler(this.tsmLogout_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Copperplate Gothic Light", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(378, 665);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(913, 43);
            this.label1.TabIndex = 4;
            this.label1.Text = "Driver and Vehicle Licensing Department";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1383, 698);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.msServices);
            this.Font = new System.Drawing.Font("Cascadia Code", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.msServices;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.msServices.ResumeLayout(false);
            this.msServices.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msServices;
        private System.Windows.Forms.ToolStripMenuItem mtsApplications;
        private System.Windows.Forms.ToolStripMenuItem mtsManagePeople;
        private System.Windows.Forms.ToolStripMenuItem mtsUsers;
        private System.Windows.Forms.ToolStripMenuItem mtsSetting;
        private System.Windows.Forms.ToolStripMenuItem tmsDrivingLicensesServices;
        private System.Windows.Forms.ToolStripMenuItem tmsNewDrivingLicense;
        private System.Windows.Forms.ToolStripMenuItem tmsRenewDrivingLicense;
        private System.Windows.Forms.ToolStripMenuItem tmsReplacementLostDamaged;
        private System.Windows.Forms.ToolStripMenuItem tmsReleaseDetainDrivingLicense;
        private System.Windows.Forms.ToolStripMenuItem tmsRetakeTest;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tmsManageApplications;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tmsDetainLicenses;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tmsManageApplicationTypes;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem tmsManageTestTypes;
        private System.Windows.Forms.ToolStripMenuItem mtsDrivers;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem tsmCurrentUserInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmChangePassword;
        private System.Windows.Forms.ToolStripMenuItem tsmLogout;
        private System.Windows.Forms.ToolStripMenuItem tmsLocalLicense;
        private System.Windows.Forms.ToolStripMenuItem tmsInternationalLicense;
        private System.Windows.Forms.ToolStripMenuItem tsmLocalDrivingLicenseApplications;
        private System.Windows.Forms.ToolStripMenuItem tsmInternationalDrivingLicenseApplications;
        private System.Windows.Forms.ToolStripMenuItem tsmManageDetained;
        private System.Windows.Forms.ToolStripMenuItem tsmDetainLicenses;
        private System.Windows.Forms.ToolStripMenuItem tsmReleaseDetainLicense;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
    }
}

