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
                Console.WriteLine("MENU ACCADEMIA BOTANICA");
                if (habitatCorrente != null)
                    Console.WriteLine($"[Habitat Attivo: {habitatCorrente.Nome}]");
                else
                    Console.WriteLine("[Nessun Habitat Selezionato]");

                Console.WriteLine("1. Creare un nuovo habitat");
                Console.WriteLine("2. Selezionare un habitat esistente");
                Console.WriteLine("3. Aggiungi pianta all'habitat attivo");
                Console.WriteLine("4. Rimuovi pianta dall'habitat attivo");
                Console.WriteLine("5. Visualizza le piante presenti nell'habitat");
                Console.WriteLine("6. Modifica i valori dei sensori e il mese corrente");
                Console.WriteLine("7. Esegui il controllo generale");
                Console.WriteLine("8. Termina programma");
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
                        ModificaSensori();
                        break;
                    case "7":
                        ControlloGenerale();
                        break;
                    case "8":
                        esci = true;
                        Console.WriteLine("Programma terminato.");
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
            Console.Write("Temperatura iniziale (-5 a 35): ");
            int t = int.Parse(Console.ReadLine());
            Console.Write("Umidità iniziale (%): ");
            int u = int.Parse(Console.ReadLine());
            Console.Write("Luminosità iniziale (%): ");
            int l = int.Parse(Console.ReadLine());
            Console.Write("Mese corrente (1-12): ");
            int m = int.Parse(Console.ReadLine());

            Habitat h = new Habitat(nome, t, u, l, m);
            listaHabitat.Add(h);
            habitatCorrente = h;
            Console.WriteLine("Habitat creato e selezionato con successo!");
        }

        private static void SelezionaHabitat()
        {
            if (listaHabitat.Count == 0)
            {
                Console.WriteLine("Nessun habitat disponibile. Creane uno prima.");
                return;
            }

            Console.WriteLine("--- ELENCO HABITAT ---");
            for (int i = 0; i < listaHabitat.Count; i++)
            {
                Console.WriteLine($"[{i}] {listaHabitat[i].Descrizione()}");
            }

            Console.Write("Seleziona indice habitat: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 0 && idx < listaHabitat.Count)
            {
                habitatCorrente = listaHabitat[idx];
                Console.WriteLine($"Habitat '{habitatCorrente.Nome}' selezionato.");
            }
            else
            {
                Console.WriteLine("Indice non valido.");
            }
        }

        private static void AggiungiPianta()
        {
            if (habitatCorrente == null)
            {
                Console.WriteLine("Prima devi selezionare o creare un habitat!");
                return;
            }

            Console.WriteLine("Scegli il tipo di pianta:");
            Console.WriteLine("1. Tropicale");
            Console.WriteLine("2. Temperata");
            Console.WriteLine("3. Mediterranea");
            string tipo = Console.ReadLine();

            Console.Write("Nome pianta: ");
            string nome = Console.ReadLine();
            Console.Write("Età (anni): ");
            int eta = int.Parse(Console.ReadLine());

            if (tipo == "1")
            {
                Console.Write("Umidità minima richiesta (%): ");
                int umMin = int.Parse(Console.ReadLine());
                Tropicale p = new Tropicale(nome, eta, true, umMin);
                habitatCorrente.AggiungiPianta(p);
                Console.WriteLine("Pianta tropicale aggiunta!");
            }
            else if (tipo == "2")
            {
                Console.Write("Temperatura ideale (°C): ");
                int tempId = int.Parse(Console.ReadLine());
                Temperate p = new Temperate(nome, eta, true, tempId);
                p.AggiornaFoglie(habitatCorrente.Stato.Mese); // Aggiorna stato foglie in base al mese
                habitatCorrente.AggiungiPianta(p);
                Console.WriteLine("Pianta temperata aggiunta!");
            }
            else if (tipo == "3")
            {
                Console.Write("Luminosità minima richiesta (%): ");
                int lumMin = int.Parse(Console.ReadLine());
                Mediterranee p = new Mediterranee(nome, eta, true, lumMin);
                p.AggiornaFoglie(habitatCorrente.Stato.Mese); // Aggiorna stato foglie in base al mese
                habitatCorrente.AggiungiPianta(p);
                Console.WriteLine("Pianta mediterranea aggiunta!");
            }
            else
            {
                Console.WriteLine("Tipo non valido.");
            }
        }

        private static void RimuoviPianta()
        {
            if (habitatCorrente == null || habitatCorrente.Piante.Count == 0)
            {
                Console.WriteLine("Nessuna pianta presente in questo habitat.");
                return;
            }

            VisualizzaPiante();
            Console.Write("Inserisci l'indice della pianta da rimuovere: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 0 && idx < habitatCorrente.Piante.Count)
            {
                Pianta p = habitatCorrente.Piante[idx];
                habitatCorrente.RimuoviPianta(p);
                Console.WriteLine($"Pianta '{p.Nome}' rimossa con successo.");
            }
            else
            {
                Console.WriteLine("Indice non valido.");
            }
        }

        private static void VisualizzaPiante()
        {
            if (habitatCorrente == null || habitatCorrente.Piante.Count == 0)
            {
                Console.WriteLine("L'habitat corrente non contiene piante.");
                return;
            }

            Console.WriteLine($"--- PIANTE NELL'HABITAT '{habitatCorrente.Nome}' ---");
            for (int i = 0; i < habitatCorrente.Piante.Count; i++)
            {
                Console.WriteLine($"[{i}] {habitatCorrente.Piante[i].Descrizione()}");
            }
        }

        private static void ModificaSensori()
        {
            if (habitatCorrente == null)
            {
                Console.WriteLine("Seleziona prima un habitat!");
                return;
            }

            Console.WriteLine($"Valori attuali -> {habitatCorrente.Stato.Descrizione()}");
            Console.Write("Nuova temperatura: ");
            habitatCorrente.Stato.Temperatura = int.Parse(Console.ReadLine());
            Console.Write("Nuova umidità: ");
            habitatCorrente.Stato.Umidita = int.Parse(Console.ReadLine());
            Console.Write("Nuova luminosità: ");
            habitatCorrente.Stato.Luminosita = int.Parse(Console.ReadLine());
            Console.Write("Nuovo mese (1-12): ");
            habitatCorrente.Stato.Mese = int.Parse(Console.ReadLine());

            // Aggiorna lo stato delle foglie per tutte le piante temperate e mediterranee presenti nell'habitat
            foreach (var p in habitatCorrente.Piante)
            {
                if (p is Temperate temp)
                    temp.AggiornaFoglie(habitatCorrente.Stato.Mese);
                else if (p is Mediterranee med)
                    med.AggiornaFoglie(habitatCorrente.Stato.Mese);
            }

            Console.WriteLine("Sensori e mese aggiornati con successo per l'habitat (validi per tutti i controlli successivi).");
        }

        private static void ControlloGenerale()
        {
            if (habitatCorrente == null || habitatCorrente.Piante.Count == 0)
            {
                Console.WriteLine("Nessun habitat selezionato o nessuna pianta presente.");
                return;
            }

            Console.WriteLine($"\n=== CONTROLLO GENERALE: {habitatCorrente.Nome} ===");
            Console.WriteLine($"Stato Ambientale: {habitatCorrente.Stato.Descrizione()}\n");

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

            Console.WriteLine("\n--- RIEPILOGO STATISTICHE ---");
            Console.WriteLine($"Numero totale di piante: {totali}");
            Console.WriteLine($"Piante in condizioni adeguate: {adeguate}");
            Console.WriteLine($"Piante tropicali: {countTropicali}");
            Console.WriteLine($"Piante temperate: {countTemperate}");
            Console.WriteLine($"Piante mediterranee: {countMediterranee}");
        }
    }
}
