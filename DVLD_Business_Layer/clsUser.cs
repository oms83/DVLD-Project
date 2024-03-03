using DVLD_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsUser 
    {
        private enum enMode { AddNew=0, UpdateNew=1 }
        private enMode _Mode;
        public int UserID { get; set; }

        public clsPerson PersonInfo; // composition
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        
        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = true;

            _Mode = enMode.AddNew;
        }

        private clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.IsActive = IsActive;
            this.Password = Password;
            PersonInfo = clsPerson.Find(PersonID);
            
            _Mode = enMode.UpdateNew;
        }

        public static clsUser FindByUserID(int UserID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = true;

            bool IsFound = clsUserData.GetUserInfoByUserID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive);

            if(IsFound)
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", Password = "";
            bool IsActive = true;

            bool isFound = clsUserData.GetUserInfoByPersonID(ref UserID, PersonID, ref UserName, ref Password, ref IsActive);

            if(isFound)
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser FindByUserNameAndPassword(string UserName, string Password)
        {
            int PersonID = -1, UserID = -1;
            bool IsActive=true;

            bool IsFound = clsUserData.GetUserInfoByUserNameAndPassword(ref UserID, ref PersonID, UserName, Password, ref IsActive);

            if(IsFound)
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);

            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
        }

        public static bool DeleteUser(int PersonID)
        {
            return clsUserData.DeleteUser(PersonID);
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool IsUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }

        public static bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }

        public static bool IsUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }

        public bool ChangePassword()
        {
            return clsUserData.ChangePassword(this.UserID, this.Password);
        }
        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:

                    if(_AddNewUser())
                    {
                        _Mode = enMode.UpdateNew;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.UpdateNew:
                    return _UpdateUser();
                
            }

            return false;
        }
    }
}
