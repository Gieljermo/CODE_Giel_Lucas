using CODE_TempleOfDoom_DownloadableContent;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using TempleOfDoom.model.Enums;
using TempleOfDoom.model.Factory;
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.model.Adapter
{
    public class Opponent : Entity
    {
        private const int AMOUNT_OF_LIVES = 1;

        public string Type { get; set; }
        public int MinX { get; set; }
        public int MaxX { get; set; }
        public int MinY { get; set; }
        public int MaxY { get; set; }
        public Room CurrentRoom { get; set; }

        private readonly Enemy _enemy;

        public Opponent(string type, IPosition position, int minX, int maxX, int minY, int maxY)
        {
            Type = type.ToString();
            Position = position;
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;


            _enemy = EnemyFactory.CreateEnemy(type, position, minX, maxX, minY, maxY);
            _enemy.CurrentField = new Field(CurrentRoom, Position);
            _enemy.OnDeath += HandleEnemyDeath;
        }

        private void HandleEnemyDeath(object? sender, EventArgs e)
        {
            CurrentRoom?.Opponents?.Remove(this);
        }

        public void DoDamage(int damage)
        {
            _enemy.DoDamage(damage);
        }

        public override void Move(IPosition position, Room room)
        {
            var targetField = room.Fields.FirstOrDefault(f =>
                f.Position.X == _enemy.CurrentXLocation &&
                f.Position.Y == _enemy.CurrentYLocation);

            if (targetField != null)
            {
                _enemy.CurrentField = targetField;
                CurrentRoom = room;
                _enemy.Move();
                Position = new Position(_enemy.CurrentXLocation, _enemy.CurrentYLocation);
            }
        }

        public override void MoveByConveyor(IEntity entity, int amount, Direction direction)
        {
            switch (direction)
            {
                case Direction.NORTH:
                    Position = new Position(Position.X, Position.Y - amount);
                    break;
                case Direction.EAST:
                    Position = new Position(Position.X + amount, Position.Y);
                    break;
                case Direction.SOUTH:
                    Position = new Position(Position.X, Position.Y + amount);
                    break;
                case Direction.WEST:
                    Position = new Position(Position.X - amount, Position.Y);
                    break;
                default:
                    throw new InvalidOperationException("Invalid direction");
            }

            _enemy.CurrentXLocation = Position.X;
            _enemy.CurrentYLocation = Position.Y;
        }
    }
}
