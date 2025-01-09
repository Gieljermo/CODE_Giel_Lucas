using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using TempleOfDoom.model;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Enums;
using TempleOfDoom.model.Interfaces;
using TempleOfDoom.model.Observers;

namespace Domain.Decorators
{
    public class DoorColorDecorator : BaseDoorDecorator, IInventoryObserver
    {

        private string _color;
        private bool _hasKey;
        private char _symbol;

        public override string GetColor()
        {
            if (_color == "red")
            {
                _symbol = '|';
                return "red";
            }
            else if (_color == "green")
            {
                _symbol = '=';
                return "green";
            }
            else
            {
                return "white";
            }
        }

        public override char? GetSymbol() => _symbol;

        public DoorColorDecorator(IDoor wrappee, string color) : base(wrappee)
        {
            this._color = color;
            this._hasKey = false;
        }

        public void OnInventoryChange(List<Item> inventory)
        {
            _hasKey = HasKeyInInventory(inventory);
        }

        public override void ChangeDoorStatus(IEntity player, Room room)
        {
            if (_hasKey)
            {
                base.ChangeDoorStatus(player, room);
            }
        }

        private bool HasKeyInInventory(List<Item> inventory)
        {
            // Check if the inventory contains a key of the right color
            return inventory.OfType<Key>().Any(k => string.Equals(k.Color.ToString(), _color.ToString(), StringComparison.OrdinalIgnoreCase));
        }

    }

}
