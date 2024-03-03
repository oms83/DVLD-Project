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

namespace DVLD.Applications.Application_Types
{
    public partial class frmListApplicationTypes : Form
    {
        clsApplicationType _ApplicationType;

        DataTable _dtApplicationType;
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void _RefreshApplicationType()
        {
            _dtApplicationType = clsApplicationType.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _dtApplicationType;
        }

        private void _SetDGVColumn()
        { 

            lblNumberOfRecord.Text = dgvApplicationTypes.Rows.Count.ToString();

            if (dgvApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "Application Type ID";
                dgvApplicationTypes.Columns[0].Width = 150;

                dgvApplicationTypes.Columns[1].HeaderText = "Application Type Title";
                dgvApplicationTypes.Columns[1].Width = 400;

                dgvApplicationTypes.Columns[2].HeaderText = "Application Fees";
                dgvApplicationTypes.Columns[2].Width = 160;
            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefreshApplicationType();

            _SetDGVColumn();
        }

        private void tsmUpdateApplicationType_Click(object sender, EventArgs e)
        {
            frmEditApplicationType frm = new frmEditApplicationType((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);

            frm.ShowDialog();

            _RefreshApplicationType();
        }

        private void dgvApplicationTypes_DoubleClick(object sender, EventArgs e)
        {
            frmEditApplicationType frm = new frmEditApplicationType((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);

            frm.ShowDialog();

            //_RefreshApplicationType();
            frmListApplicationTypes_Load(null, null);

        }
    }
}
