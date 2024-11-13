using Domain.Decorators;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.model.Decorators
{
    public class ClosingGateDoorDecorator : BaseDoorDecorator
    {
        public ClosingGateDoorDecorator(IDoor wrappee) : base(wrappee)
        {

        }

        public override void OpenDoor(Player player)
        {
            base.OpenDoor(player);
        }
    }
}
