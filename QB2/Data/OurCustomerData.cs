using System;
using System.Data;
using System.Data.SqlClient;
using QB2.Models;

namespace QB2.Data
{
    public class OurCustomerData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement
                = "SELECT "  
                + "     [OurCustomer].[UserId] "
                + "    ,[OurCustomer].[QbCustomerId] "
                + "    ,[OurCustomer].[QuickbooksAccessToken] "
                + "    ,[OurCustomer].[QuickbooksSecretToken] "
                + "    ,[OurCustomer].[QbSalesAccount] "
                + "    ,[OurCustomer].[QbSalesTax] "
                + "    ,[OurCustomer].[QbSalesDiscounts] "
                + "    ,[OurCustomer].[QbFreightIncome] "
                + "    ,[OurCustomer].[QbCash] "
                + "    ,[OurCustomer].[QbCostOfGoods] "
                + "    ,[OurCustomer].[QbUndepositiedFunds] "
                + "    ,[OurCustomer].[QbSalesId] "
                + "    ,[OurCustomer].[QbSalesTaxId] "
                + "    ,[OurCustomer].[QbDiscountsId] "
                + "    ,[OurCustomer].[QbFreightId] "
                + "    ,[OurCustomer].[QbCashId] "
                + "    ,[OurCustomer].[QbCostofGoodsId] "
                + "    ,[OurCustomer].[QbUndepositedFundsId] "
                + "FROM " 
                + "     [OurCustomer] " 
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
                + "     [OurCustomer].[UserId] "
                + "    ,[OurCustomer].[QbCustomerId] "
                + "    ,[OurCustomer].[QuickbooksAccessToken] "
                + "    ,[OurCustomer].[QuickbooksSecretToken] "
                + "    ,[OurCustomer].[QbSalesAccount] "
                + "    ,[OurCustomer].[QbSalesTax] "
                + "    ,[OurCustomer].[QbSalesDiscounts] "
                + "    ,[OurCustomer].[QbFreightIncome] "
                + "    ,[OurCustomer].[QbCash] "
                + "    ,[OurCustomer].[QbCostOfGoods] "
                + "    ,[OurCustomer].[QbUndepositiedFunds] "
                + "    ,[OurCustomer].[QbSalesId] "
                + "    ,[OurCustomer].[QbSalesTaxId] "
                + "    ,[OurCustomer].[QbDiscountsId] "
                + "    ,[OurCustomer].[QbFreightId] "
                + "    ,[OurCustomer].[QbCashId] "
                + "    ,[OurCustomer].[QbCostofGoodsId] "
                + "    ,[OurCustomer].[QbUndepositedFundsId] "
                + "FROM " 
                + "     [OurCustomer] " 
                    + "WHERE " 
                    + "     (@UserId IS NULL OR @UserId = '' OR [OurCustomer].[UserId] LIKE '%' + LTRIM(RTRIM(@UserId)) + '%') " 
                    + "AND   (@QbCustomerId IS NULL OR @QbCustomerId = '' OR [OurCustomer].[QbCustomerId] LIKE '%' + LTRIM(RTRIM(@QbCustomerId)) + '%') " 
                    + "AND   (@QuickbooksAccessToken IS NULL OR @QuickbooksAccessToken = '' OR [OurCustomer].[QuickbooksAccessToken] LIKE '%' + LTRIM(RTRIM(@QuickbooksAccessToken)) + '%') " 
                    + "AND   (@QuickbooksSecretToken IS NULL OR @QuickbooksSecretToken = '' OR [OurCustomer].[QuickbooksSecretToken] LIKE '%' + LTRIM(RTRIM(@QuickbooksSecretToken)) + '%') " 
                    + "AND   (@QbSalesAccount IS NULL OR @QbSalesAccount = '' OR [OurCustomer].[QbSalesAccount] LIKE '%' + LTRIM(RTRIM(@QbSalesAccount)) + '%') " 
                    + "AND   (@QbSalesTax IS NULL OR @QbSalesTax = '' OR [OurCustomer].[QbSalesTax] LIKE '%' + LTRIM(RTRIM(@QbSalesTax)) + '%') " 
                    + "AND   (@QbSalesDiscounts IS NULL OR @QbSalesDiscounts = '' OR [OurCustomer].[QbSalesDiscounts] LIKE '%' + LTRIM(RTRIM(@QbSalesDiscounts)) + '%') " 
                    + "AND   (@QbFreightIncome IS NULL OR @QbFreightIncome = '' OR [OurCustomer].[QbFreightIncome] LIKE '%' + LTRIM(RTRIM(@QbFreightIncome)) + '%') " 
                    + "AND   (@QbCash IS NULL OR @QbCash = '' OR [OurCustomer].[QbCash] LIKE '%' + LTRIM(RTRIM(@QbCash)) + '%') " 
                    + "AND   (@QbCostOfGoods IS NULL OR @QbCostOfGoods = '' OR [OurCustomer].[QbCostOfGoods] LIKE '%' + LTRIM(RTRIM(@QbCostOfGoods)) + '%') " 
                    + "AND   (@QbUndepositiedFunds IS NULL OR @QbUndepositiedFunds = '' OR [OurCustomer].[QbUndepositiedFunds] LIKE '%' + LTRIM(RTRIM(@QbUndepositiedFunds)) + '%') " 
                    + "AND   (@QbSalesId IS NULL OR @QbSalesId = '' OR [OurCustomer].[QbSalesId] LIKE '%' + LTRIM(RTRIM(@QbSalesId)) + '%') " 
                    + "AND   (@QbSalesTaxId IS NULL OR @QbSalesTaxId = '' OR [OurCustomer].[QbSalesTaxId] LIKE '%' + LTRIM(RTRIM(@QbSalesTaxId)) + '%') " 
                    + "AND   (@QbDiscountsId IS NULL OR @QbDiscountsId = '' OR [OurCustomer].[QbDiscountsId] LIKE '%' + LTRIM(RTRIM(@QbDiscountsId)) + '%') " 
                    + "AND   (@QbFreightId IS NULL OR @QbFreightId = '' OR [OurCustomer].[QbFreightId] LIKE '%' + LTRIM(RTRIM(@QbFreightId)) + '%') " 
                    + "AND   (@QbCashId IS NULL OR @QbCashId = '' OR [OurCustomer].[QbCashId] LIKE '%' + LTRIM(RTRIM(@QbCashId)) + '%') " 
                    + "AND   (@QbCostofGoodsId IS NULL OR @QbCostofGoodsId = '' OR [OurCustomer].[QbCostofGoodsId] LIKE '%' + LTRIM(RTRIM(@QbCostofGoodsId)) + '%') " 
                    + "AND   (@QbUndepositedFundsId IS NULL OR @QbUndepositedFundsId = '' OR [OurCustomer].[QbUndepositedFundsId] LIKE '%' + LTRIM(RTRIM(@QbUndepositedFundsId)) + '%') " 
                    + "";
            } else if (sCondition == "Equals") {
                selectStatement
                    = "SELECT "
                + "     [OurCustomer].[UserId] "
                + "    ,[OurCustomer].[QbCustomerId] "
                + "    ,[OurCustomer].[QuickbooksAccessToken] "
                + "    ,[OurCustomer].[QuickbooksSecretToken] "
                + "    ,[OurCustomer].[QbSalesAccount] "
                + "    ,[OurCustomer].[QbSalesTax] "
                + "    ,[OurCustomer].[QbSalesDiscounts] "
                + "    ,[OurCustomer].[QbFreightIncome] "
                + "    ,[OurCustomer].[QbCash] "
                + "    ,[OurCustomer].[QbCostOfGoods] "
                + "    ,[OurCustomer].[QbUndepositiedFunds] "
                + "    ,[OurCustomer].[QbSalesId] "
                + "    ,[OurCustomer].[QbSalesTaxId] "
                + "    ,[OurCustomer].[QbDiscountsId] "
                + "    ,[OurCustomer].[QbFreightId] "
                + "    ,[OurCustomer].[QbCashId] "
                + "    ,[OurCustomer].[QbCostofGoodsId] "
                + "    ,[OurCustomer].[QbUndepositedFundsId] "
                + "FROM " 
                + "     [OurCustomer] " 
                    + "WHERE " 
                    + "     (@UserId IS NULL OR @UserId = '' OR [OurCustomer].[UserId] = LTRIM(RTRIM(@UserId))) " 
                    + "AND   (@QbCustomerId IS NULL OR @QbCustomerId = '' OR [OurCustomer].[QbCustomerId] = LTRIM(RTRIM(@QbCustomerId))) " 
                    + "AND   (@QuickbooksAccessToken IS NULL OR @QuickbooksAccessToken = '' OR [OurCustomer].[QuickbooksAccessToken] = LTRIM(RTRIM(@QuickbooksAccessToken))) " 
                    + "AND   (@QuickbooksSecretToken IS NULL OR @QuickbooksSecretToken = '' OR [OurCustomer].[QuickbooksSecretToken] = LTRIM(RTRIM(@QuickbooksSecretToken))) " 
                    + "AND   (@QbSalesAccount IS NULL OR @QbSalesAccount = '' OR [OurCustomer].[QbSalesAccount] = LTRIM(RTRIM(@QbSalesAccount))) " 
                    + "AND   (@QbSalesTax IS NULL OR @QbSalesTax = '' OR [OurCustomer].[QbSalesTax] = LTRIM(RTRIM(@QbSalesTax))) " 
                    + "AND   (@QbSalesDiscounts IS NULL OR @QbSalesDiscounts = '' OR [OurCustomer].[QbSalesDiscounts] = LTRIM(RTRIM(@QbSalesDiscounts))) " 
                    + "AND   (@QbFreightIncome IS NULL OR @QbFreightIncome = '' OR [OurCustomer].[QbFreightIncome] = LTRIM(RTRIM(@QbFreightIncome))) " 
                    + "AND   (@QbCash IS NULL OR @QbCash = '' OR [OurCustomer].[QbCash] = LTRIM(RTRIM(@QbCash))) " 
                    + "AND   (@QbCostOfGoods IS NULL OR @QbCostOfGoods = '' OR [OurCustomer].[QbCostOfGoods] = LTRIM(RTRIM(@QbCostOfGoods))) " 
                    + "AND   (@QbUndepositiedFunds IS NULL OR @QbUndepositiedFunds = '' OR [OurCustomer].[QbUndepositiedFunds] = LTRIM(RTRIM(@QbUndepositiedFunds))) " 
                    + "AND   (@QbSalesId IS NULL OR @QbSalesId = '' OR [OurCustomer].[QbSalesId] = LTRIM(RTRIM(@QbSalesId))) " 
                    + "AND   (@QbSalesTaxId IS NULL OR @QbSalesTaxId = '' OR [OurCustomer].[QbSalesTaxId] = LTRIM(RTRIM(@QbSalesTaxId))) " 
                    + "AND   (@QbDiscountsId IS NULL OR @QbDiscountsId = '' OR [OurCustomer].[QbDiscountsId] = LTRIM(RTRIM(@QbDiscountsId))) " 
                    + "AND   (@QbFreightId IS NULL OR @QbFreightId = '' OR [OurCustomer].[QbFreightId] = LTRIM(RTRIM(@QbFreightId))) " 
                    + "AND   (@QbCashId IS NULL OR @QbCashId = '' OR [OurCustomer].[QbCashId] = LTRIM(RTRIM(@QbCashId))) " 
                    + "AND   (@QbCostofGoodsId IS NULL OR @QbCostofGoodsId = '' OR [OurCustomer].[QbCostofGoodsId] = LTRIM(RTRIM(@QbCostofGoodsId))) " 
                    + "AND   (@QbUndepositedFundsId IS NULL OR @QbUndepositedFundsId = '' OR [OurCustomer].[QbUndepositedFundsId] = LTRIM(RTRIM(@QbUndepositedFundsId))) " 
                    + "";
            } else if  (sCondition == "Starts with...") {
                selectStatement
                    = "SELECT "
                + "     [OurCustomer].[UserId] "
                + "    ,[OurCustomer].[QbCustomerId] "
                + "    ,[OurCustomer].[QuickbooksAccessToken] "
                + "    ,[OurCustomer].[QuickbooksSecretToken] "
                + "    ,[OurCustomer].[QbSalesAccount] "
                + "    ,[OurCustomer].[QbSalesTax] "
                + "    ,[OurCustomer].[QbSalesDiscounts] "
                + "    ,[OurCustomer].[QbFreightIncome] "
                + "    ,[OurCustomer].[QbCash] "
                + "    ,[OurCustomer].[QbCostOfGoods] "
                + "    ,[OurCustomer].[QbUndepositiedFunds] "
                + "    ,[OurCustomer].[QbSalesId] "
                + "    ,[OurCustomer].[QbSalesTaxId] "
                + "    ,[OurCustomer].[QbDiscountsId] "
                + "    ,[OurCustomer].[QbFreightId] "
                + "    ,[OurCustomer].[QbCashId] "
                + "    ,[OurCustomer].[QbCostofGoodsId] "
                + "    ,[OurCustomer].[QbUndepositedFundsId] "
                + "FROM " 
                + "     [OurCustomer] " 
                    + "WHERE " 
                    + "     (@UserId IS NULL OR @UserId = '' OR [OurCustomer].[UserId] LIKE LTRIM(RTRIM(@UserId)) + '%') " 
                    + "AND   (@QbCustomerId IS NULL OR @QbCustomerId = '' OR [OurCustomer].[QbCustomerId] LIKE LTRIM(RTRIM(@QbCustomerId)) + '%') " 
                    + "AND   (@QuickbooksAccessToken IS NULL OR @QuickbooksAccessToken = '' OR [OurCustomer].[QuickbooksAccessToken] LIKE LTRIM(RTRIM(@QuickbooksAccessToken)) + '%') " 
                    + "AND   (@QuickbooksSecretToken IS NULL OR @QuickbooksSecretToken = '' OR [OurCustomer].[QuickbooksSecretToken] LIKE LTRIM(RTRIM(@QuickbooksSecretToken)) + '%') " 
                    + "AND   (@QbSalesAccount IS NULL OR @QbSalesAccount = '' OR [OurCustomer].[QbSalesAccount] LIKE LTRIM(RTRIM(@QbSalesAccount)) + '%') " 
                    + "AND   (@QbSalesTax IS NULL OR @QbSalesTax = '' OR [OurCustomer].[QbSalesTax] LIKE LTRIM(RTRIM(@QbSalesTax)) + '%') " 
                    + "AND   (@QbSalesDiscounts IS NULL OR @QbSalesDiscounts = '' OR [OurCustomer].[QbSalesDiscounts] LIKE LTRIM(RTRIM(@QbSalesDiscounts)) + '%') " 
                    + "AND   (@QbFreightIncome IS NULL OR @QbFreightIncome = '' OR [OurCustomer].[QbFreightIncome] LIKE LTRIM(RTRIM(@QbFreightIncome)) + '%') " 
                    + "AND   (@QbCash IS NULL OR @QbCash = '' OR [OurCustomer].[QbCash] LIKE LTRIM(RTRIM(@QbCash)) + '%') " 
                    + "AND   (@QbCostOfGoods IS NULL OR @QbCostOfGoods = '' OR [OurCustomer].[QbCostOfGoods] LIKE LTRIM(RTRIM(@QbCostOfGoods)) + '%') " 
                    + "AND   (@QbUndepositiedFunds IS NULL OR @QbUndepositiedFunds = '' OR [OurCustomer].[QbUndepositiedFunds] LIKE LTRIM(RTRIM(@QbUndepositiedFunds)) + '%') " 
                    + "AND   (@QbSalesId IS NULL OR @QbSalesId = '' OR [OurCustomer].[QbSalesId] LIKE LTRIM(RTRIM(@QbSalesId)) + '%') " 
                    + "AND   (@QbSalesTaxId IS NULL OR @QbSalesTaxId = '' OR [OurCustomer].[QbSalesTaxId] LIKE LTRIM(RTRIM(@QbSalesTaxId)) + '%') " 
                    + "AND   (@QbDiscountsId IS NULL OR @QbDiscountsId = '' OR [OurCustomer].[QbDiscountsId] LIKE LTRIM(RTRIM(@QbDiscountsId)) + '%') " 
                    + "AND   (@QbFreightId IS NULL OR @QbFreightId = '' OR [OurCustomer].[QbFreightId] LIKE LTRIM(RTRIM(@QbFreightId)) + '%') " 
                    + "AND   (@QbCashId IS NULL OR @QbCashId = '' OR [OurCustomer].[QbCashId] LIKE LTRIM(RTRIM(@QbCashId)) + '%') " 
                    + "AND   (@QbCostofGoodsId IS NULL OR @QbCostofGoodsId = '' OR [OurCustomer].[QbCostofGoodsId] LIKE LTRIM(RTRIM(@QbCostofGoodsId)) + '%') " 
                    + "AND   (@QbUndepositedFundsId IS NULL OR @QbUndepositedFundsId = '' OR [OurCustomer].[QbUndepositedFundsId] LIKE LTRIM(RTRIM(@QbUndepositedFundsId)) + '%') " 
                    + "";
            } else if  (sCondition == "More than...") {
                selectStatement
                    = "SELECT "
                + "     [OurCustomer].[UserId] "
                + "    ,[OurCustomer].[QbCustomerId] "
                + "    ,[OurCustomer].[QuickbooksAccessToken] "
                + "    ,[OurCustomer].[QuickbooksSecretToken] "
                + "    ,[OurCustomer].[QbSalesAccount] "
                + "    ,[OurCustomer].[QbSalesTax] "
                + "    ,[OurCustomer].[QbSalesDiscounts] "
                + "    ,[OurCustomer].[QbFreightIncome] "
                + "    ,[OurCustomer].[QbCash] "
                + "    ,[OurCustomer].[QbCostOfGoods] "
                + "    ,[OurCustomer].[QbUndepositiedFunds] "
                + "    ,[OurCustomer].[QbSalesId] "
                + "    ,[OurCustomer].[QbSalesTaxId] "
                + "    ,[OurCustomer].[QbDiscountsId] "
                + "    ,[OurCustomer].[QbFreightId] "
                + "    ,[OurCustomer].[QbCashId] "
                + "    ,[OurCustomer].[QbCostofGoodsId] "
                + "    ,[OurCustomer].[QbUndepositedFundsId] "
                + "FROM " 
                + "     [OurCustomer] " 
                    + "WHERE " 
                    + "     (@UserId IS NULL OR @UserId = '' OR [OurCustomer].[UserId] > LTRIM(RTRIM(@UserId))) " 
                    + "AND   (@QbCustomerId IS NULL OR @QbCustomerId = '' OR [OurCustomer].[QbCustomerId] > LTRIM(RTRIM(@QbCustomerId))) " 
                    + "AND   (@QuickbooksAccessToken IS NULL OR @QuickbooksAccessToken = '' OR [OurCustomer].[QuickbooksAccessToken] > LTRIM(RTRIM(@QuickbooksAccessToken))) " 
                    + "AND   (@QuickbooksSecretToken IS NULL OR @QuickbooksSecretToken = '' OR [OurCustomer].[QuickbooksSecretToken] > LTRIM(RTRIM(@QuickbooksSecretToken))) " 
                    + "AND   (@QbSalesAccount IS NULL OR @QbSalesAccount = '' OR [OurCustomer].[QbSalesAccount] > LTRIM(RTRIM(@QbSalesAccount))) " 
                    + "AND   (@QbSalesTax IS NULL OR @QbSalesTax = '' OR [OurCustomer].[QbSalesTax] > LTRIM(RTRIM(@QbSalesTax))) " 
                    + "AND   (@QbSalesDiscounts IS NULL OR @QbSalesDiscounts = '' OR [OurCustomer].[QbSalesDiscounts] > LTRIM(RTRIM(@QbSalesDiscounts))) " 
                    + "AND   (@QbFreightIncome IS NULL OR @QbFreightIncome = '' OR [OurCustomer].[QbFreightIncome] > LTRIM(RTRIM(@QbFreightIncome))) " 
                    + "AND   (@QbCash IS NULL OR @QbCash = '' OR [OurCustomer].[QbCash] > LTRIM(RTRIM(@QbCash))) " 
                    + "AND   (@QbCostOfGoods IS NULL OR @QbCostOfGoods = '' OR [OurCustomer].[QbCostOfGoods] > LTRIM(RTRIM(@QbCostOfGoods))) " 
                    + "AND   (@QbUndepositiedFunds IS NULL OR @QbUndepositiedFunds = '' OR [OurCustomer].[QbUndepositiedFunds] > LTRIM(RTRIM(@QbUndepositiedFunds))) " 
                    + "AND   (@QbSalesId IS NULL OR @QbSalesId = '' OR [OurCustomer].[QbSalesId] > LTRIM(RTRIM(@QbSalesId))) " 
                    + "AND   (@QbSalesTaxId IS NULL OR @QbSalesTaxId = '' OR [OurCustomer].[QbSalesTaxId] > LTRIM(RTRIM(@QbSalesTaxId))) " 
                    + "AND   (@QbDiscountsId IS NULL OR @QbDiscountsId = '' OR [OurCustomer].[QbDiscountsId] > LTRIM(RTRIM(@QbDiscountsId))) " 
                    + "AND   (@QbFreightId IS NULL OR @QbFreightId = '' OR [OurCustomer].[QbFreightId] > LTRIM(RTRIM(@QbFreightId))) " 
                    + "AND   (@QbCashId IS NULL OR @QbCashId = '' OR [OurCustomer].[QbCashId] > LTRIM(RTRIM(@QbCashId))) " 
                    + "AND   (@QbCostofGoodsId IS NULL OR @QbCostofGoodsId = '' OR [OurCustomer].[QbCostofGoodsId] > LTRIM(RTRIM(@QbCostofGoodsId))) " 
                    + "AND   (@QbUndepositedFundsId IS NULL OR @QbUndepositedFundsId = '' OR [OurCustomer].[QbUndepositedFundsId] > LTRIM(RTRIM(@QbUndepositedFundsId))) " 
                    + "";
            } else if  (sCondition == "Less than...") {
                selectStatement
                    = "SELECT " 
                + "     [OurCustomer].[UserId] "
                + "    ,[OurCustomer].[QbCustomerId] "
                + "    ,[OurCustomer].[QuickbooksAccessToken] "
                + "    ,[OurCustomer].[QuickbooksSecretToken] "
                + "    ,[OurCustomer].[QbSalesAccount] "
                + "    ,[OurCustomer].[QbSalesTax] "
                + "    ,[OurCustomer].[QbSalesDiscounts] "
                + "    ,[OurCustomer].[QbFreightIncome] "
                + "    ,[OurCustomer].[QbCash] "
                + "    ,[OurCustomer].[QbCostOfGoods] "
                + "    ,[OurCustomer].[QbUndepositiedFunds] "
                + "    ,[OurCustomer].[QbSalesId] "
                + "    ,[OurCustomer].[QbSalesTaxId] "
                + "    ,[OurCustomer].[QbDiscountsId] "
                + "    ,[OurCustomer].[QbFreightId] "
                + "    ,[OurCustomer].[QbCashId] "
                + "    ,[OurCustomer].[QbCostofGoodsId] "
                + "    ,[OurCustomer].[QbUndepositedFundsId] "
                + "FROM " 
                + "     [OurCustomer] " 
                    + "WHERE " 
                    + "     (@UserId IS NULL OR @UserId = '' OR [OurCustomer].[UserId] < LTRIM(RTRIM(@UserId))) " 
                    + "AND   (@QbCustomerId IS NULL OR @QbCustomerId = '' OR [OurCustomer].[QbCustomerId] < LTRIM(RTRIM(@QbCustomerId))) " 
                    + "AND   (@QuickbooksAccessToken IS NULL OR @QuickbooksAccessToken = '' OR [OurCustomer].[QuickbooksAccessToken] < LTRIM(RTRIM(@QuickbooksAccessToken))) " 
                    + "AND   (@QuickbooksSecretToken IS NULL OR @QuickbooksSecretToken = '' OR [OurCustomer].[QuickbooksSecretToken] < LTRIM(RTRIM(@QuickbooksSecretToken))) " 
                    + "AND   (@QbSalesAccount IS NULL OR @QbSalesAccount = '' OR [OurCustomer].[QbSalesAccount] < LTRIM(RTRIM(@QbSalesAccount))) " 
                    + "AND   (@QbSalesTax IS NULL OR @QbSalesTax = '' OR [OurCustomer].[QbSalesTax] < LTRIM(RTRIM(@QbSalesTax))) " 
                    + "AND   (@QbSalesDiscounts IS NULL OR @QbSalesDiscounts = '' OR [OurCustomer].[QbSalesDiscounts] < LTRIM(RTRIM(@QbSalesDiscounts))) " 
                    + "AND   (@QbFreightIncome IS NULL OR @QbFreightIncome = '' OR [OurCustomer].[QbFreightIncome] < LTRIM(RTRIM(@QbFreightIncome))) " 
                    + "AND   (@QbCash IS NULL OR @QbCash = '' OR [OurCustomer].[QbCash] < LTRIM(RTRIM(@QbCash))) " 
                    + "AND   (@QbCostOfGoods IS NULL OR @QbCostOfGoods = '' OR [OurCustomer].[QbCostOfGoods] < LTRIM(RTRIM(@QbCostOfGoods))) " 
                    + "AND   (@QbUndepositiedFunds IS NULL OR @QbUndepositiedFunds = '' OR [OurCustomer].[QbUndepositiedFunds] < LTRIM(RTRIM(@QbUndepositiedFunds))) " 
                    + "AND   (@QbSalesId IS NULL OR @QbSalesId = '' OR [OurCustomer].[QbSalesId] < LTRIM(RTRIM(@QbSalesId))) " 
                    + "AND   (@QbSalesTaxId IS NULL OR @QbSalesTaxId = '' OR [OurCustomer].[QbSalesTaxId] < LTRIM(RTRIM(@QbSalesTaxId))) " 
                    + "AND   (@QbDiscountsId IS NULL OR @QbDiscountsId = '' OR [OurCustomer].[QbDiscountsId] < LTRIM(RTRIM(@QbDiscountsId))) " 
                    + "AND   (@QbFreightId IS NULL OR @QbFreightId = '' OR [OurCustomer].[QbFreightId] < LTRIM(RTRIM(@QbFreightId))) " 
                    + "AND   (@QbCashId IS NULL OR @QbCashId = '' OR [OurCustomer].[QbCashId] < LTRIM(RTRIM(@QbCashId))) " 
                    + "AND   (@QbCostofGoodsId IS NULL OR @QbCostofGoodsId = '' OR [OurCustomer].[QbCostofGoodsId] < LTRIM(RTRIM(@QbCostofGoodsId))) " 
                    + "AND   (@QbUndepositedFundsId IS NULL OR @QbUndepositedFundsId = '' OR [OurCustomer].[QbUndepositedFundsId] < LTRIM(RTRIM(@QbUndepositedFundsId))) " 
                    + "";
            } else if  (sCondition == "Equal or more than...") {
                selectStatement
                    = "SELECT "
                + "     [OurCustomer].[UserId] "
                + "    ,[OurCustomer].[QbCustomerId] "
                + "    ,[OurCustomer].[QuickbooksAccessToken] "
                + "    ,[OurCustomer].[QuickbooksSecretToken] "
                + "    ,[OurCustomer].[QbSalesAccount] "
                + "    ,[OurCustomer].[QbSalesTax] "
                + "    ,[OurCustomer].[QbSalesDiscounts] "
                + "    ,[OurCustomer].[QbFreightIncome] "
                + "    ,[OurCustomer].[QbCash] "
                + "    ,[OurCustomer].[QbCostOfGoods] "
                + "    ,[OurCustomer].[QbUndepositiedFunds] "
                + "    ,[OurCustomer].[QbSalesId] "
                + "    ,[OurCustomer].[QbSalesTaxId] "
                + "    ,[OurCustomer].[QbDiscountsId] "
                + "    ,[OurCustomer].[QbFreightId] "
                + "    ,[OurCustomer].[QbCashId] "
                + "    ,[OurCustomer].[QbCostofGoodsId] "
                + "    ,[OurCustomer].[QbUndepositedFundsId] "
                + "FROM " 
                + "     [OurCustomer] " 
                    + "WHERE " 
                    + "     (@UserId IS NULL OR @UserId = '' OR [OurCustomer].[UserId] >= LTRIM(RTRIM(@UserId))) " 
                    + "AND   (@QbCustomerId IS NULL OR @QbCustomerId = '' OR [OurCustomer].[QbCustomerId] >= LTRIM(RTRIM(@QbCustomerId))) " 
                    + "AND   (@QuickbooksAccessToken IS NULL OR @QuickbooksAccessToken = '' OR [OurCustomer].[QuickbooksAccessToken] >= LTRIM(RTRIM(@QuickbooksAccessToken))) " 
                    + "AND   (@QuickbooksSecretToken IS NULL OR @QuickbooksSecretToken = '' OR [OurCustomer].[QuickbooksSecretToken] >= LTRIM(RTRIM(@QuickbooksSecretToken))) " 
                    + "AND   (@QbSalesAccount IS NULL OR @QbSalesAccount = '' OR [OurCustomer].[QbSalesAccount] >= LTRIM(RTRIM(@QbSalesAccount))) " 
                    + "AND   (@QbSalesTax IS NULL OR @QbSalesTax = '' OR [OurCustomer].[QbSalesTax] >= LTRIM(RTRIM(@QbSalesTax))) " 
                    + "AND   (@QbSalesDiscounts IS NULL OR @QbSalesDiscounts = '' OR [OurCustomer].[QbSalesDiscounts] >= LTRIM(RTRIM(@QbSalesDiscounts))) " 
                    + "AND   (@QbFreightIncome IS NULL OR @QbFreightIncome = '' OR [OurCustomer].[QbFreightIncome] >= LTRIM(RTRIM(@QbFreightIncome))) " 
                    + "AND   (@QbCash IS NULL OR @QbCash = '' OR [OurCustomer].[QbCash] >= LTRIM(RTRIM(@QbCash))) " 
                    + "AND   (@QbCostOfGoods IS NULL OR @QbCostOfGoods = '' OR [OurCustomer].[QbCostOfGoods] >= LTRIM(RTRIM(@QbCostOfGoods))) " 
                    + "AND   (@QbUndepositiedFunds IS NULL OR @QbUndepositiedFunds = '' OR [OurCustomer].[QbUndepositiedFunds] >= LTRIM(RTRIM(@QbUndepositiedFunds))) " 
                    + "AND   (@QbSalesId IS NULL OR @QbSalesId = '' OR [OurCustomer].[QbSalesId] >= LTRIM(RTRIM(@QbSalesId))) " 
                    + "AND   (@QbSalesTaxId IS NULL OR @QbSalesTaxId = '' OR [OurCustomer].[QbSalesTaxId] >= LTRIM(RTRIM(@QbSalesTaxId))) " 
                    + "AND   (@QbDiscountsId IS NULL OR @QbDiscountsId = '' OR [OurCustomer].[QbDiscountsId] >= LTRIM(RTRIM(@QbDiscountsId))) " 
                    + "AND   (@QbFreightId IS NULL OR @QbFreightId = '' OR [OurCustomer].[QbFreightId] >= LTRIM(RTRIM(@QbFreightId))) " 
                    + "AND   (@QbCashId IS NULL OR @QbCashId = '' OR [OurCustomer].[QbCashId] >= LTRIM(RTRIM(@QbCashId))) " 
                    + "AND   (@QbCostofGoodsId IS NULL OR @QbCostofGoodsId = '' OR [OurCustomer].[QbCostofGoodsId] >= LTRIM(RTRIM(@QbCostofGoodsId))) " 
                    + "AND   (@QbUndepositedFundsId IS NULL OR @QbUndepositedFundsId = '' OR [OurCustomer].[QbUndepositedFundsId] >= LTRIM(RTRIM(@QbUndepositedFundsId))) " 
                    + "";
            } else if (sCondition == "Equal or less than...") {
                selectStatement 
                    = "SELECT "
                + "     [OurCustomer].[UserId] "
                + "    ,[OurCustomer].[QbCustomerId] "
                + "    ,[OurCustomer].[QuickbooksAccessToken] "
                + "    ,[OurCustomer].[QuickbooksSecretToken] "
                + "    ,[OurCustomer].[QbSalesAccount] "
                + "    ,[OurCustomer].[QbSalesTax] "
                + "    ,[OurCustomer].[QbSalesDiscounts] "
                + "    ,[OurCustomer].[QbFreightIncome] "
                + "    ,[OurCustomer].[QbCash] "
                + "    ,[OurCustomer].[QbCostOfGoods] "
                + "    ,[OurCustomer].[QbUndepositiedFunds] "
                + "    ,[OurCustomer].[QbSalesId] "
                + "    ,[OurCustomer].[QbSalesTaxId] "
                + "    ,[OurCustomer].[QbDiscountsId] "
                + "    ,[OurCustomer].[QbFreightId] "
                + "    ,[OurCustomer].[QbCashId] "
                + "    ,[OurCustomer].[QbCostofGoodsId] "
                + "    ,[OurCustomer].[QbUndepositedFundsId] "
                + "FROM " 
                + "     [OurCustomer] " 
                    + "WHERE " 
                    + "     (@UserId IS NULL OR @UserId = '' OR [OurCustomer].[UserId] <= LTRIM(RTRIM(@UserId))) " 
                    + "AND   (@QbCustomerId IS NULL OR @QbCustomerId = '' OR [OurCustomer].[QbCustomerId] <= LTRIM(RTRIM(@QbCustomerId))) " 
                    + "AND   (@QuickbooksAccessToken IS NULL OR @QuickbooksAccessToken = '' OR [OurCustomer].[QuickbooksAccessToken] <= LTRIM(RTRIM(@QuickbooksAccessToken))) " 
                    + "AND   (@QuickbooksSecretToken IS NULL OR @QuickbooksSecretToken = '' OR [OurCustomer].[QuickbooksSecretToken] <= LTRIM(RTRIM(@QuickbooksSecretToken))) " 
                    + "AND   (@QbSalesAccount IS NULL OR @QbSalesAccount = '' OR [OurCustomer].[QbSalesAccount] <= LTRIM(RTRIM(@QbSalesAccount))) " 
                    + "AND   (@QbSalesTax IS NULL OR @QbSalesTax = '' OR [OurCustomer].[QbSalesTax] <= LTRIM(RTRIM(@QbSalesTax))) " 
                    + "AND   (@QbSalesDiscounts IS NULL OR @QbSalesDiscounts = '' OR [OurCustomer].[QbSalesDiscounts] <= LTRIM(RTRIM(@QbSalesDiscounts))) " 
                    + "AND   (@QbFreightIncome IS NULL OR @QbFreightIncome = '' OR [OurCustomer].[QbFreightIncome] <= LTRIM(RTRIM(@QbFreightIncome))) " 
                    + "AND   (@QbCash IS NULL OR @QbCash = '' OR [OurCustomer].[QbCash] <= LTRIM(RTRIM(@QbCash))) " 
                    + "AND   (@QbCostOfGoods IS NULL OR @QbCostOfGoods = '' OR [OurCustomer].[QbCostOfGoods] <= LTRIM(RTRIM(@QbCostOfGoods))) " 
                    + "AND   (@QbUndepositiedFunds IS NULL OR @QbUndepositiedFunds = '' OR [OurCustomer].[QbUndepositiedFunds] <= LTRIM(RTRIM(@QbUndepositiedFunds))) " 
                    + "AND   (@QbSalesId IS NULL OR @QbSalesId = '' OR [OurCustomer].[QbSalesId] <= LTRIM(RTRIM(@QbSalesId))) " 
                    + "AND   (@QbSalesTaxId IS NULL OR @QbSalesTaxId = '' OR [OurCustomer].[QbSalesTaxId] <= LTRIM(RTRIM(@QbSalesTaxId))) " 
                    + "AND   (@QbDiscountsId IS NULL OR @QbDiscountsId = '' OR [OurCustomer].[QbDiscountsId] <= LTRIM(RTRIM(@QbDiscountsId))) " 
                    + "AND   (@QbFreightId IS NULL OR @QbFreightId = '' OR [OurCustomer].[QbFreightId] <= LTRIM(RTRIM(@QbFreightId))) " 
                    + "AND   (@QbCashId IS NULL OR @QbCashId = '' OR [OurCustomer].[QbCashId] <= LTRIM(RTRIM(@QbCashId))) " 
                    + "AND   (@QbCostofGoodsId IS NULL OR @QbCostofGoodsId = '' OR [OurCustomer].[QbCostofGoodsId] <= LTRIM(RTRIM(@QbCostofGoodsId))) " 
                    + "AND   (@QbUndepositedFundsId IS NULL OR @QbUndepositedFundsId = '' OR [OurCustomer].[QbUndepositedFundsId] <= LTRIM(RTRIM(@QbUndepositedFundsId))) " 
                    + "";
            }
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            if (sField == "User Id") {
                selectCommand.Parameters.AddWithValue("@UserId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@UserId", DBNull.Value); }
            if (sField == "Qb Customer Id") {
                selectCommand.Parameters.AddWithValue("@QbCustomerId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QbCustomerId", DBNull.Value); }
            if (sField == "Quickbooks Access Token") {
                selectCommand.Parameters.AddWithValue("@QuickbooksAccessToken", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QuickbooksAccessToken", DBNull.Value); }
            if (sField == "Quickbooks Secret Token") {
                selectCommand.Parameters.AddWithValue("@QuickbooksSecretToken", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QuickbooksSecretToken", DBNull.Value); }
            if (sField == "Qb Sales Account") {
                selectCommand.Parameters.AddWithValue("@QbSalesAccount", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QbSalesAccount", DBNull.Value); }
            if (sField == "Qb Sales Tax") {
                selectCommand.Parameters.AddWithValue("@QbSalesTax", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QbSalesTax", DBNull.Value); }
            if (sField == "Qb Sales Discounts") {
                selectCommand.Parameters.AddWithValue("@QbSalesDiscounts", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QbSalesDiscounts", DBNull.Value); }
            if (sField == "Qb Freight Income") {
                selectCommand.Parameters.AddWithValue("@QbFreightIncome", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QbFreightIncome", DBNull.Value); }
            if (sField == "Qb Cash") {
                selectCommand.Parameters.AddWithValue("@QbCash", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QbCash", DBNull.Value); }
            if (sField == "Qb Cost Of Goods") {
                selectCommand.Parameters.AddWithValue("@QbCostOfGoods", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QbCostOfGoods", DBNull.Value); }
            if (sField == "Qb Undepositied Funds") {
                selectCommand.Parameters.AddWithValue("@QbUndepositiedFunds", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QbUndepositiedFunds", DBNull.Value); }
            if (sField == "Qb Sales Id") {
                selectCommand.Parameters.AddWithValue("@QbSalesId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QbSalesId", DBNull.Value); }
            if (sField == "Qb Sales Tax Id") {
                selectCommand.Parameters.AddWithValue("@QbSalesTaxId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QbSalesTaxId", DBNull.Value); }
            if (sField == "Qb Discounts Id") {
                selectCommand.Parameters.AddWithValue("@QbDiscountsId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QbDiscountsId", DBNull.Value); }
            if (sField == "Qb Freight Id") {
                selectCommand.Parameters.AddWithValue("@QbFreightId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QbFreightId", DBNull.Value); }
            if (sField == "Qb Cash Id") {
                selectCommand.Parameters.AddWithValue("@QbCashId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QbCashId", DBNull.Value); }
            if (sField == "Qb Costof Goods Id") {
                selectCommand.Parameters.AddWithValue("@QbCostofGoodsId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QbCostofGoodsId", DBNull.Value); }
            if (sField == "Qb Undeposited Funds Id") {
                selectCommand.Parameters.AddWithValue("@QbUndepositedFundsId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QbUndepositedFundsId", DBNull.Value); }
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

        public static OurCustomer Select_Record(OurCustomer OurCustomerPara)
        {
        OurCustomer OurCustomer = new OurCustomer();
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement
            = "SELECT " 
                + "     [UserId] "
                + "    ,[QbCustomerId] "
                + "    ,[QuickbooksAccessToken] "
                + "    ,[QuickbooksSecretToken] "
                + "    ,[QbSalesAccount] "
                + "    ,[QbSalesTax] "
                + "    ,[QbSalesDiscounts] "
                + "    ,[QbFreightIncome] "
                + "    ,[QbCash] "
                + "    ,[QbCostOfGoods] "
                + "    ,[QbUndepositiedFunds] "
                + "    ,[QbSalesId] "
                + "    ,[QbSalesTaxId] "
                + "    ,[QbDiscountsId] "
                + "    ,[QbFreightId] "
                + "    ,[QbCashId] "
                + "    ,[QbCostofGoodsId] "
                + "    ,[QbUndepositedFundsId] "
                + "FROM "
                + "     [OurCustomer] "
                + "WHERE "
                + "     [UserId] = @UserId "
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            selectCommand.Parameters.AddWithValue("@UserId", OurCustomerPara.UserId);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    OurCustomer.UserId = System.Convert.ToInt32(reader["UserId"]);
                    OurCustomer.QbCustomerId = reader["QbCustomerId"] is DBNull ? null : reader["QbCustomerId"].ToString();
                    OurCustomer.QuickbooksAccessToken = reader["QuickbooksAccessToken"] is DBNull ? null : reader["QuickbooksAccessToken"].ToString();
                    OurCustomer.QuickbooksSecretToken = reader["QuickbooksSecretToken"] is DBNull ? null : reader["QuickbooksSecretToken"].ToString();
                    OurCustomer.QbSalesAccount = reader["QbSalesAccount"] is DBNull ? null : reader["QbSalesAccount"].ToString();
                    OurCustomer.QbSalesTax = reader["QbSalesTax"] is DBNull ? null : reader["QbSalesTax"].ToString();
                    OurCustomer.QbSalesDiscounts = reader["QbSalesDiscounts"] is DBNull ? null : reader["QbSalesDiscounts"].ToString();
                    OurCustomer.QbFreightIncome = reader["QbFreightIncome"] is DBNull ? null : reader["QbFreightIncome"].ToString();
                    OurCustomer.QbCash = reader["QbCash"] is DBNull ? null : reader["QbCash"].ToString();
                    OurCustomer.QbCostOfGoods = reader["QbCostOfGoods"] is DBNull ? null : reader["QbCostOfGoods"].ToString();
                    OurCustomer.QbUndepositiedFunds = reader["QbUndepositiedFunds"] is DBNull ? null : reader["QbUndepositiedFunds"].ToString();
                    OurCustomer.QbSalesId = reader["QbSalesId"] is DBNull ? null : (Int32?)reader["QbSalesId"];
                    OurCustomer.QbSalesTaxId = reader["QbSalesTaxId"] is DBNull ? null : (Int32?)reader["QbSalesTaxId"];
                    OurCustomer.QbDiscountsId = reader["QbDiscountsId"] is DBNull ? null : (Int32?)reader["QbDiscountsId"];
                    OurCustomer.QbFreightId = reader["QbFreightId"] is DBNull ? null : (Int32?)reader["QbFreightId"];
                    OurCustomer.QbCashId = reader["QbCashId"] is DBNull ? null : (Int32?)reader["QbCashId"];
                    OurCustomer.QbCostofGoodsId = reader["QbCostofGoodsId"] is DBNull ? null : (Int32?)reader["QbCostofGoodsId"];
                    OurCustomer.QbUndepositedFundsId = reader["QbUndepositedFundsId"] is DBNull ? null : (Int32?)reader["QbUndepositedFundsId"];
                }
                else
                {
                    OurCustomer = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return OurCustomer;
            }
            finally
            {
                connection.Close();
            }
            return OurCustomer;
        }

        public static bool Add(OurCustomer OurCustomer)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string insertStatement
                = "INSERT " 
                + "     [OurCustomer] "
                + "     ( "
                + "     [QbCustomerId] "
                + "    ,[QuickbooksAccessToken] "
                + "    ,[QuickbooksSecretToken] "
                + "    ,[QbSalesAccount] "
                + "    ,[QbSalesTax] "
                + "    ,[QbSalesDiscounts] "
                + "    ,[QbFreightIncome] "
                + "    ,[QbCash] "
                + "    ,[QbCostOfGoods] "
                + "    ,[QbUndepositiedFunds] "
                + "    ,[QbSalesId] "
                + "    ,[QbSalesTaxId] "
                + "    ,[QbDiscountsId] "
                + "    ,[QbFreightId] "
                + "    ,[QbCashId] "
                + "    ,[QbCostofGoodsId] "
                + "    ,[QbUndepositedFundsId] "
                + "     ) "
                + "VALUES " 
                + "     ( "
                + "     @QbCustomerId "
                + "    ,@QuickbooksAccessToken "
                + "    ,@QuickbooksSecretToken "
                + "    ,@QbSalesAccount "
                + "    ,@QbSalesTax "
                + "    ,@QbSalesDiscounts "
                + "    ,@QbFreightIncome "
                + "    ,@QbCash "
                + "    ,@QbCostOfGoods "
                + "    ,@QbUndepositiedFunds "
                + "    ,@QbSalesId "
                + "    ,@QbSalesTaxId "
                + "    ,@QbDiscountsId "
                + "    ,@QbFreightId "
                + "    ,@QbCashId "
                + "    ,@QbCostofGoodsId "
                + "    ,@QbUndepositedFundsId "
                + "     ) "
                + "";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.CommandType = CommandType.Text;
            if (OurCustomer.QbCustomerId != null) {
                insertCommand.Parameters.AddWithValue("@QbCustomerId", OurCustomer.QbCustomerId);
            } else {
                insertCommand.Parameters.AddWithValue("@QbCustomerId", DBNull.Value); }
            if (OurCustomer.QuickbooksAccessToken != null) {
                insertCommand.Parameters.AddWithValue("@QuickbooksAccessToken", OurCustomer.QuickbooksAccessToken);
            } else {
                insertCommand.Parameters.AddWithValue("@QuickbooksAccessToken", DBNull.Value); }
            if (OurCustomer.QuickbooksSecretToken != null) {
                insertCommand.Parameters.AddWithValue("@QuickbooksSecretToken", OurCustomer.QuickbooksSecretToken);
            } else {
                insertCommand.Parameters.AddWithValue("@QuickbooksSecretToken", DBNull.Value); }
            if (OurCustomer.QbSalesAccount != null) {
                insertCommand.Parameters.AddWithValue("@QbSalesAccount", OurCustomer.QbSalesAccount);
            } else {
                insertCommand.Parameters.AddWithValue("@QbSalesAccount", DBNull.Value); }
            if (OurCustomer.QbSalesTax != null) {
                insertCommand.Parameters.AddWithValue("@QbSalesTax", OurCustomer.QbSalesTax);
            } else {
                insertCommand.Parameters.AddWithValue("@QbSalesTax", DBNull.Value); }
            if (OurCustomer.QbSalesDiscounts != null) {
                insertCommand.Parameters.AddWithValue("@QbSalesDiscounts", OurCustomer.QbSalesDiscounts);
            } else {
                insertCommand.Parameters.AddWithValue("@QbSalesDiscounts", DBNull.Value); }
            if (OurCustomer.QbFreightIncome != null) {
                insertCommand.Parameters.AddWithValue("@QbFreightIncome", OurCustomer.QbFreightIncome);
            } else {
                insertCommand.Parameters.AddWithValue("@QbFreightIncome", DBNull.Value); }
            if (OurCustomer.QbCash != null) {
                insertCommand.Parameters.AddWithValue("@QbCash", OurCustomer.QbCash);
            } else {
                insertCommand.Parameters.AddWithValue("@QbCash", DBNull.Value); }
            if (OurCustomer.QbCostOfGoods != null) {
                insertCommand.Parameters.AddWithValue("@QbCostOfGoods", OurCustomer.QbCostOfGoods);
            } else {
                insertCommand.Parameters.AddWithValue("@QbCostOfGoods", DBNull.Value); }
            if (OurCustomer.QbUndepositiedFunds != null) {
                insertCommand.Parameters.AddWithValue("@QbUndepositiedFunds", OurCustomer.QbUndepositiedFunds);
            } else {
                insertCommand.Parameters.AddWithValue("@QbUndepositiedFunds", DBNull.Value); }
            if (OurCustomer.QbSalesId.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@QbSalesId", OurCustomer.QbSalesId);
            } else {
                insertCommand.Parameters.AddWithValue("@QbSalesId", DBNull.Value); }
            if (OurCustomer.QbSalesTaxId.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@QbSalesTaxId", OurCustomer.QbSalesTaxId);
            } else {
                insertCommand.Parameters.AddWithValue("@QbSalesTaxId", DBNull.Value); }
            if (OurCustomer.QbDiscountsId.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@QbDiscountsId", OurCustomer.QbDiscountsId);
            } else {
                insertCommand.Parameters.AddWithValue("@QbDiscountsId", DBNull.Value); }
            if (OurCustomer.QbFreightId.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@QbFreightId", OurCustomer.QbFreightId);
            } else {
                insertCommand.Parameters.AddWithValue("@QbFreightId", DBNull.Value); }
            if (OurCustomer.QbCashId.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@QbCashId", OurCustomer.QbCashId);
            } else {
                insertCommand.Parameters.AddWithValue("@QbCashId", DBNull.Value); }
            if (OurCustomer.QbCostofGoodsId.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@QbCostofGoodsId", OurCustomer.QbCostofGoodsId);
            } else {
                insertCommand.Parameters.AddWithValue("@QbCostofGoodsId", DBNull.Value); }
            if (OurCustomer.QbUndepositedFundsId.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@QbUndepositedFundsId", OurCustomer.QbUndepositedFundsId);
            } else {
                insertCommand.Parameters.AddWithValue("@QbUndepositedFundsId", DBNull.Value); }
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

        public static bool Update(OurCustomer oldOurCustomer, 
               OurCustomer newOurCustomer)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string updateStatement
                = "UPDATE "  
                + "     [OurCustomer] "
                + "SET "
                + "     [QbCustomerId] = @NewQbCustomerId "
                + "    ,[QuickbooksAccessToken] = @NewQuickbooksAccessToken "
                + "    ,[QuickbooksSecretToken] = @NewQuickbooksSecretToken "
                + "    ,[QbSalesAccount] = @NewQbSalesAccount "
                + "    ,[QbSalesTax] = @NewQbSalesTax "
                + "    ,[QbSalesDiscounts] = @NewQbSalesDiscounts "
                + "    ,[QbFreightIncome] = @NewQbFreightIncome "
                + "    ,[QbCash] = @NewQbCash "
                + "    ,[QbCostOfGoods] = @NewQbCostOfGoods "
                + "    ,[QbUndepositiedFunds] = @NewQbUndepositiedFunds "
                + "    ,[QbSalesId] = @NewQbSalesId "
                + "    ,[QbSalesTaxId] = @NewQbSalesTaxId "
                + "    ,[QbDiscountsId] = @NewQbDiscountsId "
                + "    ,[QbFreightId] = @NewQbFreightId "
                + "    ,[QbCashId] = @NewQbCashId "
                + "    ,[QbCostofGoodsId] = @NewQbCostofGoodsId "
                + "    ,[QbUndepositedFundsId] = @NewQbUndepositedFundsId "
                + "WHERE "
                + "     [UserId] = @OldUserId " 
                + " AND ((@OldQbCustomerId IS NULL AND [QbCustomerId] IS NULL) OR [QbCustomerId] = @OldQbCustomerId) " 
                + " AND ((@OldQuickbooksAccessToken IS NULL AND [QuickbooksAccessToken] IS NULL) OR [QuickbooksAccessToken] = @OldQuickbooksAccessToken) " 
                + " AND ((@OldQuickbooksSecretToken IS NULL AND [QuickbooksSecretToken] IS NULL) OR [QuickbooksSecretToken] = @OldQuickbooksSecretToken) " 
                + " AND ((@OldQbSalesAccount IS NULL AND [QbSalesAccount] IS NULL) OR [QbSalesAccount] = @OldQbSalesAccount) " 
                + " AND ((@OldQbSalesTax IS NULL AND [QbSalesTax] IS NULL) OR [QbSalesTax] = @OldQbSalesTax) " 
                + " AND ((@OldQbSalesDiscounts IS NULL AND [QbSalesDiscounts] IS NULL) OR [QbSalesDiscounts] = @OldQbSalesDiscounts) " 
                + " AND ((@OldQbFreightIncome IS NULL AND [QbFreightIncome] IS NULL) OR [QbFreightIncome] = @OldQbFreightIncome) " 
                + " AND ((@OldQbCash IS NULL AND [QbCash] IS NULL) OR [QbCash] = @OldQbCash) " 
                + " AND ((@OldQbCostOfGoods IS NULL AND [QbCostOfGoods] IS NULL) OR [QbCostOfGoods] = @OldQbCostOfGoods) " 
                + " AND ((@OldQbUndepositiedFunds IS NULL AND [QbUndepositiedFunds] IS NULL) OR [QbUndepositiedFunds] = @OldQbUndepositiedFunds) " 
                + " AND ((@OldQbSalesId IS NULL AND [QbSalesId] IS NULL) OR [QbSalesId] = @OldQbSalesId) " 
                + " AND ((@OldQbSalesTaxId IS NULL AND [QbSalesTaxId] IS NULL) OR [QbSalesTaxId] = @OldQbSalesTaxId) " 
                + " AND ((@OldQbDiscountsId IS NULL AND [QbDiscountsId] IS NULL) OR [QbDiscountsId] = @OldQbDiscountsId) " 
                + " AND ((@OldQbFreightId IS NULL AND [QbFreightId] IS NULL) OR [QbFreightId] = @OldQbFreightId) " 
                + " AND ((@OldQbCashId IS NULL AND [QbCashId] IS NULL) OR [QbCashId] = @OldQbCashId) " 
                + " AND ((@OldQbCostofGoodsId IS NULL AND [QbCostofGoodsId] IS NULL) OR [QbCostofGoodsId] = @OldQbCostofGoodsId) " 
                + " AND ((@OldQbUndepositedFundsId IS NULL AND [QbUndepositedFundsId] IS NULL) OR [QbUndepositedFundsId] = @OldQbUndepositedFundsId) " 
                + "";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            updateCommand.CommandType = CommandType.Text;
            if (newOurCustomer.QbCustomerId != null) {
                updateCommand.Parameters.AddWithValue("@NewQbCustomerId", newOurCustomer.QbCustomerId);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQbCustomerId", DBNull.Value); }
            if (newOurCustomer.QuickbooksAccessToken != null) {
                updateCommand.Parameters.AddWithValue("@NewQuickbooksAccessToken", newOurCustomer.QuickbooksAccessToken);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQuickbooksAccessToken", DBNull.Value); }
            if (newOurCustomer.QuickbooksSecretToken != null) {
                updateCommand.Parameters.AddWithValue("@NewQuickbooksSecretToken", newOurCustomer.QuickbooksSecretToken);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQuickbooksSecretToken", DBNull.Value); }
            if (newOurCustomer.QbSalesAccount != null) {
                updateCommand.Parameters.AddWithValue("@NewQbSalesAccount", newOurCustomer.QbSalesAccount);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQbSalesAccount", DBNull.Value); }
            if (newOurCustomer.QbSalesTax != null) {
                updateCommand.Parameters.AddWithValue("@NewQbSalesTax", newOurCustomer.QbSalesTax);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQbSalesTax", DBNull.Value); }
            if (newOurCustomer.QbSalesDiscounts != null) {
                updateCommand.Parameters.AddWithValue("@NewQbSalesDiscounts", newOurCustomer.QbSalesDiscounts);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQbSalesDiscounts", DBNull.Value); }
            if (newOurCustomer.QbFreightIncome != null) {
                updateCommand.Parameters.AddWithValue("@NewQbFreightIncome", newOurCustomer.QbFreightIncome);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQbFreightIncome", DBNull.Value); }
            if (newOurCustomer.QbCash != null) {
                updateCommand.Parameters.AddWithValue("@NewQbCash", newOurCustomer.QbCash);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQbCash", DBNull.Value); }
            if (newOurCustomer.QbCostOfGoods != null) {
                updateCommand.Parameters.AddWithValue("@NewQbCostOfGoods", newOurCustomer.QbCostOfGoods);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQbCostOfGoods", DBNull.Value); }
            if (newOurCustomer.QbUndepositiedFunds != null) {
                updateCommand.Parameters.AddWithValue("@NewQbUndepositiedFunds", newOurCustomer.QbUndepositiedFunds);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQbUndepositiedFunds", DBNull.Value); }
            if (newOurCustomer.QbSalesId.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewQbSalesId", newOurCustomer.QbSalesId);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQbSalesId", DBNull.Value); }
            if (newOurCustomer.QbSalesTaxId.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewQbSalesTaxId", newOurCustomer.QbSalesTaxId);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQbSalesTaxId", DBNull.Value); }
            if (newOurCustomer.QbDiscountsId.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewQbDiscountsId", newOurCustomer.QbDiscountsId);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQbDiscountsId", DBNull.Value); }
            if (newOurCustomer.QbFreightId.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewQbFreightId", newOurCustomer.QbFreightId);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQbFreightId", DBNull.Value); }
            if (newOurCustomer.QbCashId.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewQbCashId", newOurCustomer.QbCashId);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQbCashId", DBNull.Value); }
            if (newOurCustomer.QbCostofGoodsId.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewQbCostofGoodsId", newOurCustomer.QbCostofGoodsId);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQbCostofGoodsId", DBNull.Value); }
            if (newOurCustomer.QbUndepositedFundsId.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewQbUndepositedFundsId", newOurCustomer.QbUndepositedFundsId);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQbUndepositedFundsId", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldUserId", oldOurCustomer.UserId);
            if (oldOurCustomer.QbCustomerId != null) {
                updateCommand.Parameters.AddWithValue("@OldQbCustomerId", oldOurCustomer.QbCustomerId);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQbCustomerId", DBNull.Value); }
            if (oldOurCustomer.QuickbooksAccessToken != null) {
                updateCommand.Parameters.AddWithValue("@OldQuickbooksAccessToken", oldOurCustomer.QuickbooksAccessToken);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQuickbooksAccessToken", DBNull.Value); }
            if (oldOurCustomer.QuickbooksSecretToken != null) {
                updateCommand.Parameters.AddWithValue("@OldQuickbooksSecretToken", oldOurCustomer.QuickbooksSecretToken);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQuickbooksSecretToken", DBNull.Value); }
            if (oldOurCustomer.QbSalesAccount != null) {
                updateCommand.Parameters.AddWithValue("@OldQbSalesAccount", oldOurCustomer.QbSalesAccount);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQbSalesAccount", DBNull.Value); }
            if (oldOurCustomer.QbSalesTax != null) {
                updateCommand.Parameters.AddWithValue("@OldQbSalesTax", oldOurCustomer.QbSalesTax);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQbSalesTax", DBNull.Value); }
            if (oldOurCustomer.QbSalesDiscounts != null) {
                updateCommand.Parameters.AddWithValue("@OldQbSalesDiscounts", oldOurCustomer.QbSalesDiscounts);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQbSalesDiscounts", DBNull.Value); }
            if (oldOurCustomer.QbFreightIncome != null) {
                updateCommand.Parameters.AddWithValue("@OldQbFreightIncome", oldOurCustomer.QbFreightIncome);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQbFreightIncome", DBNull.Value); }
            if (oldOurCustomer.QbCash != null) {
                updateCommand.Parameters.AddWithValue("@OldQbCash", oldOurCustomer.QbCash);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQbCash", DBNull.Value); }
            if (oldOurCustomer.QbCostOfGoods != null) {
                updateCommand.Parameters.AddWithValue("@OldQbCostOfGoods", oldOurCustomer.QbCostOfGoods);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQbCostOfGoods", DBNull.Value); }
            if (oldOurCustomer.QbUndepositiedFunds != null) {
                updateCommand.Parameters.AddWithValue("@OldQbUndepositiedFunds", oldOurCustomer.QbUndepositiedFunds);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQbUndepositiedFunds", DBNull.Value); }
            if (oldOurCustomer.QbSalesId.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldQbSalesId", oldOurCustomer.QbSalesId);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQbSalesId", DBNull.Value); }
            if (oldOurCustomer.QbSalesTaxId.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldQbSalesTaxId", oldOurCustomer.QbSalesTaxId);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQbSalesTaxId", DBNull.Value); }
            if (oldOurCustomer.QbDiscountsId.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldQbDiscountsId", oldOurCustomer.QbDiscountsId);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQbDiscountsId", DBNull.Value); }
            if (oldOurCustomer.QbFreightId.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldQbFreightId", oldOurCustomer.QbFreightId);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQbFreightId", DBNull.Value); }
            if (oldOurCustomer.QbCashId.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldQbCashId", oldOurCustomer.QbCashId);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQbCashId", DBNull.Value); }
            if (oldOurCustomer.QbCostofGoodsId.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldQbCostofGoodsId", oldOurCustomer.QbCostofGoodsId);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQbCostofGoodsId", DBNull.Value); }
            if (oldOurCustomer.QbUndepositedFundsId.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldQbUndepositedFundsId", oldOurCustomer.QbUndepositedFundsId);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQbUndepositedFundsId", DBNull.Value); }
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

        public static bool Delete(OurCustomer OurCustomer)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string deleteStatement
                = "DELETE FROM "  
                + "     [OurCustomer] "
                + "WHERE " 
                + "     [UserId] = @OldUserId " 
                + " AND ((@OldQbCustomerId IS NULL AND [QbCustomerId] IS NULL) OR [QbCustomerId] = @OldQbCustomerId) " 
                + " AND ((@OldQuickbooksAccessToken IS NULL AND [QuickbooksAccessToken] IS NULL) OR [QuickbooksAccessToken] = @OldQuickbooksAccessToken) " 
                + " AND ((@OldQuickbooksSecretToken IS NULL AND [QuickbooksSecretToken] IS NULL) OR [QuickbooksSecretToken] = @OldQuickbooksSecretToken) " 
                + " AND ((@OldQbSalesAccount IS NULL AND [QbSalesAccount] IS NULL) OR [QbSalesAccount] = @OldQbSalesAccount) " 
                + " AND ((@OldQbSalesTax IS NULL AND [QbSalesTax] IS NULL) OR [QbSalesTax] = @OldQbSalesTax) " 
                + " AND ((@OldQbSalesDiscounts IS NULL AND [QbSalesDiscounts] IS NULL) OR [QbSalesDiscounts] = @OldQbSalesDiscounts) " 
                + " AND ((@OldQbFreightIncome IS NULL AND [QbFreightIncome] IS NULL) OR [QbFreightIncome] = @OldQbFreightIncome) " 
                + " AND ((@OldQbCash IS NULL AND [QbCash] IS NULL) OR [QbCash] = @OldQbCash) " 
                + " AND ((@OldQbCostOfGoods IS NULL AND [QbCostOfGoods] IS NULL) OR [QbCostOfGoods] = @OldQbCostOfGoods) " 
                + " AND ((@OldQbUndepositiedFunds IS NULL AND [QbUndepositiedFunds] IS NULL) OR [QbUndepositiedFunds] = @OldQbUndepositiedFunds) " 
                + " AND ((@OldQbSalesId IS NULL AND [QbSalesId] IS NULL) OR [QbSalesId] = @OldQbSalesId) " 
                + " AND ((@OldQbSalesTaxId IS NULL AND [QbSalesTaxId] IS NULL) OR [QbSalesTaxId] = @OldQbSalesTaxId) " 
                + " AND ((@OldQbDiscountsId IS NULL AND [QbDiscountsId] IS NULL) OR [QbDiscountsId] = @OldQbDiscountsId) " 
                + " AND ((@OldQbFreightId IS NULL AND [QbFreightId] IS NULL) OR [QbFreightId] = @OldQbFreightId) " 
                + " AND ((@OldQbCashId IS NULL AND [QbCashId] IS NULL) OR [QbCashId] = @OldQbCashId) " 
                + " AND ((@OldQbCostofGoodsId IS NULL AND [QbCostofGoodsId] IS NULL) OR [QbCostofGoodsId] = @OldQbCostofGoodsId) " 
                + " AND ((@OldQbUndepositedFundsId IS NULL AND [QbUndepositedFundsId] IS NULL) OR [QbUndepositedFundsId] = @OldQbUndepositedFundsId) " 
                + "";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.CommandType = CommandType.Text;
            deleteCommand.Parameters.AddWithValue("@OldUserId", OurCustomer.UserId);
            if (OurCustomer.QbCustomerId != null) {
                deleteCommand.Parameters.AddWithValue("@OldQbCustomerId", OurCustomer.QbCustomerId);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQbCustomerId", DBNull.Value); }
            if (OurCustomer.QuickbooksAccessToken != null) {
                deleteCommand.Parameters.AddWithValue("@OldQuickbooksAccessToken", OurCustomer.QuickbooksAccessToken);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQuickbooksAccessToken", DBNull.Value); }
            if (OurCustomer.QuickbooksSecretToken != null) {
                deleteCommand.Parameters.AddWithValue("@OldQuickbooksSecretToken", OurCustomer.QuickbooksSecretToken);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQuickbooksSecretToken", DBNull.Value); }
            if (OurCustomer.QbSalesAccount != null) {
                deleteCommand.Parameters.AddWithValue("@OldQbSalesAccount", OurCustomer.QbSalesAccount);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQbSalesAccount", DBNull.Value); }
            if (OurCustomer.QbSalesTax != null) {
                deleteCommand.Parameters.AddWithValue("@OldQbSalesTax", OurCustomer.QbSalesTax);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQbSalesTax", DBNull.Value); }
            if (OurCustomer.QbSalesDiscounts != null) {
                deleteCommand.Parameters.AddWithValue("@OldQbSalesDiscounts", OurCustomer.QbSalesDiscounts);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQbSalesDiscounts", DBNull.Value); }
            if (OurCustomer.QbFreightIncome != null) {
                deleteCommand.Parameters.AddWithValue("@OldQbFreightIncome", OurCustomer.QbFreightIncome);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQbFreightIncome", DBNull.Value); }
            if (OurCustomer.QbCash != null) {
                deleteCommand.Parameters.AddWithValue("@OldQbCash", OurCustomer.QbCash);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQbCash", DBNull.Value); }
            if (OurCustomer.QbCostOfGoods != null) {
                deleteCommand.Parameters.AddWithValue("@OldQbCostOfGoods", OurCustomer.QbCostOfGoods);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQbCostOfGoods", DBNull.Value); }
            if (OurCustomer.QbUndepositiedFunds != null) {
                deleteCommand.Parameters.AddWithValue("@OldQbUndepositiedFunds", OurCustomer.QbUndepositiedFunds);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQbUndepositiedFunds", DBNull.Value); }
            if (OurCustomer.QbSalesId.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldQbSalesId", OurCustomer.QbSalesId);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQbSalesId", DBNull.Value); }
            if (OurCustomer.QbSalesTaxId.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldQbSalesTaxId", OurCustomer.QbSalesTaxId);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQbSalesTaxId", DBNull.Value); }
            if (OurCustomer.QbDiscountsId.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldQbDiscountsId", OurCustomer.QbDiscountsId);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQbDiscountsId", DBNull.Value); }
            if (OurCustomer.QbFreightId.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldQbFreightId", OurCustomer.QbFreightId);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQbFreightId", DBNull.Value); }
            if (OurCustomer.QbCashId.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldQbCashId", OurCustomer.QbCashId);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQbCashId", DBNull.Value); }
            if (OurCustomer.QbCostofGoodsId.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldQbCostofGoodsId", OurCustomer.QbCostofGoodsId);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQbCostofGoodsId", DBNull.Value); }
            if (OurCustomer.QbUndepositedFundsId.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldQbUndepositedFundsId", OurCustomer.QbUndepositedFundsId);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQbUndepositedFundsId", DBNull.Value); }
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
 
