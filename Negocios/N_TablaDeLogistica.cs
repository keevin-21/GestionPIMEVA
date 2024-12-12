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
        // Método para obtener los datos logísticos
        public DataTable ObtenerLogistica()
        {
            var datosLogistica = new D_TablaDeLogistica();
            return datosLogistica.ObtenerLogistica();
        }

        // Método para actualizar los datos logísticos
        public void ActualizarLogistica()
        {
            var datosLogistica = new D_TablaDeLogistica();
            datosLogistica.ActualizarLogistica();
        }
    }
}
