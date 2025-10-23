using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjuvochpolisfinal
{
    internal class Draw
    {
        // Rita upp staden
        static void DrawBorder(int width, int height)
        {
            Console.Clear();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (y == 0 || y == height - 1 || x == 0 || x == width - 1)
                        Console.Write("#");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(2, 0);
            Console.Write("City");

            Console.SetCursorPosition(0, 25);
            Console.Write("News Feed ============================");
        }
        static void DrawPrisonBorder(int prisonWidth, int prisonHeight, int startY, int startX)
        {
            for (int y = 0; y < prisonHeight; y++)
            {
                for (int x = 0; x < prisonWidth; x++)
                {
                    Console.SetCursorPosition(startX + x, y + startY);
                    if (y == 0 || y == prisonHeight - 1 || x == 0 || x == prisonWidth - 1)
                        Console.Write("#");
                    else
                        Console.Write(" ");
                }
            }

            Console.SetCursorPosition(startX, startY);
            Console.Write("Prison");
        }
        
        internal List<string> newsFeed = new List<string>();

        internal static void DrawNews(List<string> newsFeed, int width, int height)
        {
            int startLine = height + 1;

            int maxLines = 10;

            // bara de senaste 10 nyheterna
            var recent = newsFeed.TakeLast(maxLines).ToList();

            // Rensa område
            for (int i = 0; i < maxLines; i++)
            {
                Console.SetCursorPosition(0, startLine + i);
                Console.Write(new string(' ', width));
            }

            // Rita upp
            for (int i = 0; i < recent.Count; i++)
            {
                Console.SetCursorPosition(0, startLine + i);
                Console.Write($"{recent[i]}");
            }

        }

        internal static void DrawStatus(List<Person> people, int width, int height)
        {
            int policeCount = people.Count(person => person is Police);
            int thiefCount = people.Count(person => person is Thief && person.Symbol == 'T');
            int prisonerCount = people.Count(person => person is Thief && person.Symbol == 'F');
            int citizenCount = people.Count(person => person is Citizen);

            int robbedCitizens = people.OfType<Citizen>().Count(citizen => citizen.Inventory.Items.Count < 4);




            Console.SetCursorPosition(60, 25);
            Console.WriteLine("== Status == ");
            Console.SetCursorPosition(60, 26);
            Console.Write($"Poliser: {policeCount}");

            Console.SetCursorPosition(60, 27);
            Console.Write($"Tjuvar: {thiefCount}");

            Console.SetCursorPosition(60, 28);
            Console.Write($"Fångar: {prisonerCount}");

            Console.SetCursorPosition(60, 29);
            Console.Write($"Medborgare: {citizenCount}");

            Console.SetCursorPosition(60, 30);
            Console.Write($"Antal rånade Medborgare: {robbedCitizens}");
        }

    }

}
