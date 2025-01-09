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
    public class Boobytrap : Item
    {
        public int Damage { get; set; }
        public override int GetDamage() => Damage;

        public override char? GetSymbol() => 'O';

        public Boobytrap(string type, IPosition position, int damage)
            : base(type, position)
        {
            this.Damage = damage;
        }

        public override void Interact(IEntity entity, Field field)
        {
            entity.TakeDamage(Damage);
        }
    }
}
