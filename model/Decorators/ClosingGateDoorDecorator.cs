using Domain.Decorators;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.model.Decorators
{
    public class ClosingGateDoorDecorator : BaseDoorDecorator
    {
        private bool playerPassed;
        public override char? GetSymbol() => '∩';

        public ClosingGateDoorDecorator(IDoor wrappee) : base(wrappee)
        {
            base.IsOpen = true;
            playerPassed = false;
        }

        public override void ChangeDoorStatus(IEntity player, Room room)
        {
            if (!playerPassed)
            {
                playerPassed = true;
                base.ChangeDoorStatus(player, room);
            }
            else
            {
                base.IsOpen = false;
            }
        }
    }
}
