using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccademiaBotanica
{
    public class StatoAmbientale
    {
        public int Temperatura { get; set; }
        public int Umidita { get; set; }
        public int Luminosita { get; set; }
        public int Mese { get; set; }

        public StatoAmbientale(int temperatura, int umidita, int luminosita, int mese)
        {
            Temperatura = temperatura;
            Umidita = umidita;
            Luminosita = luminosita;
            Mese = mese;
        }

        public string Descrizione()
        {
            return $"Temperatura: {Temperatura}°C | Umidità: {Umidita}% | Luminosità: {Luminosita}% | Mese: {Mese}";
        }
    }
}
