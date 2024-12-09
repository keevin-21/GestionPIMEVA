<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/SideBar.Master" AutoEventWireup="true" CodeBehind="Gestion_Buques.aspx.cs" Inherits="Presentacion.Contenido.Gestion_Buques" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Gestion de Buques</h1>
     <br />


    <h3>Agregar Nuevo Buque</h3>
    <asp:Label ID="lblNombreBuque" runat="server" Text="Nombre Buque:" AssociatedControlID="txtNombreBuque"></asp:Label>
    <asp:TextBox ID="txtNombreBuque" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="lblIdEmpresa" runat="server" Text="Agencia a cargo" AssociatedControlID="ddlIdEmpresa"></asp:Label>
    <asp:DropDownList ID="ddlIdEmpresa" runat="server" DataTextField="NombreEmpresa" DataSourceID="SqlEmpresas" DataValueField="NombreEmpresa"></asp:DropDownList>
    <asp:SqlDataSource ID="SqlEmpresas" runat="server" ConnectionString="<%$ ConnectionStrings:ConexionBD %>" SelectCommand="SELECT [NombreEmpresa] FROM [Empresa]"></asp:SqlDataSource>
    <br />
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />


    <br />
    <h3>Listado de buques</h3>
    <asp:GridView ID="grvBuques" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="NombreBuque" HeaderText="NombreBuque" SortExpression="NombreBuque" />
            <asp:BoundField DataField="IdEmpresa" HeaderText="IdEmpresa" SortExpression="IdEmpresa" />
            <asp:CheckBoxField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
        </Columns>
    </asp:GridView>

</asp:Content>

