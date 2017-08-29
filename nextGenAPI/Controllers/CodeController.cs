using nextGenAPI.DataAccess.Codes;
using System.Web.Http;
using System.Web.Http.Cors;

namespace nextGenAPI.Controllers {
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class CodeController : ApiController {

        // GET api/<controller>
        public IHttpActionResult Get() {

            var repo = new CodeRepository();
            var recentActivity = repo.GetCodes();

            if(recentActivity != null) {
                return Ok(recentActivity);
            } else {
                return NotFound();
            }
            
        }

        // GET api/<controller>/5
        public string Get(int id) {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value) {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/<controller>/5
        public void Delete(int id) {
        }

    }
}