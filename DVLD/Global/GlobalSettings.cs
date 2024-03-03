using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Global
{
    public class GlobalSettings
    {
        public static clsUser CurrentUser;
       
        private static string _FileName = "UserLoginInfo.txt";

        public static void WriteDataInFile(string UserName, string Password, bool SaveData)
        {
            if(SaveData)
            {
                string Data = UserName + "#//#" + Password;
                File.WriteAllText(_FileName, Data);
            }
            else
            {
                File.WriteAllText(_FileName, "");
            }
        }

        public static void ReadDataFromFile(ref string UserName, ref string Password)
        {
            if(!File.Exists(_FileName))
            {
                //File.WriteAllText(_FileName, "");
                UserName = "";
                Password = "";
                return;
            }

            string Data = File.ReadAllText(_FileName);

            if (Data=="" || Data == null)
            {
                UserName = "";
                Password = "";
            }
            else
            {
                string[] SeperatedData = Data.Split(new string[] { "#//#" }, StringSplitOptions.None);

                UserName = SeperatedData[0];
                Password = SeperatedData[1];
            }
        }
    }
}
