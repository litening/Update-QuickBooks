using System;
using System.Data;
using System.Data.SqlClient;
using QB2.Models;

namespace QB2.Data
{
    public class CustomerData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement
                = "SELECT "  
                + "     [Customer].[CustomerId] "
                + "    ,[Customer].[FirstName] "
                + "    ,[Customer].[LastName] "
                + "    ,[Customer].[CompanyName] "
                + "    ,[Customer].[Address1] "
                + "    ,[Customer].[Address2] "
                + "    ,[Customer].[City] "
                + "    ,[Customer].[State] "
                + "    ,[Customer].[Zip] "
                + "    ,[Customer].[Country] "
                + "    ,[Customer].[ShipToAddress1] "
                + "    ,[Customer].[ShipToAddress2] "
                + "    ,[Customer].[ShipToCity] "
                + "    ,[Customer].[ShipToState] "
                + "    ,[Customer].[ShipToZip] "
                + "    ,[Customer].[ShipToCountry] "
                + "    ,[Customer].[ShipToName] "
                + "    ,[Customer].[Phone] "
                + "    ,[Customer].[eMailAddr] "
                + "    ,[Customer].[QbCustomerId] "
                + "FROM " 
                + "     [Customer] " 
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows) {
                    dt.Load(reader); }
                reader.Close();
            }
            catch (SqlException)
            {
                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable Search(string sField, string sCondition, string sValue)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement = "";
            if (sCondition == "Contains") {
                selectStatement
                    = "SELECT "
                + "     [Customer].[CustomerId] "
                + "    ,[Customer].[FirstName] "
                + "    ,[Customer].[LastName] "
                + "    ,[Customer].[CompanyName] "
                + "    ,[Customer].[Address1] "
                + "    ,[Customer].[Address2] "
                + "    ,[Customer].[City] "
                + "    ,[Customer].[State] "
                + "    ,[Customer].[Zip] "
                + "    ,[Customer].[Country] "
                + "    ,[Customer].[ShipToAddress1] "
                + "    ,[Customer].[ShipToAddress2] "
                + "    ,[Customer].[ShipToCity] "
                + "    ,[Customer].[ShipToState] "
                + "    ,[Customer].[ShipToZip] "
                + "    ,[Customer].[ShipToCountry] "
                + "    ,[Customer].[ShipToName] "
                + "    ,[Customer].[Phone] "
                + "    ,[Customer].[eMailAddr] "
                + "    ,[Customer].[QbCustomerId] "
                + "FROM " 
                + "     [Customer] " 
                    + "WHERE " 
                    + "     (@CustomerId IS NULL OR @CustomerId = '' OR [Customer].[CustomerId] LIKE '%' + LTRIM(RTRIM(@CustomerId)) + '%') " 
                    + "AND   (@FirstName IS NULL OR @FirstName = '' OR [Customer].[FirstName] LIKE '%' + LTRIM(RTRIM(@FirstName)) + '%') " 
                    + "AND   (@LastName IS NULL OR @LastName = '' OR [Customer].[LastName] LIKE '%' + LTRIM(RTRIM(@LastName)) + '%') " 
                    + "AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Customer].[CompanyName] LIKE '%' + LTRIM(RTRIM(@CompanyName)) + '%') " 
                    + "AND   (@Address1 IS NULL OR @Address1 = '' OR [Customer].[Address1] LIKE '%' + LTRIM(RTRIM(@Address1)) + '%') " 
                    + "AND   (@Address2 IS NULL OR @Address2 = '' OR [Customer].[Address2] LIKE '%' + LTRIM(RTRIM(@Address2)) + '%') " 
                    + "AND   (@City IS NULL OR @City = '' OR [Customer].[City] LIKE '%' + LTRIM(RTRIM(@City)) + '%') " 
                    + "AND   (@State IS NULL OR @State = '' OR [Customer].[State] LIKE '%' + LTRIM(RTRIM(@State)) + '%') " 
                    + "AND   (@Zip IS NULL OR @Zip = '' OR [Customer].[Zip] LIKE '%' + LTRIM(RTRIM(@Zip)) + '%') " 
                    + "AND   (@Country IS NULL OR @Country = '' OR [Customer].[Country] LIKE '%' + LTRIM(RTRIM(@Country)) + '%') " 
                    + "AND   (@ShipToAddress1 IS NULL OR @ShipToAddress1 = '' OR [Customer].[ShipToAddress1] LIKE '%' + LTRIM(RTRIM(@ShipToAddress1)) + '%') " 
                    + "AND   (@ShipToAddress2 IS NULL OR @ShipToAddress2 = '' OR [Customer].[ShipToAddress2] LIKE '%' + LTRIM(RTRIM(@ShipToAddress2)) + '%') " 
                    + "AND   (@ShipToCity IS NULL OR @ShipToCity = '' OR [Customer].[ShipToCity] LIKE '%' + LTRIM(RTRIM(@ShipToCity)) + '%') " 
                    + "AND   (@ShipToState IS NULL OR @ShipToState = '' OR [Customer].[ShipToState] LIKE '%' + LTRIM(RTRIM(@ShipToState)) + '%') " 
                    + "AND   (@ShipToZip IS NULL OR @ShipToZip = '' OR [Customer].[ShipToZip] LIKE '%' + LTRIM(RTRIM(@ShipToZip)) + '%') " 
                    + "AND   (@ShipToCountry IS NULL OR @ShipToCountry = '' OR [Customer].[ShipToCountry] LIKE '%' + LTRIM(RTRIM(@ShipToCountry)) + '%') " 
                    + "AND   (@ShipToName IS NULL OR @ShipToName = '' OR [Customer].[ShipToName] LIKE '%' + LTRIM(RTRIM(@ShipToName)) + '%') " 
                    + "AND   (@Phone IS NULL OR @Phone = '' OR [Customer].[Phone] LIKE '%' + LTRIM(RTRIM(@Phone)) + '%') " 
                    + "AND   (@eMailAddr IS NULL OR @eMailAddr = '' OR [Customer].[eMailAddr] LIKE '%' + LTRIM(RTRIM(@eMailAddr)) + '%') " 
                    + "AND   (@QbCustomerId IS NULL OR @QbCustomerId = '' OR [Customer].[QbCustomerId] LIKE '%' + LTRIM(RTRIM(@QbCustomerId)) + '%') " 
                    + "";
            } else if (sCondition == "Equals") {
                selectStatement
                    = "SELECT "
                + "     [Customer].[CustomerId] "
                + "    ,[Customer].[FirstName] "
                + "    ,[Customer].[LastName] "
                + "    ,[Customer].[CompanyName] "
                + "    ,[Customer].[Address1] "
                + "    ,[Customer].[Address2] "
                + "    ,[Customer].[City] "
                + "    ,[Customer].[State] "
                + "    ,[Customer].[Zip] "
                + "    ,[Customer].[Country] "
                + "    ,[Customer].[ShipToAddress1] "
                + "    ,[Customer].[ShipToAddress2] "
                + "    ,[Customer].[ShipToCity] "
                + "    ,[Customer].[ShipToState] "
                + "    ,[Customer].[ShipToZip] "
                + "    ,[Customer].[ShipToCountry] "
                + "    ,[Customer].[ShipToName] "
                + "    ,[Customer].[Phone] "
                + "    ,[Customer].[eMailAddr] "
                + "    ,[Customer].[QbCustomerId] "
                + "FROM " 
                + "     [Customer] " 
                    + "WHERE " 
                    + "     (@CustomerId IS NULL OR @CustomerId = '' OR [Customer].[CustomerId] = LTRIM(RTRIM(@CustomerId))) " 
                    + "AND   (@FirstName IS NULL OR @FirstName = '' OR [Customer].[FirstName] = LTRIM(RTRIM(@FirstName))) " 
                    + "AND   (@LastName IS NULL OR @LastName = '' OR [Customer].[LastName] = LTRIM(RTRIM(@LastName))) " 
                    + "AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Customer].[CompanyName] = LTRIM(RTRIM(@CompanyName))) " 
                    + "AND   (@Address1 IS NULL OR @Address1 = '' OR [Customer].[Address1] = LTRIM(RTRIM(@Address1))) " 
                    + "AND   (@Address2 IS NULL OR @Address2 = '' OR [Customer].[Address2] = LTRIM(RTRIM(@Address2))) " 
                    + "AND   (@City IS NULL OR @City = '' OR [Customer].[City] = LTRIM(RTRIM(@City))) " 
                    + "AND   (@State IS NULL OR @State = '' OR [Customer].[State] = LTRIM(RTRIM(@State))) " 
                    + "AND   (@Zip IS NULL OR @Zip = '' OR [Customer].[Zip] = LTRIM(RTRIM(@Zip))) " 
                    + "AND   (@Country IS NULL OR @Country = '' OR [Customer].[Country] = LTRIM(RTRIM(@Country))) " 
                    + "AND   (@ShipToAddress1 IS NULL OR @ShipToAddress1 = '' OR [Customer].[ShipToAddress1] = LTRIM(RTRIM(@ShipToAddress1))) " 
                    + "AND   (@ShipToAddress2 IS NULL OR @ShipToAddress2 = '' OR [Customer].[ShipToAddress2] = LTRIM(RTRIM(@ShipToAddress2))) " 
                    + "AND   (@ShipToCity IS NULL OR @ShipToCity = '' OR [Customer].[ShipToCity] = LTRIM(RTRIM(@ShipToCity))) " 
                    + "AND   (@ShipToState IS NULL OR @ShipToState = '' OR [Customer].[ShipToState] = LTRIM(RTRIM(@ShipToState))) " 
                    + "AND   (@ShipToZip IS NULL OR @ShipToZip = '' OR [Customer].[ShipToZip] = LTRIM(RTRIM(@ShipToZip))) " 
                    + "AND   (@ShipToCountry IS NULL OR @ShipToCountry = '' OR [Customer].[ShipToCountry] = LTRIM(RTRIM(@ShipToCountry))) " 
                    + "AND   (@ShipToName IS NULL OR @ShipToName = '' OR [Customer].[ShipToName] = LTRIM(RTRIM(@ShipToName))) " 
                    + "AND   (@Phone IS NULL OR @Phone = '' OR [Customer].[Phone] = LTRIM(RTRIM(@Phone))) " 
                    + "AND   (@eMailAddr IS NULL OR @eMailAddr = '' OR [Customer].[eMailAddr] = LTRIM(RTRIM(@eMailAddr))) " 
                    + "AND   (@QbCustomerId IS NULL OR @QbCustomerId = '' OR [Customer].[QbCustomerId] = LTRIM(RTRIM(@QbCustomerId))) " 
                    + "";
            } else if  (sCondition == "Starts with...") {
                selectStatement
                    = "SELECT "
                + "     [Customer].[CustomerId] "
                + "    ,[Customer].[FirstName] "
                + "    ,[Customer].[LastName] "
                + "    ,[Customer].[CompanyName] "
                + "    ,[Customer].[Address1] "
                + "    ,[Customer].[Address2] "
                + "    ,[Customer].[City] "
                + "    ,[Customer].[State] "
                + "    ,[Customer].[Zip] "
                + "    ,[Customer].[Country] "
                + "    ,[Customer].[ShipToAddress1] "
                + "    ,[Customer].[ShipToAddress2] "
                + "    ,[Customer].[ShipToCity] "
                + "    ,[Customer].[ShipToState] "
                + "    ,[Customer].[ShipToZip] "
                + "    ,[Customer].[ShipToCountry] "
                + "    ,[Customer].[ShipToName] "
                + "    ,[Customer].[Phone] "
                + "    ,[Customer].[eMailAddr] "
                + "    ,[Customer].[QbCustomerId] "
                + "FROM " 
                + "     [Customer] " 
                    + "WHERE " 
                    + "     (@CustomerId IS NULL OR @CustomerId = '' OR [Customer].[CustomerId] LIKE LTRIM(RTRIM(@CustomerId)) + '%') " 
                    + "AND   (@FirstName IS NULL OR @FirstName = '' OR [Customer].[FirstName] LIKE LTRIM(RTRIM(@FirstName)) + '%') " 
                    + "AND   (@LastName IS NULL OR @LastName = '' OR [Customer].[LastName] LIKE LTRIM(RTRIM(@LastName)) + '%') " 
                    + "AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Customer].[CompanyName] LIKE LTRIM(RTRIM(@CompanyName)) + '%') " 
                    + "AND   (@Address1 IS NULL OR @Address1 = '' OR [Customer].[Address1] LIKE LTRIM(RTRIM(@Address1)) + '%') " 
                    + "AND   (@Address2 IS NULL OR @Address2 = '' OR [Customer].[Address2] LIKE LTRIM(RTRIM(@Address2)) + '%') " 
                    + "AND   (@City IS NULL OR @City = '' OR [Customer].[City] LIKE LTRIM(RTRIM(@City)) + '%') " 
                    + "AND   (@State IS NULL OR @State = '' OR [Customer].[State] LIKE LTRIM(RTRIM(@State)) + '%') " 
                    + "AND   (@Zip IS NULL OR @Zip = '' OR [Customer].[Zip] LIKE LTRIM(RTRIM(@Zip)) + '%') " 
                    + "AND   (@Country IS NULL OR @Country = '' OR [Customer].[Country] LIKE LTRIM(RTRIM(@Country)) + '%') " 
                    + "AND   (@ShipToAddress1 IS NULL OR @ShipToAddress1 = '' OR [Customer].[ShipToAddress1] LIKE LTRIM(RTRIM(@ShipToAddress1)) + '%') " 
                    + "AND   (@ShipToAddress2 IS NULL OR @ShipToAddress2 = '' OR [Customer].[ShipToAddress2] LIKE LTRIM(RTRIM(@ShipToAddress2)) + '%') " 
                    + "AND   (@ShipToCity IS NULL OR @ShipToCity = '' OR [Customer].[ShipToCity] LIKE LTRIM(RTRIM(@ShipToCity)) + '%') " 
                    + "AND   (@ShipToState IS NULL OR @ShipToState = '' OR [Customer].[ShipToState] LIKE LTRIM(RTRIM(@ShipToState)) + '%') " 
                    + "AND   (@ShipToZip IS NULL OR @ShipToZip = '' OR [Customer].[ShipToZip] LIKE LTRIM(RTRIM(@ShipToZip)) + '%') " 
                    + "AND   (@ShipToCountry IS NULL OR @ShipToCountry = '' OR [Customer].[ShipToCountry] LIKE LTRIM(RTRIM(@ShipToCountry)) + '%') " 
                    + "AND   (@ShipToName IS NULL OR @ShipToName = '' OR [Customer].[ShipToName] LIKE LTRIM(RTRIM(@ShipToName)) + '%') " 
                    + "AND   (@Phone IS NULL OR @Phone = '' OR [Customer].[Phone] LIKE LTRIM(RTRIM(@Phone)) + '%') " 
                    + "AND   (@eMailAddr IS NULL OR @eMailAddr = '' OR [Customer].[eMailAddr] LIKE LTRIM(RTRIM(@eMailAddr)) + '%') " 
                    + "AND   (@QbCustomerId IS NULL OR @QbCustomerId = '' OR [Customer].[QbCustomerId] LIKE LTRIM(RTRIM(@QbCustomerId)) + '%') " 
                    + "";
            } else if  (sCondition == "More than...") {
                selectStatement
                    = "SELECT "
                + "     [Customer].[CustomerId] "
                + "    ,[Customer].[FirstName] "
                + "    ,[Customer].[LastName] "
                + "    ,[Customer].[CompanyName] "
                + "    ,[Customer].[Address1] "
                + "    ,[Customer].[Address2] "
                + "    ,[Customer].[City] "
                + "    ,[Customer].[State] "
                + "    ,[Customer].[Zip] "
                + "    ,[Customer].[Country] "
                + "    ,[Customer].[ShipToAddress1] "
                + "    ,[Customer].[ShipToAddress2] "
                + "    ,[Customer].[ShipToCity] "
                + "    ,[Customer].[ShipToState] "
                + "    ,[Customer].[ShipToZip] "
                + "    ,[Customer].[ShipToCountry] "
                + "    ,[Customer].[ShipToName] "
                + "    ,[Customer].[Phone] "
                + "    ,[Customer].[eMailAddr] "
                + "    ,[Customer].[QbCustomerId] "
                + "FROM " 
                + "     [Customer] " 
                    + "WHERE " 
                    + "     (@CustomerId IS NULL OR @CustomerId = '' OR [Customer].[CustomerId] > LTRIM(RTRIM(@CustomerId))) " 
                    + "AND   (@FirstName IS NULL OR @FirstName = '' OR [Customer].[FirstName] > LTRIM(RTRIM(@FirstName))) " 
                    + "AND   (@LastName IS NULL OR @LastName = '' OR [Customer].[LastName] > LTRIM(RTRIM(@LastName))) " 
                    + "AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Customer].[CompanyName] > LTRIM(RTRIM(@CompanyName))) " 
                    + "AND   (@Address1 IS NULL OR @Address1 = '' OR [Customer].[Address1] > LTRIM(RTRIM(@Address1))) " 
                    + "AND   (@Address2 IS NULL OR @Address2 = '' OR [Customer].[Address2] > LTRIM(RTRIM(@Address2))) " 
                    + "AND   (@City IS NULL OR @City = '' OR [Customer].[City] > LTRIM(RTRIM(@City))) " 
                    + "AND   (@State IS NULL OR @State = '' OR [Customer].[State] > LTRIM(RTRIM(@State))) " 
                    + "AND   (@Zip IS NULL OR @Zip = '' OR [Customer].[Zip] > LTRIM(RTRIM(@Zip))) " 
                    + "AND   (@Country IS NULL OR @Country = '' OR [Customer].[Country] > LTRIM(RTRIM(@Country))) " 
                    + "AND   (@ShipToAddress1 IS NULL OR @ShipToAddress1 = '' OR [Customer].[ShipToAddress1] > LTRIM(RTRIM(@ShipToAddress1))) " 
                    + "AND   (@ShipToAddress2 IS NULL OR @ShipToAddress2 = '' OR [Customer].[ShipToAddress2] > LTRIM(RTRIM(@ShipToAddress2))) " 
                    + "AND   (@ShipToCity IS NULL OR @ShipToCity = '' OR [Customer].[ShipToCity] > LTRIM(RTRIM(@ShipToCity))) " 
                    + "AND   (@ShipToState IS NULL OR @ShipToState = '' OR [Customer].[ShipToState] > LTRIM(RTRIM(@ShipToState))) " 
                    + "AND   (@ShipToZip IS NULL OR @ShipToZip = '' OR [Customer].[ShipToZip] > LTRIM(RTRIM(@ShipToZip))) " 
                    + "AND   (@ShipToCountry IS NULL OR @ShipToCountry = '' OR [Customer].[ShipToCountry] > LTRIM(RTRIM(@ShipToCountry))) " 
                    + "AND   (@ShipToName IS NULL OR @ShipToName = '' OR [Customer].[ShipToName] > LTRIM(RTRIM(@ShipToName))) " 
                    + "AND   (@Phone IS NULL OR @Phone = '' OR [Customer].[Phone] > LTRIM(RTRIM(@Phone))) " 
                    + "AND   (@eMailAddr IS NULL OR @eMailAddr = '' OR [Customer].[eMailAddr] > LTRIM(RTRIM(@eMailAddr))) " 
                    + "AND   (@QbCustomerId IS NULL OR @QbCustomerId = '' OR [Customer].[QbCustomerId] > LTRIM(RTRIM(@QbCustomerId))) " 
                    + "";
            } else if  (sCondition == "Less than...") {
                selectStatement
                    = "SELECT " 
                + "     [Customer].[CustomerId] "
                + "    ,[Customer].[FirstName] "
                + "    ,[Customer].[LastName] "
                + "    ,[Customer].[CompanyName] "
                + "    ,[Customer].[Address1] "
                + "    ,[Customer].[Address2] "
                + "    ,[Customer].[City] "
                + "    ,[Customer].[State] "
                + "    ,[Customer].[Zip] "
                + "    ,[Customer].[Country] "
                + "    ,[Customer].[ShipToAddress1] "
                + "    ,[Customer].[ShipToAddress2] "
                + "    ,[Customer].[ShipToCity] "
                + "    ,[Customer].[ShipToState] "
                + "    ,[Customer].[ShipToZip] "
                + "    ,[Customer].[ShipToCountry] "
                + "    ,[Customer].[ShipToName] "
                + "    ,[Customer].[Phone] "
                + "    ,[Customer].[eMailAddr] "
                + "    ,[Customer].[QbCustomerId] "
                + "FROM " 
                + "     [Customer] " 
                    + "WHERE " 
                    + "     (@CustomerId IS NULL OR @CustomerId = '' OR [Customer].[CustomerId] < LTRIM(RTRIM(@CustomerId))) " 
                    + "AND   (@FirstName IS NULL OR @FirstName = '' OR [Customer].[FirstName] < LTRIM(RTRIM(@FirstName))) " 
                    + "AND   (@LastName IS NULL OR @LastName = '' OR [Customer].[LastName] < LTRIM(RTRIM(@LastName))) " 
                    + "AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Customer].[CompanyName] < LTRIM(RTRIM(@CompanyName))) " 
                    + "AND   (@Address1 IS NULL OR @Address1 = '' OR [Customer].[Address1] < LTRIM(RTRIM(@Address1))) " 
                    + "AND   (@Address2 IS NULL OR @Address2 = '' OR [Customer].[Address2] < LTRIM(RTRIM(@Address2))) " 
                    + "AND   (@City IS NULL OR @City = '' OR [Customer].[City] < LTRIM(RTRIM(@City))) " 
                    + "AND   (@State IS NULL OR @State = '' OR [Customer].[State] < LTRIM(RTRIM(@State))) " 
                    + "AND   (@Zip IS NULL OR @Zip = '' OR [Customer].[Zip] < LTRIM(RTRIM(@Zip))) " 
                    + "AND   (@Country IS NULL OR @Country = '' OR [Customer].[Country] < LTRIM(RTRIM(@Country))) " 
                    + "AND   (@ShipToAddress1 IS NULL OR @ShipToAddress1 = '' OR [Customer].[ShipToAddress1] < LTRIM(RTRIM(@ShipToAddress1))) " 
                    + "AND   (@ShipToAddress2 IS NULL OR @ShipToAddress2 = '' OR [Customer].[ShipToAddress2] < LTRIM(RTRIM(@ShipToAddress2))) " 
                    + "AND   (@ShipToCity IS NULL OR @ShipToCity = '' OR [Customer].[ShipToCity] < LTRIM(RTRIM(@ShipToCity))) " 
                    + "AND   (@ShipToState IS NULL OR @ShipToState = '' OR [Customer].[ShipToState] < LTRIM(RTRIM(@ShipToState))) " 
                    + "AND   (@ShipToZip IS NULL OR @ShipToZip = '' OR [Customer].[ShipToZip] < LTRIM(RTRIM(@ShipToZip))) " 
                    + "AND   (@ShipToCountry IS NULL OR @ShipToCountry = '' OR [Customer].[ShipToCountry] < LTRIM(RTRIM(@ShipToCountry))) " 
                    + "AND   (@ShipToName IS NULL OR @ShipToName = '' OR [Customer].[ShipToName] < LTRIM(RTRIM(@ShipToName))) " 
                    + "AND   (@Phone IS NULL OR @Phone = '' OR [Customer].[Phone] < LTRIM(RTRIM(@Phone))) " 
                    + "AND   (@eMailAddr IS NULL OR @eMailAddr = '' OR [Customer].[eMailAddr] < LTRIM(RTRIM(@eMailAddr))) " 
                    + "AND   (@QbCustomerId IS NULL OR @QbCustomerId = '' OR [Customer].[QbCustomerId] < LTRIM(RTRIM(@QbCustomerId))) " 
                    + "";
            } else if  (sCondition == "Equal or more than...") {
                selectStatement
                    = "SELECT "
                + "     [Customer].[CustomerId] "
                + "    ,[Customer].[FirstName] "
                + "    ,[Customer].[LastName] "
                + "    ,[Customer].[CompanyName] "
                + "    ,[Customer].[Address1] "
                + "    ,[Customer].[Address2] "
                + "    ,[Customer].[City] "
                + "    ,[Customer].[State] "
                + "    ,[Customer].[Zip] "
                + "    ,[Customer].[Country] "
                + "    ,[Customer].[ShipToAddress1] "
                + "    ,[Customer].[ShipToAddress2] "
                + "    ,[Customer].[ShipToCity] "
                + "    ,[Customer].[ShipToState] "
                + "    ,[Customer].[ShipToZip] "
                + "    ,[Customer].[ShipToCountry] "
                + "    ,[Customer].[ShipToName] "
                + "    ,[Customer].[Phone] "
                + "    ,[Customer].[eMailAddr] "
                + "    ,[Customer].[QbCustomerId] "
                + "FROM " 
                + "     [Customer] " 
                    + "WHERE " 
                    + "     (@CustomerId IS NULL OR @CustomerId = '' OR [Customer].[CustomerId] >= LTRIM(RTRIM(@CustomerId))) " 
                    + "AND   (@FirstName IS NULL OR @FirstName = '' OR [Customer].[FirstName] >= LTRIM(RTRIM(@FirstName))) " 
                    + "AND   (@LastName IS NULL OR @LastName = '' OR [Customer].[LastName] >= LTRIM(RTRIM(@LastName))) " 
                    + "AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Customer].[CompanyName] >= LTRIM(RTRIM(@CompanyName))) " 
                    + "AND   (@Address1 IS NULL OR @Address1 = '' OR [Customer].[Address1] >= LTRIM(RTRIM(@Address1))) " 
                    + "AND   (@Address2 IS NULL OR @Address2 = '' OR [Customer].[Address2] >= LTRIM(RTRIM(@Address2))) " 
                    + "AND   (@City IS NULL OR @City = '' OR [Customer].[City] >= LTRIM(RTRIM(@City))) " 
                    + "AND   (@State IS NULL OR @State = '' OR [Customer].[State] >= LTRIM(RTRIM(@State))) " 
                    + "AND   (@Zip IS NULL OR @Zip = '' OR [Customer].[Zip] >= LTRIM(RTRIM(@Zip))) " 
                    + "AND   (@Country IS NULL OR @Country = '' OR [Customer].[Country] >= LTRIM(RTRIM(@Country))) " 
                    + "AND   (@ShipToAddress1 IS NULL OR @ShipToAddress1 = '' OR [Customer].[ShipToAddress1] >= LTRIM(RTRIM(@ShipToAddress1))) " 
                    + "AND   (@ShipToAddress2 IS NULL OR @ShipToAddress2 = '' OR [Customer].[ShipToAddress2] >= LTRIM(RTRIM(@ShipToAddress2))) " 
                    + "AND   (@ShipToCity IS NULL OR @ShipToCity = '' OR [Customer].[ShipToCity] >= LTRIM(RTRIM(@ShipToCity))) " 
                    + "AND   (@ShipToState IS NULL OR @ShipToState = '' OR [Customer].[ShipToState] >= LTRIM(RTRIM(@ShipToState))) " 
                    + "AND   (@ShipToZip IS NULL OR @ShipToZip = '' OR [Customer].[ShipToZip] >= LTRIM(RTRIM(@ShipToZip))) " 
                    + "AND   (@ShipToCountry IS NULL OR @ShipToCountry = '' OR [Customer].[ShipToCountry] >= LTRIM(RTRIM(@ShipToCountry))) " 
                    + "AND   (@ShipToName IS NULL OR @ShipToName = '' OR [Customer].[ShipToName] >= LTRIM(RTRIM(@ShipToName))) " 
                    + "AND   (@Phone IS NULL OR @Phone = '' OR [Customer].[Phone] >= LTRIM(RTRIM(@Phone))) " 
                    + "AND   (@eMailAddr IS NULL OR @eMailAddr = '' OR [Customer].[eMailAddr] >= LTRIM(RTRIM(@eMailAddr))) " 
                    + "AND   (@QbCustomerId IS NULL OR @QbCustomerId = '' OR [Customer].[QbCustomerId] >= LTRIM(RTRIM(@QbCustomerId))) " 
                    + "";
            } else if (sCondition == "Equal or less than...") {
                selectStatement 
                    = "SELECT "
                + "     [Customer].[CustomerId] "
                + "    ,[Customer].[FirstName] "
                + "    ,[Customer].[LastName] "
                + "    ,[Customer].[CompanyName] "
                + "    ,[Customer].[Address1] "
                + "    ,[Customer].[Address2] "
                + "    ,[Customer].[City] "
                + "    ,[Customer].[State] "
                + "    ,[Customer].[Zip] "
                + "    ,[Customer].[Country] "
                + "    ,[Customer].[ShipToAddress1] "
                + "    ,[Customer].[ShipToAddress2] "
                + "    ,[Customer].[ShipToCity] "
                + "    ,[Customer].[ShipToState] "
                + "    ,[Customer].[ShipToZip] "
                + "    ,[Customer].[ShipToCountry] "
                + "    ,[Customer].[ShipToName] "
                + "    ,[Customer].[Phone] "
                + "    ,[Customer].[eMailAddr] "
                + "    ,[Customer].[QbCustomerId] "
                + "FROM " 
                + "     [Customer] " 
                    + "WHERE " 
                    + "     (@CustomerId IS NULL OR @CustomerId = '' OR [Customer].[CustomerId] <= LTRIM(RTRIM(@CustomerId))) " 
                    + "AND   (@FirstName IS NULL OR @FirstName = '' OR [Customer].[FirstName] <= LTRIM(RTRIM(@FirstName))) " 
                    + "AND   (@LastName IS NULL OR @LastName = '' OR [Customer].[LastName] <= LTRIM(RTRIM(@LastName))) " 
                    + "AND   (@CompanyName IS NULL OR @CompanyName = '' OR [Customer].[CompanyName] <= LTRIM(RTRIM(@CompanyName))) " 
                    + "AND   (@Address1 IS NULL OR @Address1 = '' OR [Customer].[Address1] <= LTRIM(RTRIM(@Address1))) " 
                    + "AND   (@Address2 IS NULL OR @Address2 = '' OR [Customer].[Address2] <= LTRIM(RTRIM(@Address2))) " 
                    + "AND   (@City IS NULL OR @City = '' OR [Customer].[City] <= LTRIM(RTRIM(@City))) " 
                    + "AND   (@State IS NULL OR @State = '' OR [Customer].[State] <= LTRIM(RTRIM(@State))) " 
                    + "AND   (@Zip IS NULL OR @Zip = '' OR [Customer].[Zip] <= LTRIM(RTRIM(@Zip))) " 
                    + "AND   (@Country IS NULL OR @Country = '' OR [Customer].[Country] <= LTRIM(RTRIM(@Country))) " 
                    + "AND   (@ShipToAddress1 IS NULL OR @ShipToAddress1 = '' OR [Customer].[ShipToAddress1] <= LTRIM(RTRIM(@ShipToAddress1))) " 
                    + "AND   (@ShipToAddress2 IS NULL OR @ShipToAddress2 = '' OR [Customer].[ShipToAddress2] <= LTRIM(RTRIM(@ShipToAddress2))) " 
                    + "AND   (@ShipToCity IS NULL OR @ShipToCity = '' OR [Customer].[ShipToCity] <= LTRIM(RTRIM(@ShipToCity))) " 
                    + "AND   (@ShipToState IS NULL OR @ShipToState = '' OR [Customer].[ShipToState] <= LTRIM(RTRIM(@ShipToState))) " 
                    + "AND   (@ShipToZip IS NULL OR @ShipToZip = '' OR [Customer].[ShipToZip] <= LTRIM(RTRIM(@ShipToZip))) " 
                    + "AND   (@ShipToCountry IS NULL OR @ShipToCountry = '' OR [Customer].[ShipToCountry] <= LTRIM(RTRIM(@ShipToCountry))) " 
                    + "AND   (@ShipToName IS NULL OR @ShipToName = '' OR [Customer].[ShipToName] <= LTRIM(RTRIM(@ShipToName))) " 
                    + "AND   (@Phone IS NULL OR @Phone = '' OR [Customer].[Phone] <= LTRIM(RTRIM(@Phone))) " 
                    + "AND   (@eMailAddr IS NULL OR @eMailAddr = '' OR [Customer].[eMailAddr] <= LTRIM(RTRIM(@eMailAddr))) " 
                    + "AND   (@QbCustomerId IS NULL OR @QbCustomerId = '' OR [Customer].[QbCustomerId] <= LTRIM(RTRIM(@QbCustomerId))) " 
                    + "";
            }
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            if (sField == "Customer Id") {
                selectCommand.Parameters.AddWithValue("@CustomerId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@CustomerId", DBNull.Value); }
            if (sField == "First Name") {
                selectCommand.Parameters.AddWithValue("@FirstName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@FirstName", DBNull.Value); }
            if (sField == "Last Name") {
                selectCommand.Parameters.AddWithValue("@LastName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@LastName", DBNull.Value); }
            if (sField == "Company Name") {
                selectCommand.Parameters.AddWithValue("@CompanyName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@CompanyName", DBNull.Value); }
            if (sField == "Address1") {
                selectCommand.Parameters.AddWithValue("@Address1", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Address1", DBNull.Value); }
            if (sField == "Address2") {
                selectCommand.Parameters.AddWithValue("@Address2", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Address2", DBNull.Value); }
            if (sField == "City") {
                selectCommand.Parameters.AddWithValue("@City", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@City", DBNull.Value); }
            if (sField == "State") {
                selectCommand.Parameters.AddWithValue("@State", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@State", DBNull.Value); }
            if (sField == "Zip") {
                selectCommand.Parameters.AddWithValue("@Zip", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Zip", DBNull.Value); }
            if (sField == "Country") {
                selectCommand.Parameters.AddWithValue("@Country", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Country", DBNull.Value); }
            if (sField == "Ship To Address1") {
                selectCommand.Parameters.AddWithValue("@ShipToAddress1", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ShipToAddress1", DBNull.Value); }
            if (sField == "Ship To Address2") {
                selectCommand.Parameters.AddWithValue("@ShipToAddress2", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ShipToAddress2", DBNull.Value); }
            if (sField == "Ship To City") {
                selectCommand.Parameters.AddWithValue("@ShipToCity", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ShipToCity", DBNull.Value); }
            if (sField == "Ship To State") {
                selectCommand.Parameters.AddWithValue("@ShipToState", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ShipToState", DBNull.Value); }
            if (sField == "Ship To Zip") {
                selectCommand.Parameters.AddWithValue("@ShipToZip", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ShipToZip", DBNull.Value); }
            if (sField == "Ship To Country") {
                selectCommand.Parameters.AddWithValue("@ShipToCountry", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ShipToCountry", DBNull.Value); }
            if (sField == "Ship To Name") {
                selectCommand.Parameters.AddWithValue("@ShipToName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ShipToName", DBNull.Value); }
            if (sField == "Phone") {
                selectCommand.Parameters.AddWithValue("@Phone", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Phone", DBNull.Value); }
            if (sField == "E Mail Addr") {
                selectCommand.Parameters.AddWithValue("@eMailAddr", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@eMailAddr", DBNull.Value); }
            if (sField == "Qb Customer Id") {
                selectCommand.Parameters.AddWithValue("@QbCustomerId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QbCustomerId", DBNull.Value); }
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows) {
                    dt.Load(reader); }
                reader.Close();
            }
            catch (SqlException)
            {
                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static Customer Select_Record(Customer CustomerPara)
        {
        Customer Customer = new Customer();
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement
            = "SELECT " 
                + "     [CustomerId] "
                + "    ,[FirstName] "
                + "    ,[LastName] "
                + "    ,[CompanyName] "
                + "    ,[Address1] "
                + "    ,[Address2] "
                + "    ,[City] "
                + "    ,[State] "
                + "    ,[Zip] "
                + "    ,[Country] "
                + "    ,[ShipToAddress1] "
                + "    ,[ShipToAddress2] "
                + "    ,[ShipToCity] "
                + "    ,[ShipToState] "
                + "    ,[ShipToZip] "
                + "    ,[ShipToCountry] "
                + "    ,[ShipToName] "
                + "    ,[Phone] "
                + "    ,[eMailAddr] "
                + "    ,[QbCustomerId] "
                + "FROM "
                + "     [Customer] "
                + "WHERE "
                + "     [CustomerId] = @CustomerId "
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            selectCommand.Parameters.AddWithValue("@CustomerId", CustomerPara.CustomerId);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Customer.CustomerId = System.Convert.ToInt32(reader["CustomerId"]);
                    Customer.FirstName = System.Convert.ToString(reader["FirstName"]);
                    Customer.LastName = System.Convert.ToString(reader["LastName"]);
                    Customer.CompanyName = reader["CompanyName"] is DBNull ? null : reader["CompanyName"].ToString();
                    Customer.Address1 = reader["Address1"] is DBNull ? null : reader["Address1"].ToString();
                    Customer.Address2 = reader["Address2"] is DBNull ? null : reader["Address2"].ToString();
                    Customer.City = reader["City"] is DBNull ? null : reader["City"].ToString();
                    Customer.State = reader["State"] is DBNull ? null : reader["State"].ToString();
                    Customer.Zip = reader["Zip"] is DBNull ? null : reader["Zip"].ToString();
                    Customer.Country = reader["Country"] is DBNull ? null : reader["Country"].ToString();
                    Customer.ShipToAddress1 = reader["ShipToAddress1"] is DBNull ? null : reader["ShipToAddress1"].ToString();
                    Customer.ShipToAddress2 = reader["ShipToAddress2"] is DBNull ? null : reader["ShipToAddress2"].ToString();
                    Customer.ShipToCity = reader["ShipToCity"] is DBNull ? null : reader["ShipToCity"].ToString();
                    Customer.ShipToState = reader["ShipToState"] is DBNull ? null : reader["ShipToState"].ToString();
                    Customer.ShipToZip = reader["ShipToZip"] is DBNull ? null : reader["ShipToZip"].ToString();
                    Customer.ShipToCountry = reader["ShipToCountry"] is DBNull ? null : reader["ShipToCountry"].ToString();
                    Customer.ShipToName = reader["ShipToName"] is DBNull ? null : reader["ShipToName"].ToString();
                    Customer.Phone = System.Convert.ToString(reader["Phone"]);
                    Customer.eMailAddr = System.Convert.ToString(reader["eMailAddr"]);
                    Customer.QbCustomerId = reader["QbCustomerId"] is DBNull ? null : reader["QbCustomerId"].ToString();
                }
                else
                {
                    Customer = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return Customer;
            }
            finally
            {
                connection.Close();
            }
            return Customer;
        }

        public static bool Add(Customer Customer)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string insertStatement
                = "INSERT " 
                + "     [Customer] "
                + "     ( "
                + "     [FirstName] "
                + "    ,[LastName] "
                + "    ,[CompanyName] "
                + "    ,[Address1] "
                + "    ,[Address2] "
                + "    ,[City] "
                + "    ,[State] "
                + "    ,[Zip] "
                + "    ,[Country] "
                + "    ,[ShipToAddress1] "
                + "    ,[ShipToAddress2] "
                + "    ,[ShipToCity] "
                + "    ,[ShipToState] "
                + "    ,[ShipToZip] "
                + "    ,[ShipToCountry] "
                + "    ,[ShipToName] "
                + "    ,[Phone] "
                + "    ,[eMailAddr] "
                + "    ,[QbCustomerId] "
                + "     ) "
                + "VALUES " 
                + "     ( "
                + "     @FirstName "
                + "    ,@LastName "
                + "    ,@CompanyName "
                + "    ,@Address1 "
                + "    ,@Address2 "
                + "    ,@City "
                + "    ,@State "
                + "    ,@Zip "
                + "    ,@Country "
                + "    ,@ShipToAddress1 "
                + "    ,@ShipToAddress2 "
                + "    ,@ShipToCity "
                + "    ,@ShipToState "
                + "    ,@ShipToZip "
                + "    ,@ShipToCountry "
                + "    ,@ShipToName "
                + "    ,@Phone "
                + "    ,@eMailAddr "
                + "    ,@QbCustomerId "
                + "     ) "
                + "";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.CommandType = CommandType.Text;
                insertCommand.Parameters.AddWithValue("@FirstName", Customer.FirstName);
                insertCommand.Parameters.AddWithValue("@LastName", Customer.LastName);
            if (Customer.CompanyName != null) {
                insertCommand.Parameters.AddWithValue("@CompanyName", Customer.CompanyName);
            } else {
                insertCommand.Parameters.AddWithValue("@CompanyName", DBNull.Value); }
            if (Customer.Address1 != null) {
                insertCommand.Parameters.AddWithValue("@Address1", Customer.Address1);
            } else {
                insertCommand.Parameters.AddWithValue("@Address1", DBNull.Value); }
            if (Customer.Address2 != null) {
                insertCommand.Parameters.AddWithValue("@Address2", Customer.Address2);
            } else {
                insertCommand.Parameters.AddWithValue("@Address2", DBNull.Value); }
            if (Customer.City != null) {
                insertCommand.Parameters.AddWithValue("@City", Customer.City);
            } else {
                insertCommand.Parameters.AddWithValue("@City", DBNull.Value); }
            if (Customer.State != null) {
                insertCommand.Parameters.AddWithValue("@State", Customer.State);
            } else {
                insertCommand.Parameters.AddWithValue("@State", DBNull.Value); }
            if (Customer.Zip != null) {
                insertCommand.Parameters.AddWithValue("@Zip", Customer.Zip);
            } else {
                insertCommand.Parameters.AddWithValue("@Zip", DBNull.Value); }
            if (Customer.Country != null) {
                insertCommand.Parameters.AddWithValue("@Country", Customer.Country);
            } else {
                insertCommand.Parameters.AddWithValue("@Country", DBNull.Value); }
            if (Customer.ShipToAddress1 != null) {
                insertCommand.Parameters.AddWithValue("@ShipToAddress1", Customer.ShipToAddress1);
            } else {
                insertCommand.Parameters.AddWithValue("@ShipToAddress1", DBNull.Value); }
            if (Customer.ShipToAddress2 != null) {
                insertCommand.Parameters.AddWithValue("@ShipToAddress2", Customer.ShipToAddress2);
            } else {
                insertCommand.Parameters.AddWithValue("@ShipToAddress2", DBNull.Value); }
            if (Customer.ShipToCity != null) {
                insertCommand.Parameters.AddWithValue("@ShipToCity", Customer.ShipToCity);
            } else {
                insertCommand.Parameters.AddWithValue("@ShipToCity", DBNull.Value); }
            if (Customer.ShipToState != null) {
                insertCommand.Parameters.AddWithValue("@ShipToState", Customer.ShipToState);
            } else {
                insertCommand.Parameters.AddWithValue("@ShipToState", DBNull.Value); }
            if (Customer.ShipToZip != null) {
                insertCommand.Parameters.AddWithValue("@ShipToZip", Customer.ShipToZip);
            } else {
                insertCommand.Parameters.AddWithValue("@ShipToZip", DBNull.Value); }
            if (Customer.ShipToCountry != null) {
                insertCommand.Parameters.AddWithValue("@ShipToCountry", Customer.ShipToCountry);
            } else {
                insertCommand.Parameters.AddWithValue("@ShipToCountry", DBNull.Value); }
            if (Customer.ShipToName != null) {
                insertCommand.Parameters.AddWithValue("@ShipToName", Customer.ShipToName);
            } else {
                insertCommand.Parameters.AddWithValue("@ShipToName", DBNull.Value); }
                insertCommand.Parameters.AddWithValue("@Phone", Customer.Phone);
                insertCommand.Parameters.AddWithValue("@eMailAddr", Customer.eMailAddr);
            if (Customer.QbCustomerId != null) {
                insertCommand.Parameters.AddWithValue("@QbCustomerId", Customer.QbCustomerId);
            } else {
                insertCommand.Parameters.AddWithValue("@QbCustomerId", DBNull.Value); }
            try
            {
                connection.Open();
                int count = insertCommand.ExecuteNonQuery();
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool Update(Customer oldCustomer, 
               Customer newCustomer)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string updateStatement
                = "UPDATE "  
                + "     [Customer] "
                + "SET "
                + "     [FirstName] = @NewFirstName "
                + "    ,[LastName] = @NewLastName "
                + "    ,[CompanyName] = @NewCompanyName "
                + "    ,[Address1] = @NewAddress1 "
                + "    ,[Address2] = @NewAddress2 "
                + "    ,[City] = @NewCity "
                + "    ,[State] = @NewState "
                + "    ,[Zip] = @NewZip "
                + "    ,[Country] = @NewCountry "
                + "    ,[ShipToAddress1] = @NewShipToAddress1 "
                + "    ,[ShipToAddress2] = @NewShipToAddress2 "
                + "    ,[ShipToCity] = @NewShipToCity "
                + "    ,[ShipToState] = @NewShipToState "
                + "    ,[ShipToZip] = @NewShipToZip "
                + "    ,[ShipToCountry] = @NewShipToCountry "
                + "    ,[ShipToName] = @NewShipToName "
                + "    ,[Phone] = @NewPhone "
                + "    ,[eMailAddr] = @NeweMailAddr "
                + "    ,[QbCustomerId] = @NewQbCustomerId "
                + "WHERE "
                + "     [CustomerId] = @OldCustomerId " 
                + " AND [FirstName] = @OldFirstName " 
                + " AND [LastName] = @OldLastName " 
                + " AND ((@OldCompanyName IS NULL AND [CompanyName] IS NULL) OR [CompanyName] = @OldCompanyName) " 
                + " AND ((@OldAddress1 IS NULL AND [Address1] IS NULL) OR [Address1] = @OldAddress1) " 
                + " AND ((@OldAddress2 IS NULL AND [Address2] IS NULL) OR [Address2] = @OldAddress2) " 
                + " AND ((@OldCity IS NULL AND [City] IS NULL) OR [City] = @OldCity) " 
                + " AND ((@OldState IS NULL AND [State] IS NULL) OR [State] = @OldState) " 
                + " AND ((@OldZip IS NULL AND [Zip] IS NULL) OR [Zip] = @OldZip) " 
                + " AND ((@OldCountry IS NULL AND [Country] IS NULL) OR [Country] = @OldCountry) " 
                + " AND ((@OldShipToAddress1 IS NULL AND [ShipToAddress1] IS NULL) OR [ShipToAddress1] = @OldShipToAddress1) " 
                + " AND ((@OldShipToAddress2 IS NULL AND [ShipToAddress2] IS NULL) OR [ShipToAddress2] = @OldShipToAddress2) " 
                + " AND ((@OldShipToCity IS NULL AND [ShipToCity] IS NULL) OR [ShipToCity] = @OldShipToCity) " 
                + " AND ((@OldShipToState IS NULL AND [ShipToState] IS NULL) OR [ShipToState] = @OldShipToState) " 
                + " AND ((@OldShipToZip IS NULL AND [ShipToZip] IS NULL) OR [ShipToZip] = @OldShipToZip) " 
                + " AND ((@OldShipToCountry IS NULL AND [ShipToCountry] IS NULL) OR [ShipToCountry] = @OldShipToCountry) " 
                + " AND ((@OldShipToName IS NULL AND [ShipToName] IS NULL) OR [ShipToName] = @OldShipToName) " 
                + " AND [Phone] = @OldPhone " 
                + " AND [eMailAddr] = @OldeMailAddr " 
                + " AND ((@OldQbCustomerId IS NULL AND [QbCustomerId] IS NULL) OR [QbCustomerId] = @OldQbCustomerId) " 
                + "";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            updateCommand.CommandType = CommandType.Text;
            updateCommand.Parameters.AddWithValue("@NewFirstName", newCustomer.FirstName);
            updateCommand.Parameters.AddWithValue("@NewLastName", newCustomer.LastName);
            if (newCustomer.CompanyName != null) {
                updateCommand.Parameters.AddWithValue("@NewCompanyName", newCustomer.CompanyName);
            } else {
                updateCommand.Parameters.AddWithValue("@NewCompanyName", DBNull.Value); }
            if (newCustomer.Address1 != null) {
                updateCommand.Parameters.AddWithValue("@NewAddress1", newCustomer.Address1);
            } else {
                updateCommand.Parameters.AddWithValue("@NewAddress1", DBNull.Value); }
            if (newCustomer.Address2 != null) {
                updateCommand.Parameters.AddWithValue("@NewAddress2", newCustomer.Address2);
            } else {
                updateCommand.Parameters.AddWithValue("@NewAddress2", DBNull.Value); }
            if (newCustomer.City != null) {
                updateCommand.Parameters.AddWithValue("@NewCity", newCustomer.City);
            } else {
                updateCommand.Parameters.AddWithValue("@NewCity", DBNull.Value); }
            if (newCustomer.State != null) {
                updateCommand.Parameters.AddWithValue("@NewState", newCustomer.State);
            } else {
                updateCommand.Parameters.AddWithValue("@NewState", DBNull.Value); }
            if (newCustomer.Zip != null) {
                updateCommand.Parameters.AddWithValue("@NewZip", newCustomer.Zip);
            } else {
                updateCommand.Parameters.AddWithValue("@NewZip", DBNull.Value); }
            if (newCustomer.Country != null) {
                updateCommand.Parameters.AddWithValue("@NewCountry", newCustomer.Country);
            } else {
                updateCommand.Parameters.AddWithValue("@NewCountry", DBNull.Value); }
            if (newCustomer.ShipToAddress1 != null) {
                updateCommand.Parameters.AddWithValue("@NewShipToAddress1", newCustomer.ShipToAddress1);
            } else {
                updateCommand.Parameters.AddWithValue("@NewShipToAddress1", DBNull.Value); }
            if (newCustomer.ShipToAddress2 != null) {
                updateCommand.Parameters.AddWithValue("@NewShipToAddress2", newCustomer.ShipToAddress2);
            } else {
                updateCommand.Parameters.AddWithValue("@NewShipToAddress2", DBNull.Value); }
            if (newCustomer.ShipToCity != null) {
                updateCommand.Parameters.AddWithValue("@NewShipToCity", newCustomer.ShipToCity);
            } else {
                updateCommand.Parameters.AddWithValue("@NewShipToCity", DBNull.Value); }
            if (newCustomer.ShipToState != null) {
                updateCommand.Parameters.AddWithValue("@NewShipToState", newCustomer.ShipToState);
            } else {
                updateCommand.Parameters.AddWithValue("@NewShipToState", DBNull.Value); }
            if (newCustomer.ShipToZip != null) {
                updateCommand.Parameters.AddWithValue("@NewShipToZip", newCustomer.ShipToZip);
            } else {
                updateCommand.Parameters.AddWithValue("@NewShipToZip", DBNull.Value); }
            if (newCustomer.ShipToCountry != null) {
                updateCommand.Parameters.AddWithValue("@NewShipToCountry", newCustomer.ShipToCountry);
            } else {
                updateCommand.Parameters.AddWithValue("@NewShipToCountry", DBNull.Value); }
            if (newCustomer.ShipToName != null) {
                updateCommand.Parameters.AddWithValue("@NewShipToName", newCustomer.ShipToName);
            } else {
                updateCommand.Parameters.AddWithValue("@NewShipToName", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@NewPhone", newCustomer.Phone);
            updateCommand.Parameters.AddWithValue("@NeweMailAddr", newCustomer.eMailAddr);
            if (newCustomer.QbCustomerId != null) {
                updateCommand.Parameters.AddWithValue("@NewQbCustomerId", newCustomer.QbCustomerId);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQbCustomerId", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldCustomerId", oldCustomer.CustomerId);
            updateCommand.Parameters.AddWithValue("@OldFirstName", oldCustomer.FirstName);
            updateCommand.Parameters.AddWithValue("@OldLastName", oldCustomer.LastName);
            if (oldCustomer.CompanyName != null) {
                updateCommand.Parameters.AddWithValue("@OldCompanyName", oldCustomer.CompanyName);
            } else {
                updateCommand.Parameters.AddWithValue("@OldCompanyName", DBNull.Value); }
            if (oldCustomer.Address1 != null) {
                updateCommand.Parameters.AddWithValue("@OldAddress1", oldCustomer.Address1);
            } else {
                updateCommand.Parameters.AddWithValue("@OldAddress1", DBNull.Value); }
            if (oldCustomer.Address2 != null) {
                updateCommand.Parameters.AddWithValue("@OldAddress2", oldCustomer.Address2);
            } else {
                updateCommand.Parameters.AddWithValue("@OldAddress2", DBNull.Value); }
            if (oldCustomer.City != null) {
                updateCommand.Parameters.AddWithValue("@OldCity", oldCustomer.City);
            } else {
                updateCommand.Parameters.AddWithValue("@OldCity", DBNull.Value); }
            if (oldCustomer.State != null) {
                updateCommand.Parameters.AddWithValue("@OldState", oldCustomer.State);
            } else {
                updateCommand.Parameters.AddWithValue("@OldState", DBNull.Value); }
            if (oldCustomer.Zip != null) {
                updateCommand.Parameters.AddWithValue("@OldZip", oldCustomer.Zip);
            } else {
                updateCommand.Parameters.AddWithValue("@OldZip", DBNull.Value); }
            if (oldCustomer.Country != null) {
                updateCommand.Parameters.AddWithValue("@OldCountry", oldCustomer.Country);
            } else {
                updateCommand.Parameters.AddWithValue("@OldCountry", DBNull.Value); }
            if (oldCustomer.ShipToAddress1 != null) {
                updateCommand.Parameters.AddWithValue("@OldShipToAddress1", oldCustomer.ShipToAddress1);
            } else {
                updateCommand.Parameters.AddWithValue("@OldShipToAddress1", DBNull.Value); }
            if (oldCustomer.ShipToAddress2 != null) {
                updateCommand.Parameters.AddWithValue("@OldShipToAddress2", oldCustomer.ShipToAddress2);
            } else {
                updateCommand.Parameters.AddWithValue("@OldShipToAddress2", DBNull.Value); }
            if (oldCustomer.ShipToCity != null) {
                updateCommand.Parameters.AddWithValue("@OldShipToCity", oldCustomer.ShipToCity);
            } else {
                updateCommand.Parameters.AddWithValue("@OldShipToCity", DBNull.Value); }
            if (oldCustomer.ShipToState != null) {
                updateCommand.Parameters.AddWithValue("@OldShipToState", oldCustomer.ShipToState);
            } else {
                updateCommand.Parameters.AddWithValue("@OldShipToState", DBNull.Value); }
            if (oldCustomer.ShipToZip != null) {
                updateCommand.Parameters.AddWithValue("@OldShipToZip", oldCustomer.ShipToZip);
            } else {
                updateCommand.Parameters.AddWithValue("@OldShipToZip", DBNull.Value); }
            if (oldCustomer.ShipToCountry != null) {
                updateCommand.Parameters.AddWithValue("@OldShipToCountry", oldCustomer.ShipToCountry);
            } else {
                updateCommand.Parameters.AddWithValue("@OldShipToCountry", DBNull.Value); }
            if (oldCustomer.ShipToName != null) {
                updateCommand.Parameters.AddWithValue("@OldShipToName", oldCustomer.ShipToName);
            } else {
                updateCommand.Parameters.AddWithValue("@OldShipToName", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldPhone", oldCustomer.Phone);
            updateCommand.Parameters.AddWithValue("@OldeMailAddr", oldCustomer.eMailAddr);
            if (oldCustomer.QbCustomerId != null) {
                updateCommand.Parameters.AddWithValue("@OldQbCustomerId", oldCustomer.QbCustomerId);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQbCustomerId", DBNull.Value); }
            try
            {
                connection.Open();
                int count = updateCommand.ExecuteNonQuery();
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool Delete(Customer Customer)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string deleteStatement
                = "DELETE FROM "  
                + "     [Customer] "
                + "WHERE " 
                + "     [CustomerId] = @OldCustomerId " 
                + " AND [FirstName] = @OldFirstName " 
                + " AND [LastName] = @OldLastName " 
                + " AND ((@OldCompanyName IS NULL AND [CompanyName] IS NULL) OR [CompanyName] = @OldCompanyName) " 
                + " AND ((@OldAddress1 IS NULL AND [Address1] IS NULL) OR [Address1] = @OldAddress1) " 
                + " AND ((@OldAddress2 IS NULL AND [Address2] IS NULL) OR [Address2] = @OldAddress2) " 
                + " AND ((@OldCity IS NULL AND [City] IS NULL) OR [City] = @OldCity) " 
                + " AND ((@OldState IS NULL AND [State] IS NULL) OR [State] = @OldState) " 
                + " AND ((@OldZip IS NULL AND [Zip] IS NULL) OR [Zip] = @OldZip) " 
                + " AND ((@OldCountry IS NULL AND [Country] IS NULL) OR [Country] = @OldCountry) " 
                + " AND ((@OldShipToAddress1 IS NULL AND [ShipToAddress1] IS NULL) OR [ShipToAddress1] = @OldShipToAddress1) " 
                + " AND ((@OldShipToAddress2 IS NULL AND [ShipToAddress2] IS NULL) OR [ShipToAddress2] = @OldShipToAddress2) " 
                + " AND ((@OldShipToCity IS NULL AND [ShipToCity] IS NULL) OR [ShipToCity] = @OldShipToCity) " 
                + " AND ((@OldShipToState IS NULL AND [ShipToState] IS NULL) OR [ShipToState] = @OldShipToState) " 
                + " AND ((@OldShipToZip IS NULL AND [ShipToZip] IS NULL) OR [ShipToZip] = @OldShipToZip) " 
                + " AND ((@OldShipToCountry IS NULL AND [ShipToCountry] IS NULL) OR [ShipToCountry] = @OldShipToCountry) " 
                + " AND ((@OldShipToName IS NULL AND [ShipToName] IS NULL) OR [ShipToName] = @OldShipToName) " 
                + " AND [Phone] = @OldPhone " 
                + " AND [eMailAddr] = @OldeMailAddr " 
                + " AND ((@OldQbCustomerId IS NULL AND [QbCustomerId] IS NULL) OR [QbCustomerId] = @OldQbCustomerId) " 
                + "";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.CommandType = CommandType.Text;
            deleteCommand.Parameters.AddWithValue("@OldCustomerId", Customer.CustomerId);
            deleteCommand.Parameters.AddWithValue("@OldFirstName", Customer.FirstName);
            deleteCommand.Parameters.AddWithValue("@OldLastName", Customer.LastName);
            if (Customer.CompanyName != null) {
                deleteCommand.Parameters.AddWithValue("@OldCompanyName", Customer.CompanyName);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldCompanyName", DBNull.Value); }
            if (Customer.Address1 != null) {
                deleteCommand.Parameters.AddWithValue("@OldAddress1", Customer.Address1);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldAddress1", DBNull.Value); }
            if (Customer.Address2 != null) {
                deleteCommand.Parameters.AddWithValue("@OldAddress2", Customer.Address2);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldAddress2", DBNull.Value); }
            if (Customer.City != null) {
                deleteCommand.Parameters.AddWithValue("@OldCity", Customer.City);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldCity", DBNull.Value); }
            if (Customer.State != null) {
                deleteCommand.Parameters.AddWithValue("@OldState", Customer.State);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldState", DBNull.Value); }
            if (Customer.Zip != null) {
                deleteCommand.Parameters.AddWithValue("@OldZip", Customer.Zip);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldZip", DBNull.Value); }
            if (Customer.Country != null) {
                deleteCommand.Parameters.AddWithValue("@OldCountry", Customer.Country);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldCountry", DBNull.Value); }
            if (Customer.ShipToAddress1 != null) {
                deleteCommand.Parameters.AddWithValue("@OldShipToAddress1", Customer.ShipToAddress1);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldShipToAddress1", DBNull.Value); }
            if (Customer.ShipToAddress2 != null) {
                deleteCommand.Parameters.AddWithValue("@OldShipToAddress2", Customer.ShipToAddress2);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldShipToAddress2", DBNull.Value); }
            if (Customer.ShipToCity != null) {
                deleteCommand.Parameters.AddWithValue("@OldShipToCity", Customer.ShipToCity);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldShipToCity", DBNull.Value); }
            if (Customer.ShipToState != null) {
                deleteCommand.Parameters.AddWithValue("@OldShipToState", Customer.ShipToState);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldShipToState", DBNull.Value); }
            if (Customer.ShipToZip != null) {
                deleteCommand.Parameters.AddWithValue("@OldShipToZip", Customer.ShipToZip);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldShipToZip", DBNull.Value); }
            if (Customer.ShipToCountry != null) {
                deleteCommand.Parameters.AddWithValue("@OldShipToCountry", Customer.ShipToCountry);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldShipToCountry", DBNull.Value); }
            if (Customer.ShipToName != null) {
                deleteCommand.Parameters.AddWithValue("@OldShipToName", Customer.ShipToName);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldShipToName", DBNull.Value); }
            deleteCommand.Parameters.AddWithValue("@OldPhone", Customer.Phone);
            deleteCommand.Parameters.AddWithValue("@OldeMailAddr", Customer.eMailAddr);
            if (Customer.QbCustomerId != null) {
                deleteCommand.Parameters.AddWithValue("@OldQbCustomerId", Customer.QbCustomerId);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQbCustomerId", DBNull.Value); }
            try
            {
                connection.Open();
                int count = deleteCommand.ExecuteNonQuery();
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
 
