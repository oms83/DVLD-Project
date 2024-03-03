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
    public partial class frmShowLicenseInfo : Form
    {
        private int _LicenceID = -1;
        public frmShowLicenseInfo(int LicenceID)
        {
            InitializeComponent();
            _LicenceID = LicenceID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowLicenseInfo_Load(object sender, EventArgs e)
        {
            
            ctrlDriverLicenseInfo1.LoadLicenseInfo(_LicenceID);
        }
    }
}
