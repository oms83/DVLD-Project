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
    public partial class frmShowPersonLicenseHistory : Form
    {
        int _PersonID = -1;

        clsDriver _Driver;
        public frmShowPersonLicenseHistory(int PersonID=-1)
        {
            InitializeComponent();
            _PersonID = PersonID;
            
        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            _Driver = clsDriver.FindByPersonID(_PersonID);

            ctrlUserCardWithFilter1.LoadPersonInfo(_PersonID);

            if (_Driver == null)
            {
                ctrlDriverLicenses1.LoadLicensesInfo(-1);
            }
            else
            {
                ctrlDriverLicenses1.LoadLicensesInfo(_Driver.DriverID);
            }
            ctrlUserCardWithFilter1.FilterEnable = false;
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
