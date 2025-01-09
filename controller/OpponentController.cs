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
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.controller
{
    public class OpponentController
    {
        private const int MOVE_LEFT = -1;
        private const int MOVE_RIGHT = 1;
        private const int MOVE_UP = -1;
        private const int MOVE_DOWN = 1;
        private const int NO_MOVEMENT = 0;

        private readonly Dictionary<ConsoleKey, (int xMovement, int yMovement)> movementMap;
        private BoardController boardController;
        public OpponentController(BoardController boardController)
        {
            this.boardController = boardController;
            movementMap = InitializeMovementMap();
        }

        private Dictionary<ConsoleKey, (int xMovement, int yMovement)> InitializeMovementMap()
        {
            return new Dictionary<ConsoleKey, (int, int)>
            {
                { ConsoleKey.LeftArrow, (MOVE_LEFT, NO_MOVEMENT) },
                { ConsoleKey.UpArrow, (NO_MOVEMENT, MOVE_UP) },
                { ConsoleKey.RightArrow, (MOVE_RIGHT, NO_MOVEMENT) },
                { ConsoleKey.DownArrow, (NO_MOVEMENT, MOVE_DOWN) }
            };
        }

        public void Move(ConsoleKey key, Room room)
        {
            if (movementMap.TryGetValue(key, out var movement))
            {
                foreach (var opponent in room.Opponents)
                {
                    opponent.Move(opponent.Position, room);
                    HandleFieldInteraction(opponent, room);
                }

            }

        }

        private void HandleFieldInteraction(Opponent opponent, Room room)
        {
            IInteractiveFieldElement item = boardController.GetItemAtPosition(opponent.Position);
            var field = room.Fields.Where(f => f.Position == opponent.Position).FirstOrDefault();
            if (item != null)
            {
                item.Interact(opponent, field);
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
