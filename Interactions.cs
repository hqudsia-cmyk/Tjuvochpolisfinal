using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjuvochpolisfinal
{
    internal class Interactions
    {
        private static Random rand = new Random();

        internal static void CitizenGreetings(Citizen citizen, Police police, List<Person> people, int width, int height, List<string> newsFeed)
        {
            Display.DrawPerson(police);
            Display.DrawPerson(citizen);

            newsFeed.Add($"Medborgaren {citizen.Name} hälsar på polisen {police.Name}");
            if (newsFeed.Count > 10) newsFeed.RemoveAt(0);

            Display.RedrawPeople(people);
            Thread.Sleep(1000);
        }

        internal static void HandleRobbery(Thief thief, Citizen citizen, List<Person> people, int width, int height, List<string> newsFeed)
        {
            Display.ClearCityArea(width, height);

            SafeConsole.SetCursor(thief.Position.X, thief.Position.Y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write('T');

            SafeConsole.SetCursor(citizen.Position.X + 1, citizen.Position.Y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write('C');

            ThiefStealsFromCitizen(thief, citizen, newsFeed);
            Thread.Sleep(1000);

            Display.RedrawPeople(people);
        }

        internal static void HandleArrest(Police police, Thief thief, List<Person> people,
            int width, int height, int prisonStartX, int prisonStartY, int prisonWidth, int prisonHeight, List<string> newsFeed)
        {
            Display.ClearCityArea(width, height);

            SafeConsole.SetCursor(thief.Position.X, thief.Position.Y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write('T');

            SafeConsole.SetCursor(police.Position.X + 1, police.Position.Y);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write('P');

            PoliceCatchesThief(police, thief, people, prisonStartX, prisonStartY, prisonWidth, prisonHeight, newsFeed);

            Thread.Sleep(1000);
            Display.RedrawPeople(people);
        }

        private static void ThiefStealsFromCitizen(Thief thief, Citizen citizen, List<string> newsFeed)
        {
            if (citizen.Inventory.Items.Count == 0) return;

            int index = rand.Next(citizen.Inventory.Items.Count);
            string stolenItem = citizen.Inventory.Items[index];

            thief.Inventory.Items.Add(stolenItem);
            citizen.Inventory.Items.RemoveAt(index);

            newsFeed.Add($"Tjuven {thief.Name} stal {stolenItem} från medborgaren!");
            if (newsFeed.Count > 10) newsFeed.RemoveAt(0);
        }

        private static void PoliceCatchesThief(Police police, Thief thief, List<Person> people,
            int prisonStartX, int prisonStartY, int prisonWidth, int prisonHeight, List<string> newsFeed)
        {
            if (thief.Inventory.Items.Count == 0)
            { 
                newsFeed.Add("Tjuven hade inget stulet att gripas för.");
                if (newsFeed.Count > 10) newsFeed.RemoveAt(0);
                return;
            }

            double chance = 0.9; // 90% chans att gripa tjuven

            if (rand.NextDouble() < chance)
            {
                int stolenCount = thief.Inventory.Items.Count;

                police.Inventory.Items.AddRange(thief.Inventory.Items);
                thief.Inventory.Items.Clear();

                int prisonSeconds = stolenCount * 10;
                thief.ReleaseTime = DateTime.Now.AddSeconds(prisonSeconds);

                int prisonX = prisonStartX + rand.Next(1, prisonWidth - 2);
                int prisonY = prisonStartY + rand.Next(1, prisonHeight - 2);
                thief.Position = new Position(prisonX, prisonY);
                thief.Symbol = 'F';

                newsFeed.Add($"Polisen {police.Name} grep {thief.Name} för {stolenCount} stölder!");
                newsFeed.Add($"Tjuven sitter i fängelse i {prisonSeconds} sekunder.");

                if (newsFeed.Count > 10) newsFeed.RemoveAt(0);
            }
            else
            {
                newsFeed.Add("Tjuven lyckades undkomma!");
                if (newsFeed.Count > 10) newsFeed.RemoveAt(0);
            }
        }
    }
}
