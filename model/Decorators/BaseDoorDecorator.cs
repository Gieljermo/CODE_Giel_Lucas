using Domain.Interfaces;
using System;
using TempleOfDoom.model;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Interfaces;

namespace Domain.Decorators
{
    public abstract class BaseDoorDecorator : IDoor
    {
        public IDoor _wrappee;

        public virtual char? GetSymbol() => null;
        public virtual string GetColor() => "white";

        public virtual bool IsOpen
        {
            get => _wrappee.IsOpen;
            set => _wrappee.IsOpen = value;
        }

        public BaseDoorDecorator(IDoor wrappee)
        {
            _wrappee = wrappee;
        }

        public virtual void ChangeDoorStatus(IEntity player, Room room)
        {
            _wrappee.ChangeDoorStatus(player, room);
        }
    }
}
