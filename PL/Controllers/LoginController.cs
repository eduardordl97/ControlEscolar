﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SignIn(ML.Login login)
        {
            BL.Login loginBL = new BL.Login();
            ML.Result result = new ML.Result();
            try
            {
                result = loginBL.Sp_Valida_Inicio_Sesion(login);
                if (result.Correct == true)
                {
                    if (((ML.Student)result.Object).bAdmin)
                    {
                        System.Web.HttpContext.Current.Session["rolSession"] = "ADMIN";
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Session["rolSession"] = "STUDENT";
                    }
                    System.Web.HttpContext.Current.Session["nameSession"] = ((ML.Student)result.Object).sNombre;
                    System.Web.HttpContext.Current.Session["idUserSession"] = ((ML.Student)result.Object).iIdAlumno;
                }
                result.Correct = true;

            }
            catch (Exception exc)
            {
                result.ErrorMessage = exc.Message.ToString();
                result.Correct = false;
                return Json(new { Correct = result.Correct, Error = result.ErrorMessage });
            }
            return Json(new { Correct = result.Correct, Error = result.ErrorMessage });
        }

        public ActionResult LogOut()
        {
            Session.Remove("rolSession");
            Session.Remove("nameSession");
            Session.Remove("idUserSession");
            return RedirectToAction("Login", "Login");
        }
    }
}