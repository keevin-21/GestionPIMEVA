using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_Roles
    {
        #region Atributos
        private int _IdRol;
        private string _NombreRol;
        private string _DescripcionRol;
        private bool _Estado;
        #endregion

        #region Constructores
        public E_Roles()
        {
            _IdRol = 0;
            _NombreRol = string.Empty;
            _DescripcionRol = string.Empty;
            _Estado = true;
        }
        #endregion

        #region Encapsulamiento
        public int IdRol { get => _IdRol; set => _IdRol = value; }
        public string NombreRol { get => _NombreRol; set => _NombreRol = value;}
        public string DescripcionRol { get => _DescripcionRol; set => _DescripcionRol = value;}
        public bool Estado { get => _Estado; set => _Estado = value; }
        #endregion
    }
}
