using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjuvochpolisfinal
{
    internal struct Position
    {
        internal int X { get; set; }
        internal int Y { get; set; }
        internal Position(int x, int y) { X = x; Y = y; }
    }
    internal struct Direction
    {
        internal int X { get; set; }
        internal int Y { get; set; }

        private static Random rand = new Random();

        internal Direction(int x, int y) { X = x; Y = y; }

        internal static Direction RandomDirection()
        {
            return new Direction(rand.Next(-1, 2), rand.Next(-1, 2));
        }
    }
    internal class Person
    {
        public string Name { get; set; }
        public Position Position { get; set; }
        public Direction Direction { get; set; }
        public char Symbol { get; set; }
        public Inventory Inventory { get; set; }
        public DateTime? ReleaseTime { get; set; }


        internal Person(string name, Position position, Direction direction, char symbol, Inventory inventory)
        {
            Name = name;
            Position = position;
            Direction = direction;
            Symbol = symbol;
            Inventory = inventory;
        }
        internal void Move()
        {
            Position = new Position(Position.X + Direction.X, Position.Y + Direction.Y);
        }
        internal void MoveInPrison(int prisonStartX, int prisonStartY, int prisonWidth, int prisonHeight)
        {
            if (ReleaseTime.HasValue && DateTime.Now >= ReleaseTime.Value)                                         
            {
                Symbol = 'T';
             
                ReleaseTime = null;

                Position = new Position(new Random().Next(1, 98), new Random().Next(1, 23));
                return;
            }

            if (new Random().Next(10) == 0)
                ChangeDirection();

            int newX = Position.X + Direction.X;
            int newY = Position.Y + Direction.Y;

            // Vänd riktning om vi träffar fängelsevägg
            if (newX <= prisonStartX + 1 || newX >= prisonStartX + prisonWidth - 2)
                Direction = new Direction(-Direction.X, Direction.Y);
            newX = Position.X + Direction.X;

            if (newY <= prisonStartY + 1 || newY >= prisonStartY + prisonHeight - 2)
                Direction = new Direction(Direction.X, -Direction.Y);
            newY = Position.Y + Direction.Y;

            // Se till att personen är inom gränserna
            Position = new Position(
              newX = Math.Clamp(newX, prisonStartX + 1, prisonStartX + prisonWidth - 2),
              newY = Math.Clamp(newY, prisonStartY + 1, prisonStartY + prisonHeight - 2)
            );

            Position = new Position(newX, newY);
        }
        internal void ChangeDirection()
        {
            Direction = Direction.RandomDirection();
        }
    }
    internal class Police : Person
    {
        public Police(string name, Position position)
            : base(name, position, Direction.RandomDirection(), 'P', new PoliceInventory()) { }
    }

    internal class Thief : Person
    {
        public Thief(string name, Position position)
            : base(name, position, Direction.RandomDirection(), 'T', new ThiefInventory()) { }
    }

    internal class Citizen : Person
    {
        public Citizen(string name, Position position)
            : base(name, position, Direction.RandomDirection(), 'C', new CitizenInventory()) { }
    }
}
