using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Entidades;
using Negocios;

namespace Presentacion.Contenido
{
    public partial class Gestion_Buques : System.Web.UI.Page
    {
        N_Buque NB = new N_Buque();
        N_Empresa NE = new N_Empresa();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEmpresas();
                VisualizaBuques();
            }
        }

        private void CargarEmpresas()
        {
            ddlEmpresa.DataSource = NE.ListarEmpresas();
            ddlEmpresa.DataTextField = "NombreEmpresa";
            ddlEmpresa.DataValueField = "IdEmpresa";
            ddlEmpresa.DataBind();
            ddlEmpresa.Items.Insert(0, new ListItem("--Seleccionar Empresa--", "0"));
        }

        #region Métodos generales
        protected void InicializaControles()
        {
            PnlCapturaDatos.Visible = false;
            PnlGrvBuques.Visible = true;
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
            tbNombreBuque.Text = string.Empty;
            ddlEmpresa.SelectedIndex = 0;
        }
        protected void AtributosHeaderCard(string Msg, string Color)
        {
            lblAccion.Text = Msg;
            CardHeader.Attributes["class"] = "card-header " + Color;
        }
        protected void VisualizaBuques()
        {
            InicializaControles();
            GrvBuques.DataSource = NB.ListarBuques();
            GrvBuques.DataBind();
            PnlGrvBuques.Visible = true;
        }
        protected E_Buque ControlesWebForm_ObjetoEntidad()
        {
            E_Buque Buque = new E_Buque()
            {
                NombreBuque = tbNombreBuque.Text.Trim(),
                IdEmpresa = int.Parse(ddlEmpresa.SelectedValue),
                Estado = true
            };

            return Buque;
        }
        protected void ObjetoEntidad_ControlesWebForm(int idBuque)
        {
            E_Buque Buque = NB.BuscarBuquePorID(idBuque);

            tbNombreBuque.Text = Buque.NombreBuque.Trim();
            ddlEmpresa.SelectedValue = Buque.IdEmpresa.ToString();

            BtnModificar.Visible = true;
            BtnBorrar.Visible = true;
            BtnCancelar.Visible = true;
        }
        #endregion

        #region Botones menú de navegación 
        protected void BtnNuevoBuque_Click(object sender, EventArgs e)
        {
            InicializaControles();
            AtributosHeaderCard("Registrar nuevo Buque", "bg-primary");

            PnlCapturaDatos.Visible = true;
            PnlGrvBuques.Visible = false;

            BtnInsertar.Visible = true;
            BtnCancelar.Visible = true;

            // Ocultar botones "Modificar" y "Borrar"
            BtnModificar.Visible = false;
            BtnBorrar.Visible = false;
        }
        protected void btnListarBuques_Click(object sender, EventArgs e)
        {
            VisualizaBuques();
        }
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TbCriterioBusqueda.Text.Trim()))
                {
                    List<E_Buque> Lst = NB.ListarBuques(TbCriterioBusqueda.Text);

                    if (Lst.Count == 0)
                    {
                        lblMensaje.Text = "No se encontró el buque solicitado";
                        lblMensaje.CssClass = "alert alert-warning";
                        lblMensaje.Visible = true;
                    }
                    else if (Lst.Count == 1)
                    {
                        AtributosHeaderCard("Modificar o Borrar los datos del buque", "bg-warning");
                        hfIdBuque.Value = Lst[0].IdBuque.ToString();
                        ObjetoEntidad_ControlesWebForm(Convert.ToInt32(hfIdBuque.Value));

                        PnlCapturaDatos.Visible = true;
                        PnlGrvBuques.Visible = false;
                    }
                    else
                    {
                        GrvBuques.DataSource = Lst;
                        GrvBuques.DataBind();
                        PnlGrvBuques.Visible = true;
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
            E_Buque EB = ControlesWebForm_ObjetoEntidad();

            string[] Msg = NB.InsertarBuque(EB).Split(':');
            lblMensaje.Text = Msg[1];
            lblMensaje.CssClass = Msg[0] == "Exito" ? "alert alert-success" : "alert alert-danger";
            lblMensaje.Visible = true;

            if (Msg[0] == "Exito")
            {
                InicializaControles();
                VisualizaBuques();
            }
        }
        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(hfIdBuque.Value);
            string[] Msg = NB.BorrarBuque(ID).Split(':');

            lblMensaje.Text = Msg[1];
            lblMensaje.CssClass = Msg[0] == "Exito" ? "alert alert-success" : "alert alert-danger";
            lblMensaje.Visible = true;

            if (Msg[0] == "Exito")
            {
                InicializaControles();
                VisualizaBuques();
            }
        }
        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(hfIdBuque.Value);
            E_Buque EB = new E_Buque(ID, tbNombreBuque.Text.Trim(), int.Parse(ddlEmpresa.SelectedValue), string.Empty, true);

            string[] Msg = NB.ModificarBuque(EB).Split(':');
            lblMensaje.Text = Msg[1];
            lblMensaje.CssClass = Msg[0] == "Exito" ? "alert alert-success" : "alert alert-danger";
            lblMensaje.Visible = true;

            if (Msg[0] == "Exito")
            {
                InicializaControles();
                VisualizaBuques();
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            VisualizaBuques();
        }
        #endregion

        #region Botones Grv
        protected void GrvBuques_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                AtributosHeaderCard("Modificar los datos del buque", "bg-primary");

                hfIdBuque.Value = GrvBuques.DataKeys[e.NewEditIndex].Value.ToString();
                e.Cancel = true;
                ObjetoEntidad_ControlesWebForm(Convert.ToInt32(hfIdBuque.Value));

                PnlCapturaDatos.Visible = true;
                PnlGrvBuques.Visible = false;

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
        protected void GrvBuques_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                AtributosHeaderCard("Borrar los datos del buque", "bg-danger");
                hfIdBuque.Value = GrvBuques.DataKeys[e.RowIndex].Value.ToString();
                ObjetoEntidad_ControlesWebForm(Convert.ToInt32(hfIdBuque.Value));
                e.Cancel = true;

                PnlCapturaDatos.Visible = true;
                PnlGrvBuques.Visible = false;

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
