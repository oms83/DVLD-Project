using DVLD_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsApplication
    {
        public enum enMode { AddNew, Update }

        public enum enApplicationStatus { New=1, Cancelled=2, Completed=3 }
        public enum enApplicationType
        {
            NewDrivingLicense = 1, 
            RenewDrivingLicense = 2, 
            ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, 
            ReleaseDetainedDrivingLicsense = 5, 
            NewInternationalLicense = 6, 
            RetakeTest = 7
        };

        public enMode Mode = enMode.AddNew;
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public int ApplicationTypeID { get; set; }
        public int CreatedByUserID { get; set; }
        public float PaidFees { get; set; }
        public DateTime LastStatusDate { get; set; }
        public DateTime ApplicationDate {  get;  set; }

        public clsApplicationType ApplicationTypeInfo;
        public clsPerson ApplicantPersonInfo;          
        public clsUser CreatedByUserINfo;              

        public string ApplicantPersonFullName
        {
            get
            {
                return clsPerson.Find(this.ApplicantPersonID).FullName;
            }

        }

        /*
            public string ApplicantFullName
            {
                get 
                {
                    clsPerson.Find(this.ApplicantPersonID).FullName;
                }
            }
        */

        public enApplicationStatus ApplicationStatus { get; set; }

        public string StatusText
        {
            get
            {
                switch (ApplicationStatus)
                {   
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }
        }

        private clsApplication(int ApplicationID, int ApplicantPersonID, int ApplicationTypeID, enApplicationStatus ApplicationStatus, float PaidFees,
            int CreatedByUserID, DateTime LastStatusDate, DateTime ApplicationDate)
        {
            this.ApplicationID = ApplicationID ;
            this.ApplicantPersonID = ApplicantPersonID ;
            this.ApplicationTypeID = ApplicationTypeID ;
            this.ApplicationStatus = ApplicationStatus;
            this.PaidFees = PaidFees ;
            this.CreatedByUserID = CreatedByUserID ;
            this.LastStatusDate = LastStatusDate;
            this.ApplicationDate = ApplicationDate;

            this.ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);
            this.ApplicantPersonInfo = clsPerson.Find(ApplicantPersonID);
            this.CreatedByUserINfo = clsUser.FindByUserID(CreatedByUserID);

            Mode = enMode.Update;
        }

        public clsApplication()
        {

            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.LastStatusDate = DateTime.Now;
            this.ApplicationDate = DateTime.Now;

            Mode = enMode.AddNew;
        }

        public static clsApplication GetApplicationByApplicationID(int ApplicationID)
        {
            int ApplicantPersonID = -1, CreatedByUserID = -1, ApplicationTypeID = -1;
            byte ApplicationStatus = 0;
            float PaidFees = 0;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now; 

            bool IsFound = clsApplicationData.GetApplicationByApplicationID(ApplicationID, ref ApplicantPersonID, 
                ref ApplicationTypeID, ref ApplicationStatus, ref PaidFees, ref CreatedByUserID, ref LastStatusDate, ref ApplicationDate);

            if (IsFound)
            {
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationTypeID, (enApplicationStatus)ApplicationStatus,
                                              PaidFees, CreatedByUserID, LastStatusDate, ApplicationDate);
            }
            else
            {
                return null;
            }
        }
        
        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicantPersonID, this.ApplicationTypeID, (byte)this.ApplicationStatus,
                                              this.PaidFees, this.CreatedByUserID, this.LastStatusDate, this.ApplicationDate);

            return this.ApplicationID != -1;
        }

        public bool Cancel()
        {
            return clsApplicationData.UpdateApplicationStatus(this.ApplicationID, (int)enApplicationStatus.Cancelled);
        }

        public bool SetComplete()
        {
            return clsApplicationData.UpdateApplicationStatus(this.ApplicationID, (int)enApplicationStatus.Completed);
        }

        public bool Delete()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }

        public static bool Delete(int ApplicationID)
        {
            return clsApplicationData.DeleteApplication(ApplicationID);
        }
        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationTypeID, (byte)this.ApplicationStatus,
                                              this.PaidFees, this.CreatedByUserID, this.LastStatusDate, this.ApplicationDate);
        }

        public static DataTable GetAllApplications()
        {
            return clsApplicationData.GetAllApplications();
        }
        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExist(ApplicationID);
        }

        public static bool DoesPersonHasActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHasActiveApplication(PersonID,  ApplicationTypeID);
        }

        public bool DoesPersonHasActiveApplication(int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHasActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        }
        public int GetActiveApplicationID(clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(this.ApplicantPersonID, (int)ApplicationTypeID);
        }

        public static bool UpdateApplicationStatus(int ApplicationID, int ApplicationStatus)
        {
            return clsApplicationData.UpdateApplicationStatus(ApplicationID, ApplicationStatus);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, ApplicationTypeID, LicenseClassID);
        }
        public int GetActiveApplicationIDForLicenseClass(clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(this.ApplicantPersonID, (int)ApplicationTypeID, LicenseClassID);
        }
        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateApplication();
            }

            return false;
        }





    }
}
