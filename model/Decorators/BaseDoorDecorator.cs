using Domain.Interfaces;
using System;
using TempleOfDoom.model;

namespace Domain.Decorators
{
    public abstract class BaseDoorDecorator : IDoor
    {
        public IDoor _wrappee;

        public virtual bool IsOpen
        {
            get => _wrappee.IsOpen;
            set => _wrappee.IsOpen = value;
        }

        public BaseDoorDecorator(IDoor wrappee)
        {
            _wrappee = wrappee;
        }

        public virtual void OpenDoor(Player player, Room room)
        {
            _wrappee.OpenDoor(player, room);
        }
    }
}
