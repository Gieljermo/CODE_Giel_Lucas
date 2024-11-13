using Domain.Interfaces;
using System;
using TempleOfDoom.model;

namespace Domain.Decorators
{
    public abstract class BaseDoorDecorator : IDoor
    {
        private IDoor _wrappee;

        public virtual bool IsOpen
        {
            get => _wrappee.IsOpen;
            set => _wrappee.IsOpen = value;
        }

        public virtual string? Color
        {
            get => _wrappee.Color;
            set => _wrappee.Color = value;
        }

        public virtual string Type
        {
            get => _wrappee.Type;
            set => _wrappee.Type = value;
        }

        public int NumberOfStones { get; set; }

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
