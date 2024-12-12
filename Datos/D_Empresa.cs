using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Entidades;
using Datos;

namespace Datos
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Configuration;
    using Entidades;

    namespace Datos
    {
        public class D_Empresa : D_ConexionBD
        {
            public SqlConnection conn;

            public List<E_Empresa> ListarEmpresas(string nombreEmpresa = null, bool? estado = null)
            {
                List<E_Empresa> empresas = new List<E_Empresa>();

                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ConsultarEmpresa", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreEmpresa", (object)nombreEmpresa ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Estado", (object)estado ?? DBNull.Value);

                    AbrirConexion();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
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
                }
                catch (Exception ex) 
                {
                    throw new Exception("Error al listar empresas", ex);
                }
                finally
                {
                    CerrarConexion();
                }
                return empresas;
            }

            public E_Empresa BuscarEmpresaPorID(int idEmpresa)
            {
                E_Empresa empresa = null;

                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ConsultarEmpresaPorID", conexion); // Asegúrate de que el procedimiento almacenado sp_ConsultarEmpresaPorID esté definido en la base de datos.
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdEmpresa", idEmpresa);

                    AbrirConexion();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
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
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al buscar empresa", ex);
                }
                finally
                {
                    CerrarConexion();
                }
                return empresa;
            }


            public string GestionarEmpresa(string accion, E_Empresa empresa)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_GestionarEmpresa", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", accion);
                    cmd.Parameters.AddWithValue("@IdEmpresa", (object)empresa.IdEmpresa ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NombreEmpresa", (object)empresa.NombreEmpresa ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DescripcionEmpresa", (object)empresa.DescripcionEmpresa ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Estado", (object)empresa.Estado ?? DBNull.Value);

                    AbrirConexion();
                    cmd.ExecuteNonQuery();
                    return $"Exito: Empresa {accion.ToLower()}da correctamente.";
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al gestionar empresa", ex);
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        CerrarConexion();
                    }
                }
            }
        }
    }
}

////
///
//public class D_Empresa : D_ConexionBD
//{
//    public SqlConnection conn;

//    public D_Empresa()
//    {
//        conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
//    }

//    public List<E_Empresa> ListarEmpresas(string nombreEmpresa = null, bool estado = true)
//    {
//        List<E_Empresa> empresas = new List<E_Empresa>();

//        try
//        {
//            SqlCommand cmd = new SqlCommand("sp_ConsultarEmpresa", conexion);
//            cmd.CommandType = CommandType.StoredProcedure;
//            cmd.Parameters.AddWithValue("@NombreEmpresa", (object)nombreEmpresa ?? DBNull.Value);
//            cmd.Parameters.AddWithValue("@Estado", (object)estado ?? DBNull.Value);

//            AbrirConexion();
//            using (SqlDataReader reader = cmd.ExecuteReader())
//            {
//                while (reader.Read())
//                {
//                    E_Empresa empresa = new E_Empresa
//                    {
//                        IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
//                        NombreEmpresa = reader["NombreEmpresa"].ToString(),
//                        DescripcionEmpresa = reader["DescripcionEmpresa"].ToString(),
//                        Estado = Convert.ToBoolean(reader["Estado"])
//                    };
//                    empresas.Add(empresa);
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            throw new Exception("Error al listar empresas", ex);
//        }
//        finally
//        {
//            CerrarConexion();
//        }
//        return empresas;
//    }


//    public E_Empresa BuscarEmpresaPorID(int idEmpresa)
//    {
//        E_Empresa empresa = null;

//        try
//        {
//            SqlCommand cmd = new SqlCommand("sp_ConsultarEmpresaPorID", conexion); // Asegúrate de que el procedimiento almacenado sp_ConsultarEmpresaPorID esté definido en la base de datos.
//            cmd.CommandType = CommandType.StoredProcedure;
//            cmd.Parameters.AddWithValue("@IdEmpresa", idEmpresa);

//            AbrirConexion();
//            using (SqlDataReader reader = cmd.ExecuteReader())
//            {
//                if (reader.Read())
//                {
//                    empresa = new E_Empresa
//                    {
//                        IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
//                        NombreEmpresa = reader["NombreEmpresa"].ToString(),
//                        DescripcionEmpresa = reader["DescripcionEmpresa"].ToString(),
//                        Estado = Convert.ToBoolean(reader["Estado"])
//                    };
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            throw new Exception("Error al buscar empresa por ID", ex);
//        }
//        finally
//        {
//            CerrarConexion();
//        }

//        return empresa;
//    }


//    public string GestionarEmpresa(string accion, E_Empresa empresa)
//    {
//        try
//        {
//            SqlCommand cmd = new SqlCommand("sp_GestionarEmpresa", conn);
//            cmd.CommandType = CommandType.StoredProcedure;
//            cmd.Parameters.AddWithValue("@Accion", accion);
//            cmd.Parameters.AddWithValue("@IdEmpresa", empresa.IdEmpresa);
//            cmd.Parameters.AddWithValue("@NombreEmpresa", empresa.NombreEmpresa);
//            cmd.Parameters.AddWithValue("@DescripcionEmpresa", empresa.DescripcionEmpresa);
//            cmd.Parameters.AddWithValue("@Estado", empresa.Estado);

//            conn.Open();
//            cmd.ExecuteNonQuery();
//            return $"Exito: Empresa {accion.ToLower()}da correctamente.";
//        }
//        catch (Exception ex)
//        {
//            throw new Exception($"Error al gestionar empresa: {ex.Message}", ex);
//        }
//        finally
//        {
//            if (conn.State == ConnectionState.Open)
//            {
//                conn.Close();
//            }
//        }
//    }
//}