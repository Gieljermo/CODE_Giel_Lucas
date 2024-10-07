using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.model
{
    public class Item
    {
        private string type;
        private int damage;
        private int x;
        private int y;
        private string color;

        public Item(string type, int damage, int x, int y, string color)
        {
            this.type = type;
            this.damage = damage;
            this.x = x;
            this.y = y;
            this.color = color;
        }
    }
}
