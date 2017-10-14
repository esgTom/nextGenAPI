using nextGenAPI.DataAccess.Codes;
using nextGenAPI.DataAccess.Project;
using System.Web.Http;
using System.Web.Http.Cors;

namespace nextGenAPI.Controllers {

    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ProjectController : ApiController {

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
        public IHttpActionResult SaveProjectChanges(Project project) {

            var repo = new ProjectRepository();

            Project savedProject;
            if (project.ProjectId == 0) {
                savedProject = repo.InsertProject(project);
            } else {
                savedProject = repo.UpdateProject(project);
            }

            if (savedProject != null) {
                return Ok(savedProject);
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