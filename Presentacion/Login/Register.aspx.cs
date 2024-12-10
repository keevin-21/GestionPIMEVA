using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

public partial class Register : System.Web.UI.Page
{
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string nombre = txtNombre.Text.Trim();
        string correo = txtCorreo.Text.Trim();
        string telefono = txtTelefono.Text.Trim();
        string password = txtPassword.Text;

        // Validar que los campos no estén vacíos
        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(telefono) || string.IsNullOrEmpty(password))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Todos los campos son obligatorios.');", true);
            return;
        }

        // Validar el formato del correo
        if (!Regex.IsMatch(correo, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, ingrese un correo electrónico válido.');", true);
            return;
        }

        // Validar el formato del número de teléfono
        if (!Regex.IsMatch(telefono, @"^[0-9]{7,14}$"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, ingrese un número de teléfono válido.');", true);
            return;
        }

        // Intentar registrar el usuario
        if (RegistrarUsuario(nombre, correo, telefono, password))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Usuario registrado exitosamente.');", true);
            Response.Redirect("Login.aspx");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error al registrar el usuario.');", true);
        }
    }

    private bool RegistrarUsuario(string nombre, string correo, string telefono, string password)
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            SqlTransaction transaction = conn.BeginTransaction(); // Inicia una transacción

            try
            {
                // Inserta en la tabla Usuarios
                string queryUsuario = @"
                    INSERT INTO Usuarios (IdRol, IdEmpresa, NombreUsuario, Correo, Telefono, Estado)
                    OUTPUT INSERTED.IdUsuario
                    VALUES (@IdRol, @IdEmpresa, @NombreUsuario, @Correo, @Telefono, 1)";

                SqlCommand cmdUsuario = new SqlCommand(queryUsuario, conn, transaction);
                cmdUsuario.Parameters.AddWithValue("@IdRol", 1); // Asigna el rol (ajustar según sea necesario)
                cmdUsuario.Parameters.AddWithValue("@IdEmpresa", 1); // Asigna la empresa (ajustar según sea necesario)
                cmdUsuario.Parameters.AddWithValue("@NombreUsuario", nombre);
                cmdUsuario.Parameters.AddWithValue("@Correo", correo);
                cmdUsuario.Parameters.AddWithValue("@Telefono", telefono);

                int idUsuario = (int)cmdUsuario.ExecuteScalar(); // Obtén el IdUsuario generado

                // Generar el hash y salt de la contraseña
                byte[] passwordHash, passwordSalt;
                using (var hmac = new HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                }

                // Inserta en la tabla CredencialesUsuario
                string queryCredenciales = @"
                    INSERT INTO CredencialesUsuario (IdUsuario, PasswordHash, PasswordSalt)
                    VALUES (@IdUsuario, @PasswordHash, @PasswordSalt)";

                SqlCommand cmdCredenciales = new SqlCommand(queryCredenciales, conn, transaction);
                cmdCredenciales.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmdCredenciales.Parameters.AddWithValue("@PasswordHash", passwordHash);
                cmdCredenciales.Parameters.AddWithValue("@PasswordSalt", passwordSalt);

                cmdCredenciales.ExecuteNonQuery();

                transaction.Commit(); // Confirma la transacción
                return true;
            }
            catch
            {
                transaction.Rollback(); // Revierte la transacción en caso de error
                return false;
            }
        }
    }
}
