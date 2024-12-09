<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/SideBar.Master" AutoEventWireup="true" CodeBehind="Gestion_Buques.aspx.cs" Inherits="Presentacion.Contenido.Gestion_Buques" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Gestión de Buques</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />

    <style>
        :root {
            --body-color: #E4E9F7;
            --sidebar-color: #FFF;
            --primary-color: #124072;
            --primary-color-light: #F6F5FF;
            --toggle-color: #DDD;
            --text-color: #707070;
            --alternative-color: #616161;
        }

        body {
            background-color: var(--body-color);
            color: var(--text-color);
        }

        .card-header {
            background-color: var(--primary-color);
            color: #FFF;
        }

        .btn-success {
            background-color: var(--primary-color);
            border-color: var(--primary-color);
        }

        .btn-primary {
            background-color: var(--primary-color);
            border-color: var(--primary-color);
        }

        .btn-secondary {
            background-color: var(--alternative-color);
            border-color: var(--primary-color-light);
            --text-color: var(--text-color);
        }

        .btn-danger {
            background-color: #D9534F;
            border-color: #D43F3A;
        }

        .table thead th {
            background-color: var(--primary-color);
            color: #FFF;
        }

        .table {
            background-color: var(--sidebar-color);
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <h4 class="text-black mt-5 mb-0">Gestión de Buques</h4>
        <hr class="bg-success mt-0 mb-3" />

        <div class="row">
            <div class="col-lg-8 col-md-6 col-sm-6">
                <asp:Button ID="BtnNuevoBuque" runat="server" CssClass="btn btn-success" ToolTip="Registrar un nuevo Buque" Text="Nuevo Buque" OnClick="BtnNuevoBuque_Click" />
                <asp:Button ID="BtnListadoBuques" runat="server" CssClass="btn btn-secondary" ToolTip="Listado de Buques" Text="Listado" OnClick="btnListarBuques_Click" />
            </div>

            <div class="col-lg-4 col-md-6 col-sd-6">
                <div class="row">
                    <div class="col-lg-9">
                        <asp:TextBox ID="TbCriterioBusqueda" runat="server" CssClass="form-control" placeholder="Buscar" ToolTip="Buscar" />
                    </div>
                    <div class="col-lg-3">
                        <asp:Button ID="BtnBuscar" runat="server" CssClass="btn btn-success" ToolTip="Permite buscar un Buque" Text="Buscar" OnClick="BtnBuscar_Click" />
                    </div>
                </div>
            </div>
        </div>

        <div class="container-fluid w-75 mt-5">
            <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-info" Visible="false"></asp:Label>
            <asp:Panel ID="PnlCapturaDatos" runat="server" Visible="false">
                <div class="card mt-5">
                    <div id="CardHeader" runat="server" class="card-header">
                        <h5>
                            <asp:Label ID="lblAccion" CssClass="text-white" runat="server"></asp:Label>
                        </h5>
                    </div>

                    <div class="card-body">
                        <div class="form-group">
                            <label for="tbNombreBuque">Nombre del Buque</label>
                            <asp:TextBox ID="tbNombreBuque" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label for="ddlEmpresa">Empresa</label>
                            <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="card-footer">
                        <asp:Button ID="BtnInsertar" runat="server" CssClass="btn btn-success" Text="Crear" OnClick="btnInsertar_Click" />
                        <asp:Button ID="BtnBorrar" runat="server" CssClass="btn btn-danger" Text="Borrar" CausesValidation="False" OnClick="btnBorrar_Click" OnClientClick="return confirm('Los datos del Buque serán borrados permanentemente. ¿Está seguro de borrarlos?');" />
                        <asp:Button ID="BtnModificar" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="BtnModificar_Click" />
                        <asp:Button ID="BtnCancelar" runat="server" CssClass="btn btn-secondary" Text="Cancelar" CausesValidation="False" OnClick="BtnCancelar_Click" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="PnlGrvBuques" runat="server" Visible="false">
                <div class="container">
                    <asp:GridView ID="GrvBuques" runat="server"
                        DataKeyNames="IdBuque"
                        AutoGenerateColumns="False"
                        OnRowEditing="GrvBuques_RowEditing"
                        OnRowDeleting="GrvBuques_RowDeleting"
                        CssClass="table table-sm table-bordered">
                        <Columns>
                            <asp:BoundField DataField="IdBuque" HeaderText="ID" />
                            <asp:BoundField DataField="NombreBuque" HeaderText="BUQUE" />
                            <asp:BoundField DataField="NombreEmpresa" HeaderText="EMPRESA" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="GrvBtnEditar" runat="server" CssClass="btn btn-sm btn-primary" ToolTip="Editar los datos del buque" CommandName="Edit"><i class="bi bi-pen"></i> Editar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="GrvBtnBorrar" runat="server" CssClass="btn btn-sm btn-danger" ToolTip="Borrar los datos del buque" CommandName="Delete"><i class="bi bi-trash"></i> Borrar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
        </div>

        <asp:HiddenField ID="hfIdBuque" runat="server" />
    </div>

</asp:Content>

