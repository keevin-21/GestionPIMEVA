using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class N_TablaDeLogistica
    {
        public DataTable ObtenerLogistica()
        {
            var datosLogistica = new D_TablaDeLogistica();
            return datosLogistica.ObtenerLogistica();
        }


    }
}
