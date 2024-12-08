using Entidades;
using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Contenido
{
    public partial class InicioSidebar : System.Web.UI.Page
    {
        N_Categoria NC = new N_Categoria();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }

        }

        //protected void btnInsertar_Click(object sender, EventArgs e)
        //{
        //    E_Categoria EC = new E_Categoria(0, tbNombreCategoria.Text, tbDescripcionCategoria.Text, true);

        //    lblResultado.Text = NC.InsertarCategoria(EC);
        //    tbNombreCategoria.Text = string.Empty;
        //    tbDescripcionCategoria.Text = string.Empty;
        //}

        //protected void btnBorrar_Click(object sender, EventArgs e)
        //{
        //    int idCategoria = Convert.ToInt16(tbCategoria.Text);
        //    lblResultado.Text = NC.BorrarCategoria(idCategoria);
        //    tbCategoria.Text = string.Empty;
        //    btnListado_Click(sender, e);
        //}

        //protected void btnListado_Click(object sender, EventArgs e)
        //{
        //    GrvCategorias.DataSource = NC.ListarCategorias();
        //    GrvCategorias.DataBind();
        //}
    }
}