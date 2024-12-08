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
            string Mensaje = string.Empty;
            // Validaciones
            if (string.IsNullOrWhiteSpace(buque.NombreBuque))
                Mensaje = "Error: El nombre de la categoría es obligatorio.";
            if (buque.IdEmpresa.Equals("0"))
                Mensaje = "Error: El buque tiene qe estar relacionado a una empresa";
            //if (DB.BuscarCategoriaPorCriterio(buque.NombreBuque).Count > 0) // Llamada a la capa de datos para buscar el nombre de la categoría                Mensaje = "Error: El nombre de la categoría " + categoria.NombreCategoria + " ya existe en la base de datos.";
                if (Mensaje == string.Empty) //Validaciones correctas
                {
                    int R;
                    Mensaje = "Error: El Buque " + buque.NombreBuque + " no se ha podido insertar";

                    R = DB.IBM_Buques("INSERTAR", buque);

                    if (R > 0)
                        Mensaje = "Exito: El buque " + buque.NombreBuque + " se ha insertado correctamente";
                    return Mensaje;
                }
            return Mensaje;
        }
    }
}
