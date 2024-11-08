using Domain;
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
        public List<Connection> Connections { get; set; }
        public List<Field>? Fields { get; set; }

        public List<Item> Items { get; set; }

        public Room(int id, string type, int width, int height)
        {
            this.Id = id;
            this.Type = type;
            this.Width = width;
            this.Height = height;
            Connections = new List<Connection>();
            Fields = new List<Field>();
            Items = new List<Item>();
        }

    }
}
