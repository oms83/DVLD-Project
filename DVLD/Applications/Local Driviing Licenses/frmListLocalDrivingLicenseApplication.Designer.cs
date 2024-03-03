namespace DVLD.Applications.Local_Driviing_Licenses
{
    partial class frmListLocalDrivingLicenseApplication
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
            this.components = new System.ComponentModel.Container();
            this.txtFilterBy = new System.Windows.Forms.TextBox();
            this.cmbFilterBy = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNumberOfRecord = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsApplications = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmShowApplicationDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmApplicationEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmApplicationDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCancelApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmSechduleTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmVisionTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmWrittenTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStreetTest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmIssueDrivingLicenseFirstTime = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmShowLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmShowPersonLicenseHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvListLocalDrivingApplications = new System.Windows.Forms.DataGridView();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAddNewApplication = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmsApplications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListLocalDrivingApplications)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFilterBy
            // 
            this.txtFilterBy.Font = new System.Drawing.Font("Cascadia Code", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterBy.Location = new System.Drawing.Point(369, 303);
            this.txtFilterBy.Name = "txtFilterBy";
            this.txtFilterBy.Size = new System.Drawing.Size(267, 29);
            this.txtFilterBy.TabIndex = 38;
            this.txtFilterBy.TextChanged += new System.EventHandler(this.txtFilterBy_TextChanged);
            this.txtFilterBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterBy_KeyPress);
            // 
            // cmbFilterBy
            // 
            this.cmbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterBy.Font = new System.Drawing.Font("Cascadia Code", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterBy.FormattingEnabled = true;
            this.cmbFilterBy.Items.AddRange(new object[] {
            "None",
            "L.D.L.AppID",
            "National No.",
            "Full Name",
            "Status"});
            this.cmbFilterBy.Location = new System.Drawing.Point(98, 302);
            this.cmbFilterBy.Name = "cmbFilterBy";
            this.cmbFilterBy.Size = new System.Drawing.Size(267, 30);
            this.cmbFilterBy.TabIndex = 37;
            this.cmbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cmbFilterBy_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cascadia Code", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 306);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 22);
            this.label3.TabIndex = 36;
            this.label3.Text = "Filter By:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cascadia Code", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(519, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(418, 44);
            this.label2.TabIndex = 34;
            this.label2.Text = "Local Driving License";
            // 
            // lblNumberOfRecord
            // 
            this.lblNumberOfRecord.AutoSize = true;
            this.lblNumberOfRecord.Font = new System.Drawing.Font("Cascadia Code", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfRecord.Location = new System.Drawing.Point(94, 714);
            this.lblNumberOfRecord.Name = "lblNumberOfRecord";
            this.lblNumberOfRecord.Size = new System.Drawing.Size(20, 22);
            this.lblNumberOfRecord.TabIndex = 33;
            this.lblNumberOfRecord.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cascadia Code", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 715);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 32;
            this.label1.Text = "#Record";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(395, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(395, 6);
            // 
            // cmsApplications
            // 
            this.cmsApplications.Font = new System.Drawing.Font("Cascadia Code", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsApplications.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.cmsApplications.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmShowApplicationDetails,
            this.toolStripMenuItem1,
            this.tsmApplicationEdit,
            this.tsmApplicationDelete,
            this.deleteToolStripMenuItem,
            this.tsmCancelApplication,
            this.toolStripMenuItem2,
            this.tsmSechduleTest,
            this.toolStripMenuItem3,
            this.tsmIssueDrivingLicenseFirstTime,
            this.toolStripMenuItem4,
            this.tsmShowLicense,
            this.toolStripMenuItem5,
            this.tsmShowPersonLicenseHistory});
            this.cmsApplications.Name = "contextMenuStrip1";
            this.cmsApplications.Size = new System.Drawing.Size(399, 328);
            this.cmsApplications.Opening += new System.ComponentModel.CancelEventHandler(this.cmsApplications_Opening);
            // 
            // tsmShowApplicationDetails
            // 
            this.tsmShowApplicationDetails.Font = new System.Drawing.Font("Cascadia Code", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmShowApplicationDetails.Image = global::DVLD.Properties.Resources.PersonDetails_32;
            this.tsmShowApplicationDetails.Name = "tsmShowApplicationDetails";
            this.tsmShowApplicationDetails.Size = new System.Drawing.Size(398, 36);
            this.tsmShowApplicationDetails.Text = "&Show Application Details";
            this.tsmShowApplicationDetails.Click += new System.EventHandler(this.tsmShowApplicationDetails_Click);
            // 
            // tsmApplicationEdit
            // 
            this.tsmApplicationEdit.Image = global::DVLD.Properties.Resources.edit_32;
            this.tsmApplicationEdit.Name = "tsmApplicationEdit";
            this.tsmApplicationEdit.Size = new System.Drawing.Size(398, 36);
            this.tsmApplicationEdit.Text = "&Edit Application";
            this.tsmApplicationEdit.Click += new System.EventHandler(this.tsmApplicationEdit_Click);
            // 
            // tsmApplicationDelete
            // 
            this.tsmApplicationDelete.Image = global::DVLD.Properties.Resources.Delete_32_2;
            this.tsmApplicationDelete.Name = "tsmApplicationDelete";
            this.tsmApplicationDelete.Size = new System.Drawing.Size(398, 36);
            this.tsmApplicationDelete.Text = "&Delete Application";
            this.tsmApplicationDelete.Click += new System.EventHandler(this.tsmApplicationDelete_Click);
            // 
            // tsmCancelApplication
            // 
            this.tsmCancelApplication.Image = global::DVLD.Properties.Resources.Delete_32;
            this.tsmCancelApplication.Name = "tsmCancelApplication";
            this.tsmCancelApplication.Size = new System.Drawing.Size(398, 36);
            this.tsmCancelApplication.Text = "&Cancel Application";
            this.tsmCancelApplication.Click += new System.EventHandler(this.tsmCancelApplication_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(395, 6);
            // 
            // tsmSechduleTest
            // 
            this.tsmSechduleTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmVisionTest,
            this.tsmWrittenTest,
            this.tsmStreetTest});
            this.tsmSechduleTest.Image = global::DVLD.Properties.Resources.Schedule_Test_32;
            this.tsmSechduleTest.Name = "tsmSechduleTest";
            this.tsmSechduleTest.Size = new System.Drawing.Size(398, 36);
            this.tsmSechduleTest.Text = "Sechdule &Test";
            // 
            // tsmVisionTest
            // 
            this.tsmVisionTest.Image = global::DVLD.Properties.Resources.Vision_Test_32;
            this.tsmVisionTest.Name = "tsmVisionTest";
            this.tsmVisionTest.Size = new System.Drawing.Size(291, 36);
            this.tsmVisionTest.Text = "Schedule Vision Test";
            this.tsmVisionTest.Click += new System.EventHandler(this.tsmVisionTest_Click);
            // 
            // tsmWrittenTest
            // 
            this.tsmWrittenTest.Image = global::DVLD.Properties.Resources.Written_Test_32_Sechdule;
            this.tsmWrittenTest.Name = "tsmWrittenTest";
            this.tsmWrittenTest.Size = new System.Drawing.Size(291, 36);
            this.tsmWrittenTest.Text = "Schedule Written Test";
            this.tsmWrittenTest.Click += new System.EventHandler(this.tsmWrittenTest_Click);
            // 
            // tsmStreetTest
            // 
            this.tsmStreetTest.Image = global::DVLD.Properties.Resources.Street_Test_32;
            this.tsmStreetTest.Name = "tsmStreetTest";
            this.tsmStreetTest.Size = new System.Drawing.Size(291, 36);
            this.tsmStreetTest.Text = "Schedule Street Test";
            this.tsmStreetTest.Click += new System.EventHandler(this.tsmStreetTest_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(395, 6);
            // 
            // tsmIssueDrivingLicenseFirstTime
            // 
            this.tsmIssueDrivingLicenseFirstTime.Image = global::DVLD.Properties.Resources.IssueDrivingLicense_32;
            this.tsmIssueDrivingLicenseFirstTime.Name = "tsmIssueDrivingLicenseFirstTime";
            this.tsmIssueDrivingLicenseFirstTime.Size = new System.Drawing.Size(398, 36);
            this.tsmIssueDrivingLicenseFirstTime.Text = "&Issue Driving License (First Time)";
            this.tsmIssueDrivingLicenseFirstTime.Click += new System.EventHandler(this.tsmIssueDrivingLicenseFirstTime_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(395, 6);
            // 
            // tsmShowLicense
            // 
            this.tsmShowLicense.Image = global::DVLD.Properties.Resources.License_View_32;
            this.tsmShowLicense.Name = "tsmShowLicense";
            this.tsmShowLicense.Size = new System.Drawing.Size(398, 36);
            this.tsmShowLicense.Text = "Show &License";
            this.tsmShowLicense.Click += new System.EventHandler(this.tsmShowLicense_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(395, 6);
            // 
            // tsmShowPersonLicenseHistory
            // 
            this.tsmShowPersonLicenseHistory.Image = global::DVLD.Properties.Resources.PersonLicenseHistory_32;
            this.tsmShowPersonLicenseHistory.Name = "tsmShowPersonLicenseHistory";
            this.tsmShowPersonLicenseHistory.Size = new System.Drawing.Size(398, 36);
            this.tsmShowPersonLicenseHistory.Text = "Show Person License &History";
            this.tsmShowPersonLicenseHistory.Click += new System.EventHandler(this.tsmShowPersonLicenseHistory_Click);
            // 
            // dgvListLocalDrivingApplications
            // 
            this.dgvListLocalDrivingApplications.AllowUserToAddRows = false;
            this.dgvListLocalDrivingApplications.AllowUserToDeleteRows = false;
            this.dgvListLocalDrivingApplications.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvListLocalDrivingApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListLocalDrivingApplications.ContextMenuStrip = this.cmsApplications;
            this.dgvListLocalDrivingApplications.Location = new System.Drawing.Point(12, 339);
            this.dgvListLocalDrivingApplications.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvListLocalDrivingApplications.Name = "dgvListLocalDrivingApplications";
            this.dgvListLocalDrivingApplications.ReadOnly = true;
            this.dgvListLocalDrivingApplications.RowHeadersWidth = 51;
            this.dgvListLocalDrivingApplications.RowTemplate.Height = 24;
            this.dgvListLocalDrivingApplications.Size = new System.Drawing.Size(1342, 356);
            this.dgvListLocalDrivingApplications.TabIndex = 30;
            this.dgvListLocalDrivingApplications.DoubleClick += new System.EventHandler(this.dgvListLocalDrivingApplications_DoubleClick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD.Properties.Resources.Local_32;
            this.pictureBox2.Location = new System.Drawing.Point(750, 68);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(58, 58);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 39;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Applications;
            this.pictureBox1.Location = new System.Drawing.Point(582, 14);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(201, 222);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 31;
            this.pictureBox1.TabStop = false;
            // 
            // btnAddNewApplication
            // 
            this.btnAddNewApplication.BackColor = System.Drawing.Color.Transparent;
            this.btnAddNewApplication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddNewApplication.FlatAppearance.BorderColor = System.Drawing.SystemColors.HotTrack;
            this.btnAddNewApplication.FlatAppearance.BorderSize = 2;
            this.btnAddNewApplication.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewApplication.Image = global::DVLD.Properties.Resources.New_Application_64;
            this.btnAddNewApplication.Location = new System.Drawing.Point(1280, 265);
            this.btnAddNewApplication.Name = "btnAddNewApplication";
            this.btnAddNewApplication.Size = new System.Drawing.Size(74, 71);
            this.btnAddNewApplication.TabIndex = 35;
            this.btnAddNewApplication.UseVisualStyleBackColor = false;
            this.btnAddNewApplication.Click += new System.EventHandler(this.btnAddNewApplication_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.SystemColors.HotTrack;
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Cascadia Code", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1223, 703);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(131, 45);
            this.btnClose.TabIndex = 29;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmListLocalDrivingLicenseApplication
            // 
            this.AcceptButton = this.btnAddNewApplication;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1364, 760);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.txtFilterBy);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cmbFilterBy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAddNewApplication);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNumberOfRecord);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvListLocalDrivingApplications);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Cascadia Code", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmListLocalDrivingLicenseApplication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmListLocalDrivingLicenseApplication";
            this.Load += new System.EventHandler(this.frmListLocalDrivingLicenseApplication_Load);
            this.cmsApplications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListLocalDrivingApplications)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilterBy;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cmbFilterBy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddNewApplication;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNumberOfRecord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem tsmShowLicense;
        private System.Windows.Forms.ToolStripMenuItem tsmSechduleTest;
        private System.Windows.Forms.ToolStripSeparator deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmApplicationDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmApplicationEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmShowApplicationDetails;
        private System.Windows.Forms.ContextMenuStrip cmsApplications;
        private System.Windows.Forms.DataGridView dgvListLocalDrivingApplications;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolStripMenuItem tsmCancelApplication;
        private System.Windows.Forms.ToolStripMenuItem tsmIssueDrivingLicenseFirstTime;
        private System.Windows.Forms.ToolStripMenuItem tsmShowPersonLicenseHistory;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem tsmVisionTest;
        private System.Windows.Forms.ToolStripMenuItem tsmWrittenTest;
        private System.Windows.Forms.ToolStripMenuItem tsmStreetTest;
    }
}