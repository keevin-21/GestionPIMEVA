using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Datos
{
    public class D_Buque : D_ConexionBD
    {
        public int IBM_Buques(string accion, E_Buque buque)
        {
            int resultados = 0;
            SqlCommand cmd = new SqlCommand("sp_GestionarBuque", conexion);
            try
            {
                using (conexion)
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Accion", accion);
                    cmd.Parameters.AddWithValue("@IdBuque", buque.IdBuque);
                    cmd.Parameters.AddWithValue("@NombreBuque", buque.NombreBuque);
                    cmd.Parameters.AddWithValue("@IdEmpresa", buque.IdEmpresa);
                    cmd.Parameters.AddWithValue("@IdEmpresa", buque.Estado);
                    AbrirConexion();
                    resultados = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                cmd.Dispose();
            }
            finally
            {
                CerrarConexion();
            }
            return resultados;
        }

        public List<E_Buque> ListarBuques()
        {
            List<E_Buque> LstBuques = new List<E_Buque>();
            SqlCommand cmd = new SqlCommand("sp_LstBuques", conexion);
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
                            E_Buque buque = new E_Buque
                            {
                                IdBuque = Convert.ToInt32(reader["IdBuque"]),
                                NombreBuque = reader["NombreBuque"].ToString(),
                                IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            };
                            LstBuques.Add(buque);
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
            return LstBuques;
        }
    }     
}
