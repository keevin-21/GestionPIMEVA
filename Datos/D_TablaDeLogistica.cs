using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        b.NombreBuque AS Buque,
                        l.LOA,
                        FORMAT(l.OperationTime, 'HH:mm') AS [OperationTime],
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

    }
}
