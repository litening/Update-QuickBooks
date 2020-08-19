using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using QB2.Models;

namespace QB2.Data
{

    public class OrderHeader_OurCustomerData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement 
                = "SELECT "  
                + "     [UserId] " 
                + "FROM " 
                + "     [OurCustomer] " 
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
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

        public static List<OurCustomer> List()
        {
            List<OurCustomer> OurCustomerList = new List<OurCustomer>();
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement 
                = "SELECT "  
                + "     [UserId] " 
                + "FROM " 
                + "     [OurCustomer] " 
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                OurCustomer OurCustomer = new OurCustomer();
                while (reader.Read())
                {
                    OurCustomer = new OurCustomer();
                    OurCustomer.UserId = System.Convert.ToInt32(reader["UserId"]);
                    OurCustomerList.Add(OurCustomer);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return OurCustomerList;
            }
            finally
            {
                connection.Close();
            }
            return OurCustomerList;
        }

    }

    public class OrderHeader_CustomerData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement 
                = "SELECT "  
                + "     [CustomerId] " 
                + "FROM " 
                + "     [Customer] " 
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
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

        public static List<Customer> List()
        {
            List<Customer> CustomerList = new List<Customer>();
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement 
                = "SELECT "  
                + "     [CustomerId] " 
                + "FROM " 
                + "     [Customer] " 
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                Customer Customer = new Customer();
                while (reader.Read())
                {
                    Customer = new Customer();
                    Customer.CustomerId = System.Convert.ToInt32(reader["CustomerId"]);
                    CustomerList.Add(Customer);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return CustomerList;
            }
            finally
            {
                connection.Close();
            }
            return CustomerList;
        }

    }

}

 
