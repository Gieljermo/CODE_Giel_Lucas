using Domain.Interfaces;
using System;
using System.Collections.Generic;
using TempleOfDoom.model;

namespace Domain.Factory
{
    public class ItemFactory
    {
        private readonly Dictionary<string, Func<IItem>> _itemCreators = new()
        {
            ["boobytrap"] = () => new Boobytrap { Type = "boobytrap" },
            ["disappearing boobytrap"] = () => new DisappearingBoobytrap { Type = "disappearing boobytrap" },
            ["sankara stone"] = () => new SankaraStone { Type = "sankara stone" },
            ["key"] = () => new Key { Type = "key" },
            ["pressure plate"] = () => new PressurePlate { Type = "pressure plate" },
        };

        public IEnumerable<string> ItemNames => _itemCreators.Keys;

        public IItem CreateItem(ItemJson item)
        {
            if (!_itemCreators.TryGetValue(item.type, out var itemCreator))
            {
                throw new NotSupportedException($"Item {item.type} is not supported.");
            }

            var createdItem = itemCreator.Invoke();

            createdItem.X = item.x;
            createdItem.Y = item.y;

            // Assign properties based on item type
            AssignProperties(createdItem, item);

            return createdItem;
        }

        private void AssignProperties(IItem createdItem, ItemJson item)
        {
            switch (createdItem)
            {
                case Boobytrap boobytrap:
                    boobytrap.Damage = item.damage;
                    break;

                case DisappearingBoobytrap disappearingBoobytrap:
                    disappearingBoobytrap.Damage = item.damage;
                    break;

                case Key key:
                    key.Color = item.color;
                    break;
            }
        }
    }
}
