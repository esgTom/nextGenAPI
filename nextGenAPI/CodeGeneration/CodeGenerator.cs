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

                case "API-Controller":
                    generatedCode = GenerateAPIController();
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

        private String GenerateAPIController() {

            // TODO: Update various unsuccessful return status code for update, insert, and delete

            template.Append("using System.Web.Http;" + Environment.NewLine);
            template.Append(Environment.NewLine + Environment.NewLine);
            template.Append("public class " + this.tableName + "Controller : ApiController {" + Environment.NewLine + Environment.NewLine);

            // Get
            template.Append("[HttpGet]" + Environment.NewLine);
            template.Append("public IHttpActionResult Get" + this.tableName + "(" + this.tableName + " " + Helpers.FirstCharacterToLower(this.tableName) + ") {" + Environment.NewLine + Environment.NewLine);
            template.Append("var repo = new " + this.tableName + "Repository();" + Environment.NewLine);
            template.Append("var response = repo.Get" + this.tableName + "(" + Helpers.FirstCharacterToLower(this.tableName) + ");" + Environment.NewLine);
            template.Append("if (response != null) {" + Environment.NewLine);
            template.Append("return Ok(response);" + Environment.NewLine);
            template.Append("} else {" + Environment.NewLine);
            template.Append("return NotFound();" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);

            // Post
            template.Append("[HttpPost]" + Environment.NewLine);
            template.Append("public IHttpActionResult Update" + this.tableName + "(" + this.tableName + " " + Helpers.FirstCharacterToLower(this.tableName) + ") {" + Environment.NewLine + Environment.NewLine);
            template.Append("var repo = new " + this.tableName + "Repository();" + Environment.NewLine);
            template.Append("var response = repo.Get" + this.tableName + "(" + Helpers.FirstCharacterToLower(this.tableName) + ");" + Environment.NewLine);
            template.Append("if (response != null) {" + Environment.NewLine);
            template.Append("return Ok(response);" + Environment.NewLine);
            template.Append("} else {" + Environment.NewLine);
            template.Append("return NotFound();" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);


            // Put
            template.Append("[HttpPut]" + Environment.NewLine);
            template.Append("public IHttpActionResult Insert" + this.tableName + "(" + this.tableName + " " + Helpers.FirstCharacterToLower(this.tableName) + ") {" + Environment.NewLine + Environment.NewLine);
            template.Append("var repo = new " + this.tableName + "Repository();" + Environment.NewLine);
            template.Append("var response = repo.Get" + this.tableName + "(" + Helpers.FirstCharacterToLower(this.tableName) + ");" + Environment.NewLine);
            template.Append("if (response != null) {" + Environment.NewLine);
            template.Append("return Ok(response);" + Environment.NewLine);
            template.Append("} else {" + Environment.NewLine);
            template.Append("return NotFound();" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);

            // Delete
            template.Append("[HttpDelete]" + Environment.NewLine);
            template.Append("public IHttpActionResult Delete" + this.tableName + "(" + this.tableName + " " + Helpers.FirstCharacterToLower(this.tableName) + ") {" + Environment.NewLine + Environment.NewLine);
            template.Append("var repo = new " + this.tableName + "Repository();" + Environment.NewLine);
            template.Append("var response = repo.Get" + this.tableName + "(" + Helpers.FirstCharacterToLower(this.tableName) + ");" + Environment.NewLine);
            template.Append("if (response != null) {" + Environment.NewLine);
            template.Append("return Ok(response);" + Environment.NewLine);
            template.Append("} else {" + Environment.NewLine);
            template.Append("return NotFound();" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);



            template.Append("}" + Environment.NewLine);

            return template.ToString();

        }
    
        private String GenerateAPIRepository() {

            template.Append("using nextGenAPI.DataAccess.Common;" + Environment.NewLine);
            template.Append("using System;" + Environment.NewLine);
            template.Append("using System.Collections.Generic;" + Environment.NewLine);
            template.Append("using System.Data.SqlClient;" + Environment.NewLine);
            template.Append(Environment.NewLine);

            template.Append("namespace nextGenAPI.DataAccess." + this.tableName + " {" + Environment.NewLine);

            template.Append("internal class " + this.tableName + " : RepositoryBase {" + Environment.NewLine);

            // Select
            template.Append("public List<" + this.tableName + "> Get" + this.tableName + "() {" + Environment.NewLine + Environment.NewLine);
            template.Append("var items = new List<" + this.tableName + ">();" + Environment.NewLine);
            template.Append("using (var connection = new SqlConnection(ConnectionString)) {" + Environment.NewLine);
            template.Append("connection.Open();" + Environment.NewLine + Environment.NewLine);
            template.Append("var cmd = new " + this.tableName + "CommandFactory();" + Environment.NewLine);
            template.Append("using (var command = cmd.Get" + this.tableName + "(connection)) {" + Environment.NewLine + Environment.NewLine);
            template.Append("using (var reader = command.ExecuteReader()) {" + Environment.NewLine);
            template.Append("try {" + Environment.NewLine);
            template.Append(" while (reader.Read()) {" + Environment.NewLine);
            template.Append(Helpers.APIRepositoryColumnAssignment(tableColumns));
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

            // Insert
            template.Append("public Boolean Insert" + this.tableName + "(" + this.tableName + " " + Helpers.FirstCharacterToLower(this.tableName) + ") {" + Environment.NewLine);
            template.Append("var result = true;" + Environment.NewLine + Environment.NewLine);
            template.Append("using (var connection = new SqlConnection(ConnectionString)) {" + Environment.NewLine);
            template.Append("connection.Open();" + Environment.NewLine + Environment.NewLine);
            template.Append("var cmd = new " + this.tableName + "CommandFactory();" + Environment.NewLine);
            template.Append(Helpers.APIRepositoryColumnInsert(tableColumns, tableName) + Environment.NewLine);

            // Update
            template.Append("public Boolean Update" + this.tableName + "(" + this.tableName + " " + Helpers.FirstCharacterToLower(this.tableName) + ") {" + Environment.NewLine);
            template.Append("var result = true;" + Environment.NewLine + Environment.NewLine);
            template.Append("using (var connection = new SqlConnection(ConnectionString)) {" + Environment.NewLine);
            template.Append("connection.Open();" + Environment.NewLine + Environment.NewLine);
            template.Append("var cmd = new " + this.tableName + "CommandFactory();" + Environment.NewLine);
            template.Append(Helpers.APIRepositoryColumnUpdate(tableColumns, tableName) + Environment.NewLine);


            // Class & Namespace
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

            // Retrieve all records in table
            template.Append("internal SqlCommand  Get" + this.tableName + "s(SqlConnection connection) {" + Environment.NewLine);
            template.Append("var queryString = @\"" + Environment.NewLine);
            template.Append( Helpers.SQLSelectStatement(tableColumns, this.tableName, false) + Environment.NewLine);
            template.Append(" var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);" + Environment.NewLine + Environment.NewLine);
            template.Append(" return cmd;" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);

            // Retrieve record by primary key
            template.Append("internal SqlCommand  Get" + this.tableName + "(SqlConnection connection) {" + Environment.NewLine);
            template.Append("var queryString = @\"" + Environment.NewLine);
            template.Append(Helpers.SQLSelectStatement(tableColumns, this.tableName, true) + Environment.NewLine);
            template.Append(" var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);" + Environment.NewLine + Environment.NewLine);
            template.Append(" return cmd;" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);

            // Insert record
            template.Append("internal SqlCommand  Insert" + this.tableName + "(SqlConnection connection) {" + Environment.NewLine);
            template.Append("var queryString = @\"" + Environment.NewLine);
            template.Append(Helpers.SQLInsertStatement(tableColumns, this.tableName) + Environment.NewLine);
            template.Append(" var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);" + Environment.NewLine + Environment.NewLine);
            template.Append(" return cmd;" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);

            // Update record by primary key
            template.Append("internal SqlCommand  Update" + this.tableName + "(SqlConnection connection) {" + Environment.NewLine);
            template.Append("var queryString = @\"" + Environment.NewLine);
            template.Append(Helpers.SQLUpdateStatement(tableColumns, this.tableName) + Environment.NewLine);
            template.Append(" var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);" + Environment.NewLine + Environment.NewLine);
            template.Append(" return cmd;" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);

            // Delete record by primary key
            template.Append("internal SqlCommand  Delete" + this.tableName + "(SqlConnection connection) {" + Environment.NewLine);
            template.Append("var queryString = @\"" + Environment.NewLine);
            template.Append(Helpers.SQLDeleteStatement(tableColumns, this.tableName) + Environment.NewLine);
            template.Append(" var cmd = new SqlCommand(Common.Helpers.CleanSQLText(queryString), connection);" + Environment.NewLine + Environment.NewLine);
            template.Append(" return cmd;" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);


            template.Append("}" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);


            return template.ToString();
        }
    }
}
