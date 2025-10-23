﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjuvochpolisfinal
{
    internal struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Position(int x, int y) { X = x; Y = y; }
    }

    internal struct Direction
    {
        public int X { get; set; }
        public int Y { get; set; }

        private static Random rand = new Random();

        public Direction(int x, int y) { X = x; Y = y; }

        public static Direction RandomDirection()
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
        
        public Person(string name, Position position, Direction direction, char symbol, Inventory inventory)
        {
            Name = name;
            Position = position;
            Direction = direction;
            Symbol = symbol;
            Inventory = inventory;
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
