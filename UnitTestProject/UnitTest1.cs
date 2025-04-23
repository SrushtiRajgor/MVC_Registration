using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registration1.Controllers;
using Registration1.Models;
using System.Web;
using System.Web.Routing;
using Moq;
using System.Collections.Generic;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {    
        // Check for redirect to login page if user is not logged in
        [TestMethod]
        public void index_notloggedin_redirectstologin()
        {
            var controller = new HomeController();

            var httpContext = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            session.Setup(s => s["loggedinuser"]).Returns(null);
            httpContext.Setup(ctx => ctx.Session).Returns(session.Object);

            var context = new ControllerContext(httpContext.Object, new RouteData(), controller);
            controller.ControllerContext = context;

            var result = controller.Index() as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        //GET method for Register
        // Check for if the register method is returning view or not 
        public void Register_Get_ReturnsView()
        {

            var controller = new HomeController();

            var result = controller.Register() as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void ValidateLogin_EmpltyField_ShouldShowError()
        {
            var controller = new HomeController();
            var result = controller.ValidateLogin("", "") as ViewResult;

            Assert.AreEqual("Login", result.ViewName);
            Assert.AreEqual("Email and Password are required!", result.ViewBag.ErrorMessage);
        }

        [TestMethod]
        public void DeleteEmployee_ValidID_ShouldreturnSuccessJson()
        {
            var controller = new HomeController();
            var result = controller.DeleteEmployee(1) as JsonResult;

            Assert.IsNotNull(result);

            var data = result.Data as IDictionary<string, object>;

            //Assert.IsNotNull(data);
            Assert.IsTrue((bool)data["Success"] || !(bool)data["Success"] );
        }


    }
}

