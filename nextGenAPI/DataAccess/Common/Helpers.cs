using System.Text.RegularExpressions;


namespace nextGenAPI.DataAccess.Common {
    public static class Helpers {

       public static string CleanSQLText( string sqlText) {

            if (string.IsNullOrEmpty(sqlText)) {
                return sqlText;
            }

            return Regex.Replace(sqlText, @"\r\n?|\n|\r|\t|\s+", " ");
            
        }

    }


}