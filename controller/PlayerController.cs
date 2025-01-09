using Controlllers;
using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using TempleOfDoom.model;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.controller
{
    public class PlayerController
    {
        private const int MOVE_LEFT = -1;
        private const int MOVE_RIGHT = 1;
        private const int MOVE_UP = -1;
        private const int MOVE_DOWN = 1;
        private const int NO_MOVEMENT = 0;
        private const int SHOOT_DAMAGE = 1;

        private readonly Player _player;
        private readonly Dictionary<ConsoleKey, (int xMovement, int yMovement)> _movementMap;
        private readonly BoardController _boardController;

        public PlayerController(Player player, BoardController boardController)
        {
            _player = player;
            _boardController = boardController;
            _movementMap = InitializeMovementMap();
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
            if (_movementMap.TryGetValue(key, out var movement))
            {
                var targetPosition = new Position(
                    _player.Position.X + movement.xMovement,
                    _player.Position.Y + movement.yMovement
                );

                if (_boardController.CanMoveTo(targetPosition))
                {
                    var newPosition = new Position(movement.xMovement, movement.yMovement);

                    _player.Move(newPosition, room);

                    var field = room.Fields.FirstOrDefault(f => f.Position.Equals(_player.Position));

                    field?.InteractiveFieldElement?.Interact(_player, field);
                }
            }
        }

        public void CheckDamage(Room room)
        {
            foreach (var opponent in room.Opponents)
            {
                if (opponent.Position.Equals(_player.Position))
                {
                    _player.TakeDamage(SHOOT_DAMAGE);
                }
            }
        }
    }
}
