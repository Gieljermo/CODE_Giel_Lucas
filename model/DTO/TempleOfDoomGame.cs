using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Adapter;

namespace TempleOfDoom.model.DTO
{
    public class TempleOfDoomGame
    {
        public List<Room> Rooms { get; private set; }
        public List<Connection> Connections { get; private set; }
        public Player Player { get; set; }

        public TempleOfDoomGame(List<Room> rooms, List<Connection> connections, Player player)
        {
            Rooms = rooms;
            Connections = connections;
            Player = player;
        }

    }
}
