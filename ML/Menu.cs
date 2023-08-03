using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Menu
    {
        public string sNombre { get; set; }
        public string sControlador { get; set; }
        public string sMetodo { get; set; }
        public List<ML.Menu> ListMenu { get; set; }
    }
}
