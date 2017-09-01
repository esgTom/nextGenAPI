using nextGenAPI.DataAccess.Import;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace nextGenAPI.Controllers {
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ImportController : ApiController {

        [HttpPost]
        public IHttpActionResult ImportColumnMetaData(List<ColumnImportDefinition> importColumns) {

            var repo = new ImportRepository();
            var loadStatus = repo.LoadTableColumnDefinitions(importColumns);

            if (loadStatus) {
                return Ok();
            } else {
                return BadRequest();
            }

        }

    }
}