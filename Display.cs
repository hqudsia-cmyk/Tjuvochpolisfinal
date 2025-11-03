using System;
using System.Collections.Generic;
using System.Linq;

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

            Console.WriteLine("\nPersoner, Status och Inventory");

            int personIndex = 0;
            foreach (var person in people)
            {
                string type = person switch
                {
                    Police => "Polis",
                    Thief => person.Symbol == 'F' ? "Fånge" : "Tjuv",
                    Citizen => "Medborgare"
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
                    Console.WriteLine($"{i++}. {news}");
            }

            Console.WriteLine("\nTryck på valfri tangent för att avsluta...");
            Console.ReadKey(true);
        }

        internal static void ClearCityArea(int width, int height)
        {
            for (int y = 1; y < height - 1; y++)
                for (int x = 1; x < width - 1; x++)
                    SafeConsole.WriteChar(x, y, ' ');
        }

        internal static void RedrawPeople(List<Person> people)
        {
            foreach (var p in people)
                DrawPerson(p);
        }

        internal static void DrawPerson(Person person)
        {
            SafeConsole.SetCursor(person.Position.X, person.Position.Y);

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