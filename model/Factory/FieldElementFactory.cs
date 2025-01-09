using CODE_TempleOfDoom_DownloadableContent;
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
            return type switch
            {
                "boobytrap" => new Boobytrap(type, position, damage),
                "disappearing boobytrap" => new DisappearingBoobytrap(type, position, damage),
                "sankara stone" => new SankaraStone(type,position, color),
                "key" => new Key(type,position,color),
                "pressure plate" => new PressurePlate(type,position),
                _ => throw new NotSupportedException($"Item type {type} is not supported.")
            };
        }

        public IInteractiveFieldElement CreateSpecialFloorTile(string type, IPosition position, Direction direction)
        {
            return type switch
            {
                "conveyor belt" => new ConveyorBelt(type, position, direction),
                _ => throw new NotSupportedException($"Item type {type} is not supported.")
            };

        }
    }
}
