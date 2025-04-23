using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registration1.Controllers;
using Registration1.Models;

namespace Unit.tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Index_NotLoggedIn_RedirectsToLogin()
        {
            // Arrange
            var controller = new HomeController();
            controller.Session["LoggedInUser"] = null;

            // Act
            var result = controller.Index() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Register_Get_ReturnsView()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Register() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Register_Post_ValidModel_ReturnsSuccessJson()
        {
            // Arrange
            var controller = new HomeController();
            var model = new Entities
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                Password = "pass123",
                ConfirmPassword = "pass123"
            };

            controller.ModelState.Clear(); // make model valid

            // Act
            var result = controller.Register(model) as JsonResult;
            dynamic data = result.Data;

            // Assert
            Assert.IsTrue(data.Success);
            Assert.AreEqual("Registration successful!", data.message);
        }

        [TestMethod]
        public void Register_Post_InvalidModel_ReturnsErrorJson()
        {
            // Arrange
            var controller = new HomeController();
            var model = new Entities(); // Invalid empty model

            controller.ModelState.AddModelError("FirstName", "Required");

            // Act
            var result = controller.Register(model) as JsonResult;
            dynamic data = result.Data;

            // Assert
            Assert.IsFalse(data.Success);
            Assert.AreEqual("Invalid data submitted", data.message);
        }
    }
}
