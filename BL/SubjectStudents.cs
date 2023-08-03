using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SubjectStudents : DL.Connection
    {
        public ML.Result Sp_Consulta_Informacion_AlumnosMaterias_ById_Alumno(int idStudent)
        {
            ML.Result result = new ML.Result();
            try
            {
                this.Conectar();
                SqlCommand command = new SqlCommand("sp_Consulta_Informacion_AlumnosMaterias_ById_Alumno", this.conexion) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add(new SqlParameter("@IdAlumno", idStudent));
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    result.Objects = new List<object>();
                    while (dataReader.Read())
                    {
                        ML.Subject subject = new ML.Subject();
                        subject.iIdMateria = Convert.ToInt32(dataReader["IdAlumnoMateria"]);
                        subject.sNombre = dataReader["Nombre"].ToString();
                        subject.sCosto = Convert.ToDecimal(dataReader["Costo"].ToString()).ToString("N2");
                        subject.dCosto = Convert.ToDecimal(dataReader["Costo"].ToString());
                        subject.sFechaHoraAlta = Convert.ToDateTime(dataReader["FechaHoraAlta"]).ToString("dd-MM-yyyy");
                        subject.bEstatus = (dataReader["Estatus"].ToString() == "True") ? true : false;

                        result.Objects.Add(subject);
                        result.ErrorMessage = "none";
                        result.Correct = true;
                    }
                }
                else
                {
                    result.ErrorMessage = "La consulta no retornó ningún dato";
                    result.Correct = true;
                }
                command.Parameters.Clear();
                command.Dispose();
                dataReader.Close();
            }
            catch (Exception exc)
            {
                result.Correct = false;
                result.ErrorMessage = exc.Message;
                result.Ex = exc;

            }
            finally
            {
                this.conexion.Close();
            }
            return result;
        }
        public ML.Result Sp_Consulta_Informacion_AlumnosMaterias_Disponibles_ById_Alumno(int idStudent)
        {
            ML.Result result = new ML.Result();
            try
            {
                this.Conectar();
                SqlCommand command = new SqlCommand("sp_Consulta_Informacion_AlumnosMaterias_Disponibles_ById_Alumno", this.conexion) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add(new SqlParameter("@IdAlumno", idStudent));
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    result.Objects = new List<object>();
                    while (dataReader.Read())
                    {
                        ML.Subject subject = new ML.Subject();
                        subject.iIdMateria = Convert.ToInt32(dataReader["IdMateria"]);
                        subject.sNombre = dataReader["Nombre"].ToString();
                        subject.dCosto = Convert.ToDecimal(dataReader["Costo"].ToString());
                        subject.sCosto = Convert.ToDecimal(dataReader["Costo"].ToString()).ToString();
                        subject.sFechaHoraAlta = Convert.ToDateTime(dataReader["FechaHoraAlta"]).ToString("dd-MM-yyyy");
                        subject.bEstatus = (dataReader["Estatus"].ToString() == "True") ? true : false;

                        result.Objects.Add(subject);
                        result.ErrorMessage = "none";
                        result.Correct = true;
                    }
                }
                else
                {
                    result.ErrorMessage = "La consulta no retornó ningún dato";
                    result.Correct = true;
                }
                command.Parameters.Clear();
                command.Dispose();
                dataReader.Close();
            }
            catch (Exception exc)
            {
                result.Correct = false;
                result.ErrorMessage = exc.Message;
                result.Ex = exc;

            }
            finally
            {
                this.conexion.Close();
            }
            return result;
        }

        public ML.Result Sp_Inserta_Informacion_AlumnosMaterias(int idStudent,int idSubject)
        {
            ML.Result result = new ML.Result();
            try
            {
                this.Conectar();
                SqlCommand command = new SqlCommand("sp_Inserta_Informacion_AlumnosMaterias", this.conexion) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add(new SqlParameter("@FK_Alumno", idStudent));
                command.Parameters.Add(new SqlParameter("@FK_Materia", idSubject));
                if (command.ExecuteNonQuery() > 0)
                {
                    result.ErrorMessage = "none";
                    result.Correct = true;
                }
                else
                {
                    result.ErrorMessage = "No se pudo asignar la Materia";
                    result.Correct = false;
                }
                command.Parameters.Clear();
                command.Dispose();
            }
            catch (Exception exc)
            {
                result.Correct = false;
                result.ErrorMessage = exc.Message;
                result.Ex = exc;

            }
            finally
            {
                this.conexion.Close();
            }
            return result;
        }

        public ML.Result Sp_Cambia_Estatus_AlumnoMateria(int idSubject, int status)
        {
            ML.Result result = new ML.Result();
            try
            {
                this.Conectar();
                SqlCommand command = new SqlCommand("sp_Cambia_Estatus_AlumnoMateria", this.conexion) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add(new SqlParameter("@IdAlumnoMateria", idSubject));
                command.Parameters.Add(new SqlParameter("@Estatus", status));
                if (command.ExecuteNonQuery() > 0)
                {
                    result.ErrorMessage = "none";
                    result.Correct = true;
                }
                else
                {
                    result.ErrorMessage = "No se pudo actualizar la Materia";
                    result.Correct = false;
                }
                command.Parameters.Clear();
                command.Dispose();
            }
            catch (Exception exc)
            {
                result.Correct = false;
                result.ErrorMessage = exc.Message;
                result.Ex = exc;
            }
            finally
            {
                this.conexion.Close();
            }
            return result;
        }
    }
}
