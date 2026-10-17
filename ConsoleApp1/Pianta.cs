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

    public abstract class Pianta
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
    }
}
