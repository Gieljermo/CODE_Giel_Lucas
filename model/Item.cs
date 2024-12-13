using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.model
{
    public class Item
    {
        public string Type { get; set; }
        public int Damage { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Color { get; set; }

        public Item(string type, int damage, int x, int y, string color)
        {
            this.Type = type;
            this.Damage = damage;
            this.X = x;
            this.Y = y;
            this.Color = color;
        }
    }
}
