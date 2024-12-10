using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Entidades;

namespace Datos
{
    public class D_Roles
    {
        public SqlConnection conn;

        public D_Roles()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
        }

        public List<E_Roles> ListarRoles()
        {
            List<E_Roles> roles = new List<E_Roles>();

            using (conn)
            {
                SqlCommand cmd = new SqlCommand("sp_ConsultarRoles", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    E_Roles rol = new E_Roles
                    {
                        IdRol = Convert.ToInt32(reader["IdRol"]),
                        NombreRol = reader["NombreRol"].ToString(),
                        DescripcionRol = reader["DescripcionRol"].ToString(),
                        Estado = Convert.ToBoolean(reader["Estado"])
                    };
                    roles.Add(rol);
                }
            }
            conn.Close();
            return roles;
        }

        public E_Roles BuscarRolPorID(int idRol)
        {
            E_Roles rol = null;

            using (conn)
            {
                SqlCommand cmd = new SqlCommand("sp_ConsultarRolPorID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdRol", idRol);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    rol = new E_Roles
                    {
                        IdRol = Convert.ToInt32(reader["IdRol"]),
                        NombreRol = reader["NombreRol"].ToString(),
                        DescripcionRol = reader["DescripcionRol"].ToString(),
                        Estado = Convert.ToBoolean(reader["Estado"])
                    };
                }
            }
            conn.Close();
            return rol;
        }

        public string GestionarRol(string accion, E_Roles rol)
        {
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("sp_GestionarRol", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", accion);
                cmd.Parameters.AddWithValue("@IdRol", (object)rol.IdRol ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NombreRol", (object)rol.NombreRol ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DescripcionRol", (object)rol.DescripcionRol ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", (object)rol.Estado ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            return $"Exito: Rol {accion.ToLower()} correctamente.";
        }
    }
}
