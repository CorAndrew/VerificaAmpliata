using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccademiaBotanica
{
    public enum Specie
    {
        Tropicale,
        Temperate,
        Mediterranee
    }

    public abstract class Pianta : IEquatable<Pianta>, IComparable<Pianta>
    {
        public string Nome { get; set; }
        public Specie SpeciePianta { get; set; }
        public int Eta { get; set; }
        public bool HaFoglie { get; set; }

        public Pianta(string nome, Specie specie, int eta, bool haFoglie)
        {
            Nome = nome;
            SpeciePianta = specie;
            Eta = eta;
            HaFoglie = haFoglie;
        }

        public virtual string Descrizione()
        {
            string foglieStr = HaFoglie ? "Sì" : "No";
            return $"Nome: {Nome} | Specie: {SpeciePianta} | Età: {Eta} anni | Ha le foglie: {foglieStr}";
        }

        public abstract string ValutaCondizioni(StatoAmbientale stato);
        public abstract string GetPeriodoSpoglia();

        // Due piante sono uguali se hanno stesso nome e stessa specie (case-insensitive)
        public bool Equals(Pianta other)
        {
            if (other == null) return false;
            return string.Equals(Nome, other.Nome, StringComparison.OrdinalIgnoreCase) &&
                   SpeciePianta == other.SpeciePianta;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Pianta);
        }

        // Ordine naturale: prima per specie, poi per nome, poi per età crescente
        public int CompareTo(Pianta other)
        {
            if (other == null) return 1;

            int compareSpecie = SpeciePianta.CompareTo(other.SpeciePianta);
            if (compareSpecie != 0) return compareSpecie;

            int compareNome = string.Compare(Nome, other.Nome, StringComparison.OrdinalIgnoreCase);
            if (compareNome != 0) return compareNome;

            return Eta.CompareTo(other.Eta);
        }
    }
}