using System;
using System.Collections.Generic;
using Entidades;
using Datos;

namespace Negocios
{
    public class N_Roles
    {
        D_Roles dRol = new D_Roles();

        public List<E_Roles> ListarRoles()
        {
            return dRol.ListarRoles();
        }

        public E_Roles BuscarRolPorID(int idRol)
        {
            return dRol.BuscarRolPorID(idRol);
        }

        public string InsertarRol(E_Roles rol)
        {
            return dRol.GestionarRol("INSERTAR", rol);
        }

        public string BorrarRol(int idRol)
        {
            E_Roles rol = new E_Roles { IdRol = idRol };
            return dRol.GestionarRol("BORRAR", rol);
        }

        public string ModificarRol(E_Roles rol)
        {
            return dRol.GestionarRol("MODIFICAR", rol);
        }
    }
}
