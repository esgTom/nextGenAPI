using System.Data;
using System.Data.SqlClient;

namespace nextGenAPI.DataAccess.Project.DataAccess {

    public class ProjectCommandFactory {

        internal SqlCommand GetProjects(SqlConnection connection) {

            var queryString = @"
                SELECT  Project_Id,
		                Project_Name,
		                Project_Description,
                        Created_By, 
                        Created_Date, 
                        Modified_By,  
                        Modified_Date
                FROM    Project
                ORDER BY Project_Name
            ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);
            return cmd;
        }

        internal SqlCommand InsertProject(SqlConnection connection, SqlTransaction transaction) {


            var queryString = @"
                    SET NOCOUNT ON;

                    INSERT INTO Project(Project_Name, Project_Description, Created_By, Modified_By)
                        VALUES (@projectName, @projectDescription, @createdBy, @modifiedBy )

                    SELECT @pkValue = @@IDENTITY;
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection) {
                Transaction = transaction
            };

            cmd.Parameters.Add("@projectName", SqlDbType.VarChar);
            cmd.Parameters.Add("@projectDescription", SqlDbType.VarChar);
            cmd.Parameters.Add("@createdBy", SqlDbType.VarChar);
            cmd.Parameters.Add("@modifiedBy", SqlDbType.VarChar);

            cmd.Parameters.Add(new SqlParameter("@pkValue", SqlDbType.Int) {
                Direction = ParameterDirection.Output
            });

            return cmd;

        }

        internal SqlCommand UpdateProject(SqlConnection connection, SqlTransaction transaction) {

            var queryString = @"
                    SET NOCOUNT ON;

                    UPDATE Project
                       SET Project_Name = @projectName,
                           Project_Description = @projectDescription,
                           Modified_By = @modifiedBy
                    WHERE Project_Id = @projectId
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection) {
                Transaction = transaction
            };

            cmd.Parameters.Add("@projectId", SqlDbType.Int);
            cmd.Parameters.Add("@projectName", SqlDbType.VarChar);
            cmd.Parameters.Add("@projectDescription", SqlDbType.VarChar);
            cmd.Parameters.Add("@modifiedBy", SqlDbType.VarChar);

            return cmd;

        }

        internal SqlCommand DeleteProject(SqlConnection connection, SqlTransaction transaction) {


            var queryString = @"
                    SET NOCOUNT ON;

                    DELETE Project
                    WHERE Project_Id = @projectId
        
                    SET @rowsAffected = @@ROWCOUNT;
                ";

            var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection) {
                Transaction = transaction
            };

            cmd.Parameters.Add("@projectId", SqlDbType.Int);
            cmd.Parameters.Add(new SqlParameter("@rowsAffected", SqlDbType.Int) {
                Direction = ParameterDirection.Output
            });

            return cmd;

        }

    }
}
