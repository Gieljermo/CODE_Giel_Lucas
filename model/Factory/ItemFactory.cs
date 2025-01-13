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

        private readonly Dictionary<string, Action<IItem, ItemJson>> _propertyAssigners = new()
        {
            ["boobytrap"] = (created, source) =>
            {
                if (created is Boobytrap boobytrap)
                {
                    boobytrap.Damage = source.damage;
                }
            },
            ["disappearing boobytrap"] = (created, source) =>
            {
                if (created is DisappearingBoobytrap disappearingBoobytrap)
                {
                    disappearingBoobytrap.Damage = source.damage;
                }
            },
            ["key"] = (created, source) =>
            {
                if (created is Key key)
                {
                    key.Color = source.color;
                }
            },
        };

        public IEnumerable<string> ItemNames => _itemCreators.Keys;

        public IItem CreateItem(ItemJson sourceItem)
        {
            if (!_itemCreators.TryGetValue(sourceItem.type, out var createFunc))
            {
                throw new NotSupportedException($"Item '{sourceItem.type}' is not supported.");
            }
            var createdItem = createFunc();

            createdItem.X = sourceItem.x;
            createdItem.Y = sourceItem.y;

   
            if (_propertyAssigners.TryGetValue(sourceItem.type, out var assignAction))
            {
                assignAction(createdItem, sourceItem);
            }

            return createdItem;
        }
    }
}