using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Entidades;
using Negocios;

namespace Presentacion.Contenido
{
    public partial class Gestion_Empresas : System.Web.UI.Page
    {
        N_Empresa NE = new N_Empresa();
        protected void Page_Load(object sender, EventArgs e)
        {
            VisualizaEmpresas();
        }

        #region Métodos generales
        protected void InicializaControles()
        {
            PnlCapturaDatos.Visible = false;
            PnlGrvEmpresas.Visible = false;
            lblMensaje.Visible = false;
            ControlesClear();

            // Restablecer visibilidad de botones
            BtnInsertar.Visible = false; 
            BtnModificar.Visible = false; 
            BtnBorrar.Visible = false;
            BtnCancelar.Visible = false;
        }
        protected void ControlesClear()
        {
            TbCriterioBusqueda.Text = string.Empty;
            tbNombreEmpresa.Text = string.Empty;
            tbDescripcionEmpresa.Text = string.Empty;
        }
        protected void AtributosHeaderCard(string Msg, string Color)
        {
            lblAccion.Text = Msg;
            CardHeader.Attributes["class"] = "card-header " + Color;
        }
        protected void VisualizaEmpresas()
        {
            InicializaControles();
            GrvEmpresas.DataSource = NE.ListarEmpresas();
            GrvEmpresas.DataBind();
            PnlGrvEmpresas.Visible = true;
        }
        protected E_Empresa ControlesWebForm_ObjetoEntidad()
        {
            E_Empresa Empresa = new E_Empresa()
            {
                NombreEmpresa = tbNombreEmpresa.Text.Trim(),
                DescripcionEmpresa = tbDescripcionEmpresa.Text.Trim(),
                Estado = true
            };

            return Empresa;
        }
        protected void ObjetoEntidad_ControlesWebForm(int idEmpresa)
        {
            E_Empresa Empresa = NE.BuscarEmpresaPorID(idEmpresa);

            tbNombreEmpresa.Text = Empresa.NombreEmpresa.Trim();
            tbDescripcionEmpresa.Text = Empresa.DescripcionEmpresa.Trim();
        }
        #endregion

        #region Botones menú de navegación 
        protected void BtnNuevaEmpresa_Click(object sender, EventArgs e)
        {
            InicializaControles();
            AtributosHeaderCard("Registrar nueva Empresa", "bg-success");

            PnlCapturaDatos.Visible = true;

            BtnInsertar.Visible = true;
            BtnCancelar.Visible = true; 
            
            // Ocultar botones "Modificar" y "Borrar"
            BtnModificar.Visible = false; 
            BtnBorrar.Visible = false;
        }
        protected void btnListarEmpresas_Click(object sender, EventArgs e)
        {
            VisualizaEmpresas();
        }
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TbCriterioBusqueda.Text.Trim()))
                {
                    List<E_Empresa> Lst = NE.ListarEmpresas(TbCriterioBusqueda.Text);

                    if (Lst.Count == 0)
                    {
                        lblMensaje.Text = "No se encontró la empresa solicitada";
                        lblMensaje.CssClass = "alert alert-warning";
                        lblMensaje.Visible = true;
                    }
                    else if (Lst.Count == 1)
                    {
                        AtributosHeaderCard("Modificar o Borrar los datos de la empresa", "bg-warning");
                        hfIdEmpresa.Value = Lst[0].IdEmpresa.ToString();
                        ObjetoEntidad_ControlesWebForm(Convert.ToInt32(hfIdEmpresa.Value));

                        PnlCapturaDatos.Visible = true;
                    }
                    else
                    {
                        GrvEmpresas.DataSource = Lst;
                        GrvEmpresas.DataBind();
                        PnlGrvEmpresas.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }
        }
        #endregion

        #region Botones IBM
        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            E_Empresa EE = ControlesWebForm_ObjetoEntidad();

            string[] Msg = NE.InsertarEmpresa(EE).Split(':');
            lblMensaje.Text = Msg[1];
            lblMensaje.CssClass = Msg[0] == "Exito" ? "alert alert-success" : "alert alert-danger";
            lblMensaje.Visible = true;

            if (Msg[0] == "Exito")
                InicializaControles();
        }
        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(hfIdEmpresa.Value);
            string[] Msg = NE.BorrarEmpresa(ID).Split(':');

            lblMensaje.Text = Msg[1];
            lblMensaje.CssClass = Msg[0] == "Exito" ? "alert alert-success" : "alert alert-danger";
            lblMensaje.Visible = true;

            if (Msg[0] == "Exito")
                InicializaControles();
        }
        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(hfIdEmpresa.Value);
            E_Empresa EE = new E_Empresa(ID, tbNombreEmpresa.Text.Trim(), tbDescripcionEmpresa.Text.Trim(), true);

            string[] Msg = NE.ModificarEmpresa(EE).Split(':');
            lblMensaje.Text = Msg[1];
            lblMensaje.CssClass = Msg[0] == "Exito" ? "alert alert-success" : "alert alert-danger";
            lblMensaje.Visible = true;

            if (Msg[0] == "Exito")
                InicializaControles();
        }
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            VisualizaEmpresas();
        }
        #endregion

        #region Botones Grv
        protected void GrvEmpresas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                AtributosHeaderCard("Modificar los datos de la empresa", "bg-primary");

                hfIdEmpresa.Value = GrvEmpresas.DataKeys[e.NewEditIndex].Value.ToString();
                e.Cancel = true;
                ObjetoEntidad_ControlesWebForm(Convert.ToInt32(hfIdEmpresa.Value));

                PnlCapturaDatos.Visible = true;
                PnlGrvEmpresas.Visible = false;

                BtnInsertar.Visible = false;
                BtnBorrar.Visible = false;
                BtnModificar.Visible = true;
                BtnCancelar.Visible = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }
        }
        protected void GrvEmpresas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                AtributosHeaderCard("Borrar los datos de la empresa", "bg-danger");
                hfIdEmpresa.Value = GrvEmpresas.DataKeys[e.RowIndex].Value.ToString();
                ObjetoEntidad_ControlesWebForm(Convert.ToInt32(hfIdEmpresa.Value));
                e.Cancel = true;

                PnlCapturaDatos.Visible = true;
                PnlGrvEmpresas.Visible = false;

                BtnInsertar.Visible = false;
                BtnBorrar.Visible = true;
                BtnModificar.Visible = false;
                BtnCancelar.Visible = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }
        }
        #endregion
    }
}

