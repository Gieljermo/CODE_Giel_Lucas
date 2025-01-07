using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;
using TempleOfDoom.model.Enums;
using TempleOfDoom.model.Interfaces;

namespace Domain.Factory
{
    public class FieldElementFactory
    {

        public IInteractiveFieldElement CreateItem(string type, IPosition position, int damage, string color)
        {
            switch (type.ToLower())
            {
                case "boobytrap":
                    return new Boobytrap(type, position, damage);
                case "disappearing boobytrap":
                    return new DisappearingBoobytrap(type, position, damage);
                case "sankara stone":
                    return new SankaraStone(type, position, color);
                case "key":
                    return new Key(type, position, color);
                case "pressure plate":
                    return new PressurePlate(type, position);
                default:
                    throw new NotSupportedException($"Item type {type} is not supported.");
            }
        }

        public IInteractiveFieldElement CreateSpecialFloorTile(string type, IPosition position, Direction direction)
        {
            switch (type.ToLower())
            {
                case "conveyor belt":
                    return new ConveyorBelt(type, position, direction);
                default:
                    throw new NotSupportedException($"Item type {type} is not supported.");
            }
        }
    }
}
