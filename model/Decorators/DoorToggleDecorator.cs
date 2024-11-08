using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;

namespace Domain.Decorators
{
    public class DoorToggleDecorator : BaseDoorDecorator
    {
        public DoorToggleDecorator(IDoor wrappee) : base(wrappee)
        {
            
        }

        public override void OpenDoor(Player player)
        {
            if (this.IsOpen)
            {
                CloseDoor();
            }
            else
            {
                base.OpenDoor(player);
            }
        }

        private void CloseDoor()
        {
            this.IsOpen = false;
        }
    }
}
