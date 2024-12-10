using System;
using System.Collections.Generic;
using Entidades;
using Datos;

namespace Negocios
{
    public class N_Usuarios
    {
        D_Usuarios dUsuario = new D_Usuarios();

        public List<E_Usuarios> ListarUsuarios(string nombreUsuario = null, bool? estado = null)
        {
            return dUsuario.ListarUsuarios(nombreUsuario, estado);
        }

        public E_Usuarios BuscarUsuarioPorID(int idUsuario)
        {
            return dUsuario.BuscarUsuarioPorID(idUsuario);
        }

        public string InsertarUsuario(E_Usuarios usuario)
        {
            return dUsuario.GestionarUsuario("INSERTAR", usuario);
        }

        public string BorrarUsuario(int idUsuario)
        {
            E_Usuarios usuario = new E_Usuarios { IdUsuario = idUsuario };
            return dUsuario.GestionarUsuario("BORRAR", usuario);
        }

        public string ModificarUsuario(E_Usuarios usuario)
        {
            return dUsuario.GestionarUsuario("MODIFICAR", usuario);
        }
    }
}
