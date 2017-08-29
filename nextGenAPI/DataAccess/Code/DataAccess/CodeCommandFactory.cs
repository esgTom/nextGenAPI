using System.Data.SqlClient;

namespace nextGenAPI.DataAccess.Codes.DataAccess {
    public class CodeCommandFactory {

        internal SqlCommand GetCodes(SqlConnection connection) {

            var queryString = @"
                SELECT  c.Code_Name,
		                cd.Code_Detail_Id,
                        cd.Code_Value,
		                cd.Code_Description,
                        cd.Created_By, 
                        cd.Created_Date, 
                        cd.Modified_By,  
                        cd.Modified_Date
                FROM    Code c INNER JOIN Code_Detail cd
                  ON    c.Code_Id = cd.Code_Id
            ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);
            return cmd;
        }

    }
}