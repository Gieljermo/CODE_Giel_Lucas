using Domain.Decorators;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.model.Decorators
{
    public class OpenOnOddDoorDecorator : BaseDoorDecorator
    {
        public OpenOnOddDoorDecorator(IDoor wrappee) : base(wrappee)
        {

        }

        public override void OpenDoor(Player player)
        {
            if (player.AmountOfLives % 2 != 0)
            {
                base.OpenDoor(player);
            }
        }
    }
}
