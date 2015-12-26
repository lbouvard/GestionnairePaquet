using GestionnairePaquet.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestionnairePaquet.Tests.Controllers
{
    /// <summary>
    /// Permet de tester la page de connexion, d'enregistrement, de confirmation de création de compte, de réinit de mot de passe
    /// </summary>
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void Login()
        {
            AccountController controller = new AccountController();

            /* ViewResult result = controller.Login() as ViewResult;
             Assert.IsNotNull(result);*/
        }
    }
}
