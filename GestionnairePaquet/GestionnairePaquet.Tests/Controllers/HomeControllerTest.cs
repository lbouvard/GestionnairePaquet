﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GestionnairePaquet;
using GestionnairePaquet.Controllers;

namespace GestionnairePaquet.Tests.Controllers
{
    /// <summary>
    /// Permet de tester la page d'accueil, a propos et faq
    /// </summary>
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void APropos()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.APropos() as ViewResult;

            // Assert
            Assert.AreEqual("A propos de nous...", result.ViewBag.Message);
        }

        [TestMethod]
        public void Faq()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Faq() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
