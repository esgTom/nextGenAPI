using System.Data;
using System.Data.SqlClient;

namespace nextGenAPI.DataAccess.Import {

    public class ImportCommandFactory {

        internal SqlCommand RemoveTableColumnDefs(SqlConnection connection, SqlTransaction transaction) {

            var queryString = @"
                SET NOCOUNT ON;

                DELETE c
                FROM Table_Definition t INNER JOIN Column_Definition c
	                ON t.Table_Id = c.Table_Id
                WHERE Table_Name = @tableName

                DELETE t
                FROM Table_Definition t
                WHERE Table_Name = @tableName
            ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection) {
                Transaction = transaction
            };

            cmd.Parameters.Add("@tableName", SqlDbType.VarChar);


            return cmd;

        }
        internal SqlCommand InsertTableDefs(SqlConnection connection, SqlTransaction transaction) {


            var queryString = @"
                    SET NOCOUNT ON;

                    INSERT INTO Table_Definition(Project_Id, Table_Name, Created_By, Modified_By)
                        VALUES ( @projectId, @tableName, @createdBy, @modifiedBy )

                    SELECT @pkValue = @@IDENTITY;
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection) {
                Transaction = transaction
            };

            cmd.Parameters.Add("@projectId", SqlDbType.Int);
            cmd.Parameters.Add("@tableName", SqlDbType.VarChar);
            cmd.Parameters.Add("@createdBy", SqlDbType.VarChar);
            cmd.Parameters.Add("@modifiedBy", SqlDbType.VarChar);

            cmd.Parameters.Add(new SqlParameter("@pkValue", SqlDbType.Int) {
                Direction = ParameterDirection.Output
            });

            return cmd;

        }


        internal SqlCommand InsertColumnDefs(SqlConnection connection, SqlTransaction transaction) {


            var queryString = @"

                    SET NOCOUNT ON;

                    INSERT INTO Column_Definition(Table_Id, Column_Primary_Key, Column_Name, Column_Description, 
                            Column_SQL_Data_Type, Column_API_Data_Type, Column_Client_Data_Type,
                            Column_Length, Column_Decimal_Places, Column_Nullable, 
                            Column_SQL_Name, Column_API_Name, Column_Client_Name, Column_Ordinal_Position,
                            cd.Created_By,cd.Modified_By )

                    VALUES (@TableId, @ColumnPrimaryKey, @ColumnName, @ColumnDescription, 
                            @ColumnSQLDataType, @ColumnAPIDataType, @ColumnClientDataType,
                            @ColumnLength, @ColumnDecimalPlaces, @ColumnNullable,
                            @ColumnSQLName, @ColumnAPIName, @ColumnClientName, @ColumnOrdinalPosition,
                            @CreatedBy, @ModifiedBy )

                    ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection) {
                Transaction = transaction
            };


            cmd.Parameters.Add("@TableId", SqlDbType.Int);
            cmd.Parameters.Add("@ColumnPrimaryKey", SqlDbType.TinyInt);
            cmd.Parameters.Add("@ColumnName", SqlDbType.VarChar);
            cmd.Parameters.Add("@ColumnDescription", SqlDbType.VarChar);
            cmd.Parameters.Add("@ColumnSQLDataType", SqlDbType.VarChar);
            cmd.Parameters.Add("@ColumnAPIDataType", SqlDbType.VarChar);
            cmd.Parameters.Add("@ColumnClientDataType", SqlDbType.VarChar);
            cmd.Parameters.Add("@ColumnLength", SqlDbType.Int);
            cmd.Parameters.Add("@ColumnDecimalPlaces", SqlDbType.Int);
            cmd.Parameters.Add("@ColumnNullable", SqlDbType.VarChar);
            cmd.Parameters.Add("@ColumnSQLName", SqlDbType.VarChar);
            cmd.Parameters.Add("@ColumnAPIName", SqlDbType.VarChar);
            cmd.Parameters.Add("@ColumnClientName", SqlDbType.VarChar);
            cmd.Parameters.Add("@ColumnOrdinalPosition", SqlDbType.Int);
            cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar);
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.VarChar);

            return cmd;

        }

    }
}