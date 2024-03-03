using DVLD_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Business_Layer.clsApplication;

namespace DVLD_Business_Layer
{
    public class clsLicense
    {
        public enum enMode { AddNew, Update }
        public enMode Mode;
        public enum enIssueReason { FirstTime=1, Renew=2, DamagedReplacement=3, LostReplacement=4 }

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClassID  { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees  { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }
        public int CreatedByUserID {  get; set;  }

        public clsLicenseClasses LicenseClassesInfo { get; set; }
        public clsDriver DriverInfo { get; set; }
        public string IssueReasonText
        {
            get => _SetIssueReason(this.IssueReason);
        }

        public static string _SetIssueReason(enIssueReason IssuceReason)
        {
            switch (IssuceReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Damaged Replacement";
                case enIssueReason.LostReplacement:
                    return "Lost Replacement";
                default:
                    return "First Name";
            }
        }
        public clsDetainedLicense DetainedInfo { set; get; }
        public clsLicense()
        {
            this.Notes = string.Empty;
            this.PaidFees = 0;
            this.IsActive = true;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IssueReason = enIssueReason.FirstTime;
            this.ApplicationID = -1;
            this.CreatedByUserID = -1;
            this.DriverID = -1;
            this.LicenseClassID = -1;   
            this.LicenseID = -1;

            Mode = enMode.AddNew;
        }

        private clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, 
                           string Notes, float PaidFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)
        {
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IssueReason = IssueReason;
            this.ApplicationID = ApplicationID;
            this.CreatedByUserID = CreatedByUserID;
            this.DriverID = DriverID;
            this.LicenseID = LicenseID;
            this.LicenseClassID = LicenseClass;

            this.LicenseClassesInfo = clsLicenseClasses.Find(this.LicenseClassID);
            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);
            this.DetainedInfo = clsDetainedLicense.FindByLicenseID(this.LicenseID);


            Mode = enMode.AddNew;
        }

        private bool _AddNewLicense()
        {
            //call DataAccess Layer 

            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClassID,
               this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
               this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);


            return (this.LicenseID != -1);
        }
        private bool _UpdateLicense()
        {
            //call DataAccess Layer 

            return clsLicenseData.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClassID,
               this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
               this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
        }
        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1; int DriverID = -1; int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0; bool IsActive = true; int CreatedByUserID = 1;
            byte IssueReason = 1;
            if (clsLicenseData.GetLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass,
            ref IssueDate, ref ExpirationDate, ref Notes,
            ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                                     IssueDate, ExpirationDate, Notes,
                                     PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;

        }
        public static clsLicense FindByApplicationID(int ApplicationID)
        {
            int LicenseID = -1; int DriverID = -1; int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0; bool IsActive = true; int CreatedByUserID = 1;
            byte IssueReason = 1;
            if (clsLicenseData.GetLicenseInfoByAppLicationID(ref LicenseID, ApplicationID, ref DriverID, ref LicenseClass,
            ref IssueDate, ref ExpirationDate, ref Notes,
            ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                                     IssueDate, ExpirationDate, Notes,
                                     PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;

        }
        public static clsLicense FindByDriverID(int DriverID)
        {
            int LicenseID = -1; int ApplicationID = -1; int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0; bool IsActive = true; int CreatedByUserID = 1;
            byte IssueReason = 1;
            if (clsLicenseData.GetLicenseInfoByDriverID(ref LicenseID, ref ApplicationID,  DriverID, ref LicenseClass,
            ref IssueDate, ref ExpirationDate, ref Notes,
            ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                                     IssueDate, ExpirationDate, Notes,
                                     PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;

        }
        public static DataTable GetAllLicenses()
        {
            return clsLicenseData.GetAllLicenses();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLicense();

            }

            return false;
        }

        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {

            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);
        }

        public bool DeactivateCurrentLicense()
        {
            return (clsLicenseData.DeactivateLicense(this.LicenseID));
        }

        public void DeactiveteCurrentLicenese()
        {
            clsLicenseData.DeactivateLicense(this.LicenseID);
        }
        public clsLicense Renew(int CreatedByUserID, string Notes, float TotalFees)
        {
            clsApplication RenewApplication = new clsApplication();

            RenewApplication.CreatedByUserID = CreatedByUserID;
            RenewApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            RenewApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            RenewApplication.ApplicationDate = DateTime.Now;
            RenewApplication.LastStatusDate = DateTime.Now;
            RenewApplication.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees;
            RenewApplication.ApplicantPersonID = this.DriverInfo.PersonID;
            
            if (!RenewApplication.Save())
            {
                return null;
            }
            

            clsLicense NewLicense = new clsLicense();
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassesInfo.DefaultValidityLength);
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.DriverID = this.DriverID;
            NewLicense.IssueReason = enIssueReason.Renew;
            NewLicense.Notes = Notes;
            NewLicense.ApplicationID = RenewApplication.ApplicationID;
            NewLicense.IsActive = true;
            NewLicense.CreatedByUserID = CreatedByUserID;
            NewLicense.PaidFees = TotalFees;

            if (!NewLicense.Save())
            {
                return null;
            }

            DeactiveteCurrentLicenese();
            
            return NewLicense;
        }

        public clsLicense Replace(enIssueReason IssueReason, int CreatedByUserID)
        {
            clsApplication NewApplication = new clsApplication();

            NewApplication.CreatedByUserID = CreatedByUserID;

            NewApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            NewApplication.ApplicationDate = DateTime.Now;
            NewApplication.LastStatusDate = DateTime.Now;

            if (IssueReason == enIssueReason.LostReplacement)
            {
                NewApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
                NewApplication.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense).ApplicationFees;
            }
            else
            {
                NewApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
                NewApplication.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense).ApplicationFees;
            }

            NewApplication.ApplicantPersonID = this.DriverInfo.PersonID;

            if (!NewApplication.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassesInfo.DefaultValidityLength);
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.DriverID = this.DriverID;
            NewLicense.IssueReason = IssueReason;
            NewLicense.Notes = IssueReason == enIssueReason.LostReplacement ? "Lost License Replacement" : "Dameged License Replacement";
            NewLicense.ApplicationID = NewApplication.ApplicationID;
            NewLicense.IsActive = true;
            NewLicense.CreatedByUserID = CreatedByUserID;

            NewLicense.PaidFees = this.LicenseClassesInfo.ClassFees + 
                                  clsApplicationType.Find(
                                        IssueReason == enIssueReason.LostReplacement ? 
                                        (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense : 
                                        (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense
                                ).ApplicationFees;

            if (!NewLicense.Save())
            {
                return null;
            }

            DeactiveteCurrentLicenese();

            return NewLicense;
        }
        public bool IsLicenseExpired()
        {
            return this.ExpirationDate < DateTime.Now;
        }

        public int Detain(int DetainedByUserID, float PaidFees)
        {

            clsDetainedLicense DetainedLicense = new clsDetainedLicense();
            //DetainedLicense.DetainID = 
            DetainedLicense.LicenseID = this.LicenseID;
            DetainedLicense.DetainDate = DateTime.Now;
            DetainedLicense.FineFees = PaidFees;
            DetainedLicense.CreatedByUserID = DetainedByUserID;
            DetainedLicense.IsReleased = false;
            //DetainedLicense.ReleaseDate = 
            //DetainedLicense.ReleasedByUserID = 
            //DetainedLicense.ReleaseApplicationID = 

            if (!DetainedLicense.Save())
            {
                return -1;
            }

            this.DeactivateCurrentLicense();

            return DetainedLicense.DetainID;
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        {
            clsApplication ReleaseApplication = new clsApplication();

            
            //ReleaseApplication.ApplicationID = -1;
            ReleaseApplication.ApplicantPersonID = this.DriverInfo.PersonID;
            ReleaseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            ReleaseApplication.ApplicationStatus = enApplicationStatus.Completed;
            ReleaseApplication.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees;
            ReleaseApplication.CreatedByUserID = ReleasedByUserID;
            ReleaseApplication.LastStatusDate = DateTime.Now;
            ReleaseApplication.ApplicationDate = DateTime.Now;

            if (!ReleaseApplication.Save()) 
            {
                ApplicationID = -1;
                return false;
            }

            ApplicationID = ReleaseApplication.ApplicationID;


            this.Mode = enMode.Update;
            this.IsActive = true;
            this.CreatedByUserID = ReleasedByUserID;
            this.Save();


            return this.DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID, ReleaseApplication.ApplicationID);

        }

    }
}
