using Domain.Decorators;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Adapter;

namespace TempleOfDoom.model.Decorators
{
    public class ClosingGateDoorDecorator : BaseDoorDecorator
    {
        bool playerPassed = false;
        public ClosingGateDoorDecorator(IDoor wrappee) : base(wrappee)
        {
            base.IsOpen = true;
        }

        public override void OpenDoor(Player player, Room room)
        {
            if (!playerPassed)
            {
                playerPassed = true;
                base.OpenDoor(player, room);
            }
            else
            {
                base.IsOpen = false;
            }
        }
    }
}
