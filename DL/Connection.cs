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
        //static readonly string User = "sa";
        //static readonly string Pass = "KLSR22$#jq/";

        protected SqlConnection conexion { get; set; }

        protected SqlConnection Conectar()
        {
            try
            {
                //Data Source = localhost\SQLSERVER; Initial Catalog = YourDataBaseName; Integrated Security = True; " providerName="System.Data.SqlClient
                //conexion = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + Db + ";Integrated Security=True");
                conexion = new SqlConnection("Data Source="+ Server + ";Initial Catalog=ControlEscolar;Persist Security Info=True;Integrated Security=true;");
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
