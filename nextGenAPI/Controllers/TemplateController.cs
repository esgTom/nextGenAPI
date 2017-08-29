using nextGenAPI.DataAccess.Template;
using System.Web.Http;
using System.Web.Http.Cors;

namespace nextGenAPI.Controllers {
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class TemplateController : ApiController {

        // GET api/<controller>
        public IHttpActionResult Get() {

            var repo = new TemplateRepository();
            var recentActivity = repo.GetTemplates();

            if (recentActivity != null) {
                return Ok(recentActivity);
            } else {
                return NotFound();
            }

        }

    }
}