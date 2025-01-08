using Domain.Decorators;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Observers;

namespace TempleOfDoom.model.Decorators
{
    public class OpenOnOddDoorDecorator : BaseDoorDecorator, IHealthObserver
    {

        private bool unlocked;

        public OpenOnOddDoorDecorator(IDoor wrappee) : base(wrappee)
        {
            unlocked = false;
        }

        public void OnHealthChanged(int amountOfLives)
        {
            if(amountOfLives % 2 == 0)
            {
                unlocked = true;
            } else
            {
                unlocked = false;
            }
        }

        public override void OpenDoor(Player player, Room room)
        {
            if (player.Lives % 2 != 0)
            {
                base.OpenDoor(player, room);
            }
        }
    }
}
