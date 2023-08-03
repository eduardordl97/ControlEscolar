using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class SubjectsStudentController : Controller
    {
        public ActionResult SubjectsStudent()
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

        [HttpPost]
        public JsonResult LoadDataSubjectsStudent(int idStudent)
        {
            BL.SubjectStudents subjects = new BL.SubjectStudents();
            ML.Result result = new ML.Result();
            try
            {
                result = subjects.Sp_Consulta_Informacion_AlumnosMaterias_ById_Alumno(idStudent);
            }
            catch (Exception exc)
            {
                result.ErrorMessage = exc.Message.ToString();
                result.Correct = false;
                return Json(new { Correct = result.Correct, Error = result.ErrorMessage });
            }
            return Json(new { Correct = result.Correct, Error = result.ErrorMessage, Data = result.Objects });
        }

    }
}