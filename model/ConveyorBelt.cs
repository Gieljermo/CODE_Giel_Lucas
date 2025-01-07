using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Enums;
using TempleOfDoom.model.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace TempleOfDoom.model
{
    public class ConveyorBelt : SpecialFloorTile
    {
        public ConveyorBelt(string type, IPosition position, Direction direction)
            : base(type, position, direction)
        {
        }

        public override void Interact(IEntity entity, Field field, Room room)
        {
            if(entity != null)
            {
                entity.moveByConveyor(entity, 1, Direction);
            }
        }
    }
}
