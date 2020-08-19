using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using QB2.Models;

namespace QB2.Data
{

    public class OrderDetail_OrderHeaderData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement 
                = "SELECT "  
                + "     [OrderId] " 
                + "FROM " 
                + "     [OrderHeader] " 
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

        public static List<OrderHeader> List()
        {
            List<OrderHeader> OrderHeaderList = new List<OrderHeader>();
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement 
                = "SELECT "  
                + "     [OrderId] " 
                + "FROM " 
                + "     [OrderHeader] " 
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                OrderHeader OrderHeader = new OrderHeader();
                while (reader.Read())
                {
                    OrderHeader = new OrderHeader();
                    OrderHeader.OrderId = System.Convert.ToInt32(reader["OrderId"]);
                    OrderHeaderList.Add(OrderHeader);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return OrderHeaderList;
            }
            finally
            {
                connection.Close();
            }
            return OrderHeaderList;
        }

    }

    public class OrderDetail_CatalogItemData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement 
                = "SELECT "  
                + "     [CatalogItemId] " 
                + "FROM " 
                + "     [CatalogItem] " 
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

        public static List<CatalogItem> List()
        {
            List<CatalogItem> CatalogItemList = new List<CatalogItem>();
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement 
                = "SELECT "  
                + "     [CatalogItemId] " 
                + "FROM " 
                + "     [CatalogItem] " 
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                CatalogItem CatalogItem = new CatalogItem();
                while (reader.Read())
                {
                    CatalogItem = new CatalogItem();
                    CatalogItem.CatalogItemId = System.Convert.ToInt32(reader["CatalogItemId"]);
                    CatalogItemList.Add(CatalogItem);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return CatalogItemList;
            }
            finally
            {
                connection.Close();
            }
            return CatalogItemList;
        }

    }

}

 
