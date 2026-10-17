using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccademiaBotanica
{
    public class Temperate : Pianta
    {
        public int TemperaturaIdeale { get; set; }

        public Temperate(string nome, int eta, bool haFoglie, int temperaturaIdeale)
            : base(nome, Specie.Temperate, eta, haFoglie)
        {
            TemperaturaIdeale = temperaturaIdeale;
        }

        public override string Descrizione()
        {
            return base.Descrizione() + $" | Periodo spoglia: {GetPeriodoSpoglia()}";
        }

        public override string ValutaCondizioni(StatoAmbientale stato)
        {
            // Tolleranza di ±3 °C rispetto alla temperatura ideale
            if (stato.Temperatura >= (TemperaturaIdeale - 3) && stato.Temperatura <= (TemperaturaIdeale + 3))
                return "ADEGUATE";
            else
                return "NON ADEGUATE";
        }

        public override string GetPeriodoSpoglia()
        {
            // Da novembre (11) a febbraio (2)
            return "Da novembre a febbraio";
        }

        public void AggiornaFoglie(int mese)
        {
            // Le temperate perdono le foglie da novembre a febbraio
            if (mese >= 11 || mese <= 2)
                HaFoglie = false;
            else
                HaFoglie = true;
        }
    }
}
