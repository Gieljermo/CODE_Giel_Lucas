using CODE_TempleOfDoom_DownloadableContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Enums;
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.model
{
    public abstract class Entity : IEntity
    {

        public virtual IPosition Position { get; set; }
        public virtual int Lives { get; set; }
        public virtual int AmountOfStones { get; set; }

        public virtual void moveByConveyor(IEntity entity, int amount, Direction direction)
        {
            this.Position = new Position(this.Position.X, this.Position.Y);

            switch (direction)
            {
                case Direction.NORTH:
                    this.Position.Y -= amount;
                    break;
                case Direction.EAST:
                    this.Position.X += amount;
                    break;
                case Direction.SOUTH:
                    this.Position.Y += amount;
                    break;
                case Direction.WEST:
                    this.Position.X -= amount;
                    break;
                default:
                    throw new InvalidOperationException("Invalid direction");
            }
        }

        public virtual void takeDamage(int amount)
        {
            this.Lives -= amount;
        }

        public virtual void AddItemToInvetory(Item item)
        {
            return;
        }

        public virtual void Move(IPosition position, Room room)
        {
            return;
        }
    }
}
