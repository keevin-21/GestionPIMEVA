<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/SideBar.Master" AutoEventWireup="true" CodeBehind="Gestion_Empresas.aspx.cs" Inherits="Presentacion.Contenido.Gestion_Empresas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Gestión de Empresas</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />
    <link href="../RECURSOS/CSS/empresas.css" rel="stylesheet" />
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
    <h4 class="text-black mt-5 mb-0">Gestión de Empresas</h4>
    <hr class="bg-success mt-0 mb-3" />

    <div class="row">
        <div class="col-lg-8 col-md-6 col-sm-6">
            <asp:Button ID="BtnNuevaEmpresa" runat="server" CssClass="btn btn-success" ToolTip="Registrar una nueva Empresa" Text="Nueva Empresa" OnClick="BtnNuevaEmpresa_Click" />
            <asp:Button ID="BtnListadoEmpresas" runat="server" CssClass="btn btn-secondary" ToolTip="Listado de Empresas" Text="Listado" OnClick="btnListarEmpresas_Click" />
        </div>

        <div class="col-lg-4 col-md-6 col-sd-6">
            <div class="row">
                <div class="col-lg-9">
                    <asp:TextBox ID="TbCriterioBusqueda" runat="server" CssClass="form-control" placeholder="Buscar" ToolTip="Buscar" />
                </div>
                <div class="col-lg-3">
                    <asp:Button ID="BtnBuscar" runat="server" CssClass="btn btn-success" ToolTip="Permite buscar una Empresa" Text="Buscar" OnClick="BtnBuscar_Click" />
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
                        <label for="tbNombreEmpresa">Nombre de la empresa</label>
                        <asp:TextBox ID="tbNombreEmpresa" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="tbDescripcionEmpresa">Descripción</label>
                        <asp:TextBox ID="tbDescripcionEmpresa" runat="server" CssClass="form-control" TextMode="MultiLine" />
                    </div>
                </div>

                <div class="card-footer">
                    <asp:Button ID="BtnInsertar" runat="server" CssClass="btn btn-success" Text="Crear" OnClick="btnInsertar_Click" />
                    <asp:Button ID="BtnBorrar" runat="server" CssClass="btn btn-danger" Text="Borrar" CausesValidation="False" OnClick="btnBorrar_Click" OnClientClick="return confirm('Los datos de la Empresa serán borrados permanentemente. ¿Está seguro de borrarlos?');" />
                    <asp:Button ID="BtnModificar" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="BtnModificar_Click" />
                    <asp:Button ID="BtnCancelar" runat="server" CssClass="btn btn-secondary" Text="Cancelar" CausesValidation="False" OnClick="BtnCancelar_Click" />
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="PnlGrvEmpresas" runat="server" Visible="false">
            <div class="container">
                <asp:GridView ID="GrvEmpresas" runat="server"
                    DataKeyNames="IdEmpresa"
                    AutoGenerateColumns="False"
                    OnRowEditing="GrvEmpresas_RowEditing"
                    OnRowDeleting="GrvEmpresas_RowDeleting"
                    CssClass="table table-sm table-bordered">
                    <Columns>
                        <asp:BoundField DataField="IdEmpresa" HeaderText="ID" />
                        <asp:BoundField DataField="NombreEmpresa" HeaderText="EMPRESA" />
                        <asp:BoundField DataField="DescripcionEmpresa" HeaderText="DESCRIPCIÓN" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="GrvBtnEditar" runat="server" CssClass="btn btn-sm btn-primary" ToolTip="Editar los datos de la empresa" CommandName="Edit"><i class="bi bi-pen"></i> Editar</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="GrvBtnBorrar" runat="server" CssClass="btn btn-sm btn-danger" ToolTip="Borrar los datos de la empresa" CommandName="Delete"><i class="bi bi-trash"></i> Borrar</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </asp:Panel>
    </div>

    <asp:HiddenField ID="hfIdEmpresa" runat="server" />
</div>

</asp:Content>
