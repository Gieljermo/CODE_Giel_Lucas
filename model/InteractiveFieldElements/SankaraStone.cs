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
    public class SankaraStone : Item
    {
        public string Color { get; set; }

        public SankaraStone(string type, IPosition position, string color)
           : base(type, position)
        {
            this.Type = type;
            this.Color = color;
        }

        public override void Interact(IEntity entity, Field field)
        {
            entity.AmountOfStones++;
            field.RemoveItem();
        }
    }
}
