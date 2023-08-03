using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Login : DL.Connection
    {
        public ML.Result Sp_Valida_Inicio_Sesion(ML.Login login)
        {
            ML.Result result = new ML.Result();
            
            try
            {
                this.Conectar();
                SqlCommand command = new SqlCommand("sp_Valida_Inicio_Sesion", this.conexion) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add(new SqlParameter("@Nombre", login.sNombre.Trim().ToUpper()));
                command.Parameters.Add(new SqlParameter("@ApellidoPaterno", login.sApellidoPaterno.Trim().ToUpper()));
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    if (dataReader["OK"].ToString() == "1")
                    {
                        ML.Student student = new ML.Student();
                        student.iIdAlumno = Convert.ToInt32(dataReader["IdAlumno"]);
                        student.sNombre = dataReader["Nombre"].ToString();
                        student.sApellidoPaterno = dataReader["ApellidoPaterno"].ToString();
                        student.sApellidoMaterno = dataReader["ApellidoMaterno"].ToString();
                        student.bAdmin = (dataReader["Admin"].ToString() == "True") ? true : false;

                        result.Object = student;
                        result.ErrorMessage = "none";
                        result.Correct = true;

                    }
                    else if (dataReader["OK"].ToString() == "2")
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Usuario Inactivo";
                    }
                    else if (dataReader["OK"].ToString() == "3")
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Los datos son incorrectos, vuelve a intentarlo";
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Alumnos";

                    }
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
    }
}
