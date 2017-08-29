using System.Data.SqlClient;

namespace nextGenAPI.DataAccess.TableDefinition {

    public class TableDefinitionCommandFactory {

        internal SqlCommand GetTableDefinition(SqlConnection connection) {

            var queryString = @"
                SELECT  cd.Table_Id, 
                        Table_Name,
                        Column_Id,
                        Column_Name, 
                        Column_Description, 
                        Column_SQL_Data_Type, 
                        Column_API_Data_Type, 
                        Column_Client_Data_Type, 
                        Column_Length, 
                        Column_Decimal_Places, 
                        Column_SQL_Name, 
                        Column_API_Name, 
                        Column_Client_Name, 
                        cd.Created_By, 
                        cd.Created_Date, 
                        cd.Modified_By,  
                        cd.Modified_Date
                FROM	Table_Definition t INNER JOIN Column_Definition cd
                            ON	t.Table_Id = cd.Table_Id
                ORDER BY Table_Name, Column_Name
            ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);
            return cmd;

        }
    }
}