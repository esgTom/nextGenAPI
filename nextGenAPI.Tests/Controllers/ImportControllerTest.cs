using Microsoft.VisualStudio.TestTools.UnitTesting;
using nextGenAPI.DataAccess.Import;
using System.Collections.Generic;

namespace nextGenAPI.Tests.Controllers {
    
    [TestClass]
    public class ImportControllerTest {

        [TestMethod]
        public void Get() {

            var repo = new ImportRepository();
            var columnDefs = new List<ColumnImportDefinition>();

            columnDefs.Add(LoadImportColumnDef("Workout", "Workout_Id", "int", 0, 0, false, true, 0));
            columnDefs.Add(LoadImportColumnDef("Workout", "Workout_Category", "varchar", 0, 0, false, false, 1));
            columnDefs.Add(LoadImportColumnDef("Workout", "Workout_Type", "varchar", 0, 0, true, false, 2));
            columnDefs.Add(LoadImportColumnDef("Workout", "Workout_Date", "datetime", 0, 0, true, false, 3));

            columnDefs.Add(LoadImportColumnDef("Schedule", "Schedule_Id", "int", 0, 0, false, true, 0));
            columnDefs.Add(LoadImportColumnDef("Schedule", "Schedule_Date", "int", 0, 0, true, false, 1));
            columnDefs.Add(LoadImportColumnDef("Schedule", "Schedule_Type", "int", 0, 0, true, false, 2));

            columnDefs.Add(LoadImportColumnDef("NewTable", "NewTable_Id", "int", 0, 0, true, true, 0));

            var result = repo.LoadTableColumnDefinitions(columnDefs);


            // Assert
            Assert.IsTrue(result);

        }


        private ColumnImportDefinition LoadImportColumnDef(string tableName, string columnName, string columnDataType, int columnLength, int columnDecimalPlaces, bool columnNullable, 
            bool columnPrimaryKey, int columnOrdinalPosition) {

            var columnDef = new ColumnImportDefinition();
            columnDef.TableName = tableName;
            columnDef.ColumnName = columnName;
            columnDef.ColumnDataType = columnDataType;
            columnDef.ColumnLength = columnLength;
            columnDef.ColumnNullable = columnNullable;
            columnDef.ColumnPrimaryKey = columnPrimaryKey;
            columnDef.ColumnOrdinalPosition = columnOrdinalPosition;
            return columnDef;
        }
    }
}
