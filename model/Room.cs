using CODE_TempleOfDoom_DownloadableContent;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Adapter;

namespace TempleOfDoom.model
{
    public class Room
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Connection> Connections { get; set; }
        public List<Field> Fields { get; set; }

        public List<Item> Items { get; set; }
        public List<Opponent> Opponents { get; set; }
        public List<SpecialFloorTile> SpecialFloorTiles { get; set; }

        public Room(int id, string type, int width, int height)
        {
            this.Id = id;
            this.Type = type;
            this.Width = width;
            this.Height = height;
            Connections = new List<Connection>();
            Fields = new List<Field>();
            Items = new List<Item>();
            SpecialFloorTiles = new List<SpecialFloorTile>();
            Opponents = new List<Opponent>();
        }

        public List<Opponent> GetOpponentsWithinPlayerRange(Player player)
        {
            return Opponents.Where(opponent =>
                opponent.Position.X == player.Position.X && opponent.Position.Y == player.Position.Y + 1 ||
                opponent.Position.X == player.Position.X && opponent.Position.Y == player.Position.Y - 1 ||
                opponent.Position.X == player.Position.X + 1 && opponent.Position.Y == player.Position.Y ||
                opponent.Position.X == player.Position.X - 1 && opponent.Position.Y == player.Position.Y
            ).ToList();
        }

    }
}
