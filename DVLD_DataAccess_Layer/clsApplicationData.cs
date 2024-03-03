using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.Design;

namespace DVLD_DataAccess_Layer
{
    public class clsApplicationData
    {
        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            /*
             
                SELECT * FROM LocalDrivingLicenseApplications_View

				    SElECT 
				    LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID, 
				    LicenseClasses.ClassName, 
				    People.NationalNo,
				    FullName = People.FirstName + ' ' + People.SecondName + ' ' + ISNULL(People.ThirdName, '') + ' ' + People.LastName,
				    Applications.ApplicationDate, 

				    PassedTestCount = (
									    Select COUNT(Tests.TestResult)
									    From Tests 
									    INNER JOIN TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
									    Where 
										    Tests.TestResult = 1 
									    AND TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
								      ),

				    Status = 
						    CASE
							    WHEN Applications.ApplicationStatus = 1 THEN 'New'		
							    WHEN Applications.ApplicationStatus = 2 THEN 'Cancelled'		
							    WHEN Applications.ApplicationStatus = 3 THEN 'Completed'
							    ELSE 'UNKNOWN'
						    END

				    FROM People 
				    INNER JOIN Applications ON Applications.ApplicantPersonID = People.PersonID
				    INNER JOIN ApplicationTypes ON Applications.ApplicationTypeID = ApplicationTypes.ApplicationTypeID
				    INNER JOIN LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
				    INNER JOIN LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID
				    ORDER BY Applications.ApplicationDate ASC

 
            */

            string Query = "SELECT * FROM LocalDrivingLicenseApplications_View";

            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
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

        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, int ApplicationTypeID, short ApplicationStatus, float PaidFees,
            int CreatedByUserID, DateTime LastStatusDate, DateTime ApplicationDate)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"UPDATE Applications SET
                             ApplicantPersonID = @ApplicantPersonID,
                             ApplicationTypeID = @ApplicationTypeID,
                             ApplicationStatus = @ApplicationStatus,
                             PaidFees = @PaidFees,
                             CreatedByUserID = @CreatedByUserID,
                             LastStatusDate = @LastStatusDate,
                             ApplicationDate = @ApplicationDate
                             WHERE
                             ApplicationID = @ApplicationID;
                            ";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            Command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            Command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);

            int AffectedRow = 0;

            try
            {
                Connection.Open();

                AffectedRow = Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return (AffectedRow > 0);
        }

        public static int AddNewApplication(int ApplicantPersonID, int ApplicationTypeID, byte ApplicationStatus, float PaidFees,
            int CreatedByUserID, DateTime LastStatusDate, DateTime ApplicationDate)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"INSERT INTO Applications
                            (ApplicantPersonID, ApplicationTypeID, ApplicationStatus, PaidFees, CreatedByUserID, LastStatusDate, ApplicationDate)
                            VALUES
                            (@ApplicantPersonID, @ApplicationTypeID, @ApplicationStatus, @PaidFees, @CreatedByUserID, @LastStatusDate, @ApplicationDate);
                            SELECT SCOPE_IDENTITY();
                            ";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            Command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            Command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);

            int ApplicationID = -1;

            try
            {
                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedApplicationID))
                {
                    ApplicationID = InsertedApplicationID;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return ApplicationID;
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "DELETE FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlCommand Command = new SqlCommand(@Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            int AffectedRow = 0;

            try
            {
                Connection.Open();

                AffectedRow = Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return (AffectedRow > 0);
        }

        public static bool GetApplicationByApplicationID(int ApplicationID, ref int ApplicantPersonID, ref int ApplicationTypeID, ref byte ApplicationStatus,
            ref float PaidFees, ref int CreatedByUserID, ref DateTime LastStatusDate, ref DateTime ApplicationDate)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            bool IsFound = false;

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    ApplicantPersonID = (int)Reader["ApplicantPersonID"];
                    ApplicationTypeID = (int)Reader["ApplicationTypeID"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];

                    ApplicationStatus = (byte)Reader["ApplicationStatus"];

                    PaidFees = Convert.ToSingle(Reader["PaidFees"]);

                    LastStatusDate = (DateTime)Reader["LastStatusDate"];
                    ApplicationDate = (DateTime)Reader["ApplicationDate"];
                }
                    Reader.Close();
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

        public static bool GetApplicationByApplicantPersonID(ref int ApplicationID, int ApplicantPersonID, ref int ApplicationTypeID, ref short ApplicationStatus,
           ref float PaidFees, ref int CreatedByUserID, ref DateTime LastStatusDate, ref DateTime ApplicationDate)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT * FROM Applications WHERE ApplicantPersonID = @ApplicantPersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);

            bool IsFound = false;

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    ApplicationID = (int)Reader["ApplicationID"];
                    ApplicationTypeID = (int)Reader["ApplicationTypeID"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];

                    ApplicationStatus = (short)Reader["ApplicationStatus"];

                    PaidFees = Convert.ToSingle(Reader["PaidFees"]);

                    LastStatusDate = (DateTime)Reader["LastStatusDate"];
                    ApplicationDate = (DateTime)Reader["ApplicationDate"];
                    Reader.Close();
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

        public static bool IsApplicationExist(int ApplicationID)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"Select Found=1 From Applications Where ApplicationID = @ApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            bool IsFound = false;

            try
            {
                Connection.Open();
                
                SqlDataReader Reader = Command.ExecuteReader();

                if(Reader.HasRows)
                {
                    IsFound = true;
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

        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"Select ApplicationID From 
                             ApplicantPersonID = @PersonID AND 
                             ApplicationTypeID = @ApplicationTypeID AND
                             ApplicationStatus = 1";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            int ApplicationID = -1;

            try
            {
                Connection.Open();

                //SqlDataReader Reader = Command.ExecuteReader();
                //if (Reader.Read())
                //{
                //    ApplicationID = (int)Reader["ApplicationID"]; 
                //}

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int AppID))
                {
                    ApplicationID = AppID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return ApplicationID;
        }

        public static bool DoesPersonHasActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return GetActiveApplicationID(PersonID, ApplicationTypeID) > -1;
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            /*
                Select LicenseClasses.ClassName, Status = 
                Case 
                When Applications.ApplicationStatus = 1 then 'new'
                When Applications.ApplicationStatus = 1 then 'cancelled'
                When Applications.ApplicationStatus = 1 then 'completed'
                end
                From Applications Inner join LocalDrivingLicenseApplications On LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                Inner join LicenseClasses On LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID
                Where Applications.ApplicationStatus = 1
            */

            string Query = @"SELECT Applications.ApplicationID
                             FROM Applications 
                             INNER JOIN LocalDrivingLicenseApplications 
                             ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                             WHere ApplicantPersonID = @ApplicantPersonID 
                             AND ApplicationTypeID =  @ApplicationTypeID
                             AND ApplicationStatus = 1 
                             AND LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            int ApplicationID = -1;

            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int AppID))
                {
                    ApplicationID = AppID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return ApplicationID;

        }

        public static bool UpdateApplicationStatus(int ApplicationID, int ApplicationStatus)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"UPDATE Applications SET
                             ApplicationStatus = @ApplicationStatus,
                             LastStatusDate = @LastStatusDate
                             WHERE
                             ApplicationID = @ApplicationID;
                            ";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            Command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);

            int AffectedRow = 0;

            try
            {
                Connection.Open();

                AffectedRow = Command.ExecuteNonQuery();

            }
            catch (Exception ex)
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
