using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public E_Usuarios(int idUsuario, int idRol, int idEmpresa, string nombreUsuario, string correo, string telefono, bool estado)
        {
            _IdUsuario = idUsuario;
            _IdRol = idRol;
            _IdEmpresa = idEmpresa;
            _NombreUsuario= nombreUsuario;
            _Correo = correo;
            _Telefono = telefono;
            _FechaRegistro = DateTime.Now;
            _Estado = estado;
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
        public bool Estado { get => Estado; set => Estado = value; }
        #endregion
    }
}
