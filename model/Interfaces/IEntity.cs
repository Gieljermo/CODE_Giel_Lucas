using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Enums;

namespace TempleOfDoom.model.Interfaces
{
    public interface IEntity
    {
        public IPosition Position { get; set; }
        public int Lives { get; set; }
        public int AmountOfStones { get; set; }
        public void Move(IPosition position, Room room);
        public void TakeDamage(int amount);
        public void MoveByConveyor(IEntity entity, int amount, Direction direction);
        public void AddItemToInvetory(Item item);
    }
}
