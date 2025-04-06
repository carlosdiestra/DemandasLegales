using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP_Application.Contracts
{
    public class ResolverJuicioResponse
    {
        public string Ganador { get; set; } = null!; // Demandante o Demandado
        public int PuntosDemandante { get; set; }
        public int PuntosDemandado { get; set; }
    }
}
