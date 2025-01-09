using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.model
{
    public class Door : IDoor
    {
        public bool IsOpen { get; set; }

        public void ChangeDoorStatus(IEntity entity, Room room)
        {
            this.IsOpen = true;
        }

        public virtual bool DetermineDoorStatus(IEntity entity, Room room)
        {
            return true;
        }

    }
}
