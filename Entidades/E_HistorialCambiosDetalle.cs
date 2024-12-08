using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_HistorialCambiosDetalle
    {
        #region Atributos
        private int _IdDetalleHistorial;
        private int _IdHistorial;
        private int _IdRegistro;
        private decimal _LOA;
        private DateTime _OperationTime;
        private DateTime _ETA;
        private DateTime _POB;
        private DateTime _ETB;
        private DateTime _ETC;
        private DateTime _ETD;
        private string _Cargo;
        private bool _EsHistorico;
        #endregion

        #region Constructores
        public E_HistorialCambiosDetalle()
        {
            _IdDetalleHistorial = 0;
            _IdHistorial = 0;
            _IdRegistro = 0;
            _LOA = 0;
            _OperationTime = DateTime.Now;
            _ETA = DateTime.Now;
            _POB = DateTime.Now;
            _ETB = DateTime.Now;
            _ETC = DateTime.Now;
            _ETD = DateTime.Now;
            _Cargo = string.Empty;
            _EsHistorico = false;
        }

        public E_HistorialCambiosDetalle(int idDetalleHistorial, int idHistorial, int idRegistro, decimal loa, DateTime operationTime, DateTime eta, DateTime pob, DateTime etb, DateTime etc, DateTime etd, string cargo, bool esHistorico)
        {
            _IdDetalleHistorial = idDetalleHistorial;
            _IdHistorial = idHistorial;
            _IdRegistro = idRegistro;
            _LOA = loa;
            _OperationTime = operationTime;
            _ETA = eta;
            _POB = pob;
            _ETB = etb;
            _ETC = etc;
            _ETD = etd;
            _Cargo = cargo;
            _EsHistorico = esHistorico;
        }


        #endregion

        #region Encapsulamiento
        public int IdDetalleHistorial { get => _IdDetalleHistorial; set => _IdDetalleHistorial = value; }
        public int IdHistorial { get => _IdHistorial; set => _IdHistorial = value; }
        public int IdRegistro { get => _IdRegistro; set => _IdRegistro = value; }
        public decimal LoA { get => _LOA; set => _LOA = value; }
        public DateTime OperationTime { get => _OperationTime; set => _OperationTime = value; }
        public DateTime ETA { get => ETA; set => ETA = value; }
        public DateTime POB { get => _POB; set => _POB = value; }
        public DateTime ETB { get => ETB; set => ETB = value; }
        public DateTime ETC { get => ETC; set => ETC = value; }
        public DateTime ETD { get => ETD; set => ETD = value; }
        public string Cargo { get => _Cargo; set => _Cargo = value; }
        public bool EsHistorico { get => _EsHistorico; set => _EsHistorico = value; }
        #endregion
    }
}
