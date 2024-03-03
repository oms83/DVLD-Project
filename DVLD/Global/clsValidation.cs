using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DVLD.Global
{
    public class clsValidation
    {
        public static bool ValidateEmail(string Email)
        {
            var Pattren = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

            var rgx = new Regex(Pattren);

            return rgx.IsMatch(Email);
        }

        public static bool ValidateInteger(string Number)
        {
            var Pattren = @"^[0-9]*$";
            
            var rgx = new Regex(Pattren);

            return rgx.IsMatch(Number);
        }

        public static bool ValidateFloat(string Number)
        {
            var Pattren = @"^[0-9]*(?:\.[0-9]*)?$";

            var rgx = new Regex(Pattren);

            return rgx.IsMatch(Number);
        }

        public static bool IsNumber(string Number)
        {
            return ValidateInteger(Number) || ValidateFloat(Number);
        }
    }
}
