using System;

namespace nextGenAPI.DataAccess.Column {

    public class ColumnDefinition {
        public String ColumnAPIDataType { get; set; }
        public String ColumnAPIName { get; set; }
        public String ColumnClientDataType { get; set; }
        public String ColumnClientName { get; set; }
        public int? ColumnDecimalPlaces { get; set; }
        public String ColumnDescription { get; set; }
        public int ColumnId { get; set; }
        public int? ColumnLength { get; set; }
        public String ColumnName { get; set; }
        public Byte ColumnNullable { get; set; }
        public int ColumnOrdinalPosition { get; set; }
        public Byte ColumnPrimaryKey { get; set; }
        public String ColumnSQLDataType { get; set; }
        public String ColumnSQLName { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public String ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int TableId { get; set; }

        public ColumnDefinition( string ColumnAPIDataType) {
            this.ColumnAPIDataType = ColumnAPIDataType;
        }
    }
    using System;

namespace nextGenAPI.DataAccess.Column_Definition {

        public class Column_Definition {
            public String? ColumnAPIDataType { get; set; }
            public String? ColumnAPIName { get; set; }
            public String? ColumnClientDataType { get; set; }
            public String? ColumnClientName { get; set; }
            public int? ColumnDecimalPlaces { get; set; }
            public String? ColumnDescription { get; set; }
            public int ColumnId { get; set; }
            public int? ColumnLength { get; set; }
            public String ColumnName { get; set; }
            public Byte ColumnNullable { get; set; }
            public int ColumnOrdinalPosition { get; set; }
            public Byte ColumnPrimaryKey { get; set; }
            public String? ColumnSQLDataType { get; set; }
            public String? ColumnSQLName { get; set; }
            public String CreatedBy { get; set; }
            public DateTime CreatedDate { get; set; }
            public String ModifiedBy { get; set; }
            public DateTime ModifiedDate { get; set; }
            public int TableId { get; set; }
            public Column_Definition(String columnAPIDataType, String columnAPIName, String columnClientDataType, String columnClientName, int columnDecimalPlaces, String columnDescription, int columnId, int columnLength, String columnName, Byte columnNullable, int columnOrdinalPosition, Byte columnPrimaryKey, String columnSQLDataType, String columnSQLName, String createdBy, DateTime createdDate, String modifiedBy, DateTime modifiedDate, int tableId) {

                this.ColumnAPIDataType = columnAPIDataType;
                this.ColumnAPIName = columnAPIName;
                this.ColumnClientDataType = columnClientDataType;
                this.ColumnClientName = columnClientName;
                this.ColumnDecimalPlaces = columnDecimalPlaces;
                this.ColumnDescription = columnDescription;
                this.ColumnId = columnId;
                this.ColumnLength = columnLength;
                this.ColumnName = columnName;
                this.ColumnNullable = columnNullable;
                this.ColumnOrdinalPosition = columnOrdinalPosition;
                this.ColumnPrimaryKey = columnPrimaryKey;
                this.ColumnSQLDataType = columnSQLDataType;
                this.ColumnSQLName = columnSQLName;
                this.CreatedBy = createdBy;
                this.CreatedDate = createdDate;
                this.ModifiedBy = modifiedBy;
                this.ModifiedDate = modifiedDate;
                this.TableId = tableId; 
                }
            }
        }


    }
