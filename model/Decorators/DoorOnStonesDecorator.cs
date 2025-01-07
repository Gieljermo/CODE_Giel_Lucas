using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;

namespace Domain.Decorators
{
    public class DoorOnStonesDecorator : BaseDoorDecorator
    {
        private int _neededStones;
        public DoorOnStonesDecorator(IDoor wrappee, int neededStones) : base(wrappee) 
        {
            _neededStones = neededStones;
        }

        public override void OpenDoor(Player player, Room room)
        {
            if(CheckRoomForStones(room) != _neededStones)
            {
                base.IsOpen = false;
                return;
            }
 
            base.OpenDoor(player, room);
        }

        private int CheckRoomForStones(Room room)
        {
            int amountOfStones = 0;
            foreach(var field in room.Fields)
            {
                if(field.InteractiveFieldElement != null && field.InteractiveFieldElement is SankaraStone)
                {
                    amountOfStones++;
                }
            }

            return amountOfStones;
        }
    }
}
