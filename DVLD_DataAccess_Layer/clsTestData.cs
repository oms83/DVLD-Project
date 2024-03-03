using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Layer
{
    public class clsTestData
    {
        public static bool GetTestInfoByID(int TestID, ref int TestAppoinmentsID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"Select * From Tests Where TestID = @TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestID", TestID);

            bool IsFound = false;

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    TestAppoinmentsID = (int)Reader["TestAppointmentID"];
                    TestResult = (bool)Reader["TestResult"];
                    if (Reader["Notes"] != DBNull.Value)
                    {
                        Notes = (string)Reader["Notes"];
                    }
                    else
                    {
                        Notes = "";
                    }
                    CreatedByUserID = (int)Reader["CreatedByUserID"];

                    Reader.Close();
                }
                else
                {
                    IsFound = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }
        public static bool FindTestInfoByTestAppointmentID(ref int TestID, int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"Select * From Tests Where TestAppointmentID = @TestAppointmentID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            bool IsFound = false;

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    TestID = (int)Reader["TestID"];
                    TestResult = (bool)Reader["TestResult"];
                    if (Reader["Notes"] != DBNull.Value)
                    {
                        Notes = (string)Reader["Notes"];
                    }
                    else
                    {
                        Notes = "";
                    }
                    CreatedByUserID = (int)Reader["CreatedByUserID"];

                    Reader.Close();
                }
                else
                {
                    IsFound = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }
        public static bool GetLastTestByPersonIDAndTestTypeAndLicenseClass(int PersonID, int LicenseClassID, int TestTypeID, ref int TestID,
              ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            /*
                	SELECT TOP 1 People.FirstName, TestTypes.TestTypeTitle, Applications.ApplicationID, LicenseClasses.ClassName,
	         			         TestTypes.TestTypeTitle, Tests.TestID, TestAppointments.TestAppointmentID, 
	                Result = 
	                CASE
		                WHEN Tests.TestResult = 1 THEN 'Pass'
		                WHEN Tests.TestResult = 0 THEN 'Fail'
	                END
	                FROM People
	                INNER JOIN Applications 
		                ON Applications.ApplicantPersonID = People.PersonID
	                INNER JOIN ApplicationTypes 
		                ON Applications.ApplicationTypeID = ApplicationTypes.ApplicationTypeID
	                INNER JOIN LocalDrivingLicenseApplications 
		                ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
	                INNER JOIN LicenseClasses 
		                ON LicenseClasses.LicenseClassID = LocalDrivingLicenseApplications.LicenseClassID
	                INNER JOIN TestAppointments 
		                ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
	                INNER JOIN Tests 
		                ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
	                INNER JOIN TestTypes 
		                ON TestAppointments.TestTypeID = TestTypes.TestTypeID
	                WHERE People.PersonID = 1 And LicenseClasses.LicenseClassID = 1 And TestTypes.TestTypeID = 1
	                ORDER BY TestAppointments.TestAppointmentID DESC

   
            */

            string Query = @"SELECT TOP 1 Tests.TestResult, Applications.ApplicantPersonID, Tests.TestID, 
	                                      TestAppointments.TestAppointmentID, Tests.Notes, Tests.CreatedByUserID
                             FROM Applications
	                         INNER JOIN LocalDrivingLicenseApplications 
	                         	ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
	                         INNER JOIN TestAppointments 
	                         	ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
	                         INNER JOIN Tests 
	                         	ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
	                         WHERE Applications.ApplicantPersonID =  @PersonID
                             And   LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                             And   TestAppointments.TestTypeID = @TestTypeID
	                         ORDER BY TestAppointments.TestAppointmentID DESC";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            bool IsFound = false;

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    TestID = (int)Reader["TestID"];
                    TestAppointmentID = (int)Reader["TestAppointmentID"];
                    TestResult = (bool)Reader["TestResult"];
                    if (Reader["Notes"] == DBNull.Value)

                        Notes = "";
                    else
                        Notes = (string)Reader["Notes"];

                    CreatedByUserID = (int)Reader["CreatedByUserID"];

                    Reader.Close();
                }
                else
                {
                    IsFound = false;
                }
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static DataTable GetAllTests()
        {

            DataTable dt = new DataTable();
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM Tests ORDER BY TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)

                {
                    dt.Load(Reader);
                }

                Reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return dt;

        }

        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"Insert Into Tests 
                             (TestAppointmentID, TestResult, Notes, CreatedByUserID)
                             Values 
                             (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);
                            
                             UPDATE TestAppointments 
                             SET IsLocked=1 
                             WHERE TestAppointmentID = @TestAppointmentID;
                             
                             SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            Command.Parameters.AddWithValue("@TestResult", TestResult);

            if (Notes != "" && Notes != null)
                Command.Parameters.AddWithValue("@Notes", Notes);
            else
                Command.Parameters.AddWithValue("@Notes", System.DBNull.Value);



            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                Connection.Close();
            }


            return TestID;
        }

        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"Update  Tests  
                            set TestAppointmentID = @TestAppointmentID,
                                TestResult=@TestResult,
                                Notes = @Notes,
                                CreatedByUserID=@CreatedByUserID
                                where TestID = @TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestID", TestID);
            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            Command.Parameters.AddWithValue("@TestResult", TestResult);
            Command.Parameters.AddWithValue("@Notes", Notes);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();
                rowsAffected = Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT PassedTestCount = COUNT(TestResult)
                             FROM Tests INNER JOIN TestAppointments 
                             ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestResult=1";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);


            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte ptCount))
                {
                    PassedTestCount = ptCount;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                Connection.Close();
            }

            return PassedTestCount;
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }


    }
}
