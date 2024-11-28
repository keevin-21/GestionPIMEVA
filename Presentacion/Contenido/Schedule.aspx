<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/SideBar.Master" AutoEventWireup="true" CodeBehind="Schedule.aspx.cs" Inherits="Presentacion.Contenido.InicioSidebar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HomeContent" runat="server">
    <h4>Bienvenidos al sitema de ventas 1.0.0.0</h4>
    <br />
    <br />
    <h3>
        <asp:Label ID="lblResultado" runat="server"></asp:Label>
    </h3>
    <br />
    <br />
    **************************<br />
    INSERTAR<br />
    Nombre :
 <asp:TextBox ID="tbNombreCategoria" runat="server"></asp:TextBox>
    Descripcion :
 <asp:TextBox ID="tbDescripcionCategoria" runat="server"></asp:TextBox>
    <asp:Button ID="btnInsertar" runat="server" Text="Insertar" OnClick="btnInsertar_Click" />
    <br />
    *************************<br />
    <br />
    *************************<br />
    BORRAR<br />
    Dato a borrar:
 <asp:TextBox ID="tbCategoria" runat="server"></asp:TextBox>
    <asp:Button ID="btnBorrar" runat="server" Text="BORRAR" OnClick="btnBorrar_Click" />
    <br />
    *************************<br />
    <br />
    <br />

    <asp:Button ID="btnListar" runat="server" Text="Listar categorias" OnClick="btnListado_Click" />
    <br />
    <asp:GridView ID="GrvCategorias" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" DataKeyNames="IdCategoria" ForeColor="Black" >
        <Columns>
            <asp:BoundField DataField="IdCategoria" HeaderText="IdCategoria" InsertVisible="False" ReadOnly="True" SortExpression="IdCategoria" />
            <asp:BoundField DataField="NombreCategoria" HeaderText="NombreCategoria" SortExpression="NombreCategoria" />
            <asp:BoundField DataField="DescripcionCategoria" HeaderText="DescripcionCategoria" SortExpression="DescripcionCategoria" />
            <asp:CheckBoxField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>


</asp:Content>
