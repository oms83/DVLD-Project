using DVLD_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        private enum enMode { AddNew, Update }
        private enMode  Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassesID { get; set; }

        public clsLicenseClasses LicenseClassesInfo; // Composition

        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassesID = -1;
            //this.ApplicationID = -1;
            //this.ApplicantPersonID = -1;
            //this.ApplicationTypeID = -1;
            //this.CreatedByUserID = -1;
            
            //this.ApplicationStatus = clsApplication.enApplicationStatus.New;
            
            //this.PaidFees = 0;
            
            //this.LastStatusDate = DateTime.Now;
            //this.ApplicationDate = DateTime.Now;

            Mode = enMode.AddNew;
        }
        public string PersonFullName
        {
            get
            {
                return base.ApplicantPersonFullName;
            }
        }

        public clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int LicenseClassesID, int ApplicationID, 
            int ApplicantPersonID, int ApplicationTypeID, clsApplication.enApplicationStatus ApplicationStatus, float PaidFees,
            int CreatedByUserID, DateTime LastStatusDate, DateTime ApplicationDate)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassesID = LicenseClassesID;

            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationTypeID = ApplicationTypeID;
            base.CreatedByUserID = CreatedByUserID;
            base.ApplicationStatus = ApplicationStatus;
            base.PaidFees = PaidFees;
            base.LastStatusDate = LastStatusDate;
            base.ApplicationDate = ApplicationDate;

            this.LicenseClassesInfo = clsLicenseClasses.Find(this.LicenseClassesID);

            Mode = enMode.Update;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(ApplicationID, LicenseClassesID);
            return this.LocalDrivingLicenseApplicationID != -1;
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassesID);
        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            int LicenseClassesID = -1, ApplicationID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassesID);

            if (IsFound)
            {
                clsApplication _Application = clsApplication.GetApplicationByApplicationID(ApplicationID);

                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, LicenseClassesID, ApplicationID,
                        _Application.ApplicantPersonID, _Application.ApplicationTypeID, _Application.ApplicationStatus, 
                        _Application.PaidFees, _Application.CreatedByUserID, _Application.LastStatusDate, _Application.ApplicationDate);
            }
            else
            {
                return null;
            }
        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int LicenseClassesID = -1, LocalDrivingLicenseApplicationID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID(ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassesID);

            if (IsFound)
            {
                clsApplication _Application = clsApplication.GetApplicationByApplicationID((int)ApplicationID);

                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, LicenseClassesID, ApplicationID,
                                                        _Application.ApplicantPersonID, _Application.ApplicationTypeID, 
                                                        (clsApplication.enApplicationStatus) _Application.ApplicationStatus, _Application.PaidFees, 
                                                        _Application.CreatedByUserID, _Application.LastStatusDate, _Application.ApplicationDate);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            
            if(!base.Save())
            {
                return false;
            }

            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (this._AddNewLocalDrivingLicenseApplication())
                    {
                        Mode = enMode.Update;
                        base.Mode = (clsApplication.enMode)Mode;

                        return true;
                    }
                    else
                    {
                        return false;   
                    }
                case enMode.Update:

                    return this._UpdateLocalDrivingLicenseApplication();
            }

            return false;
        }

        public bool DeleteLocalDrivingLicenseApplication()
        {
            bool IsLocalDrivingLicenseApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;

            IsLocalDrivingLicenseApplicationDeleted = clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);

            if (!IsLocalDrivingLicenseApplicationDeleted)
            {
                return false;
            }

            IsBaseApplicationDeleted = base.Delete();

            //IsBaseApplicationDeleted = clsApplication.DeleteApplication(this.ApplicationID);

            return IsBaseApplicationDeleted;
        }

        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID)
        {
            bool IsLocalDrivingLicenseApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;
            IsLocalDrivingLicenseApplicationDeleted = clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);

            if (!IsLocalDrivingLicenseApplicationDeleted)
            {
                return false;
            }

            IsBaseApplicationDeleted = clsApplication.Delete(ApplicationID);

            //IsBaseApplicationDeleted = clsApplication.DeleteApplication(this.ApplicationID);

            return IsBaseApplicationDeleted;
        }

        public bool DoesPassTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesAttendTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesPassPreviousTest(clsTestType.enTestType TestTypeID)
        {
            switch (TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    return true;

                case clsTestType.enTestType.WrittenTest:
                    return this.DoesPassTestType(clsTestType.enTestType.VisionTest);

                case clsTestType.enTestType.StreetTest:
                    return this.DoesPassTestType(clsTestType.enTestType.WrittenTest);

                default:
                    return false;
            }

        }
        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public  byte TotalTrialsPerTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static bool AttendedTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }
        public bool AttendedTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {

            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType TestTypeID)
        {

            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }
        public clsTest GetLastTestPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenseClassesID, TestTypeID);
        }

        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return clsTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        }
        
        public  int IssuedLicense(int CreatedUserID, string Notes)
        {

            clsDriver Driver = clsDriver.FindByPersonID(this.ApplicantPersonID);
            
            if (Driver == null)
            {
                Driver = new clsDriver();

                //_Driver.CreatedDate = DateTime.Now; CreatedDate this property it is read only

                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedUserID;
                
                if (!Driver.Save())
                {
                    return -1;
                }
            }

            clsLicense _License = new clsLicense();
            _License.ApplicationID = this.ApplicationID;
            _License.DriverID = Driver.DriverID;
            _License.CreatedByUserID = CreatedUserID;
            _License.LicenseClassID = this.LicenseClassesID;
            _License.IsActive = true;
            _License.IssueDate = DateTime.Now;
            _License.IssueReason = clsLicense.enIssueReason.FirstTime;
            _License.Notes = Notes;

            _License.PaidFees = this.LicenseClassesInfo.ClassFees;
            _License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassesInfo.DefaultValidityLength);
            
            if (_License.Save())
            {
                this.SetComplete();
                return _License.LicenseID;
            }
            else
            {
                return -1;
            }

        }
        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }

        public int GetActiveLicenseID()
        {//this will get the license id that belongs to this application
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassesID);
        }
    }
}


/*

using System;

public class clsA
{
    public string Name {get; set; }
    public string Surname {get; set;}
    
    public clsA()    
    {
        Console.WriteLine("Default Constructor clsA");
    }
    
    public clsA(string Name, string Surname)
    {
        this.Name = Name;
        this.Surname = Surname;
        
        Console.WriteLine("Parameteriaz Constructor clsA");
    }
    
};
public class clsB:clsA
{
    public short Age {get; set; }
    
    public clsB()    
    {
        Console.WriteLine("Default Constructor clsB");
    }
    
    public clsB(string Name, string Surname, short Age)
    {
        this.Name = Name;
        this.Surname = Surname;
        this.Age = Age;
        Console.WriteLine("Parameteriaz Constructor clsB");
    }
    
}
class HelloWorld {
    
  static void Main() {
    clsA A = new clsA();
    clsA B = new clsA("omer", "memes");
    Console.WriteLine();
    Console.WriteLine();
    clsB C = new clsB("omer", "memes", 23);
    clsB D = new clsB();
    
    
        //Default Constructor clsA
        //Parameteriaz Constructor clsA
        
        
        //Default Constructor clsA
        //Parameteriaz Constructor clsB
        
        
        //Default Constructor clsA
        //Default Constructor clsB
    
  }
} 

*/