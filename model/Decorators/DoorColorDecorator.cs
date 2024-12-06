using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using TempleOfDoom.model;

namespace Domain.Decorators
{
    public class DoorColorDecorator : BaseDoorDecorator
    {

        public string color;

        public DoorColorDecorator(IDoor wrappee, string color) : base(wrappee)
        {
           this.color = color;
        }

        public override void OpenDoor(Player player)
        {
            if (player.Inventory.OfType<Key>().Any(k => k.Color == color))
            {
                base.OpenDoor(player);
            }
        }
    }

}
