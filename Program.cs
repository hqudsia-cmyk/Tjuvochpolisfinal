using Microsoft.VisualBasic;
using Tjuvochpolisfinal;

namespace Tjuvochpolisfinal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> newsFeed = new List<string>();

            Console.SetBufferSize(120, 50);
            Console.SetWindowSize(120, 50);

            int width = 100;
            int height = 25;

            int prisonWidth = 15;
            int prisonHeight = 15;
            int startY = height - 15;
            int startX = width + 1;

            Draw.DrawBorder(width, height);
            Draw.DrawPrisonBorder(prisonWidth, prisonHeight, startY, startX);

            List<Person> people = PeopleFactory.CreatePeople(width, height);

            foreach (var person in people)
                Display.DrawPerson(person);


            // Rita första gången 
            foreach (var person in people)
            {
                Display.DrawPerson(person);
            }

            int stepCounter = 0;
            int stepsBeforeChange = 5;

            // Huvudloop
            bool running = true;

            while (running)
            {
                //  Radera gamla positioner
                foreach (var person in people)
                {
                    Console.SetCursorPosition(person.Position.X, person.Position.Y);
                    Console.Write(" ");
                }

                // Byt riktning ibland
                stepCounter++;
                if (stepCounter % stepsBeforeChange == 0)
                    foreach (var person in people)
                        person.ChangeDirection();

                //  Flytta och wrap-around
                foreach (var person in people)
                {
                    if (person.Symbol == 'F') // fångar
                    {
                        ((Thief)person).MoveInPrison(startX, startY, prisonWidth, prisonHeight);
                    }
                    else
                    {
                        person.Move();

                        if (person.Position.X <= 0) person.Position = new Position(width - 2, person.Position.Y);
                        else if (person.Position.X >= width - 1) person.Position = new Position(1, person.Position.Y);

                        if (person.Position.Y <= 0) person.Position = new Position(person.Position.X, height - 2);
                        else if (person.Position.Y >= height - 1) person.Position = new Position(person.Position.X, 1);
                    }
                }

                // Interaktioner
                foreach (var thief in people.OfType<Thief>())
                {
                    foreach (var citizen in people.OfType<Citizen>())
                    {
                        if (thief.Position.X == citizen.Position.X && thief.Position.Y == citizen.Position.Y)
                        {
                            Interactions.HandleRobbery(thief, citizen, people, width, height, newsFeed);
                        }
                    }

                    foreach (var police in people.OfType<Police>())
                    {
                        if (thief.Position.X == police.Position.X && thief.Position.Y == police.Position.Y)
                        {
                            Interactions.HandleArrest(police, thief, people, width, height, startX, startY, prisonWidth, prisonHeight, newsFeed);
                        }
                    }
                }


                foreach (var police in people.OfType<Police>())
                {
                    foreach (var citizen in people.OfType<Citizen>())
                    {
                        if (police.Position.X == citizen.Position.X && police.Position.Y == citizen.Position.Y)
                        {
                            Interactions.CitizenGreetings(citizen, police, people, width, height, newsFeed);
                        }
                    }
                }

                foreach (var person in people)
                {
                    Display.DrawPerson(person);
                }

                // Rita nya positioner
                foreach (var person in people)
                {
                    
                    Display.DrawPerson(person);
                }

                Draw.DrawNews(newsFeed, width, height);
                Draw.DrawStatus(people, width, height);

                Thread.Sleep(500);

                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    running = false;
                    Console.Clear();

                    Display.DisplayResult(people, newsFeed);
                    break;
                }
            }
        }
    }
}
               


            



