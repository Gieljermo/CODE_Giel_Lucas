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

        private string _type;
        public string Type { get => _type; set => _type = value; }

        public int Damage { get; set; }
        public override int GetDamage() => Damage;



        public Boobytrap(string type, IPosition position, int damage)
            : base(type, position)
        {
            this.Type = type; 
            this.Damage = damage;
        }

        public override void Interact(IEntity entity, Field field, Room room)
        {
            entity.takeDamage(Damage);
        }
    }
}
