using System;
using System.Data;
using System.Data.SqlClient;
using QB2.Models;

namespace QB2.Data
{
    public class OrderDetailData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement
                = "SELECT "  
                + "     [OrderDetail].[OrderDetailId] "
                + "    ,[OrderDetail].[OrderId] "
                + "    ,[OrderDetail].[Quantity] "
                + "    ,[OrderDetail].[CatalogItemId] "
                + "    ,[OrderDetail].[Price] "
                + "    ,[OrderDetail].[SpecialInstructions] "
                + "    ,[OrderDetail].[DiscountPercent] "
                + "FROM " 
                + "     [OrderDetail] " 
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
                + "     [OrderDetail].[OrderDetailId] "
                + "    ,[OrderDetail].[OrderId] "
                + "    ,[OrderDetail].[Quantity] "
                + "    ,[OrderDetail].[CatalogItemId] "
                + "    ,[OrderDetail].[Price] "
                + "    ,[OrderDetail].[SpecialInstructions] "
                + "    ,[OrderDetail].[DiscountPercent] "
                + "FROM " 
                + "     [OrderDetail] " 
                + "INNER JOIN [OrderHeader] ON [OrderDetail].[OrderId] = [OrderHeader].[OrderId] "
                + "INNER JOIN [CatalogItem] ON [OrderDetail].[CatalogItemId] = [CatalogItem].[CatalogItemId] "
                    + "WHERE " 
                    + "     (@OrderDetailId IS NULL OR @OrderDetailId = '' OR [OrderDetail].[OrderDetailId] LIKE '%' + LTRIM(RTRIM(@OrderDetailId)) + '%') " 
                    + "AND   (@OrderId IS NULL OR @OrderId = '' OR [OrderHeader].[OrderId] LIKE '%' + LTRIM(RTRIM(@OrderId)) + '%') " 
                    + "AND   (@Quantity IS NULL OR @Quantity = '' OR [OrderDetail].[Quantity] LIKE '%' + LTRIM(RTRIM(@Quantity)) + '%') " 
                    + "AND   (@CatalogItemId IS NULL OR @CatalogItemId = '' OR [CatalogItem].[CatalogItemId] LIKE '%' + LTRIM(RTRIM(@CatalogItemId)) + '%') " 
                    + "AND   (@Price IS NULL OR @Price = '' OR [OrderDetail].[Price] LIKE '%' + LTRIM(RTRIM(@Price)) + '%') " 
                    + "AND   (@SpecialInstructions IS NULL OR @SpecialInstructions = '' OR [OrderDetail].[SpecialInstructions] LIKE '%' + LTRIM(RTRIM(@SpecialInstructions)) + '%') " 
                    + "AND   (@DiscountPercent IS NULL OR @DiscountPercent = '' OR [OrderDetail].[DiscountPercent] LIKE '%' + LTRIM(RTRIM(@DiscountPercent)) + '%') " 
                    + "";
            } else if (sCondition == "Equals") {
                selectStatement
                    = "SELECT "
                + "     [OrderDetail].[OrderDetailId] "
                + "    ,[OrderDetail].[OrderId] "
                + "    ,[OrderDetail].[Quantity] "
                + "    ,[OrderDetail].[CatalogItemId] "
                + "    ,[OrderDetail].[Price] "
                + "    ,[OrderDetail].[SpecialInstructions] "
                + "    ,[OrderDetail].[DiscountPercent] "
                + "FROM " 
                + "     [OrderDetail] " 
                + "INNER JOIN [OrderHeader] ON [OrderDetail].[OrderId] = [OrderHeader].[OrderId] "
                + "INNER JOIN [CatalogItem] ON [OrderDetail].[CatalogItemId] = [CatalogItem].[CatalogItemId] "
                    + "WHERE " 
                    + "     (@OrderDetailId IS NULL OR @OrderDetailId = '' OR [OrderDetail].[OrderDetailId] = LTRIM(RTRIM(@OrderDetailId))) " 
                    + "AND   (@OrderId IS NULL OR @OrderId = '' OR [OrderHeader].[OrderId] = LTRIM(RTRIM(@OrderId))) " 
                    + "AND   (@Quantity IS NULL OR @Quantity = '' OR [OrderDetail].[Quantity] = LTRIM(RTRIM(@Quantity))) " 
                    + "AND   (@CatalogItemId IS NULL OR @CatalogItemId = '' OR [CatalogItem].[CatalogItemId] = LTRIM(RTRIM(@CatalogItemId))) " 
                    + "AND   (@Price IS NULL OR @Price = '' OR [OrderDetail].[Price] = LTRIM(RTRIM(@Price))) " 
                    + "AND   (@SpecialInstructions IS NULL OR @SpecialInstructions = '' OR [OrderDetail].[SpecialInstructions] = LTRIM(RTRIM(@SpecialInstructions))) " 
                    + "AND   (@DiscountPercent IS NULL OR @DiscountPercent = '' OR [OrderDetail].[DiscountPercent] = LTRIM(RTRIM(@DiscountPercent))) " 
                    + "";
            } else if  (sCondition == "Starts with...") {
                selectStatement
                    = "SELECT "
                + "     [OrderDetail].[OrderDetailId] "
                + "    ,[OrderDetail].[OrderId] "
                + "    ,[OrderDetail].[Quantity] "
                + "    ,[OrderDetail].[CatalogItemId] "
                + "    ,[OrderDetail].[Price] "
                + "    ,[OrderDetail].[SpecialInstructions] "
                + "    ,[OrderDetail].[DiscountPercent] "
                + "FROM " 
                + "     [OrderDetail] " 
                + "INNER JOIN [OrderHeader] ON [OrderDetail].[OrderId] = [OrderHeader].[OrderId] "
                + "INNER JOIN [CatalogItem] ON [OrderDetail].[CatalogItemId] = [CatalogItem].[CatalogItemId] "
                    + "WHERE " 
                    + "     (@OrderDetailId IS NULL OR @OrderDetailId = '' OR [OrderDetail].[OrderDetailId] LIKE LTRIM(RTRIM(@OrderDetailId)) + '%') " 
                    + "AND   (@OrderId IS NULL OR @OrderId = '' OR [OrderHeader].[OrderId] LIKE LTRIM(RTRIM(@OrderId)) + '%') " 
                    + "AND   (@Quantity IS NULL OR @Quantity = '' OR [OrderDetail].[Quantity] LIKE LTRIM(RTRIM(@Quantity)) + '%') " 
                    + "AND   (@CatalogItemId IS NULL OR @CatalogItemId = '' OR [CatalogItem].[CatalogItemId] LIKE LTRIM(RTRIM(@CatalogItemId)) + '%') " 
                    + "AND   (@Price IS NULL OR @Price = '' OR [OrderDetail].[Price] LIKE LTRIM(RTRIM(@Price)) + '%') " 
                    + "AND   (@SpecialInstructions IS NULL OR @SpecialInstructions = '' OR [OrderDetail].[SpecialInstructions] LIKE LTRIM(RTRIM(@SpecialInstructions)) + '%') " 
                    + "AND   (@DiscountPercent IS NULL OR @DiscountPercent = '' OR [OrderDetail].[DiscountPercent] LIKE LTRIM(RTRIM(@DiscountPercent)) + '%') " 
                    + "";
            } else if  (sCondition == "More than...") {
                selectStatement
                    = "SELECT "
                + "     [OrderDetail].[OrderDetailId] "
                + "    ,[OrderDetail].[OrderId] "
                + "    ,[OrderDetail].[Quantity] "
                + "    ,[OrderDetail].[CatalogItemId] "
                + "    ,[OrderDetail].[Price] "
                + "    ,[OrderDetail].[SpecialInstructions] "
                + "    ,[OrderDetail].[DiscountPercent] "
                + "FROM " 
                + "     [OrderDetail] " 
                + "INNER JOIN [OrderHeader] ON [OrderDetail].[OrderId] = [OrderHeader].[OrderId] "
                + "INNER JOIN [CatalogItem] ON [OrderDetail].[CatalogItemId] = [CatalogItem].[CatalogItemId] "
                    + "WHERE " 
                    + "     (@OrderDetailId IS NULL OR @OrderDetailId = '' OR [OrderDetail].[OrderDetailId] > LTRIM(RTRIM(@OrderDetailId))) " 
                    + "AND   (@OrderId IS NULL OR @OrderId = '' OR [OrderHeader].[OrderId] > LTRIM(RTRIM(@OrderId))) " 
                    + "AND   (@Quantity IS NULL OR @Quantity = '' OR [OrderDetail].[Quantity] > LTRIM(RTRIM(@Quantity))) " 
                    + "AND   (@CatalogItemId IS NULL OR @CatalogItemId = '' OR [CatalogItem].[CatalogItemId] > LTRIM(RTRIM(@CatalogItemId))) " 
                    + "AND   (@Price IS NULL OR @Price = '' OR [OrderDetail].[Price] > LTRIM(RTRIM(@Price))) " 
                    + "AND   (@SpecialInstructions IS NULL OR @SpecialInstructions = '' OR [OrderDetail].[SpecialInstructions] > LTRIM(RTRIM(@SpecialInstructions))) " 
                    + "AND   (@DiscountPercent IS NULL OR @DiscountPercent = '' OR [OrderDetail].[DiscountPercent] > LTRIM(RTRIM(@DiscountPercent))) " 
                    + "";
            } else if  (sCondition == "Less than...") {
                selectStatement
                    = "SELECT " 
                + "     [OrderDetail].[OrderDetailId] "
                + "    ,[OrderDetail].[OrderId] "
                + "    ,[OrderDetail].[Quantity] "
                + "    ,[OrderDetail].[CatalogItemId] "
                + "    ,[OrderDetail].[Price] "
                + "    ,[OrderDetail].[SpecialInstructions] "
                + "    ,[OrderDetail].[DiscountPercent] "
                + "FROM " 
                + "     [OrderDetail] " 
                + "INNER JOIN [OrderHeader] ON [OrderDetail].[OrderId] = [OrderHeader].[OrderId] "
                + "INNER JOIN [CatalogItem] ON [OrderDetail].[CatalogItemId] = [CatalogItem].[CatalogItemId] "
                    + "WHERE " 
                    + "     (@OrderDetailId IS NULL OR @OrderDetailId = '' OR [OrderDetail].[OrderDetailId] < LTRIM(RTRIM(@OrderDetailId))) " 
                    + "AND   (@OrderId IS NULL OR @OrderId = '' OR [OrderHeader].[OrderId] < LTRIM(RTRIM(@OrderId))) " 
                    + "AND   (@Quantity IS NULL OR @Quantity = '' OR [OrderDetail].[Quantity] < LTRIM(RTRIM(@Quantity))) " 
                    + "AND   (@CatalogItemId IS NULL OR @CatalogItemId = '' OR [CatalogItem].[CatalogItemId] < LTRIM(RTRIM(@CatalogItemId))) " 
                    + "AND   (@Price IS NULL OR @Price = '' OR [OrderDetail].[Price] < LTRIM(RTRIM(@Price))) " 
                    + "AND   (@SpecialInstructions IS NULL OR @SpecialInstructions = '' OR [OrderDetail].[SpecialInstructions] < LTRIM(RTRIM(@SpecialInstructions))) " 
                    + "AND   (@DiscountPercent IS NULL OR @DiscountPercent = '' OR [OrderDetail].[DiscountPercent] < LTRIM(RTRIM(@DiscountPercent))) " 
                    + "";
            } else if  (sCondition == "Equal or more than...") {
                selectStatement
                    = "SELECT "
                + "     [OrderDetail].[OrderDetailId] "
                + "    ,[OrderDetail].[OrderId] "
                + "    ,[OrderDetail].[Quantity] "
                + "    ,[OrderDetail].[CatalogItemId] "
                + "    ,[OrderDetail].[Price] "
                + "    ,[OrderDetail].[SpecialInstructions] "
                + "    ,[OrderDetail].[DiscountPercent] "
                + "FROM " 
                + "     [OrderDetail] " 
                + "INNER JOIN [OrderHeader] ON [OrderDetail].[OrderId] = [OrderHeader].[OrderId] "
                + "INNER JOIN [CatalogItem] ON [OrderDetail].[CatalogItemId] = [CatalogItem].[CatalogItemId] "
                    + "WHERE " 
                    + "     (@OrderDetailId IS NULL OR @OrderDetailId = '' OR [OrderDetail].[OrderDetailId] >= LTRIM(RTRIM(@OrderDetailId))) " 
                    + "AND   (@OrderId IS NULL OR @OrderId = '' OR [OrderHeader].[OrderId] >= LTRIM(RTRIM(@OrderId))) " 
                    + "AND   (@Quantity IS NULL OR @Quantity = '' OR [OrderDetail].[Quantity] >= LTRIM(RTRIM(@Quantity))) " 
                    + "AND   (@CatalogItemId IS NULL OR @CatalogItemId = '' OR [CatalogItem].[CatalogItemId] >= LTRIM(RTRIM(@CatalogItemId))) " 
                    + "AND   (@Price IS NULL OR @Price = '' OR [OrderDetail].[Price] >= LTRIM(RTRIM(@Price))) " 
                    + "AND   (@SpecialInstructions IS NULL OR @SpecialInstructions = '' OR [OrderDetail].[SpecialInstructions] >= LTRIM(RTRIM(@SpecialInstructions))) " 
                    + "AND   (@DiscountPercent IS NULL OR @DiscountPercent = '' OR [OrderDetail].[DiscountPercent] >= LTRIM(RTRIM(@DiscountPercent))) " 
                    + "";
            } else if (sCondition == "Equal or less than...") {
                selectStatement 
                    = "SELECT "
                + "     [OrderDetail].[OrderDetailId] "
                + "    ,[OrderDetail].[OrderId] "
                + "    ,[OrderDetail].[Quantity] "
                + "    ,[OrderDetail].[CatalogItemId] "
                + "    ,[OrderDetail].[Price] "
                + "    ,[OrderDetail].[SpecialInstructions] "
                + "    ,[OrderDetail].[DiscountPercent] "
                + "FROM " 
                + "     [OrderDetail] " 
                + "INNER JOIN [OrderHeader] ON [OrderDetail].[OrderId] = [OrderHeader].[OrderId] "
                + "INNER JOIN [CatalogItem] ON [OrderDetail].[CatalogItemId] = [CatalogItem].[CatalogItemId] "
                    + "WHERE " 
                    + "     (@OrderDetailId IS NULL OR @OrderDetailId = '' OR [OrderDetail].[OrderDetailId] <= LTRIM(RTRIM(@OrderDetailId))) " 
                    + "AND   (@OrderId IS NULL OR @OrderId = '' OR [OrderHeader].[OrderId] <= LTRIM(RTRIM(@OrderId))) " 
                    + "AND   (@Quantity IS NULL OR @Quantity = '' OR [OrderDetail].[Quantity] <= LTRIM(RTRIM(@Quantity))) " 
                    + "AND   (@CatalogItemId IS NULL OR @CatalogItemId = '' OR [CatalogItem].[CatalogItemId] <= LTRIM(RTRIM(@CatalogItemId))) " 
                    + "AND   (@Price IS NULL OR @Price = '' OR [OrderDetail].[Price] <= LTRIM(RTRIM(@Price))) " 
                    + "AND   (@SpecialInstructions IS NULL OR @SpecialInstructions = '' OR [OrderDetail].[SpecialInstructions] <= LTRIM(RTRIM(@SpecialInstructions))) " 
                    + "AND   (@DiscountPercent IS NULL OR @DiscountPercent = '' OR [OrderDetail].[DiscountPercent] <= LTRIM(RTRIM(@DiscountPercent))) " 
                    + "";
            }
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            if (sField == "Order Detail Id") {
                selectCommand.Parameters.AddWithValue("@OrderDetailId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@OrderDetailId", DBNull.Value); }
            if (sField == "Order Id") {
                selectCommand.Parameters.AddWithValue("@OrderId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@OrderId", DBNull.Value); }
            if (sField == "Quantity") {
                selectCommand.Parameters.AddWithValue("@Quantity", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Quantity", DBNull.Value); }
            if (sField == "Catalog Item Id") {
                selectCommand.Parameters.AddWithValue("@CatalogItemId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@CatalogItemId", DBNull.Value); }
            if (sField == "Price") {
                selectCommand.Parameters.AddWithValue("@Price", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Price", DBNull.Value); }
            if (sField == "Special Instructions") {
                selectCommand.Parameters.AddWithValue("@SpecialInstructions", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@SpecialInstructions", DBNull.Value); }
            if (sField == "Discount Percent") {
                selectCommand.Parameters.AddWithValue("@DiscountPercent", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@DiscountPercent", DBNull.Value); }
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

        public static OrderDetail Select_Record(OrderDetail OrderDetailPara)
        {
        OrderDetail OrderDetail = new OrderDetail();
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement
            = "SELECT " 
                + "     [OrderDetailId] "
                + "    ,[OrderId] "
                + "    ,[Quantity] "
                + "    ,[CatalogItemId] "
                + "    ,[Price] "
                + "    ,[SpecialInstructions] "
                + "    ,[DiscountPercent] "
                + "FROM "
                + "     [OrderDetail] "
                + "WHERE "
                + "     [OrderDetailId] = @OrderDetailId "
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            selectCommand.Parameters.AddWithValue("@OrderDetailId", OrderDetailPara.OrderDetailId);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    OrderDetail.OrderDetailId = System.Convert.ToInt32(reader["OrderDetailId"]);
                    OrderDetail.OrderId = System.Convert.ToInt32(reader["OrderId"]);
                    OrderDetail.Quantity = System.Convert.ToDecimal(reader["Quantity"]);
                    OrderDetail.CatalogItemId = System.Convert.ToInt32(reader["CatalogItemId"]);
                    OrderDetail.Price = System.Convert.ToDecimal(reader["Price"]);
                    OrderDetail.SpecialInstructions = reader["SpecialInstructions"] is DBNull ? null : reader["SpecialInstructions"].ToString();
                    OrderDetail.DiscountPercent = System.Convert.ToByte(reader["DiscountPercent"]);
                }
                else
                {
                    OrderDetail = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return OrderDetail;
            }
            finally
            {
                connection.Close();
            }
            return OrderDetail;
        }

        public static bool Add(OrderDetail OrderDetail)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string insertStatement
                = "INSERT " 
                + "     [OrderDetail] "
                + "     ( "
                + "     [OrderId] "
                + "    ,[Quantity] "
                + "    ,[CatalogItemId] "
                + "    ,[Price] "
                + "    ,[SpecialInstructions] "
                + "    ,[DiscountPercent] "
                + "     ) "
                + "VALUES " 
                + "     ( "
                + "     @OrderId "
                + "    ,@Quantity "
                + "    ,@CatalogItemId "
                + "    ,@Price "
                + "    ,@SpecialInstructions "
                + "    ,@DiscountPercent "
                + "     ) "
                + "";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.CommandType = CommandType.Text;
                insertCommand.Parameters.AddWithValue("@OrderId", OrderDetail.OrderId);
                insertCommand.Parameters.AddWithValue("@Quantity", OrderDetail.Quantity);
                insertCommand.Parameters.AddWithValue("@CatalogItemId", OrderDetail.CatalogItemId);
                insertCommand.Parameters.AddWithValue("@Price", OrderDetail.Price);
            if (OrderDetail.SpecialInstructions != null) {
                insertCommand.Parameters.AddWithValue("@SpecialInstructions", OrderDetail.SpecialInstructions);
            } else {
                insertCommand.Parameters.AddWithValue("@SpecialInstructions", DBNull.Value); }
                insertCommand.Parameters.AddWithValue("@DiscountPercent", OrderDetail.DiscountPercent);
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

        public static bool Update(OrderDetail oldOrderDetail, 
               OrderDetail newOrderDetail)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string updateStatement
                = "UPDATE "  
                + "     [OrderDetail] "
                + "SET "
                + "     [OrderId] = @NewOrderId "
                + "    ,[Quantity] = @NewQuantity "
                + "    ,[CatalogItemId] = @NewCatalogItemId "
                + "    ,[Price] = @NewPrice "
                + "    ,[SpecialInstructions] = @NewSpecialInstructions "
                + "    ,[DiscountPercent] = @NewDiscountPercent "
                + "WHERE "
                + "     [OrderDetailId] = @OldOrderDetailId " 
                + " AND [OrderId] = @OldOrderId " 
                + " AND [Quantity] = @OldQuantity " 
                + " AND [CatalogItemId] = @OldCatalogItemId " 
                + " AND [Price] = @OldPrice " 
                + " AND ((@OldSpecialInstructions IS NULL AND [SpecialInstructions] IS NULL) OR [SpecialInstructions] = @OldSpecialInstructions) " 
                + " AND [DiscountPercent] = @OldDiscountPercent " 
                + "";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            updateCommand.CommandType = CommandType.Text;
            updateCommand.Parameters.AddWithValue("@NewOrderId", newOrderDetail.OrderId);
            updateCommand.Parameters.AddWithValue("@NewQuantity", newOrderDetail.Quantity);
            updateCommand.Parameters.AddWithValue("@NewCatalogItemId", newOrderDetail.CatalogItemId);
            updateCommand.Parameters.AddWithValue("@NewPrice", newOrderDetail.Price);
            if (newOrderDetail.SpecialInstructions != null) {
                updateCommand.Parameters.AddWithValue("@NewSpecialInstructions", newOrderDetail.SpecialInstructions);
            } else {
                updateCommand.Parameters.AddWithValue("@NewSpecialInstructions", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@NewDiscountPercent", newOrderDetail.DiscountPercent);
            updateCommand.Parameters.AddWithValue("@OldOrderDetailId", oldOrderDetail.OrderDetailId);
            updateCommand.Parameters.AddWithValue("@OldOrderId", oldOrderDetail.OrderId);
            updateCommand.Parameters.AddWithValue("@OldQuantity", oldOrderDetail.Quantity);
            updateCommand.Parameters.AddWithValue("@OldCatalogItemId", oldOrderDetail.CatalogItemId);
            updateCommand.Parameters.AddWithValue("@OldPrice", oldOrderDetail.Price);
            if (oldOrderDetail.SpecialInstructions != null) {
                updateCommand.Parameters.AddWithValue("@OldSpecialInstructions", oldOrderDetail.SpecialInstructions);
            } else {
                updateCommand.Parameters.AddWithValue("@OldSpecialInstructions", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldDiscountPercent", oldOrderDetail.DiscountPercent);
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

        public static bool Delete(OrderDetail OrderDetail)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string deleteStatement
                = "DELETE FROM "  
                + "     [OrderDetail] "
                + "WHERE " 
                + "     [OrderDetailId] = @OldOrderDetailId " 
                + " AND [OrderId] = @OldOrderId " 
                + " AND [Quantity] = @OldQuantity " 
                + " AND [CatalogItemId] = @OldCatalogItemId " 
                + " AND [Price] = @OldPrice " 
                + " AND ((@OldSpecialInstructions IS NULL AND [SpecialInstructions] IS NULL) OR [SpecialInstructions] = @OldSpecialInstructions) " 
                + " AND [DiscountPercent] = @OldDiscountPercent " 
                + "";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.CommandType = CommandType.Text;
            deleteCommand.Parameters.AddWithValue("@OldOrderDetailId", OrderDetail.OrderDetailId);
            deleteCommand.Parameters.AddWithValue("@OldOrderId", OrderDetail.OrderId);
            deleteCommand.Parameters.AddWithValue("@OldQuantity", OrderDetail.Quantity);
            deleteCommand.Parameters.AddWithValue("@OldCatalogItemId", OrderDetail.CatalogItemId);
            deleteCommand.Parameters.AddWithValue("@OldPrice", OrderDetail.Price);
            if (OrderDetail.SpecialInstructions != null) {
                deleteCommand.Parameters.AddWithValue("@OldSpecialInstructions", OrderDetail.SpecialInstructions);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldSpecialInstructions", DBNull.Value); }
            deleteCommand.Parameters.AddWithValue("@OldDiscountPercent", OrderDetail.DiscountPercent);
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
 
