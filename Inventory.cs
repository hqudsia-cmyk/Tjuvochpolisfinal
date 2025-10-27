﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjuvochpolisfinal
{
    internal class Inventory
    {
        public List<string> Items { get; set; }
        public Inventory(List<string> items) { Items = items; }
    }

    internal class PoliceInventory : Inventory
    {
        public PoliceInventory() : base(new List<string>()) { }
    }

    internal class ThiefInventory : Inventory
    {
        public ThiefInventory() : base(new List<string>()) { }
    }

    internal class CitizenInventory : Inventory
    {
        public CitizenInventory() : base(new List<string> { "Nyckel", "Klocka", "Mobil", "Pengar" }) { }
    }
}

