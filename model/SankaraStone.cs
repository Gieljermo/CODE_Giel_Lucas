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
        private string _type;
        public string Type { get => _type; set => _type = value; }
        public int Damage { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Color { get; set; }

        public SankaraStone(string type, IPosition position, string color)
           : base(type, position)
        {
            this.Type = type;
            this.Color = color;
        }

        public override void Interact(IEntity entity, Field field, Room room)
        {
            entity.AmountOfStones++;
            field.RemoveItem();
        }
    }
}
