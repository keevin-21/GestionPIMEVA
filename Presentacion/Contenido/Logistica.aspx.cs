﻿using Negocios;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using Newtonsoft.Json;
using System.Linq;

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
        [WebMethod]
        public static string ObtenerBuquesRegistrados()
        {
            try
            {
                var negocioBuque = new N_Buque();
                var buques = negocioBuque.ObtenerBuques();

                // Devuelve solo los nombres de los buques
                var nombresBuques = buques.Where(b => b.Estado).Select(b => b.NombreBuque).ToList();

                return JsonConvert.SerializeObject(nombresBuques);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los datos de los buques: " + ex.Message);
            }
        }

    }
}