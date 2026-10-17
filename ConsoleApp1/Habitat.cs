using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccademiaBotanica
{
    public class Habitat
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

        public void AggiungiPianta(Pianta p)
        {
            Piante.Add(p);
        }

        public void RimuoviPianta(Pianta p)
        {
            Piante.Remove(p);
        }
    }
}