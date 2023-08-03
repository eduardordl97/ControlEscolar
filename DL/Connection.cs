using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Connection
    {
        static readonly string Server = @"LAPTOP-FCI9CQFL\MSSQLSERVER01";
        static readonly string Db = "ControlEscolar";
        static readonly string User = "";
        static readonly string Pass = "";

        protected SqlConnection conexion { get; set; }

        protected SqlConnection Conectar()
        {
            try
            {
                conexion = new SqlConnection("Data Source="+ Server + ";Initial Catalog="+Db+ ";User ID=" + User + ";Password=" + Pass + "Persist Security Info=True;Integrated Security=true;");
                conexion.Open();
                return conexion;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return null;
            }
        }
    }
}
