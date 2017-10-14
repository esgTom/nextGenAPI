using nextGenAPI.DataAccess.Column;
using nextGenAPI.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace nextGenAPI.DataAccess.Column {
    internal class ColumnRepository : RepositoryBase {

        public List<ColumnDefinition> GetColumns(int tableId) {

            var items = new List<ColumnDefinition>();
            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new ColumnCommandFactory();
                using (var command = cmd.GetColumn_Definition(connection)) {

                    command.Parameters["@tableId"].Value = tableId;

                    using (var reader = command.ExecuteReader()) {
                        try {
                            while (reader.Read()) {
                                var columnAPIDataType = (String)(reader["Column_API_Data_Type"]);
                                var columnAPIName = (String)(reader["Column_API_Name"]);
                                var columnClientDataType = (String)(reader["Column_Client_Data_Type"]);
                                var columnClientName = (String)(reader["Column_Client_Name"]);
                                var columnDecimalPlaces = (int?)(reader["Column_Decimal_Places"] == DBNull.Value ? null : reader["Column_Decimal_Places"]);
                                var columnDescription = (String)(reader["Column_Description"]);
                                var columnId = (int)(reader["Column_Id"]);
                                var columnLength = (int?)(reader["Column_Length"] == DBNull.Value ? null : reader["Column_Length"]);
                                var columnName = (String)(reader["Column_Name"]);
                                var columnNullable = (Byte)(reader["Column_Nullable"]);
                                var columnOrdinalPosition = (int)(reader["Column_Ordinal_Position"]);
                                var columnPrimaryKey = (Byte)(reader["Column_Primary_Key"]);
                                var columnSQLDataType = (String)(reader["Column_SQL_Data_Type"]);
                                var columnSQLName = (String)(reader["Column_SQL_Name"]);
                                var createdBy = (String)(reader["Created_By"]);
                                var createdDate = (DateTime)(reader["Created_Date"]);
                                var modifiedBy = (String)(reader["Modified_By"]);
                                var modifiedDate = (DateTime)(reader["Modified_Date"]);
                                var tableID = (int)(reader["Table_Id"]);

                            //    items.Add(new ColumnDefinition(columnAPIDataType, columnAPIName, columnClientDataType, columnClientName, columnDecimalPlaces, columnDescription, columnId, columnLength, columnName, columnNullable, columnOrdinalPosition, columnPrimaryKey, columnSQLDataType, columnSQLName, createdBy, createdDate, modifiedBy, modifiedDate, tableID));
                            }
                        } finally {
                            reader.Close();
                        }
                    }
                }
            }
            return items;
        }
        public Boolean InsertColumn(ColumnDefinition column_Definition) {
            var result = true;

            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new ColumnCommandFactory();
                using (var command = cmd.InsertColumn_Definition(connection)) {

                    command.Parameters["@ColumnAPIDataType"].Value = column_Definition.ColumnAPIDataType;
                    command.Parameters["@ColumnAPIName"].Value = column_Definition.ColumnAPIName;
                    command.Parameters["@ColumnClientDataType"].Value = column_Definition.ColumnClientDataType;
                    command.Parameters["@ColumnClientName"].Value = column_Definition.ColumnClientName;
                    command.Parameters["@ColumnDecimalPlaces"].Value = column_Definition.ColumnDecimalPlaces;
                    command.Parameters["@ColumnDescription"].Value = column_Definition.ColumnDescription;
                    command.Parameters["@ColumnId"].Value = column_Definition.ColumnId;
                    command.Parameters["@ColumnLength"].Value = column_Definition.ColumnLength;
                    command.Parameters["@ColumnName"].Value = column_Definition.ColumnName;
                    command.Parameters["@ColumnNullable"].Value = column_Definition.ColumnNullable;
                    command.Parameters["@ColumnOrdinalPosition"].Value = column_Definition.ColumnOrdinalPosition;
                    command.Parameters["@ColumnPrimaryKey"].Value = column_Definition.ColumnPrimaryKey;
                    command.Parameters["@ColumnSQLDataType"].Value = column_Definition.ColumnSQLDataType;
                    command.Parameters["@ColumnSQLName"].Value = column_Definition.ColumnSQLName;
                    command.Parameters["@CreatedBy"].Value = column_Definition.CreatedBy;
                    command.Parameters["@CreatedDate"].Value = column_Definition.CreatedDate;
                    command.Parameters["@ModifiedBy"].Value = column_Definition.ModifiedBy;
                    command.Parameters["@ModifiedDate"].Value = column_Definition.ModifiedDate;
                    command.Parameters["@TableId"].Value = column_Definition.TableId;

                    try {
                        command.ExecuteNonQuery();
                    } catch (Exception exception) {
                        result = false;
                    } finally { }
                }
            }
            return result;
        }

        public Boolean UpdateColumn(ColumnDefinition column_Definition) {
            var result = true;

            using (var connection = new SqlConnection(ConnectionString)) {
                connection.Open();

                var cmd = new ColumnCommandFactory();
                using (var command = cmd.UpdateColumn_Definition(connection)) {

                    command.Parameters["@ColumnAPIDataType"].Value = column_Definition.ColumnAPIDataType;
                    command.Parameters["@ColumnAPIName"].Value = column_Definition.ColumnAPIName;
                    command.Parameters["@ColumnClientDataType"].Value = column_Definition.ColumnClientDataType;
                    command.Parameters["@ColumnClientName"].Value = column_Definition.ColumnClientName;
                    command.Parameters["@ColumnDecimalPlaces"].Value = column_Definition.ColumnDecimalPlaces;
                    command.Parameters["@ColumnDescription"].Value = column_Definition.ColumnDescription;
                    command.Parameters["@ColumnId"].Value = column_Definition.ColumnId;
                    command.Parameters["@ColumnLength"].Value = column_Definition.ColumnLength;
                    command.Parameters["@ColumnName"].Value = column_Definition.ColumnName;
                    command.Parameters["@ColumnNullable"].Value = column_Definition.ColumnNullable;
                    command.Parameters["@ColumnOrdinalPosition"].Value = column_Definition.ColumnOrdinalPosition;
                    command.Parameters["@ColumnPrimaryKey"].Value = column_Definition.ColumnPrimaryKey;
                    command.Parameters["@ColumnSQLDataType"].Value = column_Definition.ColumnSQLDataType;
                    command.Parameters["@ColumnSQLName"].Value = column_Definition.ColumnSQLName;
                    command.Parameters["@CreatedBy"].Value = column_Definition.CreatedBy;
                    command.Parameters["@CreatedDate"].Value = column_Definition.CreatedDate;
                    command.Parameters["@ModifiedBy"].Value = column_Definition.ModifiedBy;
                    command.Parameters["@ModifiedDate"].Value = column_Definition.ModifiedDate;
                    command.Parameters["@TableId"].Value = column_Definition.TableId;

                    try {
                        command.ExecuteNonQuery();
                    } catch (Exception exception) {
                        result = false;
                    } finally { }
                }
            }
            return result;
        }

    }
}
