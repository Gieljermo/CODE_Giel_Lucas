using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Interfaces;

namespace Domain.Decorators
{
    public class DoorOnStonesDecorator : BaseDoorDecorator
    {
        private readonly int _neededStones;


        public DoorOnStonesDecorator(IDoor wrappee, int neededStones) : base(wrappee) 
        {
            _neededStones = neededStones;
        }

        public override void ChangeDoorStatus(IEntity player, Room room)
        {
            if(GetAmountOfStonesInRoom(room) != _neededStones)
            {
                base.IsOpen = false;
                return;
            }
            else
            {
                base.IsOpen = true;
            }
        }

        private int GetAmountOfStonesInRoom(Room room)
        {
            return room.Fields.Count(field => field.InteractiveFieldElement is SankaraStone);
        }
    }
}
