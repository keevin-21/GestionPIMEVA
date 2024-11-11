using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocios
{
    internal class CompanyLogic
    {
        private readonly CompanyData companyData = new CompanyData();

        public List<Company> GetCompanies()
        {
            return companyData.GetCompanies();
        }

    }
}
