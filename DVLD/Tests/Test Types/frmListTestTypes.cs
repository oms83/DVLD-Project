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
    public partial class frmListTestTypes : Form
    {
        private DataTable _dtTestTypes;

        public frmListTestTypes()
        {
            InitializeComponent();
        }


        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshTestTypesInfo();
            _SetDGVColums();
        }

        private void _RefreshTestTypesInfo()
        {
            _dtTestTypes = clsTestType.GetAllTestTypes();
            dgvTestTypes.DataSource = _dtTestTypes;
            lblNumberOfRecord.Text = dgvTestTypes.Rows.Count.ToString();

        }

        private void _SetDGVColums()
        {

            if (dgvTestTypes.Rows.Count > 0 )
            {
                dgvTestTypes.Columns[0].HeaderText = "Test Type ID";
                dgvTestTypes.Columns[0].Width = 120;

                dgvTestTypes.Columns[1].HeaderText = "Test Type Title";
                dgvTestTypes.Columns[1].Width = 200;

                dgvTestTypes.Columns[2].HeaderText = "Test Type Description";
                dgvTestTypes.Columns[2].Width = 450;

                dgvTestTypes.Columns[3].HeaderText = "Test Fees";
                dgvTestTypes.Columns[3].Width = 100;
            }
        }

        private void dgvTestTypes_DoubleClick(object sender, EventArgs e)
        {
            frmEditTestType frm = new frmEditTestType((int)dgvTestTypes.CurrentRow.Cells[0].Value);

            frm.ShowDialog();

            frmListTestTypes_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestType frm = new frmEditTestType((int)dgvTestTypes.CurrentRow.Cells[0].Value);

            frm.ShowDialog();

            frmListTestTypes_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
