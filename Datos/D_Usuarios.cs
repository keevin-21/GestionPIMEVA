using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Entidades;

namespace Datos
{
    public class D_Usuarios
    {
        public SqlConnection conn;
        public SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);

        public D_Usuarios()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
        }

        public List<E_Usuarios> ListarUsuarios(string nombreUsuario = null, bool? estado = null)
        {
            List<E_Usuarios> usuarios = new List<E_Usuarios>();

            using (conn2)
            {
                SqlCommand cmd = new SqlCommand("sp_ConsultarUsuarios", conn2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreUsuario", (object)nombreUsuario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", (object)estado ?? DBNull.Value);

                conn2.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    E_Usuarios usuario = new E_Usuarios
                    {
                        IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                        IdRol = Convert.ToInt32(reader["IdRol"]),
                        NombreRol = reader["NombreRol"].ToString(),
                        IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                        NombreEmpresa = reader["NombreEmpresa"].ToString(),
                        NombreUsuario = reader["NombreUsuario"].ToString(),
                        Correo = reader["Correo"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                        Estado = Convert.ToBoolean(reader["Estado"])
                    };
                    usuarios.Add(usuario);
                }
            }
            conn2.Close();
            return usuarios;
        }



        public E_Usuarios BuscarUsuarioPorID(int idUsuario)
        {
            E_Usuarios usuario = null;

            using (conn)
            {
                SqlCommand cmd = new SqlCommand("sp_ConsultarUsuarioPorID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    usuario = new E_Usuarios
                    {
                        IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                        IdRol = Convert.ToInt32(reader["IdRol"]),
                        IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                        NombreUsuario = reader["NombreUsuario"].ToString(),
                        Correo = reader["Correo"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                        Estado = Convert.ToBoolean(reader["Estado"]),
                        NombreRol = reader["NombreRol"].ToString(),       // Asignar NombreRol
                        NombreEmpresa = reader["NombreEmpresa"].ToString() // Asignar NombreEmpresa
                    };
                }
            }
            conn.Close();
            return usuario;
        }

        public string GestionarUsuario(string accion, E_Usuarios usuario)
        {
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("sp_GestionarUsuarios", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", accion);
                cmd.Parameters.AddWithValue("@IdUsuario", (object)usuario.IdUsuario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IdRol", (object)usuario.IdRol ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IdEmpresa", (object)usuario.IdEmpresa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NombreUsuario", (object)usuario.NombreUsuario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Correo", (object)usuario.Correo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Telefono", (object)usuario.Telefono ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", (object)usuario.Estado ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            return $"Exito: Usuario {accion.ToLower()} correctamente.";
        }
    }
}
