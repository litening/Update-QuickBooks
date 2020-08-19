using System;
using System.Data;
using System.Data.SqlClient;
using QB2.Models;

namespace QB2.Data
{
    public class ItemOptionData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement
                = "SELECT "  
                + "     [ItemOption].[ItemOptionId] "
                + "    ,[ItemOption].[Description] "
                + "FROM " 
                + "     [ItemOption] " 
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
                + "     [ItemOption].[ItemOptionId] "
                + "    ,[ItemOption].[Description] "
                + "FROM " 
                + "     [ItemOption] " 
                    + "WHERE " 
                    + "     (@ItemOptionId IS NULL OR @ItemOptionId = '' OR [ItemOption].[ItemOptionId] LIKE '%' + LTRIM(RTRIM(@ItemOptionId)) + '%') " 
                    + "AND   (@Description IS NULL OR @Description = '' OR [ItemOption].[Description] LIKE '%' + LTRIM(RTRIM(@Description)) + '%') " 
                    + "";
            } else if (sCondition == "Equals") {
                selectStatement
                    = "SELECT "
                + "     [ItemOption].[ItemOptionId] "
                + "    ,[ItemOption].[Description] "
                + "FROM " 
                + "     [ItemOption] " 
                    + "WHERE " 
                    + "     (@ItemOptionId IS NULL OR @ItemOptionId = '' OR [ItemOption].[ItemOptionId] = LTRIM(RTRIM(@ItemOptionId))) " 
                    + "AND   (@Description IS NULL OR @Description = '' OR [ItemOption].[Description] = LTRIM(RTRIM(@Description))) " 
                    + "";
            } else if  (sCondition == "Starts with...") {
                selectStatement
                    = "SELECT "
                + "     [ItemOption].[ItemOptionId] "
                + "    ,[ItemOption].[Description] "
                + "FROM " 
                + "     [ItemOption] " 
                    + "WHERE " 
                    + "     (@ItemOptionId IS NULL OR @ItemOptionId = '' OR [ItemOption].[ItemOptionId] LIKE LTRIM(RTRIM(@ItemOptionId)) + '%') " 
                    + "AND   (@Description IS NULL OR @Description = '' OR [ItemOption].[Description] LIKE LTRIM(RTRIM(@Description)) + '%') " 
                    + "";
            } else if  (sCondition == "More than...") {
                selectStatement
                    = "SELECT "
                + "     [ItemOption].[ItemOptionId] "
                + "    ,[ItemOption].[Description] "
                + "FROM " 
                + "     [ItemOption] " 
                    + "WHERE " 
                    + "     (@ItemOptionId IS NULL OR @ItemOptionId = '' OR [ItemOption].[ItemOptionId] > LTRIM(RTRIM(@ItemOptionId))) " 
                    + "AND   (@Description IS NULL OR @Description = '' OR [ItemOption].[Description] > LTRIM(RTRIM(@Description))) " 
                    + "";
            } else if  (sCondition == "Less than...") {
                selectStatement
                    = "SELECT " 
                + "     [ItemOption].[ItemOptionId] "
                + "    ,[ItemOption].[Description] "
                + "FROM " 
                + "     [ItemOption] " 
                    + "WHERE " 
                    + "     (@ItemOptionId IS NULL OR @ItemOptionId = '' OR [ItemOption].[ItemOptionId] < LTRIM(RTRIM(@ItemOptionId))) " 
                    + "AND   (@Description IS NULL OR @Description = '' OR [ItemOption].[Description] < LTRIM(RTRIM(@Description))) " 
                    + "";
            } else if  (sCondition == "Equal or more than...") {
                selectStatement
                    = "SELECT "
                + "     [ItemOption].[ItemOptionId] "
                + "    ,[ItemOption].[Description] "
                + "FROM " 
                + "     [ItemOption] " 
                    + "WHERE " 
                    + "     (@ItemOptionId IS NULL OR @ItemOptionId = '' OR [ItemOption].[ItemOptionId] >= LTRIM(RTRIM(@ItemOptionId))) " 
                    + "AND   (@Description IS NULL OR @Description = '' OR [ItemOption].[Description] >= LTRIM(RTRIM(@Description))) " 
                    + "";
            } else if (sCondition == "Equal or less than...") {
                selectStatement 
                    = "SELECT "
                + "     [ItemOption].[ItemOptionId] "
                + "    ,[ItemOption].[Description] "
                + "FROM " 
                + "     [ItemOption] " 
                    + "WHERE " 
                    + "     (@ItemOptionId IS NULL OR @ItemOptionId = '' OR [ItemOption].[ItemOptionId] <= LTRIM(RTRIM(@ItemOptionId))) " 
                    + "AND   (@Description IS NULL OR @Description = '' OR [ItemOption].[Description] <= LTRIM(RTRIM(@Description))) " 
                    + "";
            }
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            if (sField == "Item Option Id") {
                selectCommand.Parameters.AddWithValue("@ItemOptionId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ItemOptionId", DBNull.Value); }
            if (sField == "Description") {
                selectCommand.Parameters.AddWithValue("@Description", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Description", DBNull.Value); }
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

        public static ItemOption Select_Record(ItemOption ItemOptionPara)
        {
        ItemOption ItemOption = new ItemOption();
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string selectStatement
            = "SELECT " 
                + "     [ItemOptionId] "
                + "    ,[Description] "
                + "FROM "
                + "     [ItemOption] "
                + "WHERE "
                + "     [ItemOptionId] = @ItemOptionId "
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            selectCommand.Parameters.AddWithValue("@ItemOptionId", ItemOptionPara.ItemOptionId);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    ItemOption.ItemOptionId = System.Convert.ToInt32(reader["ItemOptionId"]);
                    ItemOption.Description = System.Convert.ToString(reader["Description"]);
                }
                else
                {
                    ItemOption = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return ItemOption;
            }
            finally
            {
                connection.Close();
            }
            return ItemOption;
        }

        public static bool Add(ItemOption ItemOption)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string insertStatement
                = "INSERT " 
                + "     [ItemOption] "
                + "     ( "
                + "     [Description] "
                + "     ) "
                + "VALUES " 
                + "     ( "
                + "     @Description "
                + "     ) "
                + "";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.CommandType = CommandType.Text;
                insertCommand.Parameters.AddWithValue("@Description", ItemOption.Description);
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

        public static bool Update(ItemOption oldItemOption, 
               ItemOption newItemOption)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string updateStatement
                = "UPDATE "  
                + "     [ItemOption] "
                + "SET "
                + "     [Description] = @NewDescription "
                + "WHERE "
                + "     [ItemOptionId] = @OldItemOptionId " 
                + " AND [Description] = @OldDescription " 
                + "";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            updateCommand.CommandType = CommandType.Text;
            updateCommand.Parameters.AddWithValue("@NewDescription", newItemOption.Description);
            updateCommand.Parameters.AddWithValue("@OldItemOptionId", oldItemOption.ItemOptionId);
            updateCommand.Parameters.AddWithValue("@OldDescription", oldItemOption.Description);
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

        public static bool Delete(ItemOption ItemOption)
        {
            SqlConnection connection = OnlineSales2Data.GetConnection();
            string deleteStatement
                = "DELETE FROM "  
                + "     [ItemOption] "
                + "WHERE " 
                + "     [ItemOptionId] = @OldItemOptionId " 
                + " AND [Description] = @OldDescription " 
                + "";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.CommandType = CommandType.Text;
            deleteCommand.Parameters.AddWithValue("@OldItemOptionId", ItemOption.ItemOptionId);
            deleteCommand.Parameters.AddWithValue("@OldDescription", ItemOption.Description);
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
 
