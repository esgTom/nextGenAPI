﻿using nextGenAPI.DataAccess.Common;
using nextGenAPI.DataAccess.TableDefinition;
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

                                var tableId = (int)(reader["Table_Id"]);
                                var tableName = (String)(reader["Table_Name"]);
                                var columnId = (int)(reader["Column_Id"]);
                                var columnName = (String)(reader["Column_Name"]);
                                var columnDescription = (String)(reader["Column_Description"]);
                                var columnSQLDataType = (String)(reader["Column_SQL_Data_Type"]);
                                var columnAPIDataType = (String)(reader["Column_API_Data_Type"]);
                                var columnClientDataType = (String)(reader["Column_API_Data_Type"]);
                                var columnLength = (int?)(reader["Column_Length"] == DBNull.Value ? null : reader["Column_Length"]);
                                var columnDecimalPlaces = (int?)(reader["Column_Decimal_Places"] == DBNull.Value ? null : reader["Column_Decimal_Places"]);  
                                var columnSQLName = (String)(reader["Column_SQL_Name"]);
                                var columnAPIName = (String)(reader["Column_API_Name"]);
                                var columnClientName = (String)(reader["Column_Client_Name"]);
                                var createdBy = (String)(reader["Created_By"] == DBNull.Value ? "" : reader["Created_By"]);
                                var createdDate = (DateTime)(reader["Created_Date"]);
                                var modifiedBy = (String)(reader["Modified_By"] == DBNull.Value ? "" : reader["Modified_By"]);
                                var modifiedDate = (DateTime)(reader["Modified_Date"]);

                                tables.Add(new TableDefinition(
                                        tableId, tableName, columnId, columnName, columnDescription, 
                                        columnSQLDataType, columnAPIDataType, columnClientDataType,
                                        columnLength, columnDecimalPlaces,
                                        columnSQLName, columnAPIName, columnClientName,
                                        createdBy, createdDate, modifiedBy, modifiedDate));
                            }

                        } finally {
                            reader.Close();
                        }
                    }
                }
            }
            return tables;
        }
    }

}