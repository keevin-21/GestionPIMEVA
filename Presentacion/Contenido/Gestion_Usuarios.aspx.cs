using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Entidades;
using Negocios;

namespace Presentacion.Contenido
{
    public partial class Gestion_Usuarios : System.Web.UI.Page
    {
        N_Usuarios NU = new N_Usuarios();
        N_Roles NR = new N_Roles(); // Asumiendo que tienes una capa de negocios para Roles
        N_Empresa NE = new N_Empresa();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();
                CargarEmpresas();
            }
        }

        private void CargarRoles()
        {
            ddlRol.DataSource = NR.ListarRoles();
            ddlRol.DataTextField = "NombreRol";
            ddlRol.DataValueField = "IdRol";
            ddlRol.DataBind();
            ddlRol.Items.Insert(0, new ListItem("--Seleccionar Rol--", "0"));
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
            PnlGrvUsuarios.Visible = false;
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
            tbNombreUsuario.Text = string.Empty;
            ddlRol.SelectedIndex = 0;
            ddlEmpresa.SelectedIndex = 0;
            tbCorreo.Text = string.Empty;
            tbTelefono.Text = string.Empty;
        }
        protected void AtributosHeaderCard(string Msg, string Color)
        {
            lblAccion.Text = Msg;
            CardHeader.Attributes["class"] = "card-header " + Color;
        }
        protected void VisualizaUsuarios()
        {
            InicializaControles();
            GrvUsuarios.DataSource = NU.ListarUsuarios();
            GrvUsuarios.DataBind();
            PnlGrvUsuarios.Visible = true;
        }
        protected E_Usuarios ControlesWebForm_ObjetoEntidad()
        {
            E_Usuarios Usuario = new E_Usuarios()
            {
                NombreUsuario = tbNombreUsuario.Text.Trim(),
                IdRol = int.Parse(ddlRol.SelectedValue),
                IdEmpresa = int.Parse(ddlEmpresa.SelectedValue),
                Correo = tbCorreo.Text.Trim(),
                Telefono = tbTelefono.Text.Trim(),
                Estado = true
            };

            return Usuario;
        }
        protected void ObjetoEntidad_ControlesWebForm(int idUsuario)
        {
            E_Usuarios Usuario = NU.BuscarUsuarioPorID(idUsuario);

            tbNombreUsuario.Text = Usuario.NombreUsuario.Trim();
            ddlRol.SelectedValue = Usuario.IdRol.ToString();
            ddlEmpresa.SelectedValue = Usuario.IdEmpresa.ToString();
            tbCorreo.Text = Usuario.Correo.Trim();
            tbTelefono.Text = Usuario.Telefono.Trim();

            BtnBorrar.Visible = true;
            BtnModificar.Visible = true;
            BtnCancelar.Visible = true;
        }
        #endregion

        #region Botones menú de navegación 
        protected void BtnNuevoUsuario_Click(object sender, EventArgs e)
        {
            InicializaControles();
            AtributosHeaderCard("Registrar nuevo Usuario", "bg-success");

            PnlCapturaDatos.Visible = true;

            BtnInsertar.Visible = true;
            BtnCancelar.Visible = true;

            // Ocultar botones "Modificar" y "Borrar"
            BtnModificar.Visible = false;
            BtnBorrar.Visible = false;
        }
        protected void btnListarUsuarios_Click(object sender, EventArgs e)
        {
            VisualizaUsuarios();
        }
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TbCriterioBusqueda.Text.Trim()))
                {
                    List<E_Usuarios> Lst = NU.ListarUsuarios(TbCriterioBusqueda.Text);

                    if (Lst.Count == 0)
                    {
                        lblMensaje.Text = "No se encontró el usuario solicitado";
                        lblMensaje.CssClass = "alert alert-warning";
                        lblMensaje.Visible = true;
                    }
                    else if (Lst.Count == 1)
                    {
                        AtributosHeaderCard("Modificar o Borrar los datos del usuario", "bg-warning");
                        hfIdUsuario.Value = Lst[0].IdUsuario.ToString();
                        ObjetoEntidad_ControlesWebForm(Convert.ToInt32(hfIdUsuario.Value));

                        PnlCapturaDatos.Visible = true;
                        PnlGrvUsuarios.Visible = false;
                    }
                    else
                    {
                        GrvUsuarios.DataSource = Lst;
                        GrvUsuarios.DataBind();
                        PnlGrvUsuarios.Visible = true;
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
            E_Usuarios EU = ControlesWebForm_ObjetoEntidad();

            string[] Msg = NU.InsertarUsuario(EU).Split(':');
            lblMensaje.Text = Msg[1];
            lblMensaje.CssClass = Msg[0] == "Exito" ? "alert alert-success" : "alert alert-danger";
            lblMensaje.Visible = true;

            if (Msg[0] == "Exito")
                InicializaControles();
        }
        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(hfIdUsuario.Value);
            string[] Msg = NU.BorrarUsuario(ID).Split(':');

            lblMensaje.Text = Msg[1];
            lblMensaje.CssClass = Msg[0] == "Exito" ? "alert alert-success" : "alert alert-danger";
            lblMensaje.Visible = true;

            if (Msg[0] == "Exito")
                InicializaControles();
        }
        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(hfIdUsuario.Value);
            E_Usuarios EU = new E_Usuarios  (ID, int.Parse(ddlRol.SelectedValue), int.Parse(ddlEmpresa.SelectedValue), tbNombreUsuario.Text.Trim(), tbCorreo.Text.Trim(), tbTelefono.Text.Trim(), DateTime.Now, true, string.Empty, string.Empty);

            string[] Msg = NU.ModificarUsuario(EU).Split(':');
            lblMensaje.Text = Msg[1];
            lblMensaje.CssClass = Msg[0] == "Exito" ? "alert alert-success" : "alert alert-danger";
            lblMensaje.Visible = true;

            if (Msg[0] == "Exito")
                InicializaControles();
        }
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            VisualizaUsuarios();
        }
        #endregion

        #region Botones Grv
        protected void GrvUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                AtributosHeaderCard("Modificar los datos del usuario", "bg-primary");

                hfIdUsuario.Value = GrvUsuarios.DataKeys[e.NewEditIndex].Value.ToString();
                e.Cancel = true;
                ObjetoEntidad_ControlesWebForm(Convert.ToInt32(hfIdUsuario.Value));

                PnlCapturaDatos.Visible = true;
                PnlGrvUsuarios.Visible = false;

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
        protected void GrvUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                AtributosHeaderCard("Borrar los datos del usuario", "bg-danger");
                hfIdUsuario.Value = GrvUsuarios.DataKeys[e.RowIndex].Value.ToString();
                ObjetoEntidad_ControlesWebForm(Convert.ToInt32(hfIdUsuario.Value));
                e.Cancel = true;

                PnlCapturaDatos.Visible = true;
                PnlGrvUsuarios.Visible = false;

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
