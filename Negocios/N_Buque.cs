using System;
using System.Collections.Generic;
using Entidades;
using Datos;

namespace Negocios
{
    public class N_Buque
    {
        D_Buque dBuque = new D_Buque();

        public List<E_Buque> ListarBuques(string nombreBuque = null, bool estado = true)
        {
            return dBuque.ListarBuques(nombreBuque, estado);
        }

        public E_Buque BuscarBuquePorID(int idBuque)
        {
            return dBuque.BuscarBuquePorID(idBuque);
        }

        public string InsertarBuque(E_Buque buque)
        {
            return dBuque.GestionarBuque("INSERTAR", buque);
        }

        public string BorrarBuque(int idBuque)
        {
            E_Buque buque = new E_Buque { IdBuque = idBuque };
            return dBuque.GestionarBuque("BORRAR", buque);
        }

        public string ModificarBuque(E_Buque buque)
        {
            return dBuque.GestionarBuque("MODIFICAR", buque);
        }
        public List<E_Buque> ObtenerBuques()
        {
            return dBuque.ListarBuques();
        }
    }


}
