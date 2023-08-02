using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.Utilities
{
    public static class OptionsTable
    {
        public static string ButtonEdit(int id)
        {
            return "<button class='btn btn-sm btn-outline-warning me-1' data-toggle='tooltip' data-placement='bottom' title='Editar' onclick='fShowDataById(" + id + ");'><i class='fas fa-edit'></i></button>";
        }

        public static string ButtonInactive(int id)
        {
            return "<button class='btn btn-sm btn-outline-danger' data-toggle='tooltip' data-placement='bottom' title='Desactivar'  onclick='fInactiveData(" + id + ",0);'><i class='fas fa-times-circle'></i></button>";
        }

        public static string ButtonActive(int id)
        {
            return "<button class='btn btn-sm btn-outilne-success' data-toggle='tooltip' data-placement='bottom' title='Activar' onclick='fActiveData(" + id + ",1);'><i class='fas fa-check-circle'></i></button>";
        }

    }
}