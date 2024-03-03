using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Layer
{
    public class clsPersonData
    {
        public static bool GetPersonInfoByID(int PersonID, ref short Gender, ref string NationalNo, ref int CountryID, ref string FirstName,
            ref string SecondName, ref string ThirdName, ref string LastName, ref string Email, 
            ref string Phone, ref string Address, ref string ImagePath, ref DateTime DateOfBirth)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"Select * From People Where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;
                    Gender = (byte)reader["Gender"];
                    CountryID = (int)reader["NationalityCountryID"];

                    NationalNo = (string)reader["NationalNo"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Phone = (string)reader["Phone"];

                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    SecondName = (string)reader["SecondName"];

                    if (reader["ThirdName"] !=DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }

                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }

                    if (reader["ImagePath"]!=DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetPersonInfoByNationalNo(ref int PersonID, string NationalNo, ref short Gender, ref int CountryID, ref string FirstName,
            ref string SecondName, ref string ThirdName, ref string LastName, ref string Email,
            ref string Phone, ref string Address, ref string ImagePath, ref DateTime DateOfBirth)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"Select * From People Where NationalNo = @NationalNo";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);

            bool isFound = false;
            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;
                    Gender = (byte)reader["Gender"];
                    PersonID = (int)reader["PersonID"];
                    CountryID = (int)reader["NationalityCountryID"];

                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Phone = (string)reader["Phone"];

                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }
                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Connection.Close();
            }
            return isFound;
        }

        public static int AddNewPerson(short Gender, int CountryID, string FirstName, string SecondName, string ThirdName, string LastName,
            string NationalNo, string Email, string Phone, string Address, string ImagePath, DateTime DateOfBirth)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            int PersonID = -1;
            string Query = @"Insert Into People (
                                            Gender, 
                                            NationalityCountryID, 
                                            FirstName, 
                                            SecondName, 
                                            ThirdName, 
                                            LastName,
                                            NationalNo,
                                            Email, 
                                            Phone, 
                                            Address, 
                                            ImagePath, 
                                            DateOfBirth
                                        )
                                        Values
                                        (
                                            @Gender, 
                                            @NationalityCountryID, 
                                            @FirstName, 
                                            @SecondName, 
                                            @ThirdName, 
                                            @LastName,
                                            @NationalNo,
                                            @Email, 
                                            @Phone, 
                                            @Address, 
                                            @ImagePath, 
                                            @DateOfBirth
                                        );
                                        SELECT SCOPE_IDENTITY();
                                        ";
            
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@Gender", Gender);
            Command.Parameters.AddWithValue("@NationalityCountryID", CountryID);
            Command.Parameters.AddWithValue("@FirstName", FirstName);
            Command.Parameters.AddWithValue("@SecondName", SecondName);
            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
            Command.Parameters.AddWithValue("@Phone", Phone);
            Command.Parameters.AddWithValue("@Address", Address);
            Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

            if (Email != null || Email != "")
                Command.Parameters.AddWithValue("@Email", Email);
            else
                Command.Parameters.AddWithValue("@Email", System.DBNull.Value);


            if (ThirdName != "" || ThirdName != null) 
                Command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                Command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);


            if (ImagePath != null || ImagePath != "") 
                Command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                Command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);



            try
            {
                Connection.Open();
                object Result = Command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    PersonID = InsertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }
            return PersonID;
        }
        
        public static bool UpdatePerson(int PersonID, short Gender, int CountryID, string FirstName, string SecondName, string ThirdName, string LastName,
            string NationalNo, string Email, string Phone, string Address, string ImagePath, DateTime DateOfBirth)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"Update People 
                             Set
                                Gender = @Gender,
                                NationalityCountryID = @CountryID,
                                FirstName = @FirstName,
                                SecondName = @SecondName,
                                ThirdName = @ThirdName,
                                LastName = @LastName,
                                NationalNo = @NationalNo,
                                Email = @Email,
                                Phone = @Phone,
                                Address = @Address,
                                ImagePath = @ImagePath,
                                DateOfBirth = @DateOfBirth
                             Where
                             PersonID = @PersonID;
                            ";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@Gender", Gender);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@CountryID", CountryID);
            Command.Parameters.AddWithValue("@FirstName", FirstName);
            Command.Parameters.AddWithValue("@SecondName", SecondName);
            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
            Command.Parameters.AddWithValue("@Phone", Phone);
            Command.Parameters.AddWithValue("@Address", Address);

            if (Email != null || Email != "")
                Command.Parameters.AddWithValue("@Email", Email);
            else
                Command.Parameters.AddWithValue("@Email", System.DBNull.Value);
            if (ThirdName != "" || ThirdName != null)
                Command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                Command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            if (ImagePath != null || ImagePath != "")
                Command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                Command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

            int EffectedRow = -1;
            try
            {
                Connection.Open();
                EffectedRow = Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }
            return (EffectedRow > 0);
        }
        
        public static bool DeletePerson(int PersonID)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            
            string Query = @"Delete From People Where PersonID = @PersonID";
            
            SqlCommand Command = new SqlCommand(Query, Connection);
            
            Command.Parameters.AddWithValue("@PersonID", PersonID);

            int EffectedRow = -1;
            try
            {
                Connection.Open();
                EffectedRow = Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }
            return (EffectedRow > 0);
        }
    
        public static DataTable GetAllPeople()
        {
            DataTable dataTable = new DataTable();
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName,
                             People.ThirdName, People.LastName, People.DateOfBirth,
                             People.Gender, 

                             CASE
                                    WHEN People.Gender = 0 THEN 'Male'
                             ELSE   'Female'
                             END as GenderCaption,

                             People.Address, People.Phone, People.Email, 
                             People.NationalityCountryID, Countries.CountryName, People.ImagePath
                             
                             FROM People
                             INNER JOIN Countries ON People.NationalityCountryID = Countries.CountryID
                             ORDER BY People.FirstName
                            ";
            
            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();
                
                SqlDataReader reader = Command.ExecuteReader();
                
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                
                reader.Close();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return dataTable;
        }

        public static bool IsExistPerson(int PersonID)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"Select found=1 From People Where PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

            bool isFound = false;

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                Connection.Close();
            }

            return isFound;
        }

        public static bool IsExistPerson(string NationalNo)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"Select found=1 From People Where NationalNo = @NationalNo";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
            
            bool isFound = false;

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                Connection.Close();
            }

            return isFound;
        }



    }


}
