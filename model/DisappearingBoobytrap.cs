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
    public class DisappearingBoobytrap : Boobytrap
    {
        public DisappearingBoobytrap(string type, IPosition position, int damage)
           : base(type, position, damage)
        {
            this.Type = type;
        }

        public override void Interact(IEntity entity, Field field, Room room)
        {
            entity.takeDamage(Damage);
            field.RemoveItem();
        }
    }
}
