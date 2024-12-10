using System;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class D_TablaDeLogistica
    {
        public DataTable ObtenerLogistica()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        l.IdRegistro,
                        b.NombreBuque AS Buque,
                        l.LOA,
                        FORMAT(OperationTime, 'HH:mm') AS [Operation Time],
                        l.ETA,
                        l.POB,
                        l.ETB,
                        l.ETC,
                        l.ETD,
                        l.Cargo
                    FROM TablaDeLogistica l
                    INNER JOIN Buque b ON l.IdBuque = b.IdBuque
                    WHERE l.EsHistorico = 1";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public bool ActualizarLogistica()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ActualizarLogistica", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new Exception("No se actualizaron registros en la base de datos.");
                        }

                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar la logística: " + ex.Message);
                }
            }
        }

    }
}
