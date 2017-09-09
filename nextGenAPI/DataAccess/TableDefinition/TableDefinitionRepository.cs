using nextGenAPI.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace nextGenAPI.DataAccess.TableDefinition {
    internal class TableDefinitionRepository : RepositoryBase {

        public List<TableDefinition> GetTableDefinition() {

            var tables = new List<TableDefinition>();
            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var tableDefCmd = new TableDefinitionCommandFactory();
                using (var command = tableDefCmd.GetTableDefinition(connection)) {

                    using (var reader = command.ExecuteReader()) {

                        try {
                            while (reader.Read()) {
                                var projectId = (int)(reader["Project_Id"]);
                                var tableId = (int)(reader["Table_Id"]);
                                var tableName = (String)(reader["Table_Name"]);
                                tables.Add(new TableDefinition(projectId, tableId, tableName));
                            }

                        } finally {
                            reader.Close();
                        }
                    }
                }
            }
            return tables;
        }


        public List<TableColumnDefinition> GetTableColumnsDefinition( int tableId) {

            var tableColumns = new List<TableColumnDefinition>();
            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var tableDefCmd = new TableDefinitionCommandFactory();
                using (var command = tableDefCmd.GetTableColumnDefinition(connection, tableId)) {

                    using (var reader = command.ExecuteReader()) {

                        try {
                            while (reader.Read()) {

                                var columnProjectId = (int)(reader["Project_Id"]);
                                var columnTableId = (int)(reader["Table_Id"]);
                                var tableName = (String)(reader["Table_Name"]);
                                var columnId = (int)(reader["Column_Id"]);
                                var columnPrimaryKey = (Byte)(reader["Column_Primary_Key"]) == 1 ? true : false;
                                var columnName = (String)(reader["Column_Name"]);
                                var columnDescription = (String)(reader["Column_Description"]);
                                var columnSQLDataType = (String)(reader["Column_SQL_Data_Type"]);
                                var columnAPIDataType = (String)(reader["Column_API_Data_Type"]);
                                var columnClientDataType = (String)(reader["Column_Client_Data_Type"]);
                                var columnLength = (int?)(reader["Column_Length"] == DBNull.Value ? null : reader["Column_Length"]);
                                var columnDecimalPlaces = (int?)(reader["Column_Decimal_Places"] == DBNull.Value ? null : reader["Column_Decimal_Places"]);
                                var columnNullable = (Byte)(reader["Column_Nullable"]) == 1 ? true : false ;
                                var columnSQLName = (String)(reader["Column_SQL_Name"]);
                                var columnAPIName = (String)(reader["Column_API_Name"]);
                                var columnClientName = (String)(reader["Column_Client_Name"]);
                                var columnOrdinalPosition = (int)(reader["Column_Ordinal_Position"]);
                                var createdBy = (String)(reader["Created_By"] == DBNull.Value ? "" : reader["Created_By"]);
                                var createdDate = (DateTime)(reader["Created_Date"]);
                                var modifiedBy = (String)(reader["Modified_By"] == DBNull.Value ? "" : reader["Modified_By"]);
                                var modifiedDate = (DateTime)(reader["Modified_Date"]);

                                tableColumns.Add(new TableColumnDefinition(
                                        columnProjectId, columnTableId, tableName, columnId, columnPrimaryKey, columnName, columnDescription,
                                        columnSQLDataType, columnAPIDataType, columnClientDataType,
                                        columnLength, columnDecimalPlaces, columnNullable,
                                        columnSQLName, columnAPIName, columnClientName, columnOrdinalPosition,
                                        createdBy, createdDate, modifiedBy, modifiedDate));
                            }

                        } finally {
                            reader.Close();
                        }
                    }
                }
            }
            return tableColumns;
        }
        
    }
}