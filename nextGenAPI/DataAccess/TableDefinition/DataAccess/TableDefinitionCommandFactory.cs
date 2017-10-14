using System.Data;
using System.Data.SqlClient;

namespace nextGenAPI.DataAccess.TableDefinition {

    public class TableDefinitionCommandFactory {

        internal SqlCommand GetTableDefinition(SqlConnection connection) {

            var queryString = @"
                SELECT  Project_Id,
                        Table_Id, 
                        Table_Name,                        
                        Created_By, 
                        Created_Date, 
                        Modified_By,  
                        Modified_Date
                FROM	Table_Definition
                WHERE   project_Id = @projectId
                ORDER BY Table_Name
            ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);

            cmd.Parameters.Add("@projectId", SqlDbType.Int);

            return cmd;

        }
         internal SqlCommand GetTableColumnDefinition(SqlConnection connection, int tableId) {

            var queryString = @"
                    SELECT  t.Project_Id,
                            t.Table_Id, 
                            Table_Name,
                            Column_Id,
                            Column_Primary_Key,
                            Column_Name, 
                            Column_Description, 
                            Column_SQL_Data_Type, 
                            Column_API_Data_Type, 
                            Column_Client_Data_Type, 
                            Column_Length, 
                            Column_Decimal_Places, 
                            Column_Nullable,
                            Column_SQL_Name, 
                            Column_API_Name, 
                            Column_Client_Name,
                            Column_Ordinal_Position,
                            cd.Created_By, 
                            cd.Created_Date, 
                            cd.Modified_By,  
                            cd.Modified_Date
                    FROM	Table_Definition t INNER JOIN Column_Definition cd
                                ON	t.Table_Id = cd.Table_Id
                    WHERE   t.Table_Id = @tableId
                    ORDER BY Table_Name, Column_Name
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);

            cmd.Parameters.Add("@tableId", SqlDbType.Int);
            cmd.Parameters["@tableId"].Value = tableId;

            return cmd;

        }
    }
}