using nextGenAPI.DataAccess.TableDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

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



        public static String APIRepositoryColumnAssignment(TableColumnDefinition column) {
            // Assigns column from datareader to local c# variable in a API Repository datareader while loop

            if (column.ColumnNullable) {
                return string.Format("var {0} = ({1}?)(reader[\"{2}\"] == DBNull.Value ? null : reader[\"{3}\"]);", 
                    FirstCharacterToLower(column.ColumnAPIName), column.ColumnAPIDataType, column.ColumnSQLName, column.ColumnSQLName) + Environment.NewLine;
            } else {
                return string.Format("var {0} = ({1})(reader[\"{2}\"]);", FirstCharacterToLower(column.ColumnAPIName), column.ColumnAPIDataType, column.ColumnSQLName) + Environment.NewLine;
            }

        }

        public static String APIParameterList(List<TableColumnDefinition> tableColumns) {
            // Returns a comma-seperated list of API column names
            var template = new StringBuilder();
            var itemCount = tableColumns.Count;
            foreach (var column in tableColumns) {
                itemCount --;
                if(itemCount > 0) {
                    template.Append($"{FirstCharacterToLower(column.ColumnAPIName)}, ");
                } else {
                    template.Append(FirstCharacterToLower(column.ColumnAPIName));
                }
            }
            return template.ToString();
        }

        public static String SQLSelectStatement(List<TableColumnDefinition> tableColumns, string tableName) {
            // Returns a SQL select returning all columns in the table

            var template = new StringBuilder();
            var itemCount = tableColumns.Count;
           
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
            template.Append("\";" + Environment.NewLine);
            return template.ToString();
        }


        #region Utility Methods

        public static string FirstCharacterToLower(string str) {
            if (String.IsNullOrEmpty(str) || Char.IsLower(str, 0))
                return str;

            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        #endregion

    }
}