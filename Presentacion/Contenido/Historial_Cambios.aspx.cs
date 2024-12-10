using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace PIMEVA
{
    public partial class HistorialCambios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarHistorialCambios();
            }
        }

        private void CargarHistorialCambios()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        hc.IdHistorial, 
                        hc.TipoCambio, 
                        u.NombreUsuario, 
                        hc.FechaModificacion, 
                        b.NombreBuque AS Buque, -- Ahora toma el nombre del buque desde la tabla Buque
                        hcd.OperationTime, 
                        hcd.ETA, 
                        hcd.ETD, 
                        hcd.Cargo
                    FROM HistorialCambios hc
                    INNER JOIN HistorialCambiosDetalle hcd ON hc.IdHistorial = hcd.IdHistorial
                    INNER JOIN Usuarios u ON hc.IdUsuario = u.IdUsuario
                    INNER JOIN TablaDeLogistica tdl ON hcd.IdRegistro = tdl.IdRegistro
                    INNER JOIN Buque b ON tdl.IdBuque = b.IdBuque -- Unión con la tabla Buque
                    ORDER BY hc.FechaModificacion DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gridHistorial.DataSource = dt;
                gridHistorial.DataBind();
            }
        }


        protected void gridHistorial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridHistorial.PageIndex = e.NewPageIndex;
            CargarHistorialCambios();
        }
    }
}
