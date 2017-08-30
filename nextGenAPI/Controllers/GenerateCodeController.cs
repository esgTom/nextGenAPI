using nextGenAPI.DataAccess.GenerateCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace nextGenAPI.Controllers {

    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class GenerateCodeController: ApiController {

        // GET api/<controller>
        public IHttpActionResult Get( string selectedTemplateName, int selectedTableId)  {

            var repo = new GenerateCodeRepository();
            var generatedCode = repo.GetGeneratedCode(selectedTemplateName, selectedTableId);

            if (generatedCode != null) {
                return Ok(generatedCode);
            } else {
                return NotFound();
            }

        }

    }

}