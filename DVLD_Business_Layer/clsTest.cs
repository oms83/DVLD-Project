using DVLD_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsTest
    {
        public enum enMode { AddNew, Update }
        enMode Mode = enMode.AddNew;
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public int CreatedByUserID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public clsTestAppointment TestAppointmentInfo { set; get; }

        private clsTest(int TestID, int TestAppointmentID, int CreatedByUserID, bool TestResult, string Notes)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.CreatedByUserID = CreatedByUserID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);
            Mode = enMode.Update;
        }


        public clsTest()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.CreatedByUserID = -1;
            this.TestResult = false;
            this.Notes = "";
            Mode = enMode.AddNew;
        }

        private bool _AddNewTest()
        {
            return -1 != ( this.TestID = clsTestData.AddNewTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID) );
        }

        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
        }

        public static clsTest FindTestInfoByTestID(int TestID)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1;
            bool TestResult = false;
            string Notes = string.Empty;

            bool IsFound = clsTestData.GetTestInfoByID(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID);

            if (IsFound)
            {
                return new clsTest(TestID, TestAppointmentID, CreatedByUserID, TestResult, Notes);
            }
            else
            {
                return null;
            }
        }
        public static clsTest FindTestInfoByTestAppointmentID(int TestAppointmentID)
        {
            int TestID = -1, CreatedByUserID = -1;
            bool TestResult = false;
            string Notes = string.Empty;

            bool IsFound = clsTestData.FindTestInfoByTestAppointmentID(ref TestID, TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID);

            if (IsFound)
            {
                return new clsTest(TestID, TestAppointmentID, CreatedByUserID, TestResult, Notes);
            }
            else
            {
                return null;
            }
        }
        public static clsTest FindLastTestPerPersonAndLicenseClass(int PersonID, int LincenseClassID, clsTestType.enTestType TestTypeID)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1, TestID = -1;
            bool TestResult = false;
            string Notes = string.Empty;

            bool IsFound = clsTestData.GetLastTestByPersonIDAndTestTypeAndLicenseClass(PersonID, LincenseClassID, (int)TestTypeID, ref TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID);

            if (IsFound)
            {
                return new clsTest(TestID, TestAppointmentID, CreatedByUserID, TestResult, Notes);
            }
            else
            {
                return null;
            }
        }   

        public static byte GetPassedTestCount(int LocalDrivingLincenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLincenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLincenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLincenseApplicationID) == 3;
        }
        public static DataTable GetAllTest()
        {
            return clsTestData.GetAllTests();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTest();

            }

            return false;
        }
    }
}
