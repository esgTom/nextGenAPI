using nextGenAPI.DataAccess.Codes.DataAccess;
using nextGenAPI.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace nextGenAPI.DataAccess.Codes {
    internal class CodeRepository : RepositoryBase  {
        
        public List<Template> GetCodes() {

            var codes = new List<Template>();
            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var activtyCmd = new CodeCommandFactory();
                using (var command = activtyCmd.GetCodes(connection)) {

                    using (var reader = command.ExecuteReader()) {

                        try {
                            while (reader.Read()) {

                                var codeDetailId = (int)(reader["Code_Detail_Id"]);
                                var codeValue = (String)(reader["Code_Value"]);
                                var codeDescription = (string)(reader["Code_Description"]);
                                var createdBy = (String)(reader["Created_By"] == DBNull.Value ? "" : reader["Created_By"]);
                                var createdDate = (DateTime)(reader["Created_Date"]);
                                var modifiedBy = (String)(reader["Modified_By"] == DBNull.Value ? "" : reader["Modified_By"]);
                                var modifiedDate = (DateTime)(reader["Modified_Date"]);

                                codes.Add(new Template(codeDetailId, codeValue, codeDescription, createdBy, createdDate, modifiedBy, modifiedDate));
                            }

                        } finally {
                            reader.Close();
                        }
                    }
                }
            }
            return codes;
        }
    }

}