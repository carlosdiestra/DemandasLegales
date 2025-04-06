using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP_Domain.Entities
{
    public class Contrato
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public ICollection<Firma> Firmas { get; set; } = new List<Firma>();
    }
}
