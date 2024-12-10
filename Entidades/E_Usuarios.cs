using System;

namespace Entidades
{
    public class E_Usuarios
    {
        #region Atributos
        private int _IdUsuario;
        private int _IdRol;
        private int _IdEmpresa;
        private string _NombreUsuario;
        private string _Correo;
        private string _Telefono;
        private DateTime _FechaRegistro;
        private bool _Estado;
        private string _NombreRol;      // Nueva propiedad para el nombre del Rol
        private string _NombreEmpresa;  // Nueva propiedad para el nombre de la Empresa
        #endregion

        #region Constructores
        public E_Usuarios()
        {
            _IdUsuario = 0;
            _IdRol = 0;
            _IdEmpresa = 0;
            _NombreUsuario = string.Empty;
            _Correo = string.Empty;
            _Telefono = string.Empty;
            _FechaRegistro = DateTime.Now;
            _Estado = true;
            _NombreRol = string.Empty;   // Inicialización
            _NombreEmpresa = string.Empty; // Inicialización
        }

        public E_Usuarios(int idUsuario, int idRol, int idEmpresa, string nombreUsuario, string correo, string telefono, DateTime fechaRegistro, bool estado, string nombreRol, string nombreEmpresa)
        {
            _IdUsuario = idUsuario;
            _IdRol = idRol;
            _IdEmpresa = idEmpresa;
            _NombreUsuario = nombreUsuario;
            _Correo = correo;
            _Telefono = telefono;
            _FechaRegistro = fechaRegistro;
            _Estado = estado;
            _NombreRol = nombreRol;   // Asignación en el constructor
            _NombreEmpresa = nombreEmpresa; // Asignación en el constructor
        }
        #endregion

        #region Encapsulamiento
        public int IdUsuario { get => _IdUsuario; set => _IdUsuario = value; }
        public int IdRol { get => _IdRol; set => _IdRol = value; }
        public int IdEmpresa { get => _IdEmpresa; set => _IdEmpresa = value; }
        public string NombreUsuario { get => _NombreUsuario; set => _NombreUsuario = value; }
        public string Correo { get => _Correo; set => _Correo = value; }
        public string Telefono { get => _Telefono; set => _Telefono = value; }
        public DateTime FechaRegistro { get => _FechaRegistro; set => _FechaRegistro = value; }
        public bool Estado { get => _Estado; set => _Estado = value; }
        public string NombreRol { get => _NombreRol; set => _NombreRol = value; }  // Encapsulamiento
        public string NombreEmpresa { get => _NombreEmpresa; set => _NombreEmpresa = value; } // Encapsulamiento
        #endregion
    }
}
