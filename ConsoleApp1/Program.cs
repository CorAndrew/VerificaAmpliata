using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccademiaBotanica
{
    internal class Program
    {
        public static List<Habitat> listaHabitat = new List<Habitat>();
        public static Habitat habitatCorrente = null;

        static void Main(string[] args)
        {
            bool esci = false;

            do
            {
                Console.WriteLine("MENU");
                if (habitatCorrente != null)
                    Console.WriteLine($"Habitat Attivo: {habitatCorrente.Nome}");
                else
                    Console.WriteLine("Nessun Habitat Selezionato");

                Console.WriteLine("1. Creare un nuovo habitat");
                Console.WriteLine("2. Selezionare un habitat esistente (in ordine alfabetico)");
                Console.WriteLine("3. Aggiungi pianta all'habitat attivo");
                Console.WriteLine("4. Rimuovi pianta dall'habitat attivo (per nome e specie)");
                Console.WriteLine("5. Visualizza le piante presenti (in ordine naturale)");
                Console.WriteLine("6. Cerca una pianta (per nome e specie)");
                Console.WriteLine("7. Modifica i valori dei sensori e il mese corrente");
                Console.WriteLine("8. Esegui il controllo generale");
                Console.Write("Scegli un'opzione: ");

                string scelta = Console.ReadLine();
                Console.WriteLine();

                switch (scelta)
                {
                    case "1":
                        CreaHabitat();
                        break;
                    case "2":
                        SelezionaHabitat();
                        break;
                    case "3":
                        AggiungiPianta();
                        break;
                    case "4":
                        RimuoviPianta();
                        break;
                    case "5":
                        VisualizzaPiante();
                        break;
                    case "6":
                        CercaPianta();
                        break;
                    case "7":
                        ModificaSensori();
                        break;
                    case "8":
                        ControlloGenerale();
                        break;
                    default:
                        Console.WriteLine("Opzione non valida. Riprova.");
                        break;
                }
            }
            while (!esci);
        }

        private static void CreaHabitat()
        {
            Console.Write("Nome habitat: ");
            string nome = Console.ReadLine();

            foreach (var h in listaHabitat)
            {
                if (string.Equals(h.Nome, nome, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Esiste già un habitat con questo nome.");
                    return;
                }
            }

            Console.Write("Temperatura iniziale (-5 a 35): ");
            if (!int.TryParse(Console.ReadLine(), out int t))
            {
                Console.WriteLine("Valore non valido per la temperatura");
                return;
            }

            Console.Write("Umidità iniziale (%): ");
            if (!int.TryParse(Console.ReadLine(), out int u))
            {
                Console.WriteLine("Valore non valido per l'umidità");
                return;
            }

            Console.Write("Luminosità iniziale (%): ");
            if (!int.TryParse(Console.ReadLine(), out int l))
            {
                Console.WriteLine("Valore non valido per la luminosità");
                return;
            }

            Console.Write("Mese corrente (1-12): ");
            if (!int.TryParse(Console.ReadLine(), out int m) || m < 1 || m > 12)
            {
                Console.WriteLine("Mese non valido");
                return;
            }

            Habitat nuovoH = new Habitat(nome, t, u, l, m);
            listaHabitat.Add(nuovoH);
            habitatCorrente = nuovoH;
            Console.WriteLine("Habitat creato e selezionato con successo");
        }

        private static void SelezionaHabitat()
        {
            if (listaHabitat.Count == 0)
            {
                Console.WriteLine("Nessun habitat disponibile");
                return;
            }

            listaHabitat.Sort();

            Console.WriteLine("ELENCO HABITAT (Ordinati)");
            for (int i = 0; i < listaHabitat.Count; i++)
            {
                Console.WriteLine($"[{i}] {listaHabitat[i].Descrizione()}");
            }

            Console.Write("Seleziona indice habitat: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 0 && idx < listaHabitat.Count)
            {
                habitatCorrente = listaHabitat[idx];
                Console.WriteLine($"Habitat {habitatCorrente.Nome} selezionato.");
            }
            else
            {
                Console.WriteLine("Indice non valido");
            }
        }

        private static void AggiungiPianta()
        {
            if (habitatCorrente == null)
            {
                Console.WriteLine("Seleziona prima un habitat");
                return;
            }

            Console.WriteLine("Scegli il tipo di pianta:");
            Console.WriteLine("0. Tropicale");
            Console.WriteLine("1. Temperata");
            Console.WriteLine("2. Mediterranea");

            if (!int.TryParse(Console.ReadLine(), out int tipoIdx) || tipoIdx < 0 || tipoIdx > 2)
            {
                Console.WriteLine("Tipo di pianta non valido");
                return;
            }

            Console.Write("Nome pianta: ");
            string nome = Console.ReadLine();

            Console.Write("Età (anni): ");
            if (!int.TryParse(Console.ReadLine(), out int eta))
            {
                Console.WriteLine("Età non valida");
                return;
            }

            Pianta p = null;
            Pianta piantaCreata = null;

            if (tipoIdx == 0)
            {
                Console.Write("Umidità minima richiesta (%): ");
                if (!int.TryParse(Console.ReadLine(), out int umMin))
                {
                    Console.WriteLine("Valore non valido");
                    return;
                }
                piantaCreata = new Tropicale(nome, eta, true, umMin);
            }
            else if (tipoIdx == 1)
            {
                Console.Write("Temperatura ideale (°C): ");
                if (!int.TryParse(Console.ReadLine(), out int tempId))
                {
                    Console.WriteLine("Valore non valido");
                    return;
                }
                Temperate tp = new Temperate(nome, eta, true, tempId);
                tp.AggiornaFoglie(habitatCorrente.Stato.Mese);
                piantaCreata = tp;
            }
            else if (tipoIdx == 2)
            {
                Console.Write("Luminosità minima richiesta (%): ");
                if (!int.TryParse(Console.ReadLine(), out int lumMin))
                {
                    Console.WriteLine("Valore non valido");
                    return;
                }
                Mediterranee mp = new Mediterranee(nome, eta, true, lumMin);
                mp.AggiornaFoglie(habitatCorrente.Stato.Mese);
                piantaCreata = mp;
            }

            bool aggiunto = habitatCorrente.AggiungiPianta(piantaCreata);
            if (aggiunto)
            {
                Console.WriteLine("Pianta aggiunta con successo");
            }
            else
            {
                Console.WriteLine("Errore impossibile aggiungere");
            }
        }

        private static void RimuoviPianta()
        {
            if (habitatCorrente == null || habitatCorrente.Piante.Count == 0)
            {
                Console.WriteLine("Nessuna pianta presente in questo habitat");
                return;
            }

            Console.Write("Inserisci il nome della pianta da rimuovere: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Scegli la specie (0: Tropicale, 1: Temperate, 2: Mediterranee): ");
            if (int.TryParse(Console.ReadLine(), out int specIdx) && specIdx >= 0 && specIdx <= 2)
            {
                Specie specie = (Specie)specIdx;
                bool rimosso = habitatCorrente.RimuoviPianta(nome, specie);
                if (rimosso)
                    Console.WriteLine("Pianta rimossa con successo");
                else
                    Console.WriteLine("Nessuna pianta trovata con questo nome e specie");
            }
            else
            {
                Console.WriteLine("Specie non valida");
            }
        }

        private static void VisualizzaPiante()
        {
            if (habitatCorrente == null || habitatCorrente.Piante.Count == 0)
            {
                Console.WriteLine("L'habitat corrente non contiene piante");
                return;
            }

            habitatCorrente.Piante.Sort();

            Console.WriteLine($"PIANTE NELL'HABITAT {habitatCorrente.Nome} (Ordine Naturale)");
            foreach (var p in habitatCorrente.Piante)
            {
                Console.WriteLine($"- {p.Descrizione()}");
            }
        }

        private static void CercaPianta()
        {
            if (habitatCorrente == null || habitatCorrente.Piante.Count == 0)
            {
                Console.WriteLine("Nessuna pianta presente nell'habitat");
                return;
            }

            Console.Write("Inserisci il nome della pianta da cercare: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Scegli la specie (0: Tropicale, 1: Temperate, 2: Mediterranee): ");
            if (int.TryParse(Console.ReadLine(), out int specIdx) && specIdx >= 0 && specIdx <= 2)
            {
                Specie specie = (Specie)specIdx;
                Pianta trovata = null;

                foreach (var p in habitatCorrente.Piante)
                {
                    if (string.Equals(p.Nome, nome, StringComparison.OrdinalIgnoreCase) && p.SpeciePianta == specie)
                    {
                        trovata = p;
                        break;
                    }
                }

                if (trovata != null)
                {
                    Console.WriteLine($"Pianta trovata {trovata.Descrizione()}");
                }
                else
                {
                    Console.WriteLine("Nessuna pianta trovata con questi parametri");
                }
            }
            else
            {
                Console.WriteLine("Specie non valida");
            }
        }

        private static void ModificaSensori()
        {
            if (habitatCorrente == null)
            {
                Console.WriteLine("Seleziona prima un habitat");
                return;
            }

            Console.WriteLine($"Valori attuali  {habitatCorrente.Stato.Descrizione()}");

            Console.Write("Nuova temperatura: ");
            if (!int.TryParse(Console.ReadLine(), out int t)) return;

            Console.Write("Nuova umidità: ");
            if (!int.TryParse(Console.ReadLine(), out int u)) return;

            Console.Write("Nuova luminosità: ");
            if (!int.TryParse(Console.ReadLine(), out int l)) return;

            Console.Write("Nuovo mese (1-12): ");
            if (!int.TryParse(Console.ReadLine(), out int m) || m < 1 || m > 12) return;

            StatoAmbientale nuovoStato = new StatoAmbientale(t, u, l, m);

            if (habitatCorrente.Stato.Equals(nuovoStato))
            {
                Console.WriteLine("Nessuna variazione delle condizioni ambientali");
                return;
            }

            habitatCorrente.Stato = nuovoStato;

            foreach (var p in habitatCorrente.Piante)
            {
                if (p is Temperate temp)
                    temp.AggiornaFoglie(habitatCorrente.Stato.Mese);
                else if (p is Mediterranee med)
                    med.AggiornaFoglie(habitatCorrente.Stato.Mese);
            }

            Console.WriteLine("Sensori e mese aggiornati con successo");
        }

        private static void ControlloGenerale()
        {
            if (habitatCorrente == null || habitatCorrente.Piante.Count == 0)
            {
                Console.WriteLine("Nessun habitat selezionato o nessuna pianta presente");
                return;
            }

            habitatCorrente.Piante.Sort();

            Console.WriteLine($"CONTROLLO GENERALE: {habitatCorrente.Nome}");
            Console.WriteLine($"Stato Ambientale: {habitatCorrente.Stato.Descrizione()}");

            int totali = habitatCorrente.Piante.Count;
            int adeguate = 0;
            int countTropicali = 0;
            int countTemperate = 0;
            int countMediterranee = 0;

            foreach (var p in habitatCorrente.Piante)
            {
                string esito = p.ValutaCondizioni(habitatCorrente.Stato);
                if (esito == "ADEGUATE") adeguate++;

                if (p is Tropicale) countTropicali++;
                else if (p is Temperate) countTemperate++;
                else if (p is Mediterranee) countMediterranee++;

                Console.WriteLine($"- Pianta: {p.Nome} | Tipo: {p.SpeciePianta} | Foglie: {(p.HaFoglie ? "Sì" : "No")} | Condizioni: {esito} | Spoglia in: {p.GetPeriodoSpoglia()}");
            }

            Console.WriteLine("ESTREMI DELL'ORDINE NATURALE");
            Console.WriteLine($"Prima pianta: {habitatCorrente.Piante[0].Nome} ({habitatCorrente.Piante[0].SpeciePianta})");
            Console.WriteLine($"Ultima pianta: {habitatCorrente.Piante[totali - 1].Nome} ({habitatCorrente.Piante[totali - 1].SpeciePianta})");

            Console.WriteLine(" RIEPILOGO STATISTICHE");
            Console.WriteLine($"Numero totale di piante: {totali}");
            Console.WriteLine($"Piante in condizioni adeguate: {adeguate}");
            Console.WriteLine($"Piante tropicali: {countTropicali}");
            Console.WriteLine($"Piante temperate: {countTemperate}");
            Console.WriteLine($"Piante mediterranee: {countMediterranee}");
        }
    }
}