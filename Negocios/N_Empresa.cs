using System;
using System.Collections.Generic;
using Entidades;
using Datos;

namespace Negocios
{
    public class N_Empresa
    {
        D_Empresa dEmpresa = new D_Empresa();

        public List<E_Empresa> ListarEmpresas(string nombreEmpresa = null, bool? estado = null)
        {
            return dEmpresa.ListarEmpresas(nombreEmpresa, estado);
        }

        public E_Empresa BuscarEmpresaPorID(int idEmpresa)
        {
            return dEmpresa.BuscarEmpresaPorID(idEmpresa);
        }

        public string InsertarEmpresa(E_Empresa empresa)
        {
            return dEmpresa.GestionarEmpresa("INSERTAR", empresa);
        }

        public string BorrarEmpresa(int idEmpresa)
        {
            E_Empresa empresa = new E_Empresa { IdEmpresa = idEmpresa };
            return dEmpresa.GestionarEmpresa("BORRAR", empresa);
        }

        public string ModificarEmpresa(E_Empresa empresa)
        {
            return dEmpresa.GestionarEmpresa("MODIFICAR", empresa);
        }
    }
}
