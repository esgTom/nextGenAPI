using nextGenAPI.DataAccess.Common;
using nextGenAPI.DataAccess.Project.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace nextGenAPI.DataAccess.Project {

    internal class ProjectRepository : RepositoryBase {

        public List<Project> GetProjects() {

            var projects = new List<Project>();
            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var activtyCmd = new ProjectCommandFactory();
                using (var command = activtyCmd.GetProjects(connection)) {

                    using (var reader = command.ExecuteReader()) {

                        try {
                            while (reader.Read()) {

                                var projectId = (int)(reader["Project_Id"]);
                                var projectName = (string)(reader["Project_Name"]);
                                var projectDescription = (string)(reader["Project_Description"]);
                                var createdBy = (String)(reader["Created_By"] == DBNull.Value ? "" : reader["Created_By"]);
                                var createdDate = (DateTime)(reader["Created_Date"]);
                                var modifiedBy = (String)(reader["Modified_By"] == DBNull.Value ? "" : reader["Modified_By"]);
                                var modifiedDate = (DateTime)(reader["Modified_Date"]);

                                projects.Add(new Project(projectId, projectName, projectDescription, createdBy, createdDate, modifiedBy, modifiedDate));
                            }

                        } finally {
                            reader.Close();
                        }
                    }
                }
            }
            return projects;
        }


        public Project InsertProject(Project project ) {

            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try {
                    var projectCmd = new ProjectCommandFactory();

                    using (var command = projectCmd.InsertProject(connection, transaction)) {
                        command.Parameters["@projectName"].Value = project.ProjectName;
                        command.Parameters["@projectDescription"].Value = project.ProjectDescription;
                        command.Parameters["@CreatedBy"].Value = project.CreatedBy;
                        command.Parameters["@ModifiedBy"].Value = project.ModifiedBy;
                        command.ExecuteNonQuery();
                        project.ProjectId = (int) command.Parameters["@pkValue"].Value;
                    }

                    transaction.Commit();

                } catch (Exception exception) {
                    transaction.Rollback();
                    project = null;
                } finally {
                    transaction = null;
                }
            }
            return project;
        }

        public Project UpdateProject(Project project) {

            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try {
                    var projectCmd = new ProjectCommandFactory();

                    using (var command = projectCmd.UpdateProject(connection, transaction)) {
                        command.Parameters["@projectId"].Value = project.ProjectId;
                        command.Parameters["@projectName"].Value = project.ProjectName;
                        command.Parameters["@projectDescription"].Value = project.ProjectDescription;
                        command.Parameters["@ModifiedBy"].Value = project.ModifiedBy;
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();

                } catch (Exception exception) {
                    transaction.Rollback();
                    project = null;
                } finally {
                    transaction = null;
                }
            }
            return project;
        }

        public bool DeleteProject(Project project) {

            var rowsAffected = 0;

            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try {
                    var projectCmd = new ProjectCommandFactory();

                    using (var command = projectCmd.DeleteProject(connection, transaction)) {
                        command.Parameters["@projectId"].Value = project.ProjectId;
                        command.ExecuteNonQuery();
                        rowsAffected = (int)command.Parameters["@rowsAffected"].Value;
                    }

                    transaction.Commit();

                } catch (Exception exception) {
                    transaction.Rollback();                    
                } finally {
                    transaction = null;
                }
            }
            return (rowsAffected > 0);
        }
    }

}
    