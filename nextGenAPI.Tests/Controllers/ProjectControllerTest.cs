using Microsoft.VisualStudio.TestTools.UnitTesting;
using nextGenAPI.Controllers;
using nextGenAPI.DataAccess.Project;
using System;
using System.Net;
using System.Web.Http.Results;

namespace nextGenAPI.Tests.Controllers {

    [TestClass]
    public class ProjectControllerTest {

        int insertedProjectId = 0;

        [TestMethod]
        public void Tests() {

            // GET
            ProjectController projectController = new ProjectController();
            var result = projectController.Get();
            Assert.IsNotNull(result);


            // INSERT
            var project = new Project(0, "test Project Name", "test Project Description", "API Test", DateTime.Now, "API TEST", DateTime.Now);
            result = projectController.InsertProject(project);
            var contentResult = result as OkNegotiatedContentResult<Project>;

            Assert.IsNotNull(contentResult);
            Assert.AreNotEqual(0, contentResult.Content.ProjectId);

            insertedProjectId = contentResult.Content.ProjectId;
            Assert.AreNotEqual(0, insertedProjectId);

            // UPDATE
            project = new Project(insertedProjectId, "test Project Name UPDATE", "test Project Description UPDATE", "APIUPTest", DateTime.Now, "APIUPTEST", DateTime.Now);
            result = projectController.UpdateProject(project);

            Assert.IsNotNull(result);
            Assert.AreEqual("APIUPTEST", project.ModifiedBy);
            Assert.AreNotEqual(0, insertedProjectId);


            // DELETE
            Assert.AreNotEqual(0, insertedProjectId);
            project = new Project(insertedProjectId, "test Project Name", "test Project Description", "APIINTest", DateTime.Now, "APIUPTEST", DateTime.Now);

            result = projectController.DeleteProject(project);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetType().Name, "OkResult");

        }
    }
} 
