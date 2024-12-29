using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using TempleOfDoom.model;

namespace Domain
{
    public class DisappearingBoobytrap : Boobytrap
    {
        public override void Interact(Player player, Field field, Room room)
        {
            player.takeDamage(Damage);
            field.RemoveItem();
        }
    }
}
