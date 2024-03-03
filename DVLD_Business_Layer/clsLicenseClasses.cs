using DVLD_DataAccess_Layer;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsLicenseClasses
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }

        public string ClassDescription { get; set; }

        public short MinimumAllowedAge { get; set; }

        public short DefaultValidityLength { get; set; }

        public float ClassFees { get; set; }

        private enum enMode { AddNew, Update }
        private enMode _Mode = enMode.AddNew;

        private clsLicenseClasses(int ClassID, string ClassName, string ClassDescription, short MinimumAllowedAge, 
            short DefaultValidityLength, float ClassFees)
        {
            this.ClassFees = ClassFees;
            this.DefaultValidityLength = DefaultValidityLength;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.ClassDescription = ClassDescription;
            this.ClassName = ClassName;
            this.ClassID = ClassID;

            _Mode = enMode.Update;
        }

        public clsLicenseClasses()
        {
            this.ClassFees = 0;
            this.DefaultValidityLength = 10;
            this.MinimumAllowedAge = 18;
            this.ClassDescription = "";
            this.ClassName = "";
            this.ClassID = -1;

            _Mode = enMode.AddNew;
        }

        public static clsLicenseClasses Find(int ClassID)
        {
            float ClassFees = 0;
            short DefaultValidityLength = 10, MinimumAllowedAge = 18;
            string ClassDescription = "", ClassName = "";

            bool IsFound = clsLicenseClassesData.Find(ClassID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge, 
                                                      ref DefaultValidityLength, ref ClassFees);

            if (IsFound)
            {
                return new clsLicenseClasses(ClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {
                return null;
            }
        }
        public static clsLicenseClasses Find(string ClassName)
        {
            int ClassID = -1; string ClassDescription = "";
            short MinimumAllowedAge = 18, DefaultValidityLength = 10; 
            float ClassFees = 0;

            if (clsLicenseClassesData.Find(ref ClassID, ClassName, ref ClassDescription, ref MinimumAllowedAge,
                                                      ref DefaultValidityLength, ref ClassFees))

                return new clsLicenseClasses(ClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;

        }
        private bool _AddNew()
        {
            this.ClassID = clsLicenseClassesData.AddNew(ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);

            return this.ClassID != -1;
        }

        private bool _Update()
        {
            return clsLicenseClassesData.Update(ClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
        }

        public static DataTable GellAllLicenseClasses()
        {
            return clsLicenseClassesData.GetAllLicenseClass();
        }

        public static bool Delete(int ClassID)
        {
            return clsLicenseClassesData.Delete(ClassID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _Update();
            }

            return false;
        }

    }
}
