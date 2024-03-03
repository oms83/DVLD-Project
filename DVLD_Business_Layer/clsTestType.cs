using DVLD_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsTestType
    {
        private enum enMode { AddNew, Update }

        private clsTestType.enMode _Mode = enMode.AddNew;

        public enum enTestType { VisionTest=1, WrittenTest=2, StreetTest=3 }

        public clsTestType.enTestType ID {  get; set; }
        public string TestTypeTitle { get; set; }

        public string TestTypeDescription { get; set;}
        public float TestFees { get; set;}

        private clsTestType()
        {
            this.ID = clsTestType.enTestType.VisionTest;
            this.TestTypeTitle = "";
            this.TestTypeDescription = string.Empty;
            this.TestFees = 0;

            _Mode = enMode.AddNew;
        }

        public clsTestType(clsTestType.enTestType ID, string TestTypeTitle, string TestTypeDescription, float TestFees)
        {
            this.ID = ID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestFees = TestFees;

            _Mode = enMode.Update;
        }
        
        private bool _AddNewTestType()
        {
            this.ID = (clsTestType.enTestType)clsTestTypeData.AddNewTestType(this.TestTypeTitle, this.TestTypeDescription, this.TestFees);

            return this.TestTypeTitle != "";
        }

        private bool _Update()
        {
            return clsTestTypeData.UpdateTestType((int)this.ID, this.TestTypeTitle, this.TestTypeDescription, this.TestFees);
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }

        public static clsTestType Find(clsTestType.enTestType ID)
        {
            string TestTypeTitle = "", TestTypeDescription = "";
            
            float TestFees = 0;

            bool IsFound = clsTestTypeData.GetTestTypeInfoByID((int)ID, ref TestTypeTitle, ref TestTypeDescription, ref TestFees);

            if (IsFound)
            {
                return new clsTestType(ID, TestTypeTitle, TestTypeDescription, TestFees);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    if(_AddNewTestType())
                    {
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
