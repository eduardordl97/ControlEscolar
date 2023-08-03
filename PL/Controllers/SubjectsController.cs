using PL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class SubjectsController : Controller
    {
        public ActionResult Subjects()
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
        public JsonResult LoadDataSubjects()
        {
            BL.Subjects subjects    = new BL.Subjects();
            ML.Result result        = new ML.Result();
            StringBuilder tableHtml = new StringBuilder();
            try
            {
                result = subjects.Sp_Consulta_Informacion_Materias();
                if(result.Objects != null)
                {
                    if (result.Objects.Count > 0)
                    {
                        foreach (ML.Subject subject in result.Objects)
                        {
                            string activeInactive = (subject.bEstatus == true) ? "<span class='badge badge-pill bg-success'>Activo</span>" : "<span class='badge badge-pill bg-danger'>Inactivo</span>";
                            string buttonActiveInactive = (subject.bEstatus == true) ? OptionsTable.ButtonInactive(subject.iIdMateria) : OptionsTable.ButtonActive(subject.iIdMateria);
                            string html = "<tr>";
                            html += "<td>" + subject.sNombre + " </td>" +
                                    "<td>$ " + subject.sCosto + "</td>" +
                                    "<td>" + subject.sFechaHoraAlta + "</td>" +
                                    "<td>" + activeInactive + "</td>" +
                                    "<td>" + OptionsTable.ButtonEdit(subject.iIdMateria) + buttonActiveInactive + "</td>";
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
            return Json(new { Correct = result.Correct, Error = result.ErrorMessage, Html = tableHtml.ToString(), Data = result.Objects });
        }

        [HttpPost]
        public JsonResult SaveData(ML.Subject subject)
        {
            BL.Subjects subjects = new BL.Subjects();
            ML.Result result = new ML.Result();
            try
            {
                result = subjects.Sp_Inserta_Informacion_Materia(subject);

            }
            catch (Exception exc)
            {
                result.ErrorMessage = exc.Message.ToString();
                result.Correct = false;
                return Json(new { Correct = result.Correct, Error = result.ErrorMessage });
            }
            return Json(new { Correct = result.Correct, Error = result.ErrorMessage });
        }

        [HttpPost]
        public JsonResult ShowDataById(int idSubject)
        {
            BL.Subjects subjects = new BL.Subjects();
            ML.Result result = new ML.Result();
            try
            {
                result = subjects.Sp_Consulta_Informacion_Materia_ById(idSubject);
            }
            catch (Exception exc)
            {
                result.ErrorMessage = exc.Message.ToString();
                result.Correct = false;
                return Json(new { Correct = result.Correct, Error = result.ErrorMessage });
            }
            return Json(new { Correct = result.Correct, Error = result.ErrorMessage, Data = result.Object });
        }

        [HttpPost]
        public JsonResult UpdateData(ML.Subject subject)
        {
            BL.Subjects subjects = new BL.Subjects();
            ML.Result result = new ML.Result();
            try
            {
                result = subjects.Sp_Edita_Informacion_Materia(subject);

            }
            catch (Exception exc)
            {
                result.ErrorMessage = exc.Message.ToString();
                result.Correct = false;
                return Json(new { Correct = result.Correct, Error = result.ErrorMessage });
            }
            return Json(new { Correct = result.Correct, Error = result.ErrorMessage });
        }

        [HttpPost]
        public JsonResult ActiveInactiveData(int idSubject, int status)
        {
            BL.Subjects subjects = new BL.Subjects();
            ML.Result result = new ML.Result();
            try
            {
                result = subjects.Sp_Cambia_Estatus_Materia(idSubject, status);
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