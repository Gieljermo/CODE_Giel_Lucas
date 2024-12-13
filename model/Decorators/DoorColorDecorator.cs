using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using TempleOfDoom.model;
using TempleOfDoom.model.Observers;

namespace Domain.Decorators
{
    public class DoorColorDecorator : BaseDoorDecorator, IInventoryObserver
    {

        public string color;
        private bool hasKey;

        public DoorColorDecorator(IDoor wrappee, string color) : base(wrappee)
        {
            this.color = color;
            this.hasKey = false;
        }

        public void onInventoryChange(List<IItem> inventory)
        {
            if(inventory.OfType<Key>().Any(k => k.Color == color))
            {
                hasKey = true;
            }
        }

        public override void OpenDoor(Player player, Room room)
        {
            if (player.Inventory.OfType<Key>().Any(k => k.Color == color))
            {
                base.OpenDoor(player, room);
            }
        }
    }

}
