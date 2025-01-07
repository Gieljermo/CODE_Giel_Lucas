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
    public class PlayerController
    {
        private const int SHOOT_DAMAGE = 1;

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
              //  IPosition positionForMove = new Position(movement.xMovement, movement.yMovement);
                IPosition canMoveToPosition = new Position(Player.Position.X + movement.xMovement, Player.Position.Y + movement.yMovement);

                if (boardController.CanMoveTo(canMoveToPosition))
                {
                    var positionForMove = new Position(movement.xMovement, movement.yMovement);
                    Player.Move(positionForMove, room);


                    IInteractiveFieldElement item = boardController.GetItemAtPosition(Player.Position);
                    Field field = room.Fields.Where(f => f.Position.Equals(Player.Position)).FirstOrDefault();

                    if (item != null)
                    {
                        item.Interact(Player, field, room);

                    }




                }

               
            }
        }
        public void CheckDamage(Room room)
        {
            foreach (var opponent in room.Opponents)
            {
                if (opponent.Position.Equals(Player.Position))
                {
                    Player.takeDamage(SHOOT_DAMAGE);
                }

            }
        }
    }
}
