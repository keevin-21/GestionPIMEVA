using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_Buque
    {
        #region Atributos
        private int idBuque;
        private string nombreBuque;
        private int idEmpresa;
        private bool estado;
        #endregion

        #region Constructores
        public E_Buque()
        {
            idBuque = 0;
            nombreBuque = string.Empty;
            idEmpresa = 0;
            estado = true;
        }

        public E_Buque(int idBuque, string nombreBuque, int idEmpresa, bool estado)
        {
            this.idBuque = idBuque;
            this.nombreBuque = nombreBuque;
            this.idEmpresa = idEmpresa;
            this.estado = estado;
        }
        #endregion

        #region Encapsulamiento
        public int IdBuque { get; set; }
        public string NombreBuque { get => nombreBuque; set => nombreBuque = value; }
        public int IdEmpresa { get; set; }
        public bool Estado { get => estado; set => estado = value; }
        #endregion
    }
}