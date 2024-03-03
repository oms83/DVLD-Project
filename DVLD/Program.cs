using DVLD.Applications.Application_Types;
using DVLD.Applications.International_Licenses;
using DVLD.Applications.Local_Driviing_Licenses;
using DVLD.Applications.Release_Detain_License;
using DVLD.Applications.Renew_Local_Licenses;
using DVLD.Applications.Replace_Lost_Or_Damaged_License;
using DVLD.Licenses.Detain_Licenses;
using DVLD.Licenses.International_Licenses;
using DVLD.Licenses.Local_Licenses;
using DVLD.Tests;
using DVLD.Tests.Test_Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}
