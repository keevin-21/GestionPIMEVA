<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Historial_Cambios.aspx.cs" Inherits="PIMEVA.HistorialCambios" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Historial de Cambios</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <h2 class="text-center">Historial de Cambios</h2>
            <div class="table-responsive">
                <asp:GridView ID="gridHistorial" runat="server" CssClass="table table-bordered table-hover" 
                              HeaderStyle-CssClass="table-dark" AutoGenerateColumns="False" 
                              AllowPaging="True" PageSize="10" 
                              OnPageIndexChanging="gridHistorial_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="IdHistorial" HeaderText="ID Historial" />
                        <asp:BoundField DataField="TipoCambio" HeaderText="Tipo de Cambio" />
                        <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />
                        <asp:BoundField DataField="FechaModificacion" HeaderText="Fecha de Modificación" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                        <asp:BoundField DataField="Buque" HeaderText="Buque" />
                        <asp:BoundField DataField="OperationTime" HeaderText="Operation Time" DataFormatString="{0:HH:mm}" />
                        <asp:BoundField DataField="ETA" HeaderText="ETA" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                        <asp:BoundField DataField="ETD" HeaderText="ETD" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                        <asp:BoundField DataField="Cargo" HeaderText="Cargo" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
