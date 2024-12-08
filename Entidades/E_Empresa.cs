using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_Empresa
    {
        #region Atributos
        private int _IdEmpresa;
        private string _NombreEmpresa;
        private string _DescripcionEmpresa;
        private bool _Estado;
        #endregion

        #region Constructores
        public E_Empresa()
        {
            _IdEmpresa = 0;
            _NombreEmpresa = string.Empty;
            _DescripcionEmpresa = string.Empty;
            _Estado = true;
        }

        public E_Empresa(int idEmpresa, string nombreEmpresa, string descripcionEmpresa, bool estado)
        {
            _IdEmpresa= idEmpresa;
            _NombreEmpresa= nombreEmpresa;
            _DescripcionEmpresa= descripcionEmpresa;
            _Estado = estado;
        }
        #endregion

        #region Encapsulamiento
        public int IdEmpresa { get => _IdEmpresa; set => _IdEmpresa = value; }
        public string NombreEmpresa { get => _NombreEmpresa; set => _NombreEmpresa = value; }
        public string DescripcionEmpresa { get => _DescripcionEmpresa; set => _DescripcionEmpresa = value; }
        public bool Estado { get => _Estado; set => _Estado = value; }
        #endregion
    }
}
