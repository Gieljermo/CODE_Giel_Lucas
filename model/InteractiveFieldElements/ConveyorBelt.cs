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
        private const int CONVEYOR_STEP_AMOUNT = 1;

        public ConveyorBelt(string type, IPosition position, Direction direction)
            : base(type, position, direction)
        {
        }

        public override char? GetSymbol()
        {
            return Direction switch
            {
                Direction.NORTH => 'ʌ', 
                Direction.EAST => '>', 
                Direction.SOUTH => 'v',  
                Direction.WEST => '<',   
                _ => ' '                 
            };
        }

        public override void Interact(IEntity entity, Field field)
        {
            if(entity == null)
            {
                return;
            }
            entity.MoveByConveyor(entity, CONVEYOR_STEP_AMOUNT, Direction);
        }
    }
}
