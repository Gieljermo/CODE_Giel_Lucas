using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.model
{
    public class Door
    {
        public string Type { get; set; }
        public string Color { get; set; }
        public int NumberOfStones { get; set; }

        public Door(string type, string color, int no_of_stones)
        {
            this.Type = type;
            this.Color = color;
            this.NumberOfStones = no_of_stones;
        }

    }
}
