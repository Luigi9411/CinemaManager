using CinemaManager.Utility;

namespace CinemaManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DB db = new DB();

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("       MENU FILM");
                Console.WriteLine("");
                Console.WriteLine("1. Carica tabella cinema nel database");
                Console.WriteLine("2. Visualizza i dati nella tabella cinema");
                Console.WriteLine("3. Visualizza una sintesi sulla struttura della tabella");
                Console.WriteLine("4. Uscita Applicazione");
                Console.WriteLine("");
                Console.Write("Seleziona un'opzione: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Hai selezionato l'Opzione 1.");
                        Console.WriteLine("");
                        Console.WriteLine("Tabella caricata precedentemente");
                        if (!db.CheckIfTableExists("Cinema"))
                        {
                            db.CreateCinemaTable();
                            Console.WriteLine("Tabella caricata correttamente");
                        }
                        Console.WriteLine();
                        Console.Write("Premi un tasto per continuare");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("Hai selezionato l'Opzione 2.");
                        Console.WriteLine("");
                        db.GetAdapterCinema();
                        Console.WriteLine();
                        Console.Write("Premi un tasto per continuare");
                        Console.ReadKey();
                        Console.Clear();

                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("Hai selezionato l'Opzione 3.");
                        Console.WriteLine("");
                        db.GetAdapterCinema2();
                        Console.WriteLine();
                        Console.Write("Premi un tasto per continuare");
                        Console.ReadKey();
                        Console.Clear();

                        break;

                    case "4":
                        Console.Clear();
                        return;

                    default:
                        Console.WriteLine("Opzione non valida. Seleziona un'opzione valida.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}




  