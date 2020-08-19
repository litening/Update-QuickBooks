using System;
using System.Data;
using System.Data.SqlClient;

namespace QB2.Data
{
    public class OnlineSales2Data
    {
        public static string connectionString
                = "Data Source=23.239.203.249,1533;Initial Catalog=OnlineSales2;User ID=DumbleDorf;Password=Rumplestilskin_02;";

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}



 
