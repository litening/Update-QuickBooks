using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using QB2.Models;

namespace QB2.Data
{

    public class OrderDetailOptions_ItemOptionData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement 
                = "SELECT "  
                + "     [ItemOptionId] " 
                + "FROM " 
                + "     [ItemOption] " 
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

        public static List<ItemOption> List()
        {
            List<ItemOption> ItemOptionList = new List<ItemOption>();
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement 
                = "SELECT "  
                + "     [ItemOptionId] " 
                + "FROM " 
                + "     [ItemOption] " 
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                ItemOption ItemOption = new ItemOption();
                while (reader.Read())
                {
                    ItemOption = new ItemOption();
                    ItemOption.ItemOptionId = System.Convert.ToInt32(reader["ItemOptionId"]);
                    ItemOptionList.Add(ItemOption);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return ItemOptionList;
            }
            finally
            {
                connection.Close();
            }
            return ItemOptionList;
        }

    }

}

 
