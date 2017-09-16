using nextGenAPI.DataAccess.Codes;
using nextGenAPI.DataAccess.Project;
using System.Web.Http;
using System.Web.Http.Cors;

namespace nextGenAPI.Controllers {

    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ProjectController : ApiController{

        public IHttpActionResult Get() {

            var repo = new DataAccess.Project.ProjectRepository();
            var recentActivity = repo.GetProjects();

            if (recentActivity != null) {
                return Ok(recentActivity);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        public IHttpActionResult InsertProject(Project project) {

            var repo = new ProjectRepository();
            var insertedProject = repo.InsertProject(project);

            if (insertedProject != null) {
                return Ok(insertedProject);
            } else {
                return BadRequest();
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateProject(Project project) {

            var repo = new ProjectRepository();
            var updatedProject = repo.UpdateProject(project);

            if (updatedProject != null) {
                return Ok(updatedProject);
            } else {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteProject(Project project) {

            var repo = new ProjectRepository();
            var result = repo.DeleteProject(project);

            if (result) {
                return Ok();
            } else {
                return BadRequest();
            }
        }

    }
}