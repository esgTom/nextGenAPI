using System;

namespace nextGenAPI.DataAccess.TableDefinition {
    public class TableDefinition {
        public int TableId {get; set;}
        public String TableName {get; set;}          
        public TableDefinition( int tableId, String tableName) {

            this.TableId = tableId;
            this.TableName = tableName;
        }

    }

    public class TableColumnDefinition {
        public int TableId { get; set; }
        public String TableName { get; set; }
        public int ColumnId { get; set; }
        public String ColumnName { get; set; }
        public String ColumnDescription { get; set; }
        public String ColumnSQLDataType { get; set; }
        public String ColumnAPIDataType { get; set; }
        public String ColumnClientDataType { get; set; }
        public int? ColumnLength { get; set; }
        public int? ColumnDecimalPlaces { get; set; }
        public Boolean ColumnNullable { get; set; }
        public String ColumnSQLName { get; set; }
        public String ColumnAPIName { get; set; }
        public String ColumnClientName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }

        public TableColumnDefinition(
            int tableId, String tableName, int columnId, String columnName, String columnDescription,
            String columnSQLDataType, String columnAPIDataType, String columnClientDataType,
            int? columnLength, int? columnDecimalPlaces, Boolean ColumnNullable, String columnSQLName, String columnAPIName, String columnClientName,
            string createdBy, DateTime createdDate, string modifiedBy, DateTime modifiedDate) {

            this.TableId = tableId;
            this.TableName = tableName;
            this.ColumnId = columnId;
            this.ColumnName = columnName;
            this.ColumnDescription = columnDescription;
            this.ColumnSQLDataType = columnSQLDataType;
            this.ColumnAPIDataType = columnAPIDataType;
            this.ColumnClientDataType = columnClientDataType;
            this.ColumnLength = columnLength;
            this.ColumnDecimalPlaces = columnDecimalPlaces;
            this.ColumnNullable = ColumnNullable;
            this.ColumnSQLName = columnSQLName;
            this.ColumnAPIName = columnAPIName;
            this.ColumnClientName = columnClientName;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate.ToShortDateString();
            this.ModifiedBy = modifiedBy;
            this.ModifiedDate = modifiedDate.ToShortDateString();
        }

    }

}