using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_CredencialesUsuario
    {
        #region Atributos
        private int _IdUsuario;
        private string _PasswordHash;
        private string _PasswordSalt;
        #endregion

        #region Constructores
        public E_CredencialesUsuario()
        {
            _IdUsuario= 0;
            _PasswordHash = string.Empty;
            _PasswordSalt = string.Empty;
        }

        public E_CredencialesUsuario(int idUsuario, string passwordHash, string passwordSalt)
        {
            _IdUsuario = idUsuario;
            _PasswordHash = passwordHash;
            _PasswordSalt = passwordSalt;
        }
        #endregion

        #region Encapsulamiento
        public int IdUsuario { get => _IdUsuario; set => _IdUsuario = value; }
        public string PasswordHash { get => _PasswordHash; set => _PasswordHash = value; }
        public string PasswordSalt { get => _PasswordSalt; set => _PasswordSalt = value; }
        #endregion
    }
}
