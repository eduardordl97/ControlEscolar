using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Student
    {
        public int iIdAlumno { get; set; }
        public string sNombre { get; set; }
        public string sApellidoPaterno { get; set; }
        public string sApellidoMaterno { get; set; }
        public DateTime dtFechaHoraAlta { get; set; }
        public string sFechaHoraAlta { get; set; }
        public bool bEstatus { get; set; }
        public bool bAdmin { get; set; }
    }
}
