using Domain.Decorators;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Interfaces;
using TempleOfDoom.model.Observers;

namespace TempleOfDoom.model.Decorators
{
    public class OpenOnOddDoorDecorator : BaseDoorDecorator, IHealthObserver
    {
        private const int EVEN_LIVES_CONDITION = 0;
        private const int ODD_LIVES_CONDITION = 1;
        private const int MODULUS_BASE = 2;
        private bool unlocked;

        public OpenOnOddDoorDecorator(IDoor wrappee) : base(wrappee)
        {
            unlocked = false;
        }

        public void OnHealthChanged(int amountOfLives)
        {
            //Check lives are odd
            if (amountOfLives % MODULUS_BASE == EVEN_LIVES_CONDITION)
            {
                unlocked = true;
            }
            else
            {
                unlocked = false;
            }
        }

        public override void ChangeDoorStatus(IEntity player, Room room)
        {
            //Check lives are odd
            if (player.Lives % MODULUS_BASE == EVEN_LIVES_CONDITION)
            {
                base.IsOpen = false;
                return;
            }
            else
            {
                base.IsOpen = true;
            }
        }
    }
}
