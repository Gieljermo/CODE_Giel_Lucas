using CODE_TempleOfDoom_DownloadableContent;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Enums;
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.model.Adapter
{
    public class Opponent : Entity
    {
        public string Type { get; set; }
        public int MinX { get; set; }
        public int MaxX { get; set; }
        public int MinY { get; set; }
        public int MaxY { get; set; }
        public Room CurrentRoom { get; set; }

        private readonly Enemy _enemy;

        private readonly Enemy _enemyAdaptee;


        public Opponent(string type, IPosition position, int minXLocation, int maxXLocation, int minYLocation, int maxYLocation)
        {
            this.Type = type;
            this.Position = position;
            this.MinX = minXLocation;
            this.MaxX = maxXLocation;
            this.MinY = minYLocation;
            this.MaxY = maxYLocation;

            if (type == "horizontal")
            {
                _enemy = new HorizontallyMovingEnemy(1, position.X, position.Y, minXLocation, maxXLocation);
                _enemy.CurrentField = new Field(CurrentRoom, Position);
                _enemy.OnDeath += _enemyAdaptee_OnDeath;

            }
            else if(type == "vertical")
            {
                _enemy = new VerticallyMovingEnemy(1, position.X, position.Y, minYLocation, maxYLocation);

                _enemy.CurrentField = new Field(CurrentRoom, Position);

                _enemy.OnDeath += _enemyAdaptee_OnDeath;
            }


        }

        private void _enemyAdaptee_OnDeath(object? sender, EventArgs e)
        {
            CurrentRoom.Opponents.Remove(this);
        }

        public void DoDamage(int damage)
        {
            
            _enemy.DoDamage(damage);
        }

        public override void Move(IPosition position,Room room)
        {
            Field fieldToMoveTo = room.Fields.Where(f => f.Position.Y == this.Position.Y).FirstOrDefault(f => f.Position.X == this.Position.X);
            fieldToMoveTo.Position.X = _enemy.CurrentXLocation;
            fieldToMoveTo.Position.Y = _enemy.CurrentYLocation;
            _enemy.CurrentField = fieldToMoveTo;
            CurrentRoom = room;
            _enemy.Move();
            this.Position.X = _enemy.CurrentXLocation;
            this.Position.Y = _enemy.CurrentYLocation;

        }

        public override void MoveByConveyor(IEntity entity, int amount, Direction direction)
        {
            this.Position = new Position(this.Position.X, this.Position.Y);

            switch (direction)
            {
                case Direction.NORTH:
                    this.Position.Y -= amount;
                    _enemy.CurrentYLocation = this.Position.Y;
                    break;
                case Direction.EAST:
                    this.Position.X += amount;
                    _enemy.CurrentXLocation = this.Position.X;
                    break;
                case Direction.SOUTH:
                    this.Position.Y += amount;
                    _enemy.CurrentYLocation = this.Position.Y;
                    break;
                case Direction.WEST:
                    this.Position.X -= amount;
                    _enemy.CurrentXLocation = this.Position.X;
                    break;
                default:
                    throw new InvalidOperationException("Invalid direction");
            }
        }
    }
}
