using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using TempleOfDoom.model;

namespace Domain.Decorators
{
    public class DoorColorDecorator : BaseDoorDecorator
    {

        public DoorColorDecorator(IDoor wrappee, string color) : base(wrappee)
        {
            base.Color = color;
        }

        public override void OpenDoor(Player player)
        {
            base.OpenDoor(player);
        }
    }

}
