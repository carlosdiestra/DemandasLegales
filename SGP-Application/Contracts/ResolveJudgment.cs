using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP_Application.Contracts
{
    public class ResolveJudgment
    {
        public string Ganador { get; set; } = null!;
        public int PuntosDemandante { get; set; }
        public int PuntosDemandado { get; set; }
    }
}
