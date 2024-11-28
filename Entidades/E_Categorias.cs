using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_Categoria
    {
        #region Atributos
        private int _IdCategoria;
        private string _NombreCategoria;
        private string _DescripcionCategoria;
        private bool _Estado;
        #endregion

        #region Constructores

        public E_Categoria()
        {
            _IdCategoria = 0;
            _NombreCategoria = string.Empty;
            _DescripcionCategoria = string.Empty;
            _Estado = true;
        }
        public E_Categoria(int idCategoria, string nombreCategoria, string descripcionCategoria, bool estado)
        {
            _IdCategoria = idCategoria;
            _NombreCategoria = nombreCategoria;
            _DescripcionCategoria = descripcionCategoria;
            _Estado = estado;
        }

        #endregion

        #region Encapsulamiento
        public int IdCategoria { get => _IdCategoria; set => _IdCategoria = value; }
        public string NombreCategoria { get => _NombreCategoria; set => _NombreCategoria = value; }
        public string DescripcionCategoria { get => _DescripcionCategoria; set => _DescripcionCategoria = value; }
        public bool Estado { get => _Estado; set => _Estado = value; }
        #endregion
    }
}
