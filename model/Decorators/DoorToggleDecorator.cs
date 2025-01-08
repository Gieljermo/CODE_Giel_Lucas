using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;
using TempleOfDoom.model.Adapter;

namespace Domain.Decorators
{
    public class DoorToggleDecorator : BaseDoorDecorator
    {
        public DoorToggleDecorator(IDoor wrappee) : base(wrappee)
        {
            
        }

        public override void OpenDoor(Player player, Room room)
        {
            if (this.IsOpen)
            {
                CloseDoor();
            }
            else
            {
                base.OpenDoor(player, room);
            }
        }

        private void CloseDoor()
        {
            base.IsOpen = false;
        }
    }
}
