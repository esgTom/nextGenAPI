﻿using nextGenAPI.DataAccess.TableDefinition;
using System.Web.Http;
using System.Web.Http.Cors;

namespace nextGenAPI.Controllers {
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class TableDefinitionController : ApiController {

        // GET api/<controller>
        public IHttpActionResult Get(int projectId) {

            var repo = new TableDefinitionRepository();
            var tableDefinition = repo.GetTableDefinition(projectId);

            if (tableDefinition != null) {
                return Ok(tableDefinition);
            } else {
                return NotFound();
            }

        }

    }
}