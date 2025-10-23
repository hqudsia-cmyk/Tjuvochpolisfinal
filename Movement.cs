using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjuvochpolisfinal
{
    
    internal class Movement
    {
       /public Position Position { get; set; }
        public Direction Direction { get; set; }
        public char Symbol { get; set; }
       
        public int PrisonTime { get; set; } = 0;

        public int PrisonCounter { get; set; } = 0;

        public Movement(Position position, Direction direction, char symbol)
        {
            
            Position = position;
            Direction = direction;
            Symbol = symbol;
            
        }
        public void Move()
        {
            Position = new Position(Position.X + Direction.X, Position.Y + Direction.Y);
        }

        public void MoveInPrison(int prisonStartX, int prisonStartY, int prisonWidth, int prisonHeight)
        {
            PrisonCounter++;

            if (PrisonCounter >= PrisonTime)
            {
                Symbol = 'T';
                PrisonTime = 0;
                PrisonCounter = 0;
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
        public void ChangeDirection()
        {
            Direction = Direction.RandomDirection();
        }


    }
}
