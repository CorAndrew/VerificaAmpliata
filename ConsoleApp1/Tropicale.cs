using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccademiaBotanica
{
    public class Tropicale : Pianta
    {
        public int UmiditaMinima { get; set; }

        public Tropicale(string nome, int eta, bool haFoglie, int umiditaMinima)
            : base(nome, Specie.Tropicale, eta, haFoglie)
        {
            UmiditaMinima = umiditaMinima;
            HaFoglie = true; // Le tropicali non perdono le foglie
        }

        public override string ValutaCondizioni(StatoAmbientale stato)
        {
            // Le piante tropicali hanno bisogno di un'umidità minima
            if (stato.Umidita >= UmiditaMinima)
                return "ADEGUATE";
            else
                return "NON ADEGUATE";
        }

        public override string GetPeriodoSpoglia()
        {
            return "Non perde le foglie";
        }
    }
}