using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.model
{
    public class Door : IDoor
    {
        private bool _isOpen;
        public bool IsOpen { get => _isOpen; set => _isOpen = value; }

        public Door()
        {

        }

        public void OpenDoor(Player player, Room room)
        {
            this.IsOpen = true;
        }
    }
}
