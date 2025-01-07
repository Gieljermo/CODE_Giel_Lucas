using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Enums;
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.model
{
    public class SpecialFloorTile : IInteractiveFieldElement
    {
        private string _type;
        public string Type { get => _type; set => _type = value; }

        public int Amount { get; set; }
        public Direction Direction { get; set; }
        public IPosition Position { get; set; }

        public SpecialFloorTile(string type, IPosition position, Direction direction)
        {
            this.Type = type;
            this.Position = position;
            this.Direction = direction;
        }

        public virtual void Interact(IEntity entity, Field field, Room room)
        {
            return;
        }
    }
}
