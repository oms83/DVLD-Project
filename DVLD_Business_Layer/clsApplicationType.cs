using DVLD_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsApplicationType
    {
        private enum enMode { AddNew, Update }
        
        private enMode _Mode = enMode.AddNew;

        public int ApplicationTypeID { get; set; }

        public string ApplicationTypeTitle { get; set; }

        public float ApplicationFees { get; set; }

        private clsApplicationType(int ApplicationTypeID,  string ApplicationTypeTitle, float ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;

            _Mode = enMode.Update;
        }

        public clsApplicationType()
        {
            this.ApplicationTypeID = -1;
            this.ApplicationTypeTitle = string.Empty;
            this.ApplicationFees = 0;

            _Mode = enMode.AddNew;
        }

        public static clsApplicationType Find(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = "";  float ApplicationFees = 0;

            bool IsFound = clsApplicationTypeData.GetApplicationTypeInfo(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees);

            if (IsFound)
            {
                return new clsApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewApplicationType()
        {
            this.ApplicationTypeID = clsApplicationTypeData.AddNewApplicationType(this.ApplicationTypeTitle, this.ApplicationFees);

            return this.ApplicationTypeID != -1;
        }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypeData.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);
        }

        public static bool DeleteApplicationType(int ApplicationTypeID)
        {
            return clsApplicationTypeData.DeleteApplicationTypes(ApplicationTypeID);
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateApplicationType();
            }

            return false;
        }
    }
}
