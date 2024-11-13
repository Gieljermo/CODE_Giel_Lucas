using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;

namespace TempleOfDoom.controller
{
    public class PlayerController
    {
        private Player Player;
        private readonly Dictionary<ConsoleKey, (int xMovement, int yMovement)> movementMap;
        public PlayerController(Player player) { 
            this.Player = player;

            movementMap = new Dictionary<ConsoleKey, (int, int)>
            {
                { ConsoleKey.LeftArrow, (-1, 0) },
                { ConsoleKey.UpArrow, (0, -1) },
                { ConsoleKey.RightArrow, (1, 0) },
                { ConsoleKey.DownArrow, (0, 1) }
            };
        }
        public void Move(ConsoleKey key, Room room)
        {
            if (movementMap.TryGetValue(key, out var movement))
            {
                Player.Move(movement.xMovement, movement.yMovement, room);
            }
        }
    }
}
