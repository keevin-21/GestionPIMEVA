using PdfSharp.Pdf.Content.Objects;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public partial class Login : System.Web.UI.Page
{
    protected void btnLogin_Click(object sender, EventArgs e)
    {

        if (ValidarUsuario())
        {
            // Crea la cookie de autenticación
            // El segundo parámetro(false) indica que la cookie no será
            // persistente(cerrará sesión al cerrar el navegador).
            // Redirige al dashboard o página principal
            Response.Redirect("~/Contenido/Gestion_Buques.aspx");
        }
        else
        {
            // Mostrar mensaje de error
            Response.Write("<script>alert('Credenciales incorrectas.');</script>");
        }
    }
    private bool ValidarUsuario()
    {
        
        return true;
    }

}