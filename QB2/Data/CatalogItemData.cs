using System;
using System.Data;
using System.Data.SqlClient;
using QB2.Models;

namespace QB2.Data
{
    public class CatalogItemData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement
                = "SELECT "  
                + "     [CatalogItem].[CatalogItemId] "
                + "    ,[CatalogItem].[Name] "
                + "    ,[CatalogItem].[Description] "
                + "    ,[CatalogItem].[Sku] "
                + "    ,[CatalogItem].[QbItemId] "
                + "FROM " 
                + "     [CatalogItem] " 
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
                + "     [CatalogItem].[CatalogItemId] "
                + "    ,[CatalogItem].[Name] "
                + "    ,[CatalogItem].[Description] "
                + "    ,[CatalogItem].[Sku] "
                + "    ,[CatalogItem].[QbItemId] "
                + "FROM " 
                + "     [CatalogItem] " 
                    + "WHERE " 
                    + "     (@CatalogItemId IS NULL OR @CatalogItemId = '' OR [CatalogItem].[CatalogItemId] LIKE '%' + LTRIM(RTRIM(@CatalogItemId)) + '%') " 
                    + "AND   (@Name IS NULL OR @Name = '' OR [CatalogItem].[Name] LIKE '%' + LTRIM(RTRIM(@Name)) + '%') " 
                    + "AND   (@Description IS NULL OR @Description = '' OR [CatalogItem].[Description] LIKE '%' + LTRIM(RTRIM(@Description)) + '%') " 
                    + "AND   (@Sku IS NULL OR @Sku = '' OR [CatalogItem].[Sku] LIKE '%' + LTRIM(RTRIM(@Sku)) + '%') " 
                    + "AND   (@QbItemId IS NULL OR @QbItemId = '' OR [CatalogItem].[QbItemId] LIKE '%' + LTRIM(RTRIM(@QbItemId)) + '%') " 
                    + "";
            } else if (sCondition == "Equals") {
                selectStatement
                    = "SELECT "
                + "     [CatalogItem].[CatalogItemId] "
                + "    ,[CatalogItem].[Name] "
                + "    ,[CatalogItem].[Description] "
                + "    ,[CatalogItem].[Sku] "
                + "    ,[CatalogItem].[QbItemId] "
                + "FROM " 
                + "     [CatalogItem] " 
                    + "WHERE " 
                    + "     (@CatalogItemId IS NULL OR @CatalogItemId = '' OR [CatalogItem].[CatalogItemId] = LTRIM(RTRIM(@CatalogItemId))) " 
                    + "AND   (@Name IS NULL OR @Name = '' OR [CatalogItem].[Name] = LTRIM(RTRIM(@Name))) " 
                    + "AND   (@Description IS NULL OR @Description = '' OR [CatalogItem].[Description] = LTRIM(RTRIM(@Description))) " 
                    + "AND   (@Sku IS NULL OR @Sku = '' OR [CatalogItem].[Sku] = LTRIM(RTRIM(@Sku))) " 
                    + "AND   (@QbItemId IS NULL OR @QbItemId = '' OR [CatalogItem].[QbItemId] = LTRIM(RTRIM(@QbItemId))) " 
                    + "";
            } else if  (sCondition == "Starts with...") {
                selectStatement
                    = "SELECT "
                + "     [CatalogItem].[CatalogItemId] "
                + "    ,[CatalogItem].[Name] "
                + "    ,[CatalogItem].[Description] "
                + "    ,[CatalogItem].[Sku] "
                + "    ,[CatalogItem].[QbItemId] "
                + "FROM " 
                + "     [CatalogItem] " 
                    + "WHERE " 
                    + "     (@CatalogItemId IS NULL OR @CatalogItemId = '' OR [CatalogItem].[CatalogItemId] LIKE LTRIM(RTRIM(@CatalogItemId)) + '%') " 
                    + "AND   (@Name IS NULL OR @Name = '' OR [CatalogItem].[Name] LIKE LTRIM(RTRIM(@Name)) + '%') " 
                    + "AND   (@Description IS NULL OR @Description = '' OR [CatalogItem].[Description] LIKE LTRIM(RTRIM(@Description)) + '%') " 
                    + "AND   (@Sku IS NULL OR @Sku = '' OR [CatalogItem].[Sku] LIKE LTRIM(RTRIM(@Sku)) + '%') " 
                    + "AND   (@QbItemId IS NULL OR @QbItemId = '' OR [CatalogItem].[QbItemId] LIKE LTRIM(RTRIM(@QbItemId)) + '%') " 
                    + "";
            } else if  (sCondition == "More than...") {
                selectStatement
                    = "SELECT "
                + "     [CatalogItem].[CatalogItemId] "
                + "    ,[CatalogItem].[Name] "
                + "    ,[CatalogItem].[Description] "
                + "    ,[CatalogItem].[Sku] "
                + "    ,[CatalogItem].[QbItemId] "
                + "FROM " 
                + "     [CatalogItem] " 
                    + "WHERE " 
                    + "     (@CatalogItemId IS NULL OR @CatalogItemId = '' OR [CatalogItem].[CatalogItemId] > LTRIM(RTRIM(@CatalogItemId))) " 
                    + "AND   (@Name IS NULL OR @Name = '' OR [CatalogItem].[Name] > LTRIM(RTRIM(@Name))) " 
                    + "AND   (@Description IS NULL OR @Description = '' OR [CatalogItem].[Description] > LTRIM(RTRIM(@Description))) " 
                    + "AND   (@Sku IS NULL OR @Sku = '' OR [CatalogItem].[Sku] > LTRIM(RTRIM(@Sku))) " 
                    + "AND   (@QbItemId IS NULL OR @QbItemId = '' OR [CatalogItem].[QbItemId] > LTRIM(RTRIM(@QbItemId))) " 
                    + "";
            } else if  (sCondition == "Less than...") {
                selectStatement
                    = "SELECT " 
                + "     [CatalogItem].[CatalogItemId] "
                + "    ,[CatalogItem].[Name] "
                + "    ,[CatalogItem].[Description] "
                + "    ,[CatalogItem].[Sku] "
                + "    ,[CatalogItem].[QbItemId] "
                + "FROM " 
                + "     [CatalogItem] " 
                    + "WHERE " 
                    + "     (@CatalogItemId IS NULL OR @CatalogItemId = '' OR [CatalogItem].[CatalogItemId] < LTRIM(RTRIM(@CatalogItemId))) " 
                    + "AND   (@Name IS NULL OR @Name = '' OR [CatalogItem].[Name] < LTRIM(RTRIM(@Name))) " 
                    + "AND   (@Description IS NULL OR @Description = '' OR [CatalogItem].[Description] < LTRIM(RTRIM(@Description))) " 
                    + "AND   (@Sku IS NULL OR @Sku = '' OR [CatalogItem].[Sku] < LTRIM(RTRIM(@Sku))) " 
                    + "AND   (@QbItemId IS NULL OR @QbItemId = '' OR [CatalogItem].[QbItemId] < LTRIM(RTRIM(@QbItemId))) " 
                    + "";
            } else if  (sCondition == "Equal or more than...") {
                selectStatement
                    = "SELECT "
                + "     [CatalogItem].[CatalogItemId] "
                + "    ,[CatalogItem].[Name] "
                + "    ,[CatalogItem].[Description] "
                + "    ,[CatalogItem].[Sku] "
                + "    ,[CatalogItem].[QbItemId] "
                + "FROM " 
                + "     [CatalogItem] " 
                    + "WHERE " 
                    + "     (@CatalogItemId IS NULL OR @CatalogItemId = '' OR [CatalogItem].[CatalogItemId] >= LTRIM(RTRIM(@CatalogItemId))) " 
                    + "AND   (@Name IS NULL OR @Name = '' OR [CatalogItem].[Name] >= LTRIM(RTRIM(@Name))) " 
                    + "AND   (@Description IS NULL OR @Description = '' OR [CatalogItem].[Description] >= LTRIM(RTRIM(@Description))) " 
                    + "AND   (@Sku IS NULL OR @Sku = '' OR [CatalogItem].[Sku] >= LTRIM(RTRIM(@Sku))) " 
                    + "AND   (@QbItemId IS NULL OR @QbItemId = '' OR [CatalogItem].[QbItemId] >= LTRIM(RTRIM(@QbItemId))) " 
                    + "";
            } else if (sCondition == "Equal or less than...") {
                selectStatement 
                    = "SELECT "
                + "     [CatalogItem].[CatalogItemId] "
                + "    ,[CatalogItem].[Name] "
                + "    ,[CatalogItem].[Description] "
                + "    ,[CatalogItem].[Sku] "
                + "    ,[CatalogItem].[QbItemId] "
                + "FROM " 
                + "     [CatalogItem] " 
                    + "WHERE " 
                    + "     (@CatalogItemId IS NULL OR @CatalogItemId = '' OR [CatalogItem].[CatalogItemId] <= LTRIM(RTRIM(@CatalogItemId))) " 
                    + "AND   (@Name IS NULL OR @Name = '' OR [CatalogItem].[Name] <= LTRIM(RTRIM(@Name))) " 
                    + "AND   (@Description IS NULL OR @Description = '' OR [CatalogItem].[Description] <= LTRIM(RTRIM(@Description))) " 
                    + "AND   (@Sku IS NULL OR @Sku = '' OR [CatalogItem].[Sku] <= LTRIM(RTRIM(@Sku))) " 
                    + "AND   (@QbItemId IS NULL OR @QbItemId = '' OR [CatalogItem].[QbItemId] <= LTRIM(RTRIM(@QbItemId))) " 
                    + "";
            }
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            if (sField == "Catalog Item Id") {
                selectCommand.Parameters.AddWithValue("@CatalogItemId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@CatalogItemId", DBNull.Value); }
            if (sField == "Name") {
                selectCommand.Parameters.AddWithValue("@Name", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Name", DBNull.Value); }
            if (sField == "Description") {
                selectCommand.Parameters.AddWithValue("@Description", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Description", DBNull.Value); }
            if (sField == "Sku") {
                selectCommand.Parameters.AddWithValue("@Sku", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Sku", DBNull.Value); }
            if (sField == "Qb Item Id") {
                selectCommand.Parameters.AddWithValue("@QbItemId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@QbItemId", DBNull.Value); }
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

        public static CatalogItem Select_Record(CatalogItem CatalogItemPara)
        {
        CatalogItem CatalogItem = new CatalogItem();
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement
            = "SELECT " 
                + "     [CatalogItemId] "
                + "    ,[Name] "
                + "    ,[Description] "
                + "    ,[Sku] "
                + "    ,[QbItemId] "
                + "FROM "
                + "     [CatalogItem] "
                + "WHERE "
                + "     [CatalogItemId] = @CatalogItemId "
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            selectCommand.Parameters.AddWithValue("@CatalogItemId", CatalogItemPara.CatalogItemId);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    CatalogItem.CatalogItemId = System.Convert.ToInt32(reader["CatalogItemId"]);
                    CatalogItem.Name = System.Convert.ToString(reader["Name"]);
                    CatalogItem.Description = reader["Description"] is DBNull ? null : reader["Description"].ToString();
                    CatalogItem.Sku = reader["Sku"] is DBNull ? null : reader["Sku"].ToString();
                    CatalogItem.QbItemId = reader["QbItemId"] is DBNull ? null : reader["QbItemId"].ToString();
                }
                else
                {
                    CatalogItem = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return CatalogItem;
            }
            finally
            {
                connection.Close();
            }
            return CatalogItem;
        }

        public static bool Add(CatalogItem CatalogItem)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string insertStatement
                = "INSERT " 
                + "     [CatalogItem] "
                + "     ( "
                + "     [Name] "
                + "    ,[Description] "
                + "    ,[Sku] "
                + "    ,[QbItemId] "
                + "     ) "
                + "VALUES " 
                + "     ( "
                + "     @Name "
                + "    ,@Description "
                + "    ,@Sku "
                + "    ,@QbItemId "
                + "     ) "
                + "";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.CommandType = CommandType.Text;
                insertCommand.Parameters.AddWithValue("@Name", CatalogItem.Name);
            if (CatalogItem.Description != null) {
                insertCommand.Parameters.AddWithValue("@Description", CatalogItem.Description);
            } else {
                insertCommand.Parameters.AddWithValue("@Description", DBNull.Value); }
            if (CatalogItem.Sku != null) {
                insertCommand.Parameters.AddWithValue("@Sku", CatalogItem.Sku);
            } else {
                insertCommand.Parameters.AddWithValue("@Sku", DBNull.Value); }
            if (CatalogItem.QbItemId != null) {
                insertCommand.Parameters.AddWithValue("@QbItemId", CatalogItem.QbItemId);
            } else {
                insertCommand.Parameters.AddWithValue("@QbItemId", DBNull.Value); }
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

        public static bool Update(CatalogItem oldCatalogItem, 
               CatalogItem newCatalogItem)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string updateStatement
                = "UPDATE "  
                + "     [CatalogItem] "
                + "SET "
                + "     [Name] = @NewName "
                + "    ,[Description] = @NewDescription "
                + "    ,[Sku] = @NewSku "
                + "    ,[QbItemId] = @NewQbItemId "
                + "WHERE "
                + "     [CatalogItemId] = @OldCatalogItemId " 
                + " AND [Name] = @OldName " 
                + " AND ((@OldDescription IS NULL AND [Description] IS NULL) OR [Description] = @OldDescription) " 
                + " AND ((@OldSku IS NULL AND [Sku] IS NULL) OR [Sku] = @OldSku) " 
                + " AND ((@OldQbItemId IS NULL AND [QbItemId] IS NULL) OR [QbItemId] = @OldQbItemId) " 
                + "";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            updateCommand.CommandType = CommandType.Text;
            updateCommand.Parameters.AddWithValue("@NewName", newCatalogItem.Name);
            if (newCatalogItem.Description != null) {
                updateCommand.Parameters.AddWithValue("@NewDescription", newCatalogItem.Description);
            } else {
                updateCommand.Parameters.AddWithValue("@NewDescription", DBNull.Value); }
            if (newCatalogItem.Sku != null) {
                updateCommand.Parameters.AddWithValue("@NewSku", newCatalogItem.Sku);
            } else {
                updateCommand.Parameters.AddWithValue("@NewSku", DBNull.Value); }
            if (newCatalogItem.QbItemId != null) {
                updateCommand.Parameters.AddWithValue("@NewQbItemId", newCatalogItem.QbItemId);
            } else {
                updateCommand.Parameters.AddWithValue("@NewQbItemId", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldCatalogItemId", oldCatalogItem.CatalogItemId);
            updateCommand.Parameters.AddWithValue("@OldName", oldCatalogItem.Name);
            if (oldCatalogItem.Description != null) {
                updateCommand.Parameters.AddWithValue("@OldDescription", oldCatalogItem.Description);
            } else {
                updateCommand.Parameters.AddWithValue("@OldDescription", DBNull.Value); }
            if (oldCatalogItem.Sku != null) {
                updateCommand.Parameters.AddWithValue("@OldSku", oldCatalogItem.Sku);
            } else {
                updateCommand.Parameters.AddWithValue("@OldSku", DBNull.Value); }
            if (oldCatalogItem.QbItemId != null) {
                updateCommand.Parameters.AddWithValue("@OldQbItemId", oldCatalogItem.QbItemId);
            } else {
                updateCommand.Parameters.AddWithValue("@OldQbItemId", DBNull.Value); }
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

        public static bool Delete(CatalogItem CatalogItem)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string deleteStatement
                = "DELETE FROM "  
                + "     [CatalogItem] "
                + "WHERE " 
                + "     [CatalogItemId] = @OldCatalogItemId " 
                + " AND [Name] = @OldName " 
                + " AND ((@OldDescription IS NULL AND [Description] IS NULL) OR [Description] = @OldDescription) " 
                + " AND ((@OldSku IS NULL AND [Sku] IS NULL) OR [Sku] = @OldSku) " 
                + " AND ((@OldQbItemId IS NULL AND [QbItemId] IS NULL) OR [QbItemId] = @OldQbItemId) " 
                + "";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.CommandType = CommandType.Text;
            deleteCommand.Parameters.AddWithValue("@OldCatalogItemId", CatalogItem.CatalogItemId);
            deleteCommand.Parameters.AddWithValue("@OldName", CatalogItem.Name);
            if (CatalogItem.Description != null) {
                deleteCommand.Parameters.AddWithValue("@OldDescription", CatalogItem.Description);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldDescription", DBNull.Value); }
            if (CatalogItem.Sku != null) {
                deleteCommand.Parameters.AddWithValue("@OldSku", CatalogItem.Sku);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldSku", DBNull.Value); }
            if (CatalogItem.QbItemId != null) {
                deleteCommand.Parameters.AddWithValue("@OldQbItemId", CatalogItem.QbItemId);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldQbItemId", DBNull.Value); }
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
 
