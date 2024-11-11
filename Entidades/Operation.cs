using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    internal class Operation
    {
        public int Id { get; set; }
        public int VesselId { get; set; }
        public double LOA { get; set; }
        public TimeSpan OperationTime { get; set; }
        public DateTime ETA { get; set; }
        public DateTime POB { get; set; }
        public DateTime ETB { get; set; }
        public DateTime ETC { get; set; }
        public DateTime ETD { get; set; }
        public string Cargo { get; set; }
    }
}
