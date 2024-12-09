using System;

namespace Entidades
{
    public class E_Buque
    {
        #region Atributos
        private int _IdBuque;
        private string _NombreBuque;
        private int _IdEmpresa;
        private string _NombreEmpresa;  // Nueva propiedad
        private bool _Estado;
        #endregion

        #region Constructores
        public E_Buque()
        {
            _IdBuque = 0;
            _NombreBuque = string.Empty;
            _IdEmpresa = 0;
            _NombreEmpresa = string.Empty;  // Inicialización
            _Estado = true;
        }

        public E_Buque(int idBuque, string nombreBuque, int idEmpresa, string nombreEmpresa, bool estado)
        {
            _IdBuque = idBuque;
            _NombreBuque = nombreBuque;
            _IdEmpresa = idEmpresa;
            _NombreEmpresa = nombreEmpresa;  // Asignación en el constructor
            _Estado = estado;
        }
        #endregion

        #region Encapsulamiento
        public int IdBuque { get => _IdBuque; set => _IdBuque = value; }
        public string NombreBuque { get => _NombreBuque; set => _NombreBuque = value; }
        public int IdEmpresa { get => _IdEmpresa; set => _IdEmpresa = value; }
        public string NombreEmpresa { get => _NombreEmpresa; set => _NombreEmpresa = value; }  // Encapsulamiento
        public bool Estado { get => _Estado; set => _Estado = value; }
        #endregion
    }

}
