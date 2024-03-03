using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Layer
{
    public class clsLicenseClassesData
    {
        public static DataTable GetAllLicenseClass()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT * FROM LicenseClasses ";

            SqlCommand Command = new SqlCommand(Query, Connection);


            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if(Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return dt;
        }

        public static bool Find(int LicenseClassID, ref string ClassName, ref string ClassDescription, ref short MinimumAllowedAge,
            ref short DefaultValidityLength, ref float ClassFees)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"Select * From LicenseClasses Where LicenseClassID = @LicenseClassID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                Connection.Open();
                
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    ClassName = (string)Reader["ClassName"];
                    ClassDescription = (string)Reader["ClassDescription"];
                    MinimumAllowedAge = (byte)Reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)Reader["DefaultValidityLength"];
                    ClassFees = Convert.ToSingle(Reader["ClassFees"]);
                }
                Reader.Close();
            }
            catch (SqlException ex)
            {
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool Find(ref int LicenseClassID, string ClassName, ref string ClassDescription, ref short MinimumAllowedAge,
            ref short DefaultValidityLength, ref float ClassFees)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"Select * From LicenseClasses Where ClassName = @ClassName";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    LicenseClassID = (int)Reader["LicenseClassID"];
                    ClassDescription = (string)Reader["ClassDescription"];
                    MinimumAllowedAge = (byte)Reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)Reader["DefaultValidityLength"];
                    ClassFees = Convert.ToSingle(Reader["ClassFees"]);
                }
                Reader.Close();
            }
            catch (SqlException ex)
            {
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }
        public static bool Update(int LicenseClassID, string ClassName, string ClassDescription, short MinimumAllowedAge,
            short DefaultValidityLength, float ClassFees)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"Update LicenseClasses Set
                             ClassName = @ClassName, 
                             ClassDescription = @ClassDescription, 
                             MinimumAllowedAge = @MinimumAllowedAge, 
                             DefaultValidityLength = @DefaultValidityLength, 
                             ClassFees = @ClassFees
                             Where
                             LicenseClassID = @LicenseClassID;
                            ";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            Command.Parameters.AddWithValue("@ClassName", ClassName);
            Command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            Command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            Command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            Command.Parameters.AddWithValue("@ClassFees", ClassFees);

            int AffectedRow = 0;

            try
            {
                Connection.Open();

                AffectedRow = Command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return (AffectedRow > 0);
        }

        public static int AddNew(string ClassName, string ClassDescription, short MinimumAllowedAge, short DefaultValidityLength, float ClassFees)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            
            string Query = @"Insert Into LicenseClasses
                             (ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees)
                             Values
                             (@ClassName, @ClassDescription, @MinimumAllowedAge, @DefaultValidityLength, @ClassFees);
                             Select Scope_Indetity();
                            ";

            SqlCommand Command = new SqlCommand(Query, Connection);

            
            Command.Parameters.AddWithValue("@ClassName", ClassName);
            Command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            Command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            Command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            Command.Parameters.AddWithValue("@ClassFees", ClassFees);

            int LicenseClassID = -1;

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedLicenseClassID)) 
                {
                    LicenseClassID = InsertedLicenseClassID;
                }
            }
            catch (SqlException ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return LicenseClassID;
        }

        public static bool Delete(int LicenseClassID)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"Delete From LicenseClasses
                             Where
                             LicenseClassID = @LicenseClassID;
                            ";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            

            int AffectedRow = 0;

            try
            {
                Connection.Open();

                AffectedRow = Command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return (AffectedRow > 0);
        }

    }


}
