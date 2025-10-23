using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjuvochpolisfinal
{
    internal class Display
    {
        internal static void DisplayResult(List<Person> people, List<string> newsFeed)
        {

            Console.Clear();
            Console.WriteLine("======== SLUTRESULTAT =======");

            int policeCount = people.Count(person => person is Police);
            int thiefCount = people.Count(person => person is Thief && person.Symbol == 'T');
            int prisonerCount = people.Count(person => person is Thief && person.Symbol == 'F');
            int citizenCount = people.Count(person => person is Citizen);

            int robbedCitizens = people.OfType<Citizen>().Count(citizen => citizen.Inventory.Items.Count < 4);

            Console.Write($"Poliser: {policeCount}");
            Console.Write($"Tjuvar: {thiefCount}");
            Console.Write($"Fångar: {prisonerCount}");
            Console.Write($"Medborgare: {citizenCount}");
            Console.Write($"Antal rånade Medborgare: {robbedCitizens}");

            Console.WriteLine("Personer, Status och Inventory");

            int personIndex = 0;
            foreach (var person in people)
            {
                string type = person switch
                {
                    Police => "Polis",
                    Thief => person.Symbol == 'F' ? "Fånge" : "Tjuv",
                    Citizen => "Medborgare",

                };

                Console.WriteLine($"Person {personIndex++}: {type} - {person.Name}");
                Console.WriteLine($"Position: ({person.Position.X}, {person.Position.Y})");
                Console.WriteLine($"Inventory: {string.Join(", ", person.Inventory.Items)}");

            }
            Console.WriteLine("==== Alla Händelser ====");
            if (newsFeed.Count == 0)
            {
                Console.WriteLine("Inga händelser");
            }
            else
            {
                int i = 1;
                foreach (var news in newsFeed)
                {
                    Console.WriteLine($"{i++}. {news}");
                }
            }

            Console.WriteLine("\nTryck på valfri tangent för att avsluta...");
            Console.ReadKey(true);
        }

        /* internal static void DrawPerson(Person person)
         {
             int x = Math.Max(0, Math.Min(Console.WindowWidth - 1, person.Position.X));
             int y = Math.Max(0, Math.Min(Console.WindowHeight - 1, person.Position.Y));
             Console.SetCursorPosition(x, y);

             // choose color
             if (person is Police)
                 Console.ForegroundColor = ConsoleColor.Blue;
             else if (person is Thief && person.Symbol == 'T')
                 Console.ForegroundColor = ConsoleColor.Red;
             else if (person is Thief && person.Symbol == 'F')
                 Console.ForegroundColor = ConsoleColor.DarkRed;
             else if (person is Citizen)
                 Console.ForegroundColor = ConsoleColor.Green;
             else
                 Console.ForegroundColor = ConsoleColor.White;

             Console.Write(person.Symbol);
             Console.ResetColor();
         }
        */


        internal static void ClearCityArea(int width, int height)
        {
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                }
            }
        }

        internal static void RedrawPeople(List<Person> people)
        {
            foreach (var p in people)
            {
                DrawPerson(p);
            }
        }

        internal static void DrawPerson(Person person)
        {
            Console.SetCursorPosition(person.Position.X, person.Position.Y);

            // Färger på people
            switch (person)
            {
                case Police:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case Thief t when t.Symbol == 'F':
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case Thief:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Citizen:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }

            Console.Write(person.Symbol);
            Console.ResetColor();
        }
    }
} 
    

