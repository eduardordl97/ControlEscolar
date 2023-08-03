using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var session = Session["rolSession"];
            if (session == "ADMIN")
            {
                return View();
            }
            else if (session == "STUDENT")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
                
            }
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}