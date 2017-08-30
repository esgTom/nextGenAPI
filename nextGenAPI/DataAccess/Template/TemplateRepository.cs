using nextGenAPI.DataAccess.Codes;
using nextGenAPI.DataAccess.Common;
using nextGenAPI.DataAccess.Template.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace nextGenAPI.DataAccess.Template {
    internal class TemplateRepository : RepositoryBase {
        
        public List<TemplateDefinition> GetTemplates() {

            var templates = new List<TemplateDefinition>();
            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var activtyCmd = new TemplateCommandFactory();
                using (var command = activtyCmd.GetTemplates(connection)) {

                    using (var reader = command.ExecuteReader()) {

                        try {
                            while (reader.Read()) {

                                var templateId = (int)(reader["template_Id"]);
                                var templateCategory = (string)(reader["template_Category"]);
                                var templateName = (string)(reader["template_Name"]);
                                var createdBy = (String)(reader["Created_By"] == DBNull.Value ? "" : reader["Created_By"]);
                                var createdDate = (DateTime)(reader["Created_Date"]);
                                var modifiedBy = (String)(reader["Modified_By"] == DBNull.Value ? "" : reader["Modified_By"]);
                                var modifiedDate = (DateTime)(reader["Modified_Date"]);

                                templates.Add(new TemplateDefinition(templateId, templateCategory, templateName, createdBy, createdDate, modifiedBy, modifiedDate));
                            }

                        } finally {
                            reader.Close();
                        }
                    }
                }
            }
            return templates;
        }
    }
    
}