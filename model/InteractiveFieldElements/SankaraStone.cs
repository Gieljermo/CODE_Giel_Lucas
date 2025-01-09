using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using TempleOfDoom.model;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace Domain
{
    public class SankaraStone : Item
    {
        public string Color { get; set; }
        public override string GetColor() => "magenta";
        public override char? GetSymbol() => 'S';

        public SankaraStone(string type, IPosition position, string color)
           : base(type, position)
        {
            this.Type = type;
            this.Color = color;
        }

        public override void Interact(IEntity entity, Field field)
        {
            if (entity is Player player)
            {
                player.AmountOfStones++;
            }

            field.RemoveItem();
        }
    }
}
