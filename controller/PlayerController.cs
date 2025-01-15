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
using TempleOfDoom.model.Observers;

namespace TempleOfDoom.controller
{
    public class PlayerController
    {
        private Player Player;
        private readonly Dictionary<ConsoleKey, Action<Room>> actionMap;
        private BoardController boardController;

        public PlayerController(Player player, BoardController boardController)
        {
            this.Player = player;
            this.boardController = boardController;

            actionMap = new Dictionary<ConsoleKey, Action<Room>>
            {
                { ConsoleKey.LeftArrow, room => Move(-1, 0, room) },
                { ConsoleKey.UpArrow, room => Move(0, -1, room) },
                { ConsoleKey.RightArrow, room => Move(1, 0, room) },
                { ConsoleKey.DownArrow, room => Move(0, 1, room) },
                { ConsoleKey.Spacebar, room => Attack(room) }
            };
        }

        public void Action(ConsoleKey key, Room room)
        {
            if (actionMap.TryGetValue(key, out var action))
            {
                action(room);
            }
        }

        private void Move(int xMovement, int yMovement, Room room)
        {
            if (boardController.CanMoveTo(Player.X + xMovement, Player.Y + yMovement))
            {
                Player.move(xMovement, yMovement, room);
            }

            IItem item = boardController.GetItemAtPosition(Player.X, Player.Y);
            if (item != null)
            {
                item.Interact(Player, room.Fields
                    .FirstOrDefault(f => f.X == Player.X && f.Y == Player.Y), room);
            }

        }

        private void Attack(Room room)
        {
            var directions = new List<(int x, int y)>
            {
                (0, -1),  // Up
                (0, 1),   // Down
                (-1, 0),  // Left
                (1, 0)    // Right
            };

            foreach (var direction in directions)
            {
                int targetX = Player.X + direction.x;
                int targetY = Player.Y + direction.y;

                var enemy = room.Enemies
                    .FirstOrDefault(e => e.X == targetX && e.Y == targetY);

                if (enemy != null)
                {
                    enemy.takeDamage(Player.DAMAGE);

                    if (enemy.isDead())
                    {
                        if(enemy is IMovementObserver observer)
                        {
                            this.Player.RemoveMovementObserver(observer);
                        }

                        room.Enemies.Remove(enemy);
                    }
                }
            }
        }
    }
}
