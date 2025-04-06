using SGP_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP_Domain.Rules
{
    public class FirmaRules : IFirmaRules
    {
        public int CalcularPuntos(string parte)
        {
            var roles = parte.ToUpper().ToCharArray();

            if (roles.Contains('K'))
            {
                // Nota: Cuando un Rey firma, las firmas de los validadores en su parte no tienen valor
                return roles.Count(r => r == 'K') * (int)Rol.K + roles.Count(r => r == 'N') * (int)Rol.N;
            }

            return roles.Sum(c => (int)Enum.Parse<Rol>(c.ToString()));
        }
    }
}
