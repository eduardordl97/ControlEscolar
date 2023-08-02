using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Subjects : DL.Connection
    {
        public ML.Result Sp_Consulta_Informacion_Alumnos()
        {
            ML.Result result = new ML.Result();
            try
            {
                this.Conectar();
                SqlCommand command = new SqlCommand("sp_Consulta_Informacion_Materias", this.conexion) { CommandType = CommandType.StoredProcedure };
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    result.Objects = new List<object>();
                    while (dataReader.Read())
                    {
                        ML.Subject subject      = new ML.Subject();
                        subject.iIdMateria      = Convert.ToInt32(dataReader["IdMateria"]);
                        subject.sNombre         = dataReader["Nombre"].ToString();
                        subject.sCosto          = Convert.ToDecimal(dataReader["Costo"].ToString()).ToString("N2");
                        subject.sFechaHoraAlta  = Convert.ToDateTime(dataReader["FechaHoraAlta"]).ToString("dd-MM-yyyy");
                        subject.bEstatus        = (dataReader["Estatus"].ToString() == "True") ? true : false;

                        result.Objects.Add(subject);
                        result.ErrorMessage = "none";
                        result.Correct = true;
                    }
                }
                else
                {
                    result.ErrorMessage = "La consulta no retornó ningún dato";
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

        public ML.Result Sp_Inserta_Informacion_Materia(ML.Subject subject)
        {
            ML.Result result = new ML.Result();
            try
            {
                this.Conectar();
                SqlCommand command = new SqlCommand("sp_Inserta_Informacion_Materia", this.conexion) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add(new SqlParameter("@Nombre", subject.sNombre));
                command.Parameters.Add(new SqlParameter("@Costo", subject.dCosto));
                if (command.ExecuteNonQuery() > 0)
                {
                    result.ErrorMessage = "none";
                    result.Correct = true;
                }
                else
                {
                    result.ErrorMessage = "No se pudo registrar la Materia";
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

        public ML.Result Sp_Consulta_Informacion_Materia_ById(int idSubject)
        {
            ML.Result result = new ML.Result();
            try
            {
                this.Conectar();
                SqlCommand command = new SqlCommand("sp_Consulta_Informacion_Materia_ById", this.conexion) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add(new SqlParameter("@IdMateria", idSubject));
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    ML.Subject subject = new ML.Subject();
                    subject.iIdMateria = Convert.ToInt32(dataReader["IdMateria"]);
                    subject.sNombre = dataReader["Nombre"].ToString();
                    subject.dCosto = Convert.ToDecimal(dataReader["Costo"]);
                    subject.sFechaHoraAlta = Convert.ToDateTime(dataReader["FechaHoraAlta"]).ToString("dd-MM-yyyy");
                    subject.bEstatus = (dataReader["Estatus"].ToString() == "True") ? true : false;

                    result.Object = subject;
                    result.ErrorMessage = "none";
                    result.Correct = true;
                }
                else
                {
                    result.ErrorMessage = "La consulta no retornó ninguna Materia";
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

        public ML.Result Sp_Edita_Informacion_Materia(ML.Subject subject)
        {
            ML.Result result = new ML.Result();
            try
            {
                this.Conectar();
                SqlCommand command = new SqlCommand("sp_Edita_Informacion_Materia", this.conexion) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add(new SqlParameter("@IdMateria", subject.iIdMateria));
                command.Parameters.Add(new SqlParameter("@Nombre", subject.sNombre));
                command.Parameters.Add(new SqlParameter("@Costo", subject.dCosto));
                if (command.ExecuteNonQuery() > 0)
                {
                    result.ErrorMessage = "none";
                    result.Correct = true;
                }
                else
                {
                    result.ErrorMessage = "No se pudo editar la Materia";
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

        public ML.Result Sp_Cambia_Estatus_Materia(int idSubject, int status)
        {
            ML.Result result = new ML.Result();
            try
            {
                this.Conectar();
                SqlCommand command = new SqlCommand("sp_Cambia_Estatus_Materia", this.conexion) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add(new SqlParameter("@IdMateria", idSubject));
                command.Parameters.Add(new SqlParameter("@Estatus", status));
                if (command.ExecuteNonQuery() > 0)
                {
                    result.ErrorMessage = "none";
                    result.Correct = true;
                }
                else
                {
                    result.ErrorMessage = "No se pudo editar la Materia";
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
