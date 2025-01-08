using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.model
{
    public abstract class Item : IInteractiveFieldElement
    {
        public string Type { get; set; }
        public IPosition Position { get; set; }

        public virtual string GetColor() => "default";
        public virtual int GetDamage() => 0;

        public Item(string type, IPosition position)
        {
            this.Type = type;
            this.Position = position;
        }



        public virtual void Interact(IEntity entity, Field field)
        {
            return;
        }
    }
}
