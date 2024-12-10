<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>
<html>
<head>
    <title>Registro de Usuario</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">

    <script>
        function validarCorreo() {
            var correo = document.getElementById('<%= txtCorreo.ClientID %>').value;
            var regexCorreo = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; // Expresión para validar correos

            if (!regexCorreo.test(correo)) {
                alert("Por favor, ingrese un correo electrónico válido.");
                return false;
            }
            return true;
        }

        function validarTelefono() {
            var telefono = document.getElementById('<%= txtTelefono.ClientID %>').value;
            var regexTelefono = /^[0-9]{7,14}$/; // Validación básica para teléfonos (solo números)

            if (!regexTelefono.test(telefono)) {
                alert("Por favor, ingrese un número de teléfono válido.");
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <div class="container d-flex justify-content-center align-items-center vh-100">
        <div class="card p-4 shadow" style="width: 25rem;">
            <h3 class="text-center mb-4">Registrar Usuario</h3>
            <form id="formRegister" runat="server" onsubmit="return validarCorreo() && validarTelefono();">
                <div class="mb-3">
                    <label for="txtNombre" class="form-label">Nombre de Usuario</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingrese su nombre"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="txtCorreo" class="form-label">Correo Electrónico</label>
                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" placeholder="Ingrese su correo"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="txtTelefono" class="form-label">Número de Teléfono</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ingrese su teléfono"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="txtPassword" class="form-label">Contraseña</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingrese su contraseña"></asp:TextBox>
                </div>
                <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-primary w-100" Text="Registrar" OnClick="btnRegister_Click" />
            </form>
            <div class="text-center mt-3">
                <p>¿Ya tienes una cuenta? <a href="Login.aspx">Inicia sesión aquí</a></p>
            </div>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
