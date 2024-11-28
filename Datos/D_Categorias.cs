using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace Datos
{
    public class D_Categorias : D_ConexionBD
    {
        public int IBM_Categorias(string accion, E_Categoria categoria)
        {
            int resultados = 0;
            SqlCommand cmd = new SqlCommand("IBM_Categoria", conexion);
            try
            {
                using (conexion)
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Accion", accion);
                    cmd.Parameters.AddWithValue("@IdCategoria", categoria.IdCategoria);
                    cmd.Parameters.AddWithValue("@NombreCategoria", categoria.NombreCategoria);
                    cmd.Parameters.AddWithValue("@DescripcionCategoria", categoria.DescripcionCategoria);
                    cmd.Parameters.AddWithValue("@Estado", categoria.Estado);
                    AbrirConexion();
                    resultados = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al tratar de insertar, Borrar o modificar Datos " + e.Message);
            }
            finally
            {
                CerrarConexion();
                cmd.Dispose();
            }

            return resultados;
        }

        public bool ModificarCategoria(E_Categoria categoria)
        {
            try
            {
                using (SqlConnection con = ObtenConexion())
                {
                    SqlCommand cmd = new SqlCommand("IMB_Categoria", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", "MODIFICAR");
                    cmd.Parameters.AddWithValue("@IdCategoria", categoria.IdCategoria);
                    cmd.Parameters.AddWithValue("@NombreCategoria", categoria.NombreCategoria);
                    cmd.Parameters.AddWithValue("@DescripcionCategoria", categoria.DescripcionCategoria);
                    cmd.Parameters.AddWithValue("@Estado", categoria.Estado);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                CerrarConexion();
            }
        }

        public List<E_Categoria> ListarCategorias()
        {
            List<E_Categoria> LstCategorias = new List<E_Categoria>();
            SqlCommand cmd = new SqlCommand("LstCategorias", conexion);
            try
            {
                using (conexion)
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            E_Categoria categoria = new E_Categoria
                            {
                                IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                                NombreCategoria = reader["NombreCategoria"].ToString(),
                                DescripcionCategoria = reader["DescripcionCategoria"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            };
                            LstCategorias.Add(categoria);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CerrarConexion();
                cmd.Dispose();
            }
            return LstCategorias;
        }

        public E_Categoria BuscarCategoriaPorID(int idCategoria)
        {
            E_Categoria categoria = null;
            try
            {
                using (conexion)
                {
                    //todo: Crear Storeprocedure
                    SqlCommand cmd = new SqlCommand("BuscarCategoriaPorId", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            categoria = new E_Categoria
                            {
                                IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                                NombreCategoria = reader["NombreCategoria"].ToString(),
                                DescripcionCategoria = reader["DescripcionCategoria"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
            return categoria;
        }

        public List<E_Categoria> BuscarCategoriaPorCriterio(string criterio)
        {
            List<E_Categoria> LstCategorias = new List<E_Categoria>();
            try
            {
                using (SqlConnection con = ObtenConexion())
                {
                    SqlCommand cmd = new SqlCommand("BuscarCategoriaPorCriterio", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Criterio", criterio);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            E_Categoria categoria = new E_Categoria
                            {
                                IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                                NombreCategoria = reader["NombreCategoria"].ToString(),
                                DescripcionCategoria = reader["DescripcionCategoria"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            };
                            LstCategorias.Add(categoria);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
            return LstCategorias;
        }
    }
}
