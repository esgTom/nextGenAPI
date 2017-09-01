using Microsoft.VisualStudio.TestTools.UnitTesting;
using nextGenAPI.Controllers;
using nextGenAPI.DataAccess.GenerateCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nextGenAPI.Tests.Controllers {
    class GenerateCodeControllerTest {
        [TestClass]
        public class CodeGenerationControllerTest {

            [TestMethod]
            public void Get() {

                // Arrange
                var repo = new GenerateCodeRepository();

                // Act
                //var result = repo.GetGeneratedCode("API-Model", 18);
                //var result = repo.GetGeneratedCode("Client-Model", 18);
                var result = repo.GetGeneratedCode("API-Controller", 51);
                //var result = repo.GetGeneratedCode("API-Repository", 51);
                //var result = repo.GetGeneratedCode("API-CommandFactory", 51);


                // Assert
                Assert.IsNotNull(result);

            }
        }
    }
           
}
