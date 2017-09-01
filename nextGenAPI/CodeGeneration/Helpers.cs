using nextGenAPI.DataAccess.TableDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nextGenAPI.CodeGeneration {
    internal static class Helpers {

        public static String APIModelProperty(TableColumnDefinition column) {

            if (column.ColumnNullable) {
                return string.Format("public {0}? {1} ", column.ColumnAPIDataType, column.ColumnAPIName) + "{ get; set; }";
            } else {
                return string.Format("public {0} {1} ", column.ColumnAPIDataType, column.ColumnAPIName) + "{ get; set; }";
            }
           
        }

        public static String ClientModelProperty(TableColumnDefinition column) {
            return string.Format("    {0}: {1}", column.ColumnClientName, column.ColumnClientDataType) + ";";
        }


        #region API Repository Methods
        public static String APIRepositoryColumnAssignment(List<TableColumnDefinition> tableColumns) {
            // Assigns column from datareader to local c# variable in a API Repository datareader while loop

            var template = new StringBuilder();
            foreach (var column in tableColumns) {

                if (column.ColumnNullable && column.ColumnAPIDataType != "String") {
                    template.Append(string.Format("var {0} = ({1}?)(reader[\"{2}\"] == DBNull.Value ? null : reader[\"{3}\"]);",
                        FirstCharacterToLower(column.ColumnAPIName), column.ColumnAPIDataType, column.ColumnSQLName, column.ColumnSQLName) + Environment.NewLine);
                } else {
                    template.Append(string.Format("var {0} = ({1})(reader[\"{2}\"]);", FirstCharacterToLower(column.ColumnAPIName), column.ColumnAPIDataType, column.ColumnSQLName) + Environment.NewLine);
                }
            }
            return template.ToString();
        }

        public static String APIRepositoryColumnInsert(List<TableColumnDefinition> tableColumns, string tableName) {

            var template = new StringBuilder();

            template.Append("using (var command = cmd.Insert" + tableName + "(connection)) {" + Environment.NewLine + Environment.NewLine);

            foreach (var column in tableColumns) {
                template.Append($"cmd.Parameters[\"{ GetAPISQLParameterName(column.ColumnAPIName)}\"].Value = { Helpers.FirstCharacterToLower(tableName) }.{ column.ColumnAPIName };" + Environment.NewLine);
            }

            template.Append(Environment.NewLine + "try {" + Environment.NewLine);
            template.Append("command.ExecuteNonQuery();");
            template.Append("} catch (Exception exception) {" + Environment.NewLine);
            template.Append("result = false;" + Environment.NewLine);
            template.Append("} finally {}" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);
            template.Append("return result;" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);

            return template.ToString();

        }

        public static String APIRepositoryColumnUpdate(List<TableColumnDefinition> tableColumns, string tableName) {

            var template = new StringBuilder();


            template.Append("using (var command = cmd.Update" + tableName + "(connection)) {" + Environment.NewLine + Environment.NewLine);

            foreach (var column in tableColumns) {
                template.Append($"cmd.Parameters[\"{ GetAPISQLParameterName(column.ColumnAPIName)}\"].Value = { Helpers.FirstCharacterToLower(tableName) }.{ column.ColumnAPIName };" + Environment.NewLine);
            }

            template.Append(Environment.NewLine + "try {" + Environment.NewLine);
            template.Append("command.ExecuteNonQuery();");
            template.Append("} catch (Exception exception) {" + Environment.NewLine);
            template.Append("result = false;" + Environment.NewLine);
            template.Append("} finally {}" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);
            template.Append("return result;" + Environment.NewLine);
            template.Append("}" + Environment.NewLine);

            return template.ToString();

        }

        #endregion


        #region API Command Factory Methods

        public static String SQLSelectStatement(List<TableColumnDefinition> tableColumns, string tableName, bool isPrimaryKeyGet) {
            // Returns a SQL select statment that returns all columns in the table

            var template = new StringBuilder();
            var itemCount = tableColumns.Count;

            template.Append("SET NOCOUNT ON " + Environment.NewLine);
            template.Append("SELECT " + Environment.NewLine);
            foreach (var column in tableColumns) {
                itemCount--;
                if (itemCount > 0) {
                    template.Append($"{column.ColumnSQLName}, " + Environment.NewLine);
                } else {
                    template.Append(column.ColumnSQLName + Environment.NewLine);
                }
            }

            template.Append("FROM " + tableName + Environment.NewLine);

            if (isPrimaryKeyGet) {
                var primaryKeyColumn = GetPrimaryKeyColumn(tableColumns);
                template.Append($"WHERE { primaryKeyColumn.ColumnAPIName} = {GetAPISQLParameterName(primaryKeyColumn.ColumnAPIName)} " + Environment.NewLine);
            }

            template.Append("\";" + Environment.NewLine);
            return template.ToString();
        }

        public static String SQLInsertStatement(List<TableColumnDefinition> tableColumns, string tableName ) {
            // Returns a SQL Insert statement inserting all columns in the table

            var template = new StringBuilder();

            template.Append("SET NOCOUNT ON " + Environment.NewLine );
            template.Append("INSERT INTO " + tableName + "(" + Environment.NewLine);
            template.Append(GetSQLColumnList(tableColumns));
            template.Append(") " + Environment.NewLine);

            template.Append("VALUES (");
            template.Append(GetAPISQLParameterList(tableColumns));
            template.Append(") " + Environment.NewLine);

            template.Append("\";" + Environment.NewLine);
            return template.ToString();
        }


        public static String SQLUpdateStatement(List<TableColumnDefinition> tableColumns, string tableName) {
            // Returns a SQL update statement updating all columns in the table for the specified primary key

            var template = new StringBuilder();

            template.Append("SET NOCOUNT ON " + Environment.NewLine);
            template.Append("UPDATE " + tableName + "(" + Environment.NewLine);
            template.Append(GetSQLUpdateColumnList(tableColumns));

            var primaryKeyColumn = GetPrimaryKeyColumn(tableColumns);
            template.Append($"WHERE { primaryKeyColumn.ColumnAPIName} = {GetAPISQLParameterName(primaryKeyColumn.ColumnAPIName)} " + Environment.NewLine);

            template.Append("\";" + Environment.NewLine);
            return template.ToString();
        }

        public static String SQLDeleteStatement(List<TableColumnDefinition> tableColumns, string tableName) {
            // Returns a SQL delete statement that deletes the specified primary key

            var template = new StringBuilder();

            template.Append("SET NOCOUNT ON " + Environment.NewLine);
            template.Append("DELETE " + tableName + Environment.NewLine);

            var primaryKeyColumn = GetPrimaryKeyColumn(tableColumns);
            template.Append($"WHERE { primaryKeyColumn.ColumnAPIName} = {GetAPISQLParameterName(primaryKeyColumn.ColumnAPIName)} " + Environment.NewLine);

            template.Append("\";" + Environment.NewLine);
            return template.ToString();
        }
        #endregion

             
        public static String APIParameterList(List<TableColumnDefinition> tableColumns) {
            // Returns a comma-seperated list of API column names

            var template = new StringBuilder();
            var itemCount = tableColumns.Count;

            foreach (var column in tableColumns) {
                itemCount--;
                if (itemCount > 0) {
                    template.Append($"{FirstCharacterToLower(column.ColumnAPIName)}, ");
                } else {
                    template.Append(FirstCharacterToLower(column.ColumnAPIName));
                }
            }
            return template.ToString();
        }

        public static String GetSQLColumnList(List<TableColumnDefinition> tableColumns) {
            // Returns list of SQL column names for use in SQL Insert statements

            var template = new StringBuilder();
            var itemCount = tableColumns.Count;

            foreach (var column in tableColumns) {
                itemCount--;
                if (itemCount > 0) {
                    template.Append($"{column.ColumnSQLName}, ");
                } else {
                    template.Append(column.ColumnSQLName);
                }
            }
            return template.ToString();
        }

        public static String GetAPISQLParameterList(List<TableColumnDefinition> tableColumns) {
            // Returns list of parameter names for use in SQL statements ex: @WorkoutId, @WorkoutType, etc.
            var itemCount = tableColumns.Count;
            var template = new StringBuilder();

            foreach (var column in tableColumns) {
                itemCount--;
                if (itemCount > 0) {
                    template.Append($"{GetAPISQLParameterName(column.ColumnAPIName)}, ");
                } else {
                    template.Append(GetAPISQLParameterName(column.ColumnAPIName));
                }
            }
            return template.ToString();
        }

        public static String GetSQLUpdateColumnList(List<TableColumnDefinition> tableColumns) {
            // Returns list of parameter names for use in SQL statements ex: @WorkoutId, @WorkoutType, etc.
            var itemCount = tableColumns.Count;
            var template = new StringBuilder();

            foreach (var column in tableColumns) {
                itemCount--;
                if (itemCount > 0) {
                    template.Append($"{column.ColumnSQLName} = {GetAPISQLParameterName(column.ColumnAPIName)}," + Environment.NewLine);
                } else {
                    template.Append($"{column.ColumnSQLName} = {GetAPISQLParameterName(column.ColumnAPIName)}" + Environment.NewLine);
                }
            }
            return template.ToString();
        }



        public static TableColumnDefinition GetPrimaryKeyColumn(List<TableColumnDefinition> tableColumns) {
            // Returns the primary key column for the table 
            return tableColumns.Where(c => c.ColumnPrimaryKey == true).FirstOrDefault();
        }
        

        public static String GetAPISQLParameterName(string columnName) {
            // Returns parameter name for use in SQL statements ex: @WorkoutId
            return $"@{columnName}"; ;
        }





        #region Import Metadata methods
        public static String GetColumnAPIName(string columnName) {
            // Returns the name of the column for use in the API
            return columnName.Replace("_", "");
        }

        public static String GetColumnClientName(string columnName) {
            // Returns the name of the column for use in the API
            return columnName.Replace("_", "");
        }

        public static String GetColumnAPIDataType(string columnDataType) {
            // Returns the .Net data type for use in the API

            var returnDataType = string.Empty;

            switch (columnDataType) {

                case "varchar":
                case "nvarchar":
                case "text":
                case "ntext":
                case "char": {
                        returnDataType =  "String";
                        break;
                }

                case "uniqueidentifier": {
                        returnDataType = "GUID";
                        break;
                    }

                case "rowversion": {
                        returnDataType = "Byte[]";
                        break;
                    }

                case "bit": {
                        returnDataType = "Boolean";
                        break;
                    }

                case "tinyint": {
                        returnDataType = "Byte";
                        break;
                    }

                case "smallint":
                case "int": {
                        returnDataType = "int";
                        break;
                    }

                case "bigint": {
                        returnDataType = "int64";
                        break;
                    }

                case "smallmoney":
                case "money":
                case "numeric":
                case "decimal": {
                        returnDataType = "Decimal";
                        break;
                    }

                case "real": {
                        returnDataType = "Single";
                        break;
                    }

                case "float": {
                        returnDataType = "Double";
                        break;
                    }

                case "smalldatetime":
                case "datetime": {
                        returnDataType = "DateTime";
                        break;
                    }


                default:
                    break;
            }

            return returnDataType;

        }

        public static String GetColumnClientDataType(string columnDataType) {
            // Returns the TypeScript data type for use on the client

            var returnDataType = string.Empty;

            switch (columnDataType) {

                case "varchar":
                case "nvarchar":
                case "text":
                case "ntext":
                case "char":
                case "smalldatetime":
                case "datetime":
                case "uniqueidentifier":
                case "rowversion": {
                        returnDataType = "string";
                        break;
                    }

                case "bit": {
                        returnDataType = "boolean";
                        break;
                    }

                case "tinyint":
                case "smallint":
                case "int":
                case "bigint":
                case "smallmoney":
                case "money":
                case "numeric":
                case "decimal":
                case "real":
                case "float": {
                        returnDataType = "number";
                        break;
                    }

                default:
                    break;
            }

            return returnDataType;

        }

#endregion

        #region Utility Methods

        public static string FirstCharacterToLower(string str) {
            if (String.IsNullOrEmpty(str) || Char.IsLower(str, 0))
                return str;

            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        #endregion

    }
}