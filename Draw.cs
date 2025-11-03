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
        internal static void DrawBorder(int width, int height)
        {
            Console.Clear();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    char c = (y == 0 || y == height - 1 || x == 0 || x == width - 1) ? '#' : ' ';
                    SafeConsole.WriteChar(x, y, c);
                }
            }

            SafeConsole.WriteAt(2, 0, "City");
            SafeConsole.WriteAt(0, 25, "News Feed ============================");
        }

        internal static void DrawPrisonBorder(int prisonWidth, int prisonHeight, int startY, int startX)
        {
            for (int y = 0; y < prisonHeight; y++)
            {
                for (int x = 0; x < prisonWidth; x++)
                {
                    char c = (y == 0 || y == prisonHeight - 1 || x == 0 || x == prisonWidth - 1) ? '#' : ' ';
                    SafeConsole.WriteChar(startX + x, startY + y, c);
                }
            }

            SafeConsole.WriteAt(startX, startY, "Prison");
        }

        internal static void DrawNews(List<string> newsFeed, int width, int height)
        {
            int startLine = height + 1;
            int maxLines = 10;

            var recent = newsFeed.TakeLast(maxLines).ToList();

            for (int i = 0; i < maxLines; i++)
                SafeConsole.WriteAt(0, startLine + i, new string(' ', width));

            for (int i = 0; i < recent.Count; i++)
                SafeConsole.WriteAt(0, startLine + i, recent[i]);
        }

        internal static void DrawStatus(List<Person> people, int width, int height)
        {
            int policeCount = people.Count(person => person is Police);
            int thiefCount = people.Count(person => person is Thief && person.Symbol == 'T');
            int prisonerCount = people.Count(person => person is Thief && person.Symbol == 'F');
            int citizenCount = people.Count(person => person is Citizen);
            int robbedCitizens = people.OfType<Citizen>().Count(citizen => citizen.Inventory.Items.Count < 4);

            SafeConsole.WriteAt(50, 25, "== Status ==");
            SafeConsole.WriteAt(50, 26, $"Poliser: {policeCount}");
            SafeConsole.WriteAt(50, 27, $"Tjuvar: {thiefCount}");
            SafeConsole.WriteAt(50, 28, $"Fångar: {prisonerCount}");
            SafeConsole.WriteAt(50, 29, $"Medborgare: {citizenCount}");
            SafeConsole.WriteAt(50, 30, $"Antal rånade Medborgare: {robbedCitizens}");

            SafeConsole.WriteAt(70, 25, "** Esc - Avsluta **");
            SafeConsole.WriteAt(70, 26, "** 'T' - öka antalet tjuvar **");
        }
    }
}