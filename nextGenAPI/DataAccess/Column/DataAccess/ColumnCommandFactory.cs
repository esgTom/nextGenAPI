using System;
using System.Data;
using System.Data.SqlClient;

namespace nextGenAPI.DataAccess.Column {
    public class ColumnCommandFactory {
        internal SqlCommand GetColumn_Definitions(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 
                SELECT 
                    Column_API_Data_Type, 
                    Column_API_Name, 
                    Column_Client_Data_Type, 
                    Column_Client_Name, 
                    Column_Decimal_Places, 
                    Column_Description, 
                    Column_Id, 
                    Column_Length, 
                    Column_Name, 
                    Column_Nullable, 
                    Column_Ordinal_Position, 
                    Column_Primary_Key, 
                    Column_SQL_Data_Type, 
                    Column_SQL_Name, 
                    Created_By, 
                    Created_Date, 
                    Modified_By, 
                    Modified_Date, 
                    Table_Id
                FROM Column_Definition
                WHERE Table_Id = @tableId
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);

            return cmd;
        }
        internal SqlCommand GetColumn_Definition(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 
                SELECT 
                    Column_API_Data_Type, 
                    Column_API_Name, 
                    Column_Client_Data_Type, 
                    Column_Client_Name, 
                    Column_Decimal_Places, 
                    Column_Description, 
                    Column_Id, 
                    Column_Length, 
                    Column_Name, 
                    Column_Nullable, 
                    Column_Ordinal_Position, 
                    Column_Primary_Key, 
                    Column_SQL_Data_Type, 
                    Column_SQL_Name, 
                    Created_By, 
                    Created_Date, 
                    Modified_By, 
                    Modified_Date, 
                    Table_Id
                FROM Column_Definition
                WHERE ColumnId = @ColumnId 
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);

            return cmd;
        }
        internal SqlCommand InsertColumn_Definition(SqlConnection connection) {
            var queryString = @"
            SET NOCOUNT ON 
            INSERT INTO Column_Definition(
                Column_API_Data_Type, Column_API_Name, Column_Client_Data_Type, Column_Client_Name, Column_Decimal_Places, 
                Column_Description, Column_Id, Column_Length, Column_Name, Column_Nullable, Column_Ordinal_Position, Column_Primary_Key, 
                Column_SQL_Data_Type, Column_SQL_Name, Created_By, Created_Date, Modified_By, Modified_Date, Table_Id) 
            VALUES (@ColumnAPIDataType, @ColumnAPIName, @ColumnClientDataType, @ColumnClientName, @ColumnDecimalPlaces, 
                @ColumnDescription, @ColumnId, @ColumnLength, @ColumnName, @ColumnNullable, @ColumnOrdinalPosition, @ColumnPrimaryKey, 
                @ColumnSQLDataType, @ColumnSQLName, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @TableId) 
            ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);

            return cmd;
        }
        internal SqlCommand UpdateColumn_Definition(SqlConnection connection) {
            var queryString = @"
            SET NOCOUNT ON 
            UPDATE Column_Definition(
                Column_API_Data_Type = @ColumnAPIDataType,
                Column_API_Name = @ColumnAPIName,
                Column_Client_Data_Type = @ColumnClientDataType,
                Column_Client_Name = @ColumnClientName,
                Column_Decimal_Places = @ColumnDecimalPlaces,
                Column_Description = @ColumnDescription,
                Column_Id = @ColumnId,
                Column_Length = @ColumnLength,
                Column_Name = @ColumnName,
                Column_Nullable = @ColumnNullable,
                Column_Ordinal_Position = @ColumnOrdinalPosition,
                Column_Primary_Key = @ColumnPrimaryKey,
                Column_SQL_Data_Type = @ColumnSQLDataType,
                Column_SQL_Name = @ColumnSQLName,
                Created_By = @CreatedBy,
                Created_Date = @CreatedDate,
                Modified_By = @ModifiedBy,
                Modified_Date = @ModifiedDate,
                Table_Id = @TableId
            WHERE ColumnId = @ColumnId 
            ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);

            return cmd;
        }
        internal SqlCommand DeleteColumn_Definition(SqlConnection connection) {
            var queryString = @"
                SET NOCOUNT ON 
                DELETE Column_Definition
                WHERE ColumnId = @ColumnId 
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);

            return cmd;
        }
    }
}
