using Negocios;
using Entidades;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Contenido
{
    public partial class Gestion_Buques : System.Web.UI.Page
    {
        N_Buque negocioBuque = new N_Buque();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarBuques();
            }
        }

        private void CargarBuques()
        {
            grvBuques.DataSource = negocioBuque.ListarBuques();
            grvBuques.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            E_Buque nuevoBuque = new E_Buque
            {
                NombreBuque = txtNombreBuque.Text,
                IdEmpresa = int.Parse(ddlIdEmpresa.SelectedValue),
                Estado = true // Estado se establecerá en 1 automáticamente en la base de datos
            };
            string resultado = negocioBuque.InsertarBuque(nuevoBuque);
            Response.Write("<script>alert('" + resultado + "');</script>");
            // Recargar los datos después de guardar
            CargarBuques();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            txtNombreBuque.Text = string.Empty;
            ddlIdEmpresa.SelectedIndex = 0;
        }
    }
}
