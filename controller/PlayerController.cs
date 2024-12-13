using Controlllers;
using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;

namespace TempleOfDoom.controller
{
    public class PlayerController
    {
        private Player Player;
        private readonly Dictionary<ConsoleKey, (int xMovement, int yMovement)> movementMap;
        private BoardController boardController;
        public PlayerController(Player player, BoardController boardController)
        {
            this.Player = player;
            this.boardController = boardController;

            movementMap = new Dictionary<ConsoleKey, (int, int)>
            {
                { ConsoleKey.LeftArrow, (-1, 0) },
                { ConsoleKey.UpArrow, (0, -1) },
                { ConsoleKey.RightArrow, (1, 0) },
                { ConsoleKey.DownArrow, (0, 1) }
            };
            this.boardController = boardController;
        }
        public void Move(ConsoleKey key, Room room)
        {
            if (movementMap.TryGetValue(key, out var movement))
            {
                IItem item = boardController.GetItemAtPosition(Player.XPositon, Player.YPositon);
                if (item != null)
                {
                    item.Interact(Player, room.Fields.Where(f => f.X == Player.XPositon).Where(f => f.Y == Player.YPositon).FirstOrDefault(), room);
                }

                if (boardController.CanMoveTo(Player.XPositon + movement.xMovement, Player.YPositon + movement.yMovement)){
                    Player.Move(movement.xMovement, movement.yMovement, room);
                }
            }
        }
    }
}
