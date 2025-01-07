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
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.controller
{
    public class OpponentController
    {
        private readonly Dictionary<ConsoleKey, (int xMovement, int yMovement)> movementMap;
        private BoardController boardController;
        public OpponentController(BoardController boardController)
        {
            this.boardController = boardController;
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
                foreach (var opponent in room.Opponents)
                {
                    opponent.Move(opponent.Position, room);
                    IInteractiveFieldElement item = boardController.GetItemAtPosition(opponent.Position);
                    Field field = room.Fields.Where(f => f.Position == opponent.Position).FirstOrDefault();
                    if (item != null)
                    {
                        item.Interact(opponent, field, room);
                    }
                }

            }

        }




        public void CheckDamage(ConsoleKey key, Room room, Player player)
        {
            if (key == ConsoleKey.Spacebar)
            {
                foreach (var opponent in room.GetOpponentsWithinPlayerRange(player))
                {
                   opponent.DoDamage(1);
                }
            }

        }
    }
}
