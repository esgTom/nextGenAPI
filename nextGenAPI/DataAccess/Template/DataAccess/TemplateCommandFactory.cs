using System.Data.SqlClient;

namespace nextGenAPI.DataAccess.Template.DataAccess {
    public class TemplateCommandFactory {

        internal SqlCommand GetTemplates(SqlConnection connection) {

            var queryString = @"
                SELECT	Template_Category, 
		            Template_Name, 
		            Template_Text, 
		            Created_By, 
		            Created_Date, 
		            Modified_By, 
        		    Modified_Date
                FROM Template 
                ORDER BY Template_Category, Template_Name
            ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);
            return cmd;
        }

    }
}
