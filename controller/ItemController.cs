using Domain;
using Domain.Factory;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;

namespace Controlllers
{
    public class ItemController
    {
        private ItemFactory _itemFactory = new ItemFactory();
        public IItem CreateItem(Item item)
        {
            var createdItem = _itemFactory.CreateItem(item.Type);
            if(item.Damage != null)
            {
                if(createdItem is Boobytrap)
                {
                    ((Boobytrap) createdItem).Damage = item.Damage;
                } else if(createdItem is DisappearingBoobytrap)
                {
                    ((DisappearingBoobytrap)createdItem).Damage = item.Damage;
                }
            }
            return createdItem;
        }
    }
}
