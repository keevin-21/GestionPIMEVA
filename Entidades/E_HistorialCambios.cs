using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_HistorialCambios
    {
        #region Atributos
        private int _IdHistorial;
        private string _TipoCambio;
        private DateTime _FechaModificacion;
        private int _IdUsuario;
        #endregion

        #region Constructores
        public E_HistorialCambios()
        {
            _IdHistorial = 0;
            _TipoCambio = string.Empty;
            _FechaModificacion = DateTime.MinValue;
            _IdUsuario = 0;
        }

        public E_HistorialCambios(int idHistorial, string tipoCambio, DateTime fechaModificacion, int idUsuario)
        {
            _IdHistorial = idHistorial;
            _TipoCambio = tipoCambio;
            _FechaModificacion = fechaModificacion;
            _IdUsuario = idUsuario;
        }
        #endregion

        #region Encapsulamiento
        public int IdHistorial { get => _IdHistorial; set => _IdHistorial = value; }
        public string TipoCambio { get => _TipoCambio; set => _TipoCambio = value; }
        public DateTime FechaModificacion {  get => _FechaModificacion; set =>  _FechaModificacion = value; }
        public int IdUsuario { get => _IdUsuario; set => _IdUsuario = value; }
        #endregion
    }
}
