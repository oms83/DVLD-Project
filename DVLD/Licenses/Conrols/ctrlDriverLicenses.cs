using DVLD.Licenses.International_Licenses;
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
    public partial class ctrlDriverLicenses : UserControl
    {
        DataTable _dtLocalLicenses;
        DataTable _dtInternationalLicenses;

        int _DriverID = -1;
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }
        
        private void _LoadInternationalLicensesInfo()
        {
            _dtInternationalLicenses = clsDriver.GetInternationalLicenses(_DriverID);
            dgvInternationalLicenses.DataSource = _dtInternationalLicenses;
        }

        private void _LoadLocalLicensesInfo()
        {
            _dtLocalLicenses = clsDriver.GetLicenses(_DriverID);
            dgvLocalLicenses.DataSource = _dtLocalLicenses;   
        }
        private void _SetLocalDGVColumns()
        {
            if (dgvLocalLicenses.Rows.Count > 0) 
            {
                dgvLocalLicenses.Columns[0].Width = 115;
                dgvLocalLicenses.Columns[1].Width = 115;
                dgvLocalLicenses.Columns[2].Width = 280;
                dgvLocalLicenses.Columns[3].Width = 200;
                dgvLocalLicenses.Columns[4].Width = 200;
                dgvLocalLicenses.Columns[4].Width = 200;
            }
            lblNumberOfRecord.Text = dgvLocalLicenses.Rows.Count.ToString();
        }

        private void _SetInterantionalDGVColumns()
        {
            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].Width = 135;
                dgvInternationalLicenses.Columns[1].Width = 135;
                dgvInternationalLicenses.Columns[2].Width = 240;
                dgvInternationalLicenses.Columns[3].Width = 200;
                dgvInternationalLicenses.Columns[4].Width = 200;
                dgvInternationalLicenses.Columns[4].Width = 200;
            }
            lblNumberOfRecord.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }
        
        public void LoadLicensesInfo(int DriverID=-1)
        {
            _DriverID = DriverID;

            if (tabControl1.SelectedTab == tabControl1.TabPages["tbLocalLicenses"])
            {
                _LoadLocalLicensesInfo();
                _SetLocalDGVColumns();
            }
            else
            {
                _LoadInternationalLicensesInfo();
                _SetInterantionalDGVColumns();
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tbLocalLicenses"])
            {
                _LoadLocalLicensesInfo();
                _SetLocalDGVColumns();
            }
            else
            {
                _LoadInternationalLicensesInfo();
                _SetInterantionalDGVColumns();
            }
        }

        private void ShowLocalLicenseInfo_Click(object sender, EventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo((int)dgvLocalLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }

        private void tsmShowInternationalLicenseInfo_Click(object sender, EventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo((int)dgvInternationalLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
