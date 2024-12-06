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
        private Room _room;
        public DoorOnStonesDecorator(IDoor wrappee, int neededStones, Room room) : base(wrappee) 
        {
            _neededStones = neededStones;
            _room = room;
        }

        public override void OpenDoor()
        {
            if(CheckRoomForStones() == _neededStones)
            {
                base.OpenDoor();
            }
        }

        private int CheckRoomForStones()
        {
            int amountOfStones = 0;
            foreach(var field in _room.Fields)
            {
                if(field.Item != null && field.Item is SankaraStone)
                {
                    amountOfStones++;
                }
            }

            return amountOfStones;
        }
    }
}
