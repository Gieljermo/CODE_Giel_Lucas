using CODE_TempleOfDoom_DownloadableContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Enums;
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.model.Adapter
{
    public abstract class Entity : IEntity
    {

        public virtual IPosition Position { get; set; }
        public virtual int Lives { get; set; }

        public virtual void MoveByConveyor(IEntity entity, int amount, Direction direction)
        {
            Position = new Position(Position.X, Position.Y);

            switch (direction)
            {
                case Direction.NORTH:
                    Position.Y -= amount;
                    break;
                case Direction.EAST:
                    Position.X += amount;
                    break;
                case Direction.SOUTH:
                    Position.Y += amount;
                    break;
                case Direction.WEST:
                    Position.X -= amount;
                    break;
                default:
                    throw new InvalidOperationException("Invalid direction");
            }
        }

        public virtual void TakeDamage(int amount)
        {
            Lives -= amount;
        }

        public virtual void Move(IPosition position, Room room)
        {
            return;
        }
    }
}
