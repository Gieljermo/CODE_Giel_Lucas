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
    public class Door : IDoor, IDrawable
    {
        public string Color { get; set; }
        private bool _isOpen;
        public bool IsOpen { get => _isOpen; set => _isOpen = value; }
        public string GetColor() => "green";
        public char? GetSymbol() => '=';

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
