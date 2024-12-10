<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<!DOCTYPE html>
<html>
<head>
    <title>Cambiar Contraseña</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <div class="container d-flex justify-content-center align-items-center vh-100">
        <div class="card p-4 shadow" style="width: 25rem;">
            <h3 class="text-center mb-4">Cambiar Contraseña</h3>
            <form id="formChangePassword" runat="server">
                <div class="mb-3">
                    <label for="txtCorreo" class="form-label">Correo Electrónico</label>
                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" placeholder="Ingrese su correo electrónico"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="txtCurrentPassword" class="form-label">Contraseña Actual</label>
                    <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingrese su contraseña actual"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="txtNewPassword" class="form-label">Nueva Contraseña</label>
                    <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingrese la nueva contraseña"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="txtConfirmPassword" class="form-label">Confirmar Nueva Contraseña</label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Confirme la nueva contraseña"></asp:TextBox>
                </div>
                <asp:Button ID="btnChangePassword" runat="server" CssClass="btn btn-primary w-100" Text="Cambiar Contraseña" OnClick="btnChangePassword_Click" />
            </form>
        </div>
    </div>
</body>
</html>
