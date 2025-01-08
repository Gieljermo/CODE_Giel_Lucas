using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using TempleOfDoom.model;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Interfaces;

namespace Domain
{
    public class Key : Item
    {
        private string _type;
        public string Type { get => _type; set => _type = value; }
        public string Color { get; set; }
        public override string GetColor() => Color;

        public Key(string type, IPosition position, string color)
           : base(type, position)
        {
            this.Color = color;
        }

        public override void Interact(IEntity entity, Field field)
        {
            entity.AddItemToInvetory(this);
            field.RemoveItem();
        }
    }
}
