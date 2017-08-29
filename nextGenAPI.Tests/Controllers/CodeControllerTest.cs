using Microsoft.VisualStudio.TestTools.UnitTesting;
using nextGenAPI.Controllers;
using nextGenAPI.DataAccess.Codes;
using System.Collections.Generic;
using System.Linq;

namespace nextGenAPI.Tests.Controllers {
    class CodeControllerTest {
        [TestClass]
        public class ValuesControllerTest {

            [TestMethod]
            public void Get() {
                // Arrange
                CodeController codeController = new CodeController();

                // Act
                var result = codeController.Get();

                // Assert
                Assert.IsNotNull(result);

            }
        }
    }
}
