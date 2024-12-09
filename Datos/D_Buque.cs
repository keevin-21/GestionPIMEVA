using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Entidades;

namespace Datos
{
    public class D_Buque
    {
        public SqlConnection conn;
        public SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);

        public D_Buque()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
        }

        public List<E_Buque> ListarBuques(string nombreBuque = null, bool? estado = null)
        {
            List<E_Buque> buques = new List<E_Buque>();

            using (conn2)
            {
                SqlCommand cmd = new SqlCommand("sp_ConsultarBuque", conn2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreBuque", (object)nombreBuque ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", (object)estado ?? DBNull.Value);

                conn2.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    E_Buque buque = new E_Buque
                    {
                        IdBuque = Convert.ToInt32(reader["IdBuque"]),
                        NombreBuque = reader["NombreBuque"].ToString(),
                        NombreEmpresa = reader["NombreEmpresa"].ToString(),  // Actualización para manejar NombreEmpresa
                        Estado = Convert.ToBoolean(reader["Estado"])
                    };
                    buques.Add(buque);
                }
            }
            conn2.Close();
            return buques;
        }


        public E_Buque BuscarBuquePorID(int idBuque)
        {
            E_Buque buque = null;

            using (conn)
            {
                SqlCommand cmd = new SqlCommand("sp_ConsultarBuquePorID", conn); // Asegúrate de que el procedimiento almacenado sp_ConsultarBuquePorID esté definido en la base de datos.
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdBuque", idBuque);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    buque = new E_Buque
                    {
                        IdBuque = Convert.ToInt32(reader["IdBuque"]),
                        NombreBuque = reader["NombreBuque"].ToString(),
                        IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                        Estado = Convert.ToBoolean(reader["Estado"])
                    };
                }
            }
            conn.Close();
            return buque;
        }

        public string GestionarBuque(string accion, E_Buque buque)
        {
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("sp_GestionarBuque", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", accion);
                cmd.Parameters.AddWithValue("@IdBuque", (object)buque.IdBuque ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NombreBuque", (object)buque.NombreBuque ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IdEmpresa", (object)buque.IdEmpresa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", (object)buque.Estado ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            return $"Exito: Buque {accion.ToLower()} correctamente.";
        }
    }
}
