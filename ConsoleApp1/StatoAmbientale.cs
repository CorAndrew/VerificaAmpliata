using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccademiaBotanica
{
    public class StatoAmbientale : IEquatable<StatoAmbientale>
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

        public bool Equals(StatoAmbientale other)
        {
            if (other == null) return false;
            return Temperatura == other.Temperatura &&
                   Umidita == other.Umidita &&
                   Luminosita == other.Luminosita &&
                   Mese == other.Mese;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as StatoAmbientale);
        }
    }
}
