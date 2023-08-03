using PL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                return RedirectToAction("Index", "Home");
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
            decimal total = 0;
            BL.SubjectStudents subjects = new BL.SubjectStudents();
            ML.Result result = new ML.Result();
            StringBuilder tableHtml = new StringBuilder();
            try
            {
                result = subjects.Sp_Consulta_Informacion_AlumnosMaterias_ById_Alumno(idStudent);
                if (result.Objects != null)
                {
                    if (result.Objects.Count > 0)
                    {
                        
                        foreach (ML.Subject subject in result.Objects)
                        {
                            total += subject.dCosto;
                            string activeInactive = (subject.bEstatus == true) ? "<span class='badge badge-pill bg-success'>Activo</span>" : "<span class='badge badge-pill bg-danger'>Inactivo</span>";
                            string buttonActiveInactive = (subject.bEstatus == true) ? OptionsTable.ButtonInactive(subject.iIdMateria) : OptionsTable.ButtonActive(subject.iIdMateria);
                            string html = "<tr>";
                            html += "<td>" + subject.sNombre + " </td>" +
                                    "<td>$ " + subject.sCosto + "</td>" +
                                    "<td>" + buttonActiveInactive + "</td>";
                            html += "</tr>";
                            tableHtml.Append(html);
                        }
                    }
                }

                result.Correct = true;
            }
            catch (Exception exc)
            {
                result.ErrorMessage = exc.Message.ToString();
                result.Correct = false;
                return Json(new { Correct = result.Correct, Error = result.ErrorMessage });
            }
            return Json(new { Correct = result.Correct, Error = result.ErrorMessage, Data = result.Objects, Html = tableHtml.ToString(), Total  = total });
        }

        [HttpPost]
        public JsonResult LoadDataAvailableSubjectsStudent(int idStudent)
        {
            BL.SubjectStudents subjects = new BL.SubjectStudents();
            ML.Result result = new ML.Result();
            StringBuilder tableHtml = new StringBuilder();
            try
            {
                result = subjects.Sp_Consulta_Informacion_AlumnosMaterias_Disponibles_ById_Alumno(idStudent);
            }
            catch (Exception exc)
            {
                result.ErrorMessage = exc.Message.ToString();
                result.Correct = false;
                return Json(new { Correct = result.Correct, Error = result.ErrorMessage });
            }
            return Json(new { Correct = result.Correct, Error = result.ErrorMessage, Data = result.Objects, Html = tableHtml.ToString() });
        }

        [HttpPost]
        public JsonResult SaveData(List<string> Subjects, int idStudent)
        {
            BL.SubjectStudents subjects = new BL.SubjectStudents();
            ML.Result result = new ML.Result();
            try
            {
                foreach (string idSubject in Subjects)
                {
                    result = subjects.Sp_Inserta_Informacion_AlumnosMaterias(idStudent,Convert.ToInt32(idSubject));
                }   
            }
            catch (Exception exc)
            {
                result.ErrorMessage = exc.Message.ToString();
                result.Correct = false;
                return Json(new { Correct = result.Correct, Error = result.ErrorMessage });
            }
            return Json(new { Correct = result.Correct, Error = result.ErrorMessage, Data = result.Objects });
        }


        [HttpPost]
        public JsonResult ActiveInactiveData(int idSubject, int status)
        {
            BL.SubjectStudents subjects = new BL.SubjectStudents();
            ML.Result result = new ML.Result();
            try
            {
                result = subjects.Sp_Cambia_Estatus_AlumnoMateria(idSubject, status);
            }
            catch (Exception exc)
            {
                result.ErrorMessage = exc.Message.ToString();
                result.Correct = false;
                return Json(new { Correct = result.Correct, Error = result.ErrorMessage });
            }
            return Json(new { Correct = result.Correct, Error = result.ErrorMessage });
        }

    }
}