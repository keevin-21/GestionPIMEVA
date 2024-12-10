using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Entidades;

namespace Datos
{
    public class D_Empresa
    {
        public SqlConnection conn;
        public SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);

        public D_Empresa()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
        }

        public List<E_Empresa> ListarEmpresas(string nombreEmpresa = null, bool? estado = null)
        {
            List<E_Empresa> empresas = new List<E_Empresa>();

            using (conn2)
            {
                SqlCommand cmd = new SqlCommand("sp_ConsultarEmpresa", conn2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreEmpresa", (object)nombreEmpresa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", (object)estado ?? DBNull.Value);

                conn2.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    E_Empresa empresa = new E_Empresa
                    {
                        IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                        NombreEmpresa = reader["NombreEmpresa"].ToString(),
                        DescripcionEmpresa = reader["DescripcionEmpresa"].ToString(),
                        Estado = Convert.ToBoolean(reader["Estado"])
                    };
                    empresas.Add(empresa);
                }
            }
            conn2.Close();
            return empresas;
        }

        public E_Empresa BuscarEmpresaPorID(int idEmpresa)
        {
            E_Empresa empresa = null;

            using (conn)
            {
                SqlCommand cmd = new SqlCommand("sp_ConsultarEmpresaPorID", conn); // Asegúrate de que el procedimiento almacenado sp_ConsultarEmpresaPorID esté definido en la base de datos.
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdEmpresa", idEmpresa);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    empresa = new E_Empresa
                    {
                        IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                        NombreEmpresa = reader["NombreEmpresa"].ToString(),
                        DescripcionEmpresa = reader["DescripcionEmpresa"].ToString(),
                        Estado = Convert.ToBoolean(reader["Estado"])
                    };
                }
            }
            conn.Close();
            return empresa;
        }


        public string GestionarEmpresa(string accion, E_Empresa empresa)
        {
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("sp_GestionarEmpresa", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", accion);
                cmd.Parameters.AddWithValue("@IdEmpresa", (object)empresa.IdEmpresa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NombreEmpresa", (object)empresa.NombreEmpresa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DescripcionEmpresa", (object)empresa.DescripcionEmpresa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", (object)empresa.Estado ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            return $"Exito: Empresa {accion.ToLower()}da correctamente.";
        }
    }
}
