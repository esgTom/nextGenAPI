using nextGenAPI.DataAccess.Common;
// using nextGenAPI.DataAccess.Import.DataAccess;
using nextGenAPI.DataAccess.TableDefinition;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace nextGenAPI.DataAccess.Import {
    public class ImportRepository : RepositoryBase {

        public bool LoadTableColumnDefinitions(List<ColumnImportDefinition> columDefinitions) {

            var result = true;
            var tableColumns = new List<TableColumnDefinition>();

            // Create TableDefinition objects, adding column Meta data along the way
            foreach (ColumnImportDefinition columnDef in columDefinitions) {

                if (!string.IsNullOrEmpty(columnDef.TableName)) {

                    // Get Column Names
                    var columnAPIName = CodeGeneration.Helpers.GetColumnAPIName(columnDef.ColumnName);
                    var columnClientName = CodeGeneration.Helpers.GetColumnClientName(columnDef.ColumnName);

                    // Get Column Data types
                    var columnAPIDataType = CodeGeneration.Helpers.GetColumnAPIDataType(columnDef.ColumnDataType);
                    var columnClientDataType = CodeGeneration.Helpers.GetColumnClientDataType(columnDef.ColumnDataType);


                    tableColumns.Add(new TableColumnDefinition(
                        0, columnDef.TableName, 0, columnDef.ColumnPrimaryKey, columnDef.ColumnName, "", columnDef.ColumnDataType, columnAPIDataType, columnClientDataType,
                        columnDef.ColumnLength, columnDef.ColumnDecimalPlaces, columnDef.ColumnNullable,
                        columnDef.ColumnName, columnAPIName, columnClientName, columnDef.ColumnOrdinalPosition,
                        "Import", DateTime.Now, "Import", DateTime.Now));
                }

            }

            var incomingTables = tableColumns.GroupBy(t => t.TableName).Select(group => new ImportTableInfo{
                    TableName = group.First().TableName,
                    TableId = 0
                }).ToList();


            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try {
                    var activtyCmd = new ImportCommandFactory();

                    using (var command = activtyCmd.RemoveTableColumnDefs(connection, transaction)) {
                        // Removes existing Table and Column definitions

                        foreach (ImportTableInfo tableInfo in incomingTables) {
                            command.Parameters["@tableName"].Value = tableInfo.TableName;
                            command.ExecuteNonQuery();
                        }
                    }

                    using (var command = activtyCmd.InsertTableDefs(connection, transaction)) {
                        // Inserts new Table definitions

                        foreach (ImportTableInfo tableInfo in incomingTables) {
                            command.Parameters["@tableName"].Value = tableInfo.TableName;
                            command.Parameters["@CreatedBy"].Value = "Import";
                            command.Parameters["@ModifiedBy"].Value = "Import";
                            command.ExecuteNonQuery();
                            tableInfo.TableId = (int)command.Parameters["@pkValue"].Value;
                        }
                    }

                    var tableId = 0;
                    using (var command = activtyCmd.InsertColumnDefs(connection, transaction)) {
                        // Inserts new Column definitions

                        foreach (TableColumnDefinition table in tableColumns) {

                            tableId = incomingTables.Where(t => t.TableName == table.TableName).Select(t => t.TableId).SingleOrDefault();
                            
                            command.Parameters["@tableId"].Value = tableId;
                            command.Parameters["@ColumnPrimaryKey"].Value = table.ColumnPrimaryKey == false ? 0 : 1;
                            command.Parameters["@ColumnName"].Value = table.ColumnName;
                            command.Parameters["@ColumnDescription"].Value = table.ColumnDescription;
                            command.Parameters["@ColumnSQLDataType"].Value = table.ColumnSQLDataType;
                            command.Parameters["@ColumnAPIDataType"].Value = table.ColumnAPIDataType;
                            command.Parameters["@ColumnClientDataType"].Value = table.ColumnClientDataType;
                            command.Parameters["@ColumnLength"].Value = table.ColumnLength == null ? 0 : table.ColumnLength;
                            command.Parameters["@ColumnDecimalPlaces"].Value = table.ColumnDecimalPlaces == null ? 0 : table.ColumnDecimalPlaces;
                            command.Parameters["@ColumnNullable"].Value = table.ColumnNullable == false ? 0 : 1;
                            command.Parameters["@ColumnSQLName"].Value = table.ColumnSQLName;
                            command.Parameters["@ColumnAPIName"].Value = table.ColumnAPIName;
                            command.Parameters["@ColumnClientName"].Value = table.ColumnClientName;
                            command.Parameters["@ColumnOrdinalPosition"].Value = table.ColumnOrdinalPosition;
                            command.Parameters["@CreatedBy"].Value = "Import";
                            command.Parameters["@ModifiedBy"].Value = "Import";
                                                        
                            command.ExecuteNonQuery();

                        }
                    }


                    transaction.Commit();

                } catch (Exception exception) {
                    transaction.Rollback();
                    result = false;
                } finally {
                    transaction = null;
                }
            }

            return result;

        }
    }

}