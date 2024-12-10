<%@ Page Title="Tabla Logistica" Language="C#" MasterPageFile="~/PaginasMaestras/SideBar.Master" AutoEventWireup="true" CodeBehind="Logistica.aspx.cs" Inherits="Presentacion.Contenido.Logistica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Logística</title> 
    <!-- Cargar librerías de DataTables --> 
    <link href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.min.css" rel="stylesheet"> 
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script> 
    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar Datos" CssClass="btn btn-primary" OnClick="btnActualizar_Click" />
    </div>

    <table id="logisticaTable" class="display" style="width:100%">
        <thead>
            <tr>
                <th>ID Registro</th>
                <th>Buque</th>
                <th>LOA</th>
                <th>Operation Time</th>
                <th>ETA</th>
                <th>POB</th>
                <th>ETB</th>
                <th>ETC</th>
                <th>ETD</th>
                <th>Cargo</th>
            </tr>
        </thead>
    </table>

    <!-- Referencia al archivo JS -->
    <script type="module" src="../RECURSOS/JavaScript/TablaDeLogistica.js"></script>
</asp:Content>
