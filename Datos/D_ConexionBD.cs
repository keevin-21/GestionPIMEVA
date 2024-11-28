using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace Datos
{
    public class D_ConexionBD
    {
        public SqlConnection conexion;
        public D_ConexionBD()
        {
            conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
        }

        public void AbrirConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Broken || conexion.State == System.Data.ConnectionState.Closed)
            {
                conexion.Open();
            }
        }

        public void CerrarConexion()
        {
            try
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cerrar la conexión: " + ex.Message);
            }
        }

        public SqlConnection ObtenConexion()
        {
            return conexion;
        }
    }
}
