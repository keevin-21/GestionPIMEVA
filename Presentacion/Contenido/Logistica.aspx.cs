using Negocios;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using Newtonsoft.Json;

namespace Presentacion.Contenido
{
    public partial class Logistica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string ObtenerDatosLogistica()
        {
            try
            {
                // Llamar al método de la capa de negocios
                var negocioLogistica = new N_TablaDeLogistica();
                var dt = negocioLogistica.ObtenerLogistica();

                // Serializar a JSON
                return JsonConvert.SerializeObject(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener datos: " + ex.Message);
            }
        }
    }
}