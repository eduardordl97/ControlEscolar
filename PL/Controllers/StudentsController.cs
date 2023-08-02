using PL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class StudentsController : Controller
    {
        public ActionResult Students()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoadDataStudents()
        {
            BL.Students students    = new BL.Students();
            ML.Result result        = new ML.Result();
            StringBuilder tableHtml = new StringBuilder();
            try
            {
                result = students.Sp_Consulta_Informacion_Alumnos();
                if (result.Objects.Count > 0)
                {
                    foreach (ML.Student student in result.Objects)
                    {
                        string activeInactive       = (student.bEstatus == true) ? "<span class='badge badge-pill bg-success'>Activo</span>" : "<span class='badge badge-pill bg-danger'>Inactivo</span>";
                        string buttonActiveInactive = (student.bEstatus == true) ? OptionsTable.ButtonInactive(student.iIdAlumno) : OptionsTable.ButtonActive(student.iIdAlumno);
                        string html = "<tr>";
                        html += "<td>" + student.sNombre + " </td>" +
                                "<td>" + student.sApellidoPaterno + "</td>" +
                                "<td>" + student.sApellidoMaterno + "</td>" +
                                "<td>" + student.sFechaHoraAlta + "</td>" +
                                "<td>" + activeInactive + "</td>"+
                                "<td>" + OptionsTable.ButtonEdit(student.iIdAlumno)+ buttonActiveInactive + "</td>";
                        html += "</tr>";
                        tableHtml.Append(html);
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
        public JsonResult SaveData(ML.Student student)
        {
            BL.Students students = new BL.Students();
            ML.Result result     = new ML.Result();
            try
            {
                result = students.Sp_Inserta_Informacion_Alumno(student);
                
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
        public JsonResult ShowDataById(int idStudent)
        {
            BL.Students students = new BL.Students();
            ML.Result result     = new ML.Result();
            try
            {
                result = students.Sp_Consulta_Informacion_Alumno_ById(idStudent);
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
        public JsonResult UpdateData(ML.Student student)
        {
            BL.Students students = new BL.Students();
            ML.Result result = new ML.Result();
            try
            {
                result = students.Sp_Edita_Informacion_Alumno(student);

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