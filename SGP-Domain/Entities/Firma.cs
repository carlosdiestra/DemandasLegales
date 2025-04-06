using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP_Domain.Entities
{
    public class Firma
    {
        public Guid Id { get; set; }
        public string Rol { get; set; } = null!; // K, N, V
        public Guid ContratoId { get; set; }
        public string Parte { get; set; } = null!; // Demandante o Demandado
    }
}
