using GestionnairePaquet.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GestionnairePaquet.Tests.Controllers
{
    /// <summary>
    /// Permet de tester la page de connexion, d'enregistrement, de confirmation de création de compte, de réinit de mot de passe
    /// </summary>
    [TestClass]
    public class ClientControllerTest
    {
        ClientController CreateClientControllerAs(string userName)
        {

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userName);
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = new ClientController();
            controller.ControllerContext = mock.Object;

            return controller;
        }

        [TestMethod]
        public void Index()
        {
            ClientController controller = CreateClientControllerAs("PlastProdIT");

            ViewResult result = controller.Index() as ViewResult;

            Assert.AreEqual(2, result.Model);
        }
    }
}