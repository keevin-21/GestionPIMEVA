using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class N_Buque
    {
        D_Buque DB = new D_Buque();
        public string InsertarBuque(E_Buque buque)
        {
            return DB.IBM_Buques("INSERTAR", buque) > 0 ? "Buque insertado exitosamente" : "Error al insertar buque";
        }

        public string ModificarBuque(E_Buque buque)
        {
            return DB.IBM_Buques("MODIFICAR", buque) > 0 ? "Buque modificado exitosamente" : "Error al modificar buque";
        }

        public string BorrarBuque(int idBuque)
        {
            E_Buque buque = new E_Buque { IdBuque = idBuque };
            return DB.IBM_Buques("BORRAR", buque) > 0 ? "Buque borrado exitosamente" : "Error al borrar buque";
        }

        public List<E_Buque> ListarBuques()
        {
            return DB.ListarBuques();
        }
    }
}

