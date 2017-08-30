using nextGenAPI.DataAccess.TableDefinition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace nextGenAPI.CodeGeneration {
    internal class CodeGenerator {

        List<TableColumnDefinition> tableColumns = null;
        String tableName = string.Empty;
        StringBuilder template = new StringBuilder();

        public String GetCode(string selectedTemplateName, int selectedTableId) {

            String generatedCode = string.Empty;

            var tableColumnRepo = new TableDefinitionRepository();
            tableColumns = tableColumnRepo.GetTableColumnsDefinition(selectedTableId);
            tableName = tableColumns[0].TableName;

            switch (selectedTemplateName) {

                case "API-Model":
                    generatedCode = GenerateAPIModel();
                    break;

                case "Client-Model":
                    generatedCode = GenerateClientModel();
                    break;

                case "API-Repository":
                    generatedCode = GenerateAPIRepository();
                    break;

                case "API-CommandFactory":
                    generatedCode = GenerateAPICommandFactory();
                    break;

                default:
                    break;
            }


            StreamWriter sw = new StreamWriter("C:\\CodeGen.txt");
            sw.Write(generatedCode);
            sw.Close();

            return generatedCode;

        }

        #region Models
        private String GenerateAPIModel() {
                        
            template.Append("using System;" + Environment.NewLine);
            template.Append(Environment.NewLine);

            template.Append("namespace nextGenAPI.DataAccess." + this.tableName + " {" + Environment.NewLine + Environment.NewLine);
            template.Append("public class " + this.tableName + " { " + Environment.NewLine);
                        
            foreach (var column in tableColumns) {
                template.Append(Helpers.APIModelProperty(column) + Environment.NewLine);
            }

            template.Append("}" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);

            return template.ToString();
        }

        private String GenerateClientModel() {

            template.Append("export class " + this.tableName + " {" + Environment.NewLine);

            foreach (var column in tableColumns) {
                template.Append(Helpers.ClientModelProperty(column) + Environment.NewLine);
            }

            template.Append("}" + Environment.NewLine);

            return template.ToString();
        }

        #endregion

        private String GenerateAPIRepository() {

            template.Append("using nextGenAPI.DataAccess.Common;" + Environment.NewLine);
            template.Append("using System;" + Environment.NewLine);
            template.Append("using System.Collections.Generic;" + Environment.NewLine);
            template.Append("using System.Data.SqlClient;" + Environment.NewLine);
            template.Append(Environment.NewLine);

            template.Append("namespace nextGenAPI.DataAccess." + this.tableName + " {" + Environment.NewLine);

            template.Append("internal class " + this.tableName + " : RepositoryBase {" + Environment.NewLine);
            template.Append("public List<" + this.tableName + "> GetTableDefinition() {" + Environment.NewLine + Environment.NewLine);
            template.Append("var items = new List<" + this.tableName + ">();" + Environment.NewLine);
            template.Append("using (var connection = new SqlConnection(ConnectionString)) {" + Environment.NewLine);
            template.Append("connection.Open();" + Environment.NewLine + Environment.NewLine);
            template.Append("var cmd = new " + this.tableName + "CommandFactory();" + Environment.NewLine);
            template.Append("using (var command = cmd.Get" + this.tableName + "(connection)) {" + Environment.NewLine + Environment.NewLine);
            template.Append("using (var reader = command.ExecuteReader()) {" + Environment.NewLine);
            template.Append("try {" + Environment.NewLine);
            template.Append(" while (reader.Read()) {" + Environment.NewLine);
            
            foreach (var column in tableColumns) {
                template.Append(Helpers.APIRepositoryColumnAssignment(column));
            }

            template.Append(Environment.NewLine);
            template.Append("items.Add(new " + this.tableName + "("+ Helpers.APIParameterList(tableColumns) + "));" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);
            template.Append("} finally {" + Environment.NewLine);
            template.Append("reader.Close();" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);
            template.Append("return items;" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);
            
            return template.ToString();
        }

        private string GenerateAPICommandFactory() {

            template.Append("using System;" + Environment.NewLine);
            template.Append("using System.Data;" + Environment.NewLine);
            template.Append("using System.Data.SqlClient;" + Environment.NewLine);
            template.Append(Environment.NewLine);

            template.Append("namespace nextGenAPI.DataAccess." + this.tableName + " {" + Environment.NewLine);

            template.Append("public class " + this.tableName + "CommandFactory {" + Environment.NewLine);
            template.Append("internal SqlCommand GetTableDefinition(SqlConnection connection) {" + Environment.NewLine);
            template.Append("var queryString = @\"" + Environment.NewLine);
            template.Append( Helpers.SQLSelectStatement(tableColumns, this.tableName) + Environment.NewLine);
            template.Append(" var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);" + Environment.NewLine + Environment.NewLine);
            template.Append(" return cmd;" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);

            return template.ToString();
        }
    }
}
