using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Global
{
    public class clsFormat
    {
        public static string DateFormat(DateTime Date)
        {
            return Date.ToString("dd/MMM/yyyy");
        }
    }
}
