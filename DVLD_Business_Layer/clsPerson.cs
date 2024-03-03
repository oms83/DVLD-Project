using DVLD_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsPerson
    {
        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        public int PersonID { set; get; }
        public short Gender { set; get; }
        public int CountryID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string NationalNo { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }

        public string FullName 
        {
             get
            {
                return FirstName + " " + SecondName + " " + ThirdName + " " +  LastName;
            } 
        }
        public clsCountry CountryInfo { set; get; }
        public string Address { set; get; }
        private string _ImagePath;
        public string ImagePath
        {
            set { _ImagePath = value; }
            get { return _ImagePath; }
        }
        public DateTime DateOfBirth { set; get; }

        private clsPerson(int PersonID, short Gender, int CountryID, string FirstName, string SecondName, string ThirdName, string LastName,
            string NationalNo, string Email, string Phone, string Address, string ImagePath, DateTime DateOfBirth)
        {
            this.PersonID = PersonID;
            this.Gender = Gender;
            this.CountryID = CountryID;


            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalNo = NationalNo;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.ImagePath = ImagePath;

            this.DateOfBirth = DateOfBirth;
            this.CountryInfo = clsCountry.Find(CountryID);


            _Mode = enMode.Update;
        }

        public clsPerson()
        {
            this.PersonID = -1;
            this.Gender = 0;
            this.CountryID = -1;

            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.NationalNo = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.ImagePath = "";

            this.DateOfBirth = DateTime.Now;

            _Mode = enMode.AddNew;
        }

        private bool _AddNew()
        {
            int CountryID = -1;
            short Gender = 0;

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "",
                Address = "", ImagePath = "", NationalNo = "";

            DateTime DateOfBirth = DateTime.Now;

            this.PersonID = clsPersonData.AddNewPerson(this.Gender, this.CountryID, this.FirstName, this.SecondName, this.ThirdName, this.LastName,
                                                       this.NationalNo, this.Email, this.Phone, this.Address, this.ImagePath, this.DateOfBirth);

            return (this.PersonID != -1);
        }

        private bool _Update()
        {
            return clsPersonData.UpdatePerson(this.PersonID, this.Gender, this.CountryID, this.FirstName, this.SecondName,
                                              this.ThirdName, this.LastName, this.NationalNo, this.Email,
                                              this.Phone, this.Address, this.ImagePath, this.DateOfBirth);
        }

        public static clsPerson Find(int PersonID)
        {
            int CountryID = -1;
            short Gender = 0;

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "",
                NationalNo = "";

            DateTime DateOfBirth = DateTime.Now;

            bool isFound = clsPersonData.GetPersonInfoByID(PersonID, ref Gender, ref NationalNo, ref CountryID, ref FirstName,
                                                           ref SecondName, ref ThirdName, ref LastName, ref Email,
                                                           ref Phone, ref Address, ref ImagePath, ref DateOfBirth);
            if (isFound)
            {
                return new clsPerson(PersonID, Gender, CountryID, FirstName, SecondName, ThirdName, LastName,
                                     NationalNo, Email, Phone, Address, ImagePath, DateOfBirth);
            }
            else
            {
                return null;
            }
        }
        public static clsPerson Find(string NationalNo)
        {
            int CountryID = -1, PersonID = -1;
            short Gender = 0;

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "",
                   Address = "", ImagePath = "";

            DateTime DateOfBirth = DateTime.Now;

            bool isFound = clsPersonData.GetPersonInfoByNationalNo(ref PersonID, NationalNo, ref Gender, ref CountryID, ref FirstName,
                                                           ref SecondName, ref ThirdName, ref LastName, ref Email,
                                                           ref Phone, ref Address, ref ImagePath, ref DateOfBirth);
            if (isFound)
            {
                return new clsPerson(PersonID, Gender, CountryID, FirstName, SecondName, ThirdName, LastName,
                                     NationalNo, Email, Phone, Address, ImagePath, DateOfBirth);
            }
            else
            {
                return null;
            }
        }

        public static bool IsPersonExist(int PersonID)
        {
            return clsPersonData.IsExistPerson(PersonID);
        }
        public static bool IsPersonExist(string NationalNo)
        {
            return clsPersonData.IsExistPerson(NationalNo);
        }

        public static bool Delete(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }
        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    if(_AddNew())
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
