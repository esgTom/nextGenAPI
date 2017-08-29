using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nextGenAPI.DataAccess.Common {
    public class RepositoryBase {

        private string connectionString;
        public string ConnectionString {
            get {
                if (connectionString == null) {
                    connectionString = GetConnectionString();
                    if (connectionString == null) {
                        throw new Exception("API Connection string not found");
                    }
                }
                return connectionString;
            }
        }

        private string GetConnectionString() {
            return System.Configuration.ConfigurationManager.ConnectionStrings["nextGenSQLConnection"].ConnectionString;
        }

    }
}