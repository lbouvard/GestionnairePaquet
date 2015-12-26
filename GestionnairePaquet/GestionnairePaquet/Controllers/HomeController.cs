using GestionnairePaquet.Migrations;
using GestionnairePaquet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionnairePaquet.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult APropos()
        {
            ViewBag.Message = "A propos de nous...";

            return View();
        }

        public ActionResult Faq()
        {
            return View();
        }

    }
}