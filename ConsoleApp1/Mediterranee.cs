using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccademiaBotanica
{
    public class Mediterranee : Pianta
    {
        public int LuminositaMinima { get; set; }

        public Mediterranee(string nome, int eta, bool haFoglie, int luminositaMinima)
            : base(nome, Specie.Mediterranee, eta, haFoglie)
        {
            LuminositaMinima = luminositaMinima;
        }

        public override string Descrizione()
        {
            return base.Descrizione() + $" | Periodo spoglia: {GetPeriodoSpoglia()}";
        }

        public override string ValutaCondizioni(StatoAmbientale stato)
        {
            // Hanno bisogno di una luminosità minima
            if (stato.Luminosita >= LuminositaMinima)
                return "ADEGUATE";
            else
                return "NON ADEGUATE";
        }

        public override string GetPeriodoSpoglia()
        {
            // Luglio e agosto
            return "Luglio e agosto (siccità estiva)";
        }

        public void AggiornaFoglie(int mese)
        {
            // Le mediterranee perdono le foglie a luglio (7) e agosto (8)
            if (mese == 7 || mese == 8)
                HaFoglie = false;
            else
                HaFoglie = true;
        }
    }
}