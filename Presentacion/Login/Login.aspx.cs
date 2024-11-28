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
        string correo = txtCorreo.Text.Trim();
        string password = txtPassword.Text;

        if (ValidarUsuario(correo, password))
        {
            // Crea la cookie de autenticación
            // El segundo parámetro(false) indica que la cookie no será
            // persistente(cerrará sesión al cerrar el navegador).
            System.Web.Security.FormsAuthentication.SetAuthCookie(correo, false);

            // Redirige al dashboard o página principal
            Response.Redirect("~/Contenido/Schedule.aspx");
        }
        else
        {
            // Mostrar mensaje de error
            Response.Write("<script>alert('Credenciales incorrectas.');</script>");
        }
    }
    private bool ValidarUsuario(string correo, string password)
    {
        //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
        //using (SqlConnection conn = new SqlConnection(connectionString))
        //{
        //    conn.Open();
        //    string query = "SELECT PasswordHash, PasswordSalt FROM Usuarios WHERE Correo = @Correo AND Estado = 1";
        //    SqlCommand cmd = new SqlCommand(query, conn);
        //    cmd.Parameters.AddWithValue("@Correo", correo);

        //    using (SqlDataReader reader = cmd.ExecuteReader())
        //    {
        //        if (reader.Read())
        //        {
        //            byte[] storedHash = (byte[])reader["PasswordHash"];
        //            byte[] storedSalt = (byte[])reader["PasswordSalt"];

        //            // Verificar contraseña
        //            using (var hmac = new HMACSHA512(storedSalt))
        //            {
        //                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //                return storedHash.SequenceEqual(computedHash);
        //            }
        //        }
        //    }
        //}
        //return false;
        return true;
    }

}