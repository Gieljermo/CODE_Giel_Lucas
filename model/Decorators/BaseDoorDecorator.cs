using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;

namespace Domain.Decorators
{
    public abstract class BaseDoorDecorator : IDoor
    {

        private IDoor _wrappee;

        private bool _isOpen;
        public bool IsOpen { get => _isOpen; set => _isOpen = value; }

        public BaseDoorDecorator(IDoor wrappee)
        {
            _wrappee = wrappee;
        }

        public virtual void OpenDoor(Player player)
        {
            _wrappee.OpenDoor(player);
        }
    }
}
