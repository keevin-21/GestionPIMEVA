using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_TablaDeLogistica
    {
        #region Atributos
        private int _IdRegistro;
        private int _IdBuque;
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
        public E_TablaDeLogistica() 
        {
            _IdRegistro = 0;
            _IdBuque = 0;
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

        public E_TablaDeLogistica(int idRegistro, int idBuque, decimal loa, DateTime operationTime, DateTime eta, DateTime pob, DateTime etb, DateTime etc, DateTime etd, string cargo, bool esHistorico)
        {
            _IdRegistro = idRegistro;
            _IdBuque = idBuque;
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
        public int IdRegistro { get => _IdRegistro; set => _IdRegistro = value; }
        public int IdBuque { get => _IdBuque; set => _IdBuque = value; }
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
