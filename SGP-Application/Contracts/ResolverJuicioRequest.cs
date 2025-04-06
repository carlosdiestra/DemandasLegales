using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP_Application.Contracts
{
    public class ResolverJuicioRequest : IRequest<ResolverJuicioResponse>
    {
        public string ParteDemandante { get; set; } = null!; // Ej: "KN"
        public string ParteDemandado { get; set; } = null!;  // Ej: "NNV"
    }
}
