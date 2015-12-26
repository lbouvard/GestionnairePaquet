using GestionnairePaquet.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionnairePaquet.Tests.Controllers
{
    /// <summary>
    /// Permet de tester la page de connexion, d'enregistrement, de confirmation de création de compte, de réinit de mot de passe
    /// </summary>
    [TestClass]
    public class ClientControllerTest
    {
        [TestMethod]
        public void Index()
        {
            ClientController controller = new ClientController();

            /* ViewResult result = controller.Login() as ViewResult;
             Assert.IsNotNull(result);*/
        }
    }
}