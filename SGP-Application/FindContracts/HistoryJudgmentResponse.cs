using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP_Application.FindContracts
{
    public class HistoryJudgmentResponse
    {
        public string ParteDemandante { get; set; } = null!;
        public string ParteDemandado { get; set; } = null!;
        public string Ganador { get; set; } = null!;
        public DateTime Fecha { get; set; }
    }
}
