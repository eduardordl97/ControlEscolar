using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Students : DL.Connection
    {
        public ML.Result Sp_Consulta_Informacion_Alumnos()
        {
            ML.Result result = new ML.Result();
            try
            {
                this.Conectar();
                SqlCommand command = new SqlCommand("sp_Consulta_Informacion_Alumnos", this.conexion) { CommandType = CommandType.StoredProcedure };
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    result.Objects = new List<object>();
                    while (dataReader.Read())
                    {
                        ML.Student student = new ML.Student();
                        student.iIdAlumno           = Convert.ToInt32(dataReader["IdAlumno"]);
                        student.sNombre             = dataReader["Nombre"].ToString();
                        student.sApellidoPaterno    = dataReader["ApellidoPaterno"].ToString();
                        student.sApellidoMaterno    = dataReader["ApellidoMaterno"].ToString();
                        student.sFechaHoraAlta      = Convert.ToDateTime(dataReader["FechaHoraAlta"]).ToString("dd-MM-yyyy");
                        student.bEstatus            = (dataReader["Estatus"].ToString() == "True") ? true : false;

                        result.Objects.Add(student);
                        result.ErrorMessage = "none";
                        result.Correct = true;
                    }
                }
                else
                {
                    result.ErrorMessage = "La consulta no retornó ningún usuario";
                    result.Correct = false;
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

        public ML.Result Sp_Inserta_Informacion_Alumno(ML.Student student)
        {
            ML.Result result = new ML.Result();
            try
            {
                this.Conectar();
                SqlCommand command = new SqlCommand("sp_Inserta_Informacion_Alumno", this.conexion) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add(new SqlParameter("@Nombre", student.sNombre));
                command.Parameters.Add(new SqlParameter("@ApellidoPaterno", student.sApellidoPaterno));
                command.Parameters.Add(new SqlParameter("@ApellidoMaterno", student.sApellidoMaterno));
                if (command.ExecuteNonQuery() > 0)
                {
                    result.ErrorMessage = "none";
                    result.Correct = true;
                }
                else
                {
                    result.ErrorMessage = "No se pudo registrar el alumno";
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
