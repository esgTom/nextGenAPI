using System;

namespace nextGenAPI.DataAccess.Import {
    public class ColumnImportDefinition {

        public String TableName { get; set; }
        public String ColumnName { get; set; }
        public String ColumnDataType { get; set; }
        public int ColumnLength { get; set; }
        public int ColumnDecimalPlaces { get; set; }
        public bool ColumnNullable { get; set; }
        public bool ColumnPrimaryKey { get; set; }
        public int ColumnOrdinalPosition { get; set; }

    }
}