using System;
using System.Data;
using System.Data.SqlClient;
using QB2.Models;

namespace QB2.Data
{
    public class OrderDetailOptionsData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement
                = "SELECT "  
                + "     [OrderDetailOptions].[OrderOptionId] "
                + "    ,[OrderDetailOptions].[OrderDetailId] "
                + "    ,[OrderDetailOptions].[ItemOptionId] "
                + "    ,[OrderDetailOptions].[Price] "
                + "FROM " 
                + "     [OrderDetailOptions] " 
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
                + "     [OrderDetailOptions].[OrderOptionId] "
                + "    ,[OrderDetailOptions].[OrderDetailId] "
                + "    ,[OrderDetailOptions].[ItemOptionId] "
                + "    ,[OrderDetailOptions].[Price] "
                + "FROM " 
                + "     [OrderDetailOptions] " 
                + "INNER JOIN [ItemOption] ON [OrderDetailOptions].[ItemOptionId] = [ItemOption].[ItemOptionId] "
                    + "WHERE " 
                    + "     (@OrderOptionId IS NULL OR @OrderOptionId = '' OR [OrderDetailOptions].[OrderOptionId] LIKE '%' + LTRIM(RTRIM(@OrderOptionId)) + '%') " 
                    + "AND   (@OrderDetailId IS NULL OR @OrderDetailId = '' OR [OrderDetailOptions].[OrderDetailId] LIKE '%' + LTRIM(RTRIM(@OrderDetailId)) + '%') " 
                    + "AND   (@ItemOptionId IS NULL OR @ItemOptionId = '' OR [ItemOption].[ItemOptionId] LIKE '%' + LTRIM(RTRIM(@ItemOptionId)) + '%') " 
                    + "AND   (@Price IS NULL OR @Price = '' OR [OrderDetailOptions].[Price] LIKE '%' + LTRIM(RTRIM(@Price)) + '%') " 
                    + "";
            } else if (sCondition == "Equals") {
                selectStatement
                    = "SELECT "
                + "     [OrderDetailOptions].[OrderOptionId] "
                + "    ,[OrderDetailOptions].[OrderDetailId] "
                + "    ,[OrderDetailOptions].[ItemOptionId] "
                + "    ,[OrderDetailOptions].[Price] "
                + "FROM " 
                + "     [OrderDetailOptions] " 
                + "INNER JOIN [ItemOption] ON [OrderDetailOptions].[ItemOptionId] = [ItemOption].[ItemOptionId] "
                    + "WHERE " 
                    + "     (@OrderOptionId IS NULL OR @OrderOptionId = '' OR [OrderDetailOptions].[OrderOptionId] = LTRIM(RTRIM(@OrderOptionId))) " 
                    + "AND   (@OrderDetailId IS NULL OR @OrderDetailId = '' OR [OrderDetailOptions].[OrderDetailId] = LTRIM(RTRIM(@OrderDetailId))) " 
                    + "AND   (@ItemOptionId IS NULL OR @ItemOptionId = '' OR [ItemOption].[ItemOptionId] = LTRIM(RTRIM(@ItemOptionId))) " 
                    + "AND   (@Price IS NULL OR @Price = '' OR [OrderDetailOptions].[Price] = LTRIM(RTRIM(@Price))) " 
                    + "";
            } else if  (sCondition == "Starts with...") {
                selectStatement
                    = "SELECT "
                + "     [OrderDetailOptions].[OrderOptionId] "
                + "    ,[OrderDetailOptions].[OrderDetailId] "
                + "    ,[OrderDetailOptions].[ItemOptionId] "
                + "    ,[OrderDetailOptions].[Price] "
                + "FROM " 
                + "     [OrderDetailOptions] " 
                + "INNER JOIN [ItemOption] ON [OrderDetailOptions].[ItemOptionId] = [ItemOption].[ItemOptionId] "
                    + "WHERE " 
                    + "     (@OrderOptionId IS NULL OR @OrderOptionId = '' OR [OrderDetailOptions].[OrderOptionId] LIKE LTRIM(RTRIM(@OrderOptionId)) + '%') " 
                    + "AND   (@OrderDetailId IS NULL OR @OrderDetailId = '' OR [OrderDetailOptions].[OrderDetailId] LIKE LTRIM(RTRIM(@OrderDetailId)) + '%') " 
                    + "AND   (@ItemOptionId IS NULL OR @ItemOptionId = '' OR [ItemOption].[ItemOptionId] LIKE LTRIM(RTRIM(@ItemOptionId)) + '%') " 
                    + "AND   (@Price IS NULL OR @Price = '' OR [OrderDetailOptions].[Price] LIKE LTRIM(RTRIM(@Price)) + '%') " 
                    + "";
            } else if  (sCondition == "More than...") {
                selectStatement
                    = "SELECT "
                + "     [OrderDetailOptions].[OrderOptionId] "
                + "    ,[OrderDetailOptions].[OrderDetailId] "
                + "    ,[OrderDetailOptions].[ItemOptionId] "
                + "    ,[OrderDetailOptions].[Price] "
                + "FROM " 
                + "     [OrderDetailOptions] " 
                + "INNER JOIN [ItemOption] ON [OrderDetailOptions].[ItemOptionId] = [ItemOption].[ItemOptionId] "
                    + "WHERE " 
                    + "     (@OrderOptionId IS NULL OR @OrderOptionId = '' OR [OrderDetailOptions].[OrderOptionId] > LTRIM(RTRIM(@OrderOptionId))) " 
                    + "AND   (@OrderDetailId IS NULL OR @OrderDetailId = '' OR [OrderDetailOptions].[OrderDetailId] > LTRIM(RTRIM(@OrderDetailId))) " 
                    + "AND   (@ItemOptionId IS NULL OR @ItemOptionId = '' OR [ItemOption].[ItemOptionId] > LTRIM(RTRIM(@ItemOptionId))) " 
                    + "AND   (@Price IS NULL OR @Price = '' OR [OrderDetailOptions].[Price] > LTRIM(RTRIM(@Price))) " 
                    + "";
            } else if  (sCondition == "Less than...") {
                selectStatement
                    = "SELECT " 
                + "     [OrderDetailOptions].[OrderOptionId] "
                + "    ,[OrderDetailOptions].[OrderDetailId] "
                + "    ,[OrderDetailOptions].[ItemOptionId] "
                + "    ,[OrderDetailOptions].[Price] "
                + "FROM " 
                + "     [OrderDetailOptions] " 
                + "INNER JOIN [ItemOption] ON [OrderDetailOptions].[ItemOptionId] = [ItemOption].[ItemOptionId] "
                    + "WHERE " 
                    + "     (@OrderOptionId IS NULL OR @OrderOptionId = '' OR [OrderDetailOptions].[OrderOptionId] < LTRIM(RTRIM(@OrderOptionId))) " 
                    + "AND   (@OrderDetailId IS NULL OR @OrderDetailId = '' OR [OrderDetailOptions].[OrderDetailId] < LTRIM(RTRIM(@OrderDetailId))) " 
                    + "AND   (@ItemOptionId IS NULL OR @ItemOptionId = '' OR [ItemOption].[ItemOptionId] < LTRIM(RTRIM(@ItemOptionId))) " 
                    + "AND   (@Price IS NULL OR @Price = '' OR [OrderDetailOptions].[Price] < LTRIM(RTRIM(@Price))) " 
                    + "";
            } else if  (sCondition == "Equal or more than...") {
                selectStatement
                    = "SELECT "
                + "     [OrderDetailOptions].[OrderOptionId] "
                + "    ,[OrderDetailOptions].[OrderDetailId] "
                + "    ,[OrderDetailOptions].[ItemOptionId] "
                + "    ,[OrderDetailOptions].[Price] "
                + "FROM " 
                + "     [OrderDetailOptions] " 
                + "INNER JOIN [ItemOption] ON [OrderDetailOptions].[ItemOptionId] = [ItemOption].[ItemOptionId] "
                    + "WHERE " 
                    + "     (@OrderOptionId IS NULL OR @OrderOptionId = '' OR [OrderDetailOptions].[OrderOptionId] >= LTRIM(RTRIM(@OrderOptionId))) " 
                    + "AND   (@OrderDetailId IS NULL OR @OrderDetailId = '' OR [OrderDetailOptions].[OrderDetailId] >= LTRIM(RTRIM(@OrderDetailId))) " 
                    + "AND   (@ItemOptionId IS NULL OR @ItemOptionId = '' OR [ItemOption].[ItemOptionId] >= LTRIM(RTRIM(@ItemOptionId))) " 
                    + "AND   (@Price IS NULL OR @Price = '' OR [OrderDetailOptions].[Price] >= LTRIM(RTRIM(@Price))) " 
                    + "";
            } else if (sCondition == "Equal or less than...") {
                selectStatement 
                    = "SELECT "
                + "     [OrderDetailOptions].[OrderOptionId] "
                + "    ,[OrderDetailOptions].[OrderDetailId] "
                + "    ,[OrderDetailOptions].[ItemOptionId] "
                + "    ,[OrderDetailOptions].[Price] "
                + "FROM " 
                + "     [OrderDetailOptions] " 
                + "INNER JOIN [ItemOption] ON [OrderDetailOptions].[ItemOptionId] = [ItemOption].[ItemOptionId] "
                    + "WHERE " 
                    + "     (@OrderOptionId IS NULL OR @OrderOptionId = '' OR [OrderDetailOptions].[OrderOptionId] <= LTRIM(RTRIM(@OrderOptionId))) " 
                    + "AND   (@OrderDetailId IS NULL OR @OrderDetailId = '' OR [OrderDetailOptions].[OrderDetailId] <= LTRIM(RTRIM(@OrderDetailId))) " 
                    + "AND   (@ItemOptionId IS NULL OR @ItemOptionId = '' OR [ItemOption].[ItemOptionId] <= LTRIM(RTRIM(@ItemOptionId))) " 
                    + "AND   (@Price IS NULL OR @Price = '' OR [OrderDetailOptions].[Price] <= LTRIM(RTRIM(@Price))) " 
                    + "";
            }
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            if (sField == "Order Option Id") {
                selectCommand.Parameters.AddWithValue("@OrderOptionId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@OrderOptionId", DBNull.Value); }
            if (sField == "Order Detail Id") {
                selectCommand.Parameters.AddWithValue("@OrderDetailId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@OrderDetailId", DBNull.Value); }
            if (sField == "Item Option Id") {
                selectCommand.Parameters.AddWithValue("@ItemOptionId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ItemOptionId", DBNull.Value); }
            if (sField == "Price") {
                selectCommand.Parameters.AddWithValue("@Price", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Price", DBNull.Value); }
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

        public static OrderDetailOptions Select_Record(OrderDetailOptions OrderDetailOptionsPara)
        {
        OrderDetailOptions OrderDetailOptions = new OrderDetailOptions();
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement
            = "SELECT " 
                + "     [OrderOptionId] "
                + "    ,[OrderDetailId] "
                + "    ,[ItemOptionId] "
                + "    ,[Price] "
                + "FROM "
                + "     [OrderDetailOptions] "
                + "WHERE "
                + "     [OrderOptionId] = @OrderOptionId "
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            selectCommand.Parameters.AddWithValue("@OrderOptionId", OrderDetailOptionsPara.OrderOptionId);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    OrderDetailOptions.OrderOptionId = System.Convert.ToInt32(reader["OrderOptionId"]);
                    OrderDetailOptions.OrderDetailId = System.Convert.ToInt32(reader["OrderDetailId"]);
                    OrderDetailOptions.ItemOptionId = System.Convert.ToInt32(reader["ItemOptionId"]);
                    OrderDetailOptions.Price = System.Convert.ToDecimal(reader["Price"]);
                }
                else
                {
                    OrderDetailOptions = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return OrderDetailOptions;
            }
            finally
            {
                connection.Close();
            }
            return OrderDetailOptions;
        }

        public static bool Add(OrderDetailOptions OrderDetailOptions)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string insertStatement
                = "INSERT " 
                + "     [OrderDetailOptions] "
                + "     ( "
                + "     [OrderDetailId] "
                + "    ,[ItemOptionId] "
                + "    ,[Price] "
                + "     ) "
                + "VALUES " 
                + "     ( "
                + "     @OrderDetailId "
                + "    ,@ItemOptionId "
                + "    ,@Price "
                + "     ) "
                + "";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.CommandType = CommandType.Text;
                insertCommand.Parameters.AddWithValue("@OrderDetailId", OrderDetailOptions.OrderDetailId);
                insertCommand.Parameters.AddWithValue("@ItemOptionId", OrderDetailOptions.ItemOptionId);
                insertCommand.Parameters.AddWithValue("@Price", OrderDetailOptions.Price);
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

        public static bool Update(OrderDetailOptions oldOrderDetailOptions, 
               OrderDetailOptions newOrderDetailOptions)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string updateStatement
                = "UPDATE "  
                + "     [OrderDetailOptions] "
                + "SET "
                + "     [OrderDetailId] = @NewOrderDetailId "
                + "    ,[ItemOptionId] = @NewItemOptionId "
                + "    ,[Price] = @NewPrice "
                + "WHERE "
                + "     [OrderOptionId] = @OldOrderOptionId " 
                + " AND [OrderDetailId] = @OldOrderDetailId " 
                + " AND [ItemOptionId] = @OldItemOptionId " 
                + " AND [Price] = @OldPrice " 
                + "";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            updateCommand.CommandType = CommandType.Text;
            updateCommand.Parameters.AddWithValue("@NewOrderDetailId", newOrderDetailOptions.OrderDetailId);
            updateCommand.Parameters.AddWithValue("@NewItemOptionId", newOrderDetailOptions.ItemOptionId);
            updateCommand.Parameters.AddWithValue("@NewPrice", newOrderDetailOptions.Price);
            updateCommand.Parameters.AddWithValue("@OldOrderOptionId", oldOrderDetailOptions.OrderOptionId);
            updateCommand.Parameters.AddWithValue("@OldOrderDetailId", oldOrderDetailOptions.OrderDetailId);
            updateCommand.Parameters.AddWithValue("@OldItemOptionId", oldOrderDetailOptions.ItemOptionId);
            updateCommand.Parameters.AddWithValue("@OldPrice", oldOrderDetailOptions.Price);
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

        public static bool Delete(OrderDetailOptions OrderDetailOptions)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string deleteStatement
                = "DELETE FROM "  
                + "     [OrderDetailOptions] "
                + "WHERE " 
                + "     [OrderOptionId] = @OldOrderOptionId " 
                + " AND [OrderDetailId] = @OldOrderDetailId " 
                + " AND [ItemOptionId] = @OldItemOptionId " 
                + " AND [Price] = @OldPrice " 
                + "";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.CommandType = CommandType.Text;
            deleteCommand.Parameters.AddWithValue("@OldOrderOptionId", OrderDetailOptions.OrderOptionId);
            deleteCommand.Parameters.AddWithValue("@OldOrderDetailId", OrderDetailOptions.OrderDetailId);
            deleteCommand.Parameters.AddWithValue("@OldItemOptionId", OrderDetailOptions.ItemOptionId);
            deleteCommand.Parameters.AddWithValue("@OldPrice", OrderDetailOptions.Price);
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
 
