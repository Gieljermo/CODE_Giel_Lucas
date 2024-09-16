using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.model
{
    public class Room
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Item>? Items { get; private set; }

        public Room(int id, string type, int width, int height, List<Item> items)
        {
            this.Id = id;
            this.Type = type;
            this.Width = width;
            this.Height = height;
            this.Items = items;
        }

    }
}
