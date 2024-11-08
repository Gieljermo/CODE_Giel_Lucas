using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Factory
{
    public class ItemFactory
    {
        private Dictionary<string, Func<IItem>> _items = new Dictionary<string, Func<IItem>>()
        {
            ["boobytrap"] = () => new Boobytrap { Type = "boobytrap" },
            ["disappearing boobytrap"] = () => new DisappearingBoobytrap {Type = "disappearing boobytrap"},
            ["sankara stone"] = () => new SankaraStone { Type = "sankara stone"},
            ["key"] = () => new Key { Type = "key"},
            ["pressure plate"] = () => new PressurePlate { Type = "pressure plate"},
        };
        public IEnumerable<string> ItemNames => _items.Keys;

        public IItem CreateItem(string type)
        {
            Func<IItem> itemCreator;
            if (_items.TryGetValue(type, out itemCreator))
            {
                var item = itemCreator.Invoke();
                return item;
            }
            else
            {
                throw new NotSupportedException($"Item {type} is not supported.");
            }
        }
    }
}
