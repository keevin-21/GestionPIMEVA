using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;

public partial class Login : System.Web.UI.Page
{
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string correo = txtCorreo.Text.Trim();
        string password = txtPassword.Text;

        if (ValidarCredenciales(correo, password))
        {
            // Redirige al usuario al panel principal
            Response.Redirect("../Contenido/Logistica.aspx");
        }
        else
        {
            // Muestra un mensaje de error
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Correo o contraseña incorrectos.');", true);
        }
    }

    private bool ValidarCredenciales(string correo, string password)
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = @"
                SELECT 
                    cu.PasswordHash, 
                    cu.PasswordSalt
                FROM CredencialesUsuario cu
                INNER JOIN Usuarios u ON cu.IdUsuario = u.IdUsuario
                WHERE u.Correo = @Correo AND u.Estado = 1";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Correo", correo);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                byte[] storedHash = (byte[])reader["PasswordHash"]; 
                byte[] storedSalt = (byte[])reader["PasswordSalt"];
                reader.Close();

                return VerificarPassword(password, storedHash, storedSalt);
            }

            return false; // Usuario no encontrado o inactivo
        }
    }

    private bool VerificarPassword(string password, byte[] storedHash, byte[] storedSalt)
    {
        using (var hmac = new HMACSHA512(storedSalt))
        {
            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return StructuralComparisons.StructuralEqualityComparer.Equals(computedHash, storedHash);
        }
    }

    public void RegistrarUsuario(string correo, string password)
    {
        byte[] passwordSalt, passwordHash;

        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key; // Genera un salt único
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // Calcula el hash
        }

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            // Inserta el usuario en la tabla Usuarios
            string queryUsuario = @"
            INSERT INTO Usuarios (IdRol, IdEmpresa, NombreUsuario, Correo, Telefono, Estado)
            OUTPUT INSERTED.IdUsuario
            VALUES (@IdRol, @IdEmpresa, @NombreUsuario, @Correo, @Telefono, 1)";

            SqlCommand cmdUsuario = new SqlCommand(queryUsuario, conn);
            cmdUsuario.Parameters.AddWithValue("@IdRol", 1); // Asigna el rol
            cmdUsuario.Parameters.AddWithValue("@IdEmpresa", 1); // Asigna la empresa
            cmdUsuario.Parameters.AddWithValue("@NombreUsuario", "Nombre Ejemplo");
            cmdUsuario.Parameters.AddWithValue("@Correo", correo);
            cmdUsuario.Parameters.AddWithValue("@Telefono", "123456789");

            int idUsuario = (int)cmdUsuario.ExecuteScalar();

            // Inserta las credenciales en la tabla CredencialesUsuario
            string queryCredenciales = @"
            INSERT INTO CredencialesUsuario (IdUsuario, PasswordHash, PasswordSalt)
            VALUES (@IdUsuario, @PasswordHash, @PasswordSalt)";

            SqlCommand cmdCredenciales = new SqlCommand(queryCredenciales, conn);
            cmdCredenciales.Parameters.AddWithValue("@IdUsuario", idUsuario);
            cmdCredenciales.Parameters.AddWithValue("@PasswordHash", passwordHash);
            cmdCredenciales.Parameters.AddWithValue("@PasswordSalt", passwordSalt);

            cmdCredenciales.ExecuteNonQuery();
        }
    }

}
