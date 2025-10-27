using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjuvochpolisfinal
{
    internal class Inventory
    {
        internal List<string> Items { get; set; }
        internal Inventory(List<string> items) { Items = items; }
    }

    internal class PoliceInventory : Inventory
    {
        internal PoliceInventory() : base(new List<string>()) { }
    }

    internal class ThiefInventory : Inventory
    {
        internal ThiefInventory() : base(new List<string>()) { }
    }

    internal class CitizenInventory : Inventory
    {
        internal CitizenInventory() : base(new List<string> { "Nyckel", "Klocka", "Mobil", "Pengar" }) { }
    }
}

