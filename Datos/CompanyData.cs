using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class CompanyData
    {
        private readonly string connectionString = "Server=192.168.0.100;Database=PIMEVA;Trusted_Connection=True;";

        public List<Company>GetCompanies()
        {
            var companies = new List<Company>();

            using(var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Name FROM Companies";
                var command = new SqlCommand(query, connection);
                connection.Open();

                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        companies.Add(new Company
                        {
                            Id = reader["Id"].ToString(),
                            Name = (string)reader["Name"]
                        });
                    }
                }
            }

            return companies;
        }

        // TODO: Implementar el resto de los métodos CRUD (Create, Update, Delete)
    }
}
