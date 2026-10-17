using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccademiaBotanica
{
    public class Habitat : IEquatable<Habitat>, IComparable<Habitat>
    {
        public string Nome { get; set; }
        public StatoAmbientale Stato { get; set; }
        public List<Pianta> Piante { get; set; }

        public Habitat(string nome, int t, int u, int l, int m)
        {
            Nome = nome;
            Stato = new StatoAmbientale(t, u, l, m);
            Piante = new List<Pianta>();
        }

        public string Descrizione()
        {
            return $"Habitat: {Nome} | {Stato.Descrizione()} | Numero piante: {Piante.Count}";
        }

        public bool AggiungiPianta(Pianta p)
        {
            // Non si può aggiungere una pianta uguale a una già presente
            foreach (var pianta in Piante)
            {
                if (pianta.Equals(p))
                {
                    return false; // Già presente
                }
            }
            Piante.Add(p);
            return true;
        }

        public bool RimuoviPianta(string nome, Specie specie)
        {
            Pianta daRimuovere = null;
            foreach (var p in Piante)
            {
                if (string.Equals(p.Nome, nome, StringComparison.OrdinalIgnoreCase) && p.SpeciePianta == specie)
                {
                    daRimuovere = p;
                    break;
                }
            }

            if (daRimuovere != null)
            {
                Piante.Remove(daRimuovere);
                return true;
            }
            return false;
        }

        public bool Equals(Habitat other)
        {
            if (other == null) return false;
            return string.Equals(Nome, other.Nome, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Habitat);
        }

        public int CompareTo(Habitat other)
        {
            if (other == null) return 1;
            return string.Compare(Nome, other.Nome, StringComparison.OrdinalIgnoreCase);
        }
    }
}