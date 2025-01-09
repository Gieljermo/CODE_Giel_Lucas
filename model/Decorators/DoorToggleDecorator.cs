using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Interfaces;

namespace Domain.Decorators
{
    public class DoorToggleDecorator : BaseDoorDecorator
    {
        public override char? GetSymbol() => '-';

        public DoorToggleDecorator(IDoor wrappee) : base(wrappee)
        {
            
        }

        public override void ChangeDoorStatus(IEntity player, Room room)
        {
            if (this.IsOpen)
            {
                base.IsOpen = false; 
            }
            else
            {
                base.ChangeDoorStatus(player, room);
            }
        }
    }
}
