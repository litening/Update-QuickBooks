using System;
using System.Data;
using System.Data.SqlClient;
using QB2.Models;

namespace QB2.Data
{
    public class OrderHeaderData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement
                = "SELECT "  
                + "     [OrderHeader].[OrderId] "
                + "    ,[OrderHeader].[UserId] "
                + "    ,[OrderHeader].[OrderDate] "
                + "    ,[OrderHeader].[CustomerId] "
                + "    ,[OrderHeader].[OrderTotal] "
                + "    ,[OrderHeader].[SalesTax] "
                + "    ,[OrderHeader].[SalesTaxCode] "
                + "    ,[OrderHeader].[ShippingCharge] "
                + "    ,[OrderHeader].[QbUpdated] "
                + "    ,[OrderHeader].[SalesTaxAmt] "
                + "    ,[OrderHeader].[DiscountAmount] "
                + "    ,[OrderHeader].[Status] "
                + "    ,[OrderHeader].[bTestOrder] "
                + "FROM " 
                + "     [OrderHeader] " 
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
                + "     [OrderHeader].[OrderId] "
                + "    ,[OrderHeader].[UserId] "
                + "    ,[OrderHeader].[OrderDate] "
                + "    ,[OrderHeader].[CustomerId] "
                + "    ,[OrderHeader].[OrderTotal] "
                + "    ,[OrderHeader].[SalesTax] "
                + "    ,[OrderHeader].[SalesTaxCode] "
                + "    ,[OrderHeader].[ShippingCharge] "
                + "    ,[OrderHeader].[QbUpdated] "
                + "    ,[OrderHeader].[SalesTaxAmt] "
                + "    ,[OrderHeader].[DiscountAmount] "
                + "    ,[OrderHeader].[Status] "
                + "    ,[OrderHeader].[bTestOrder] "
                + "FROM " 
                + "     [OrderHeader] " 
                + "INNER JOIN [OurCustomer] ON [OrderHeader].[UserId] = [OurCustomer].[UserId] "
                + "INNER JOIN [Customer] ON [OrderHeader].[CustomerId] = [Customer].[CustomerId] "
                    + "WHERE " 
                    + "     (@OrderId IS NULL OR @OrderId = '' OR [OrderHeader].[OrderId] LIKE '%' + LTRIM(RTRIM(@OrderId)) + '%') " 
                    + "AND   (@UserId IS NULL OR @UserId = '' OR [OurCustomer].[UserId] LIKE '%' + LTRIM(RTRIM(@UserId)) + '%') " 
                    + "AND   (@OrderDate IS NULL OR @OrderDate = '' OR [OrderHeader].[OrderDate] LIKE '%' + LTRIM(RTRIM(@OrderDate)) + '%') " 
                    + "AND   (@CustomerId IS NULL OR @CustomerId = '' OR [Customer].[CustomerId] LIKE '%' + LTRIM(RTRIM(@CustomerId)) + '%') " 
                    + "AND   (@OrderTotal IS NULL OR @OrderTotal = '' OR [OrderHeader].[OrderTotal] LIKE '%' + LTRIM(RTRIM(@OrderTotal)) + '%') " 
                    + "AND   (@SalesTax IS NULL OR @SalesTax = '' OR [OrderHeader].[SalesTax] LIKE '%' + LTRIM(RTRIM(@SalesTax)) + '%') " 
                    + "AND   (@SalesTaxCode IS NULL OR @SalesTaxCode = '' OR [OrderHeader].[SalesTaxCode] LIKE '%' + LTRIM(RTRIM(@SalesTaxCode)) + '%') " 
                    + "AND   (@ShippingCharge IS NULL OR @ShippingCharge = '' OR [OrderHeader].[ShippingCharge] LIKE '%' + LTRIM(RTRIM(@ShippingCharge)) + '%') " 
                    + "AND   (@QbUpdated IS NULL OR @QbUpdated = '' OR [OrderHeader].[QbUpdated] LIKE '%' + LTRIM(RTRIM(@QbUpdated)) + '%') " 
                    + "AND   (@SalesTaxAmt IS NULL OR @SalesTaxAmt = '' OR [OrderHeader].[SalesTaxAmt] LIKE '%' + LTRIM(RTRIM(@SalesTaxAmt)) + '%') " 
                    + "AND   (@DiscountAmount IS NULL OR @DiscountAmount = '' OR [OrderHeader].[DiscountAmount] LIKE '%' + LTRIM(RTRIM(@DiscountAmount)) + '%') " 
                    + "AND   (@Status IS NULL OR @Status = '' OR [OrderHeader].[Status] LIKE '%' + LTRIM(RTRIM(@Status)) + '%') " 
                    + "AND   (@bTestOrder IS NULL OR @bTestOrder = '' OR [OrderHeader].[bTestOrder] LIKE '%' + LTRIM(RTRIM(@bTestOrder)) + '%') " 
                    + "";
            } else if (sCondition == "Equals") {
                selectStatement
                    = "SELECT "
                + "     [OrderHeader].[OrderId] "
                + "    ,[OrderHeader].[UserId] "
                + "    ,[OrderHeader].[OrderDate] "
                + "    ,[OrderHeader].[CustomerId] "
                + "    ,[OrderHeader].[OrderTotal] "
                + "    ,[OrderHeader].[SalesTax] "
                + "    ,[OrderHeader].[SalesTaxCode] "
                + "    ,[OrderHeader].[ShippingCharge] "
                + "    ,[OrderHeader].[QbUpdated] "
                + "    ,[OrderHeader].[SalesTaxAmt] "
                + "    ,[OrderHeader].[DiscountAmount] "
                + "    ,[OrderHeader].[Status] "
                + "    ,[OrderHeader].[bTestOrder] "
                + "FROM " 
                + "     [OrderHeader] " 
                + "INNER JOIN [OurCustomer] ON [OrderHeader].[UserId] = [OurCustomer].[UserId] "
                + "INNER JOIN [Customer] ON [OrderHeader].[CustomerId] = [Customer].[CustomerId] "
                    + "WHERE " 
                    + "     (@OrderId IS NULL OR @OrderId = '' OR [OrderHeader].[OrderId] = LTRIM(RTRIM(@OrderId))) " 
                    + "AND   (@UserId IS NULL OR @UserId = '' OR [OurCustomer].[UserId] = LTRIM(RTRIM(@UserId))) " 
                    + "AND   (@OrderDate IS NULL OR @OrderDate = '' OR [OrderHeader].[OrderDate] = LTRIM(RTRIM(@OrderDate))) " 
                    + "AND   (@CustomerId IS NULL OR @CustomerId = '' OR [Customer].[CustomerId] = LTRIM(RTRIM(@CustomerId))) " 
                    + "AND   (@OrderTotal IS NULL OR @OrderTotal = '' OR [OrderHeader].[OrderTotal] = LTRIM(RTRIM(@OrderTotal))) " 
                    + "AND   (@SalesTax IS NULL OR @SalesTax = '' OR [OrderHeader].[SalesTax] = LTRIM(RTRIM(@SalesTax))) " 
                    + "AND   (@SalesTaxCode IS NULL OR @SalesTaxCode = '' OR [OrderHeader].[SalesTaxCode] = LTRIM(RTRIM(@SalesTaxCode))) " 
                    + "AND   (@ShippingCharge IS NULL OR @ShippingCharge = '' OR [OrderHeader].[ShippingCharge] = LTRIM(RTRIM(@ShippingCharge))) " 
                    + "AND   (@QbUpdated IS NULL OR @QbUpdated = '' OR [OrderHeader].[QbUpdated] = LTRIM(RTRIM(@QbUpdated))) " 
                    + "AND   (@SalesTaxAmt IS NULL OR @SalesTaxAmt = '' OR [OrderHeader].[SalesTaxAmt] = LTRIM(RTRIM(@SalesTaxAmt))) " 
                    + "AND   (@DiscountAmount IS NULL OR @DiscountAmount = '' OR [OrderHeader].[DiscountAmount] = LTRIM(RTRIM(@DiscountAmount))) " 
                    + "AND   (@Status IS NULL OR @Status = '' OR [OrderHeader].[Status] = LTRIM(RTRIM(@Status))) " 
                    + "AND   (@bTestOrder IS NULL OR @bTestOrder = '' OR [OrderHeader].[bTestOrder] = LTRIM(RTRIM(@bTestOrder))) " 
                    + "";
            } else if  (sCondition == "Starts with...") {
                selectStatement
                    = "SELECT "
                + "     [OrderHeader].[OrderId] "
                + "    ,[OrderHeader].[UserId] "
                + "    ,[OrderHeader].[OrderDate] "
                + "    ,[OrderHeader].[CustomerId] "
                + "    ,[OrderHeader].[OrderTotal] "
                + "    ,[OrderHeader].[SalesTax] "
                + "    ,[OrderHeader].[SalesTaxCode] "
                + "    ,[OrderHeader].[ShippingCharge] "
                + "    ,[OrderHeader].[QbUpdated] "
                + "    ,[OrderHeader].[SalesTaxAmt] "
                + "    ,[OrderHeader].[DiscountAmount] "
                + "    ,[OrderHeader].[Status] "
                + "    ,[OrderHeader].[bTestOrder] "
                + "FROM " 
                + "     [OrderHeader] " 
                + "INNER JOIN [OurCustomer] ON [OrderHeader].[UserId] = [OurCustomer].[UserId] "
                + "INNER JOIN [Customer] ON [OrderHeader].[CustomerId] = [Customer].[CustomerId] "
                    + "WHERE " 
                    + "     (@OrderId IS NULL OR @OrderId = '' OR [OrderHeader].[OrderId] LIKE LTRIM(RTRIM(@OrderId)) + '%') " 
                    + "AND   (@UserId IS NULL OR @UserId = '' OR [OurCustomer].[UserId] LIKE LTRIM(RTRIM(@UserId)) + '%') " 
                    + "AND   (@OrderDate IS NULL OR @OrderDate = '' OR [OrderHeader].[OrderDate] LIKE LTRIM(RTRIM(@OrderDate)) + '%') " 
                    + "AND   (@CustomerId IS NULL OR @CustomerId = '' OR [Customer].[CustomerId] LIKE LTRIM(RTRIM(@CustomerId)) + '%') " 
                    + "AND   (@OrderTotal IS NULL OR @OrderTotal = '' OR [OrderHeader].[OrderTotal] LIKE LTRIM(RTRIM(@OrderTotal)) + '%') " 
                    + "AND   (@SalesTax IS NULL OR @SalesTax = '' OR [OrderHeader].[SalesTax] LIKE LTRIM(RTRIM(@SalesTax)) + '%') " 
                    + "AND   (@SalesTaxCode IS NULL OR @SalesTaxCode = '' OR [OrderHeader].[SalesTaxCode] LIKE LTRIM(RTRIM(@SalesTaxCode)) + '%') " 
                    + "AND   (@ShippingCharge IS NULL OR @ShippingCharge = '' OR [OrderHeader].[ShippingCharge] LIKE LTRIM(RTRIM(@ShippingCharge)) + '%') " 
                    + "AND   (@QbUpdated IS NULL OR @QbUpdated = '' OR [OrderHeader].[QbUpdated] LIKE LTRIM(RTRIM(@QbUpdated)) + '%') " 
                    + "AND   (@SalesTaxAmt IS NULL OR @SalesTaxAmt = '' OR [OrderHeader].[SalesTaxAmt] LIKE LTRIM(RTRIM(@SalesTaxAmt)) + '%') " 
                    + "AND   (@DiscountAmount IS NULL OR @DiscountAmount = '' OR [OrderHeader].[DiscountAmount] LIKE LTRIM(RTRIM(@DiscountAmount)) + '%') " 
                    + "AND   (@Status IS NULL OR @Status = '' OR [OrderHeader].[Status] LIKE LTRIM(RTRIM(@Status)) + '%') " 
                    + "AND   (@bTestOrder IS NULL OR @bTestOrder = '' OR [OrderHeader].[bTestOrder] LIKE LTRIM(RTRIM(@bTestOrder)) + '%') " 
                    + "";
            } else if  (sCondition == "More than...") {
                selectStatement
                    = "SELECT "
                + "     [OrderHeader].[OrderId] "
                + "    ,[OrderHeader].[UserId] "
                + "    ,[OrderHeader].[OrderDate] "
                + "    ,[OrderHeader].[CustomerId] "
                + "    ,[OrderHeader].[OrderTotal] "
                + "    ,[OrderHeader].[SalesTax] "
                + "    ,[OrderHeader].[SalesTaxCode] "
                + "    ,[OrderHeader].[ShippingCharge] "
                + "    ,[OrderHeader].[QbUpdated] "
                + "    ,[OrderHeader].[SalesTaxAmt] "
                + "    ,[OrderHeader].[DiscountAmount] "
                + "    ,[OrderHeader].[Status] "
                + "    ,[OrderHeader].[bTestOrder] "
                + "FROM " 
                + "     [OrderHeader] " 
                + "INNER JOIN [OurCustomer] ON [OrderHeader].[UserId] = [OurCustomer].[UserId] "
                + "INNER JOIN [Customer] ON [OrderHeader].[CustomerId] = [Customer].[CustomerId] "
                    + "WHERE " 
                    + "     (@OrderId IS NULL OR @OrderId = '' OR [OrderHeader].[OrderId] > LTRIM(RTRIM(@OrderId))) " 
                    + "AND   (@UserId IS NULL OR @UserId = '' OR [OurCustomer].[UserId] > LTRIM(RTRIM(@UserId))) " 
                    + "AND   (@OrderDate IS NULL OR @OrderDate = '' OR [OrderHeader].[OrderDate] > LTRIM(RTRIM(@OrderDate))) " 
                    + "AND   (@CustomerId IS NULL OR @CustomerId = '' OR [Customer].[CustomerId] > LTRIM(RTRIM(@CustomerId))) " 
                    + "AND   (@OrderTotal IS NULL OR @OrderTotal = '' OR [OrderHeader].[OrderTotal] > LTRIM(RTRIM(@OrderTotal))) " 
                    + "AND   (@SalesTax IS NULL OR @SalesTax = '' OR [OrderHeader].[SalesTax] > LTRIM(RTRIM(@SalesTax))) " 
                    + "AND   (@SalesTaxCode IS NULL OR @SalesTaxCode = '' OR [OrderHeader].[SalesTaxCode] > LTRIM(RTRIM(@SalesTaxCode))) " 
                    + "AND   (@ShippingCharge IS NULL OR @ShippingCharge = '' OR [OrderHeader].[ShippingCharge] > LTRIM(RTRIM(@ShippingCharge))) " 
                    + "AND   (@QbUpdated IS NULL OR @QbUpdated = '' OR [OrderHeader].[QbUpdated] > LTRIM(RTRIM(@QbUpdated))) " 
                    + "AND   (@SalesTaxAmt IS NULL OR @SalesTaxAmt = '' OR [OrderHeader].[SalesTaxAmt] > LTRIM(RTRIM(@SalesTaxAmt))) " 
                    + "AND   (@DiscountAmount IS NULL OR @DiscountAmount = '' OR [OrderHeader].[DiscountAmount] > LTRIM(RTRIM(@DiscountAmount))) " 
                    + "AND   (@Status IS NULL OR @Status = '' OR [OrderHeader].[Status] > LTRIM(RTRIM(@Status))) " 
                    + "AND   (@bTestOrder IS NULL OR @bTestOrder = '' OR [OrderHeader].[bTestOrder] > LTRIM(RTRIM(@bTestOrder))) " 
                    + "";
            } else if  (sCondition == "Less than...") {
                selectStatement
                    = "SELECT " 
                + "     [OrderHeader].[OrderId] "
                + "    ,[OrderHeader].[UserId] "
                + "    ,[OrderHeader].[OrderDate] "
                + "    ,[OrderHeader].[CustomerId] "
                + "    ,[OrderHeader].[OrderTotal] "
                + "    ,[OrderHeader].[SalesTax] "
                + "    ,[OrderHeader].[SalesTaxCode] "
                + "    ,[OrderHeader].[ShippingCharge] "
                + "    ,[OrderHeader].[QbUpdated] "
                + "    ,[OrderHeader].[SalesTaxAmt] "
                + "    ,[OrderHeader].[DiscountAmount] "
                + "    ,[OrderHeader].[Status] "
                + "    ,[OrderHeader].[bTestOrder] "
                + "FROM " 
                + "     [OrderHeader] " 
                + "INNER JOIN [OurCustomer] ON [OrderHeader].[UserId] = [OurCustomer].[UserId] "
                + "INNER JOIN [Customer] ON [OrderHeader].[CustomerId] = [Customer].[CustomerId] "
                    + "WHERE " 
                    + "     (@OrderId IS NULL OR @OrderId = '' OR [OrderHeader].[OrderId] < LTRIM(RTRIM(@OrderId))) " 
                    + "AND   (@UserId IS NULL OR @UserId = '' OR [OurCustomer].[UserId] < LTRIM(RTRIM(@UserId))) " 
                    + "AND   (@OrderDate IS NULL OR @OrderDate = '' OR [OrderHeader].[OrderDate] < LTRIM(RTRIM(@OrderDate))) " 
                    + "AND   (@CustomerId IS NULL OR @CustomerId = '' OR [Customer].[CustomerId] < LTRIM(RTRIM(@CustomerId))) " 
                    + "AND   (@OrderTotal IS NULL OR @OrderTotal = '' OR [OrderHeader].[OrderTotal] < LTRIM(RTRIM(@OrderTotal))) " 
                    + "AND   (@SalesTax IS NULL OR @SalesTax = '' OR [OrderHeader].[SalesTax] < LTRIM(RTRIM(@SalesTax))) " 
                    + "AND   (@SalesTaxCode IS NULL OR @SalesTaxCode = '' OR [OrderHeader].[SalesTaxCode] < LTRIM(RTRIM(@SalesTaxCode))) " 
                    + "AND   (@ShippingCharge IS NULL OR @ShippingCharge = '' OR [OrderHeader].[ShippingCharge] < LTRIM(RTRIM(@ShippingCharge))) " 
                    + "AND   (@QbUpdated IS NULL OR @QbUpdated = '' OR [OrderHeader].[QbUpdated] < LTRIM(RTRIM(@QbUpdated))) " 
                    + "AND   (@SalesTaxAmt IS NULL OR @SalesTaxAmt = '' OR [OrderHeader].[SalesTaxAmt] < LTRIM(RTRIM(@SalesTaxAmt))) " 
                    + "AND   (@DiscountAmount IS NULL OR @DiscountAmount = '' OR [OrderHeader].[DiscountAmount] < LTRIM(RTRIM(@DiscountAmount))) " 
                    + "AND   (@Status IS NULL OR @Status = '' OR [OrderHeader].[Status] < LTRIM(RTRIM(@Status))) " 
                    + "AND   (@bTestOrder IS NULL OR @bTestOrder = '' OR [OrderHeader].[bTestOrder] < LTRIM(RTRIM(@bTestOrder))) " 
                    + "";
            } else if  (sCondition == "Equal or more than...") {
                selectStatement
                    = "SELECT "
                + "     [OrderHeader].[OrderId] "
                + "    ,[OrderHeader].[UserId] "
                + "    ,[OrderHeader].[OrderDate] "
                + "    ,[OrderHeader].[CustomerId] "
                + "    ,[OrderHeader].[OrderTotal] "
                + "    ,[OrderHeader].[SalesTax] "
                + "    ,[OrderHeader].[SalesTaxCode] "
                + "    ,[OrderHeader].[ShippingCharge] "
                + "    ,[OrderHeader].[QbUpdated] "
                + "    ,[OrderHeader].[SalesTaxAmt] "
                + "    ,[OrderHeader].[DiscountAmount] "
                + "    ,[OrderHeader].[Status] "
                + "    ,[OrderHeader].[bTestOrder] "
                + "FROM " 
                + "     [OrderHeader] " 
                + "INNER JOIN [OurCustomer] ON [OrderHeader].[UserId] = [OurCustomer].[UserId] "
                + "INNER JOIN [Customer] ON [OrderHeader].[CustomerId] = [Customer].[CustomerId] "
                    + "WHERE " 
                    + "     (@OrderId IS NULL OR @OrderId = '' OR [OrderHeader].[OrderId] >= LTRIM(RTRIM(@OrderId))) " 
                    + "AND   (@UserId IS NULL OR @UserId = '' OR [OurCustomer].[UserId] >= LTRIM(RTRIM(@UserId))) " 
                    + "AND   (@OrderDate IS NULL OR @OrderDate = '' OR [OrderHeader].[OrderDate] >= LTRIM(RTRIM(@OrderDate))) " 
                    + "AND   (@CustomerId IS NULL OR @CustomerId = '' OR [Customer].[CustomerId] >= LTRIM(RTRIM(@CustomerId))) " 
                    + "AND   (@OrderTotal IS NULL OR @OrderTotal = '' OR [OrderHeader].[OrderTotal] >= LTRIM(RTRIM(@OrderTotal))) " 
                    + "AND   (@SalesTax IS NULL OR @SalesTax = '' OR [OrderHeader].[SalesTax] >= LTRIM(RTRIM(@SalesTax))) " 
                    + "AND   (@SalesTaxCode IS NULL OR @SalesTaxCode = '' OR [OrderHeader].[SalesTaxCode] >= LTRIM(RTRIM(@SalesTaxCode))) " 
                    + "AND   (@ShippingCharge IS NULL OR @ShippingCharge = '' OR [OrderHeader].[ShippingCharge] >= LTRIM(RTRIM(@ShippingCharge))) " 
                    + "AND   (@QbUpdated IS NULL OR @QbUpdated = '' OR [OrderHeader].[QbUpdated] >= LTRIM(RTRIM(@QbUpdated))) " 
                    + "AND   (@SalesTaxAmt IS NULL OR @SalesTaxAmt = '' OR [OrderHeader].[SalesTaxAmt] >= LTRIM(RTRIM(@SalesTaxAmt))) " 
                    + "AND   (@DiscountAmount IS NULL OR @DiscountAmount = '' OR [OrderHeader].[DiscountAmount] >= LTRIM(RTRIM(@DiscountAmount))) " 
                    + "AND   (@Status IS NULL OR @Status = '' OR [OrderHeader].[Status] >= LTRIM(RTRIM(@Status))) " 
                    + "AND   (@bTestOrder IS NULL OR @bTestOrder = '' OR [OrderHeader].[bTestOrder] >= LTRIM(RTRIM(@bTestOrder))) " 
                    + "";
            } else if (sCondition == "Equal or less than...") {
                selectStatement 
                    = "SELECT "
                + "     [OrderHeader].[OrderId] "
                + "    ,[OrderHeader].[UserId] "
                + "    ,[OrderHeader].[OrderDate] "
                + "    ,[OrderHeader].[CustomerId] "
                + "    ,[OrderHeader].[OrderTotal] "
                + "    ,[OrderHeader].[SalesTax] "
                + "    ,[OrderHeader].[SalesTaxCode] "
                + "    ,[OrderHeader].[ShippingCharge] "
                + "    ,[OrderHeader].[QbUpdated] "
                + "    ,[OrderHeader].[SalesTaxAmt] "
                + "    ,[OrderHeader].[DiscountAmount] "
                + "    ,[OrderHeader].[Status] "
                + "    ,[OrderHeader].[bTestOrder] "
                + "FROM " 
                + "     [OrderHeader] " 
                + "INNER JOIN [OurCustomer] ON [OrderHeader].[UserId] = [OurCustomer].[UserId] "
                + "INNER JOIN [Customer] ON [OrderHeader].[CustomerId] = [Customer].[CustomerId] "
                    + "WHERE " 
                    + "     (@OrderId IS NULL OR @OrderId = '' OR [OrderHeader].[OrderId] <= LTRIM(RTRIM(@OrderId))) " 
                    + "AND   (@UserId IS NULL OR @UserId = '' OR [OurCustomer].[UserId] <= LTRIM(RTRIM(@UserId))) " 
                    + "AND   (@OrderDate IS NULL OR @OrderDate = '' OR [OrderHeader].[OrderDate] <= LTRIM(RTRIM(@OrderDate))) " 
                    + "AND   (@CustomerId IS NULL OR @CustomerId = '' OR [Customer].[CustomerId] <= LTRIM(RTRIM(@CustomerId))) " 
                    + "AND   (@OrderTotal IS NULL OR @OrderTotal = '' OR [OrderHeader].[OrderTotal] <= LTRIM(RTRIM(@OrderTotal))) " 
                    + "AND   (@SalesTax IS NULL OR @SalesTax = '' OR [OrderHeader].[SalesTax] <= LTRIM(RTRIM(@SalesTax))) " 
                    + "AND   (@SalesTaxCode IS NULL OR @SalesTaxCode = '' OR [OrderHeader].[SalesTaxCode] <= LTRIM(RTRIM(@SalesTaxCode))) " 
                    + "AND   (@ShippingCharge IS NULL OR @ShippingCharge = '' OR [OrderHeader].[ShippingCharge] <= LTRIM(RTRIM(@ShippingCharge))) " 
                    + "AND   (@QbUpdated IS NULL OR @QbUpdated = '' OR [OrderHeader].[QbUpdated] <= LTRIM(RTRIM(@QbUpdated))) " 
                    + "AND   (@SalesTaxAmt IS NULL OR @SalesTaxAmt = '' OR [OrderHeader].[SalesTaxAmt] <= LTRIM(RTRIM(@SalesTaxAmt))) " 
                    + "AND   (@DiscountAmount IS NULL OR @DiscountAmount = '' OR [OrderHeader].[DiscountAmount] <= LTRIM(RTRIM(@DiscountAmount))) " 
                    + "AND   (@Status IS NULL OR @Status = '' OR [OrderHeader].[Status] <= LTRIM(RTRIM(@Status))) " 
                    + "AND   (@bTestOrder IS NULL OR @bTestOrder = '' OR [OrderHeader].[bTestOrder] <= LTRIM(RTRIM(@bTestOrder))) " 
                    + "";
            }
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            if (sField == "Order Id") {
                selectCommand.Parameters.AddWithValue("@OrderId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@OrderId", DBNull.Value); }
            if (sField == "User Id") {
                selectCommand.Parameters.AddWithValue("@UserId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@UserId", DBNull.Value); }
            if (sField == "Order Date") {
                selectCommand.Parameters.AddWithValue("@OrderDate", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@OrderDate", DBNull.Value); }
            if (sField == "Customer Id") {
                selectCommand.Parameters.AddWithValue("@CustomerId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@CustomerId", DBNull.Value); }
            if (sField == "Order Total") {
                selectCommand.Parameters.AddWithValue("@OrderTotal", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@OrderTotal", DBNull.Value); }
            if (sField == "Sales Tax") {
                selectCommand.Parameters.AddWithValue("@SalesTax", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@SalesTax", DBNull.Value); }
            if (sField == "Sales Tax Code") {
                selectCommand.Parameters.AddWithValue("@SalesTaxCode", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@SalesTaxCode", DBNull.Value); }
            if (sField == "Shipping Charge") {
                selectCommand.Parameters.AddWithValue("@ShippingCharge", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ShippingCharge", DBNull.Value); }
            if (sField == "Qb Updated") {
                selectCommand.Parameters.AddWithValue("@QbUpdated", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QbUpdated", DBNull.Value); }
            if (sField == "Sales Tax Amt") {
                selectCommand.Parameters.AddWithValue("@SalesTaxAmt", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@SalesTaxAmt", DBNull.Value); }
            if (sField == "Discount Amount") {
                selectCommand.Parameters.AddWithValue("@DiscountAmount", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@DiscountAmount", DBNull.Value); }
            if (sField == "Status") {
                selectCommand.Parameters.AddWithValue("@Status", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Status", DBNull.Value); }
            if (sField == "B Test Order") {
                selectCommand.Parameters.AddWithValue("@bTestOrder", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@bTestOrder", DBNull.Value); }
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

        public static OrderHeader Select_Record(OrderHeader OrderHeaderPara)
        {
        OrderHeader OrderHeader = new OrderHeader();
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement
            = "SELECT " 
                + "     [OrderId] "
                + "    ,[UserId] "
                + "    ,[OrderDate] "
                + "    ,[CustomerId] "
                + "    ,[OrderTotal] "
                + "    ,[SalesTax] "
                + "    ,[SalesTaxCode] "
                + "    ,[ShippingCharge] "
                + "    ,[QbUpdated] "
                + "    ,[SalesTaxAmt] "
                + "    ,[DiscountAmount] "
                + "    ,[Status] "
                + "    ,[bTestOrder] "
                + "FROM "
                + "     [OrderHeader] "
                + "WHERE "
                + "     [OrderId] = @OrderId "
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            selectCommand.Parameters.AddWithValue("@OrderId", OrderHeaderPara.OrderId);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    OrderHeader.OrderId = System.Convert.ToInt32(reader["OrderId"]);
                    OrderHeader.UserId = System.Convert.ToInt32(reader["UserId"]);
                    OrderHeader.OrderDate = System.Convert.ToDateTime(reader["OrderDate"]);
                    OrderHeader.CustomerId = reader["CustomerId"] is DBNull ? null : (Int32?)reader["CustomerId"];
                    OrderHeader.OrderTotal = System.Convert.ToDecimal(reader["OrderTotal"]);
                    OrderHeader.SalesTax = System.Convert.ToDecimal(reader["SalesTax"]);
                    OrderHeader.SalesTaxCode = reader["SalesTaxCode"] is DBNull ? null : (Int32?)reader["SalesTaxCode"];
                    OrderHeader.ShippingCharge = System.Convert.ToDecimal(reader["ShippingCharge"]);
                    OrderHeader.QbUpdated = System.Convert.ToBoolean(reader["QbUpdated"]);
                    OrderHeader.SalesTaxAmt = reader["SalesTaxAmt"] is DBNull ? null : (Decimal?)reader["SalesTaxAmt"];
                    OrderHeader.DiscountAmount = reader["DiscountAmount"] is DBNull ? null : (Decimal?)reader["DiscountAmount"];
                    OrderHeader.Status = reader["Status"] is DBNull ? null : (Byte?)reader["Status"];
                    OrderHeader.bTestOrder = reader["bTestOrder"] is DBNull ? null : (Boolean?)reader["bTestOrder"];
                }
                else
                {
                    OrderHeader = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return OrderHeader;
            }
            finally
            {
                connection.Close();
            }
            return OrderHeader;
        }

        public static bool Add(OrderHeader OrderHeader)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string insertStatement
                = "INSERT " 
                + "     [OrderHeader] "
                + "     ( "
                + "     [UserId] "
                + "    ,[OrderDate] "
                + "    ,[CustomerId] "
                + "    ,[OrderTotal] "
                + "    ,[SalesTax] "
                + "    ,[SalesTaxCode] "
                + "    ,[ShippingCharge] "
                + "    ,[QbUpdated] "
                + "    ,[SalesTaxAmt] "
                + "    ,[DiscountAmount] "
                + "    ,[Status] "
                + "    ,[bTestOrder] "
                + "     ) "
                + "VALUES " 
                + "     ( "
                + "     @UserId "
                + "    ,@OrderDate "
                + "    ,@CustomerId "
                + "    ,@OrderTotal "
                + "    ,@SalesTax "
                + "    ,@SalesTaxCode "
                + "    ,@ShippingCharge "
                + "    ,@QbUpdated "
                + "    ,@SalesTaxAmt "
                + "    ,@DiscountAmount "
                + "    ,@Status "
                + "    ,@bTestOrder "
                + "     ) "
                + "";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.CommandType = CommandType.Text;
                insertCommand.Parameters.AddWithValue("@UserId", OrderHeader.UserId);
                insertCommand.Parameters.AddWithValue("@OrderDate", OrderHeader.OrderDate);
            if (OrderHeader.CustomerId.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@CustomerId", OrderHeader.CustomerId);
            } else {
                insertCommand.Parameters.AddWithValue("@CustomerId", DBNull.Value); }
                insertCommand.Parameters.AddWithValue("@OrderTotal", OrderHeader.OrderTotal);
                insertCommand.Parameters.AddWithValue("@SalesTax", OrderHeader.SalesTax);
            if (OrderHeader.SalesTaxCode.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@SalesTaxCode", OrderHeader.SalesTaxCode);
            } else {
                insertCommand.Parameters.AddWithValue("@SalesTaxCode", DBNull.Value); }
                insertCommand.Parameters.AddWithValue("@ShippingCharge", OrderHeader.ShippingCharge);
                insertCommand.Parameters.AddWithValue("@QbUpdated", OrderHeader.QbUpdated);
            if (OrderHeader.SalesTaxAmt.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@SalesTaxAmt", OrderHeader.SalesTaxAmt);
            } else {
                insertCommand.Parameters.AddWithValue("@SalesTaxAmt", DBNull.Value); }
            if (OrderHeader.DiscountAmount.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@DiscountAmount", OrderHeader.DiscountAmount);
            } else {
                insertCommand.Parameters.AddWithValue("@DiscountAmount", DBNull.Value); }
            if (OrderHeader.Status.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@Status", OrderHeader.Status);
            } else {
                insertCommand.Parameters.AddWithValue("@Status", DBNull.Value); }
            if (OrderHeader.bTestOrder.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@bTestOrder", OrderHeader.bTestOrder);
            } else {
                insertCommand.Parameters.AddWithValue("@bTestOrder", DBNull.Value); }
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

        public static bool Update(OrderHeader oldOrderHeader, 
               OrderHeader newOrderHeader)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string updateStatement
                = "UPDATE "  
                + "     [OrderHeader] "
                + "SET "
                + "     [UserId] = @NewUserId "
                + "    ,[OrderDate] = @NewOrderDate "
                + "    ,[CustomerId] = @NewCustomerId "
                + "    ,[OrderTotal] = @NewOrderTotal "
                + "    ,[SalesTax] = @NewSalesTax "
                + "    ,[SalesTaxCode] = @NewSalesTaxCode "
                + "    ,[ShippingCharge] = @NewShippingCharge "
                + "    ,[QbUpdated] = @NewQbUpdated "
                + "    ,[SalesTaxAmt] = @NewSalesTaxAmt "
                + "    ,[DiscountAmount] = @NewDiscountAmount "
                + "    ,[Status] = @NewStatus "
                + "    ,[bTestOrder] = @NewbTestOrder "
                + "WHERE "
                + "     [OrderId] = @OldOrderId " 
                + " AND [UserId] = @OldUserId " 
                + " AND [OrderDate] = @OldOrderDate " 
                + " AND ((@OldCustomerId IS NULL AND [CustomerId] IS NULL) OR [CustomerId] = @OldCustomerId) " 
                + " AND [OrderTotal] = @OldOrderTotal " 
                + " AND [SalesTax] = @OldSalesTax " 
                + " AND ((@OldSalesTaxCode IS NULL AND [SalesTaxCode] IS NULL) OR [SalesTaxCode] = @OldSalesTaxCode) " 
                + " AND [ShippingCharge] = @OldShippingCharge " 
                + " AND [QbUpdated] = @OldQbUpdated " 
                + " AND ((@OldSalesTaxAmt IS NULL AND [SalesTaxAmt] IS NULL) OR [SalesTaxAmt] = @OldSalesTaxAmt) " 
                + " AND ((@OldDiscountAmount IS NULL AND [DiscountAmount] IS NULL) OR [DiscountAmount] = @OldDiscountAmount) " 
                + " AND ((@OldStatus IS NULL AND [Status] IS NULL) OR [Status] = @OldStatus) " 
                + " AND ((@OldbTestOrder IS NULL AND [bTestOrder] IS NULL) OR [bTestOrder] = @OldbTestOrder) " 
                + "";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            updateCommand.CommandType = CommandType.Text;
            updateCommand.Parameters.AddWithValue("@NewUserId", newOrderHeader.UserId);
            updateCommand.Parameters.AddWithValue("@NewOrderDate", newOrderHeader.OrderDate);
            if (newOrderHeader.CustomerId.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewCustomerId", newOrderHeader.CustomerId);
            } else {
                updateCommand.Parameters.AddWithValue("@NewCustomerId", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@NewOrderTotal", newOrderHeader.OrderTotal);
            updateCommand.Parameters.AddWithValue("@NewSalesTax", newOrderHeader.SalesTax);
            if (newOrderHeader.SalesTaxCode.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewSalesTaxCode", newOrderHeader.SalesTaxCode);
            } else {
                updateCommand.Parameters.AddWithValue("@NewSalesTaxCode", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@NewShippingCharge", newOrderHeader.ShippingCharge);
            updateCommand.Parameters.AddWithValue("@NewQbUpdated", newOrderHeader.QbUpdated);
            if (newOrderHeader.SalesTaxAmt.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewSalesTaxAmt", newOrderHeader.SalesTaxAmt);
            } else {
                updateCommand.Parameters.AddWithValue("@NewSalesTaxAmt", DBNull.Value); }
            if (newOrderHeader.DiscountAmount.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewDiscountAmount", newOrderHeader.DiscountAmount);
            } else {
                updateCommand.Parameters.AddWithValue("@NewDiscountAmount", DBNull.Value); }
            if (newOrderHeader.Status.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewStatus", newOrderHeader.Status);
            } else {
                updateCommand.Parameters.AddWithValue("@NewStatus", DBNull.Value); }
            if (newOrderHeader.bTestOrder.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewbTestOrder", newOrderHeader.bTestOrder);
            } else {
                updateCommand.Parameters.AddWithValue("@NewbTestOrder", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldOrderId", oldOrderHeader.OrderId);
            updateCommand.Parameters.AddWithValue("@OldUserId", oldOrderHeader.UserId);
            updateCommand.Parameters.AddWithValue("@OldOrderDate", oldOrderHeader.OrderDate);
            if (oldOrderHeader.CustomerId.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldCustomerId", oldOrderHeader.CustomerId);
            } else {
                updateCommand.Parameters.AddWithValue("@OldCustomerId", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldOrderTotal", oldOrderHeader.OrderTotal);
            updateCommand.Parameters.AddWithValue("@OldSalesTax", oldOrderHeader.SalesTax);
            if (oldOrderHeader.SalesTaxCode.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldSalesTaxCode", oldOrderHeader.SalesTaxCode);
            } else {
                updateCommand.Parameters.AddWithValue("@OldSalesTaxCode", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldShippingCharge", oldOrderHeader.ShippingCharge);
            updateCommand.Parameters.AddWithValue("@OldQbUpdated", oldOrderHeader.QbUpdated);
            if (oldOrderHeader.SalesTaxAmt.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldSalesTaxAmt", oldOrderHeader.SalesTaxAmt);
            } else {
                updateCommand.Parameters.AddWithValue("@OldSalesTaxAmt", DBNull.Value); }
            if (oldOrderHeader.DiscountAmount.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldDiscountAmount", oldOrderHeader.DiscountAmount);
            } else {
                updateCommand.Parameters.AddWithValue("@OldDiscountAmount", DBNull.Value); }
            if (oldOrderHeader.Status.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldStatus", oldOrderHeader.Status);
            } else {
                updateCommand.Parameters.AddWithValue("@OldStatus", DBNull.Value); }
            if (oldOrderHeader.bTestOrder.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldbTestOrder", oldOrderHeader.bTestOrder);
            } else {
                updateCommand.Parameters.AddWithValue("@OldbTestOrder", DBNull.Value); }
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

        public static bool Delete(OrderHeader OrderHeader)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string deleteStatement
                = "DELETE FROM "  
                + "     [OrderHeader] "
                + "WHERE " 
                + "     [OrderId] = @OldOrderId " 
                + " AND [UserId] = @OldUserId " 
                + " AND [OrderDate] = @OldOrderDate " 
                + " AND ((@OldCustomerId IS NULL AND [CustomerId] IS NULL) OR [CustomerId] = @OldCustomerId) " 
                + " AND [OrderTotal] = @OldOrderTotal " 
                + " AND [SalesTax] = @OldSalesTax " 
                + " AND ((@OldSalesTaxCode IS NULL AND [SalesTaxCode] IS NULL) OR [SalesTaxCode] = @OldSalesTaxCode) " 
                + " AND [ShippingCharge] = @OldShippingCharge " 
                + " AND [QbUpdated] = @OldQbUpdated " 
                + " AND ((@OldSalesTaxAmt IS NULL AND [SalesTaxAmt] IS NULL) OR [SalesTaxAmt] = @OldSalesTaxAmt) " 
                + " AND ((@OldDiscountAmount IS NULL AND [DiscountAmount] IS NULL) OR [DiscountAmount] = @OldDiscountAmount) " 
                + " AND ((@OldStatus IS NULL AND [Status] IS NULL) OR [Status] = @OldStatus) " 
                + " AND ((@OldbTestOrder IS NULL AND [bTestOrder] IS NULL) OR [bTestOrder] = @OldbTestOrder) " 
                + "";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.CommandType = CommandType.Text;
            deleteCommand.Parameters.AddWithValue("@OldOrderId", OrderHeader.OrderId);
            deleteCommand.Parameters.AddWithValue("@OldUserId", OrderHeader.UserId);
            deleteCommand.Parameters.AddWithValue("@OldOrderDate", OrderHeader.OrderDate);
            if (OrderHeader.CustomerId.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldCustomerId", OrderHeader.CustomerId);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldCustomerId", DBNull.Value); }
            deleteCommand.Parameters.AddWithValue("@OldOrderTotal", OrderHeader.OrderTotal);
            deleteCommand.Parameters.AddWithValue("@OldSalesTax", OrderHeader.SalesTax);
            if (OrderHeader.SalesTaxCode.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldSalesTaxCode", OrderHeader.SalesTaxCode);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldSalesTaxCode", DBNull.Value); }
            deleteCommand.Parameters.AddWithValue("@OldShippingCharge", OrderHeader.ShippingCharge);
            deleteCommand.Parameters.AddWithValue("@OldQbUpdated", OrderHeader.QbUpdated);
            if (OrderHeader.SalesTaxAmt.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldSalesTaxAmt", OrderHeader.SalesTaxAmt);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldSalesTaxAmt", DBNull.Value); }
            if (OrderHeader.DiscountAmount.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldDiscountAmount", OrderHeader.DiscountAmount);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldDiscountAmount", DBNull.Value); }
            if (OrderHeader.Status.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldStatus", OrderHeader.Status);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldStatus", DBNull.Value); }
            if (OrderHeader.bTestOrder.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldbTestOrder", OrderHeader.bTestOrder);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldbTestOrder", DBNull.Value); }
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
 
