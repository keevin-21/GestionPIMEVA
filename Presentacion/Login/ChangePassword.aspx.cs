using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

public partial class ChangePassword : System.Web.UI.Page
{
    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        string email = txtCorreo.Text.Trim();
        string currentPassword = txtCurrentPassword.Text.Trim();
        string newPassword = txtNewPassword.Text.Trim();
        string confirmPassword = txtConfirmPassword.Text.Trim();

        // Validaciones iniciales
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Todos los campos son obligatorios.');", true);
            return;
        }

        if (!IsValidEmail(email))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor, ingrese un correo electrónico válido.');", true);
            return;
        }

        if (newPassword != confirmPassword)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('La nueva contraseña y la confirmación no coinciden.');", true);
            return;
        }

        if (ActualizarPassword(email, currentPassword, newPassword))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Contraseña actualizada exitosamente.');", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error al actualizar la contraseña. Verifique sus datos.');", true);
        }
    }

    private bool ActualizarPassword(string email, string currentPassword, string newPassword)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            // Inicia la transacción
            SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                // Obtiene las credenciales y el IdUsuario asociado al correo
                string queryGetCredentials = @"
                    SELECT IdUsuario, PasswordHash, PasswordSalt
                    FROM Usuarios u
                    JOIN CredencialesUsuario cu ON u.IdUsuario = cu.IdUsuario
                    WHERE u.Correo = @Correo";

                SqlCommand cmdGetCredentials = new SqlCommand(queryGetCredentials, conn, transaction);
                cmdGetCredentials.Parameters.AddWithValue("@Correo", email);

                SqlDataReader reader = cmdGetCredentials.ExecuteReader();

                if (!reader.Read())
                {
                    reader.Close();
                    transaction.Rollback();
                    return false; // Correo no encontrado
                }

                int userId = (int)reader["IdUsuario"];
                byte[] storedHash = (byte[])reader["PasswordHash"];
                byte[] storedSalt = (byte[])reader["PasswordSalt"];
                reader.Close();

                // Verifica la contraseña actual
                using (var hmac = new HMACSHA512(storedSalt))
                {
                    byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(currentPassword));
                    if (!computedHash.SequenceEqual(storedHash))
                    {
                        transaction.Rollback();
                        return false; // Contraseña actual incorrecta
                    }
                }

                // Genera nuevo hash y salt para la nueva contraseña
                byte[] newHash, newSalt;
                using (var hmac = new HMACSHA512())
                {
                    newSalt = hmac.Key;
                    newHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
                }

                // Actualiza las credenciales del usuario
                string queryUpdateCredentials = @"
                    UPDATE CredencialesUsuario
                    SET PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt
                    WHERE IdUsuario = @IdUsuario";

                SqlCommand cmdUpdateCredentials = new SqlCommand(queryUpdateCredentials, conn, transaction);
                cmdUpdateCredentials.Parameters.AddWithValue("@PasswordHash", newHash);
                cmdUpdateCredentials.Parameters.AddWithValue("@PasswordSalt", newSalt);
                cmdUpdateCredentials.Parameters.AddWithValue("@IdUsuario", userId);

                cmdUpdateCredentials.ExecuteNonQuery();

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

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
