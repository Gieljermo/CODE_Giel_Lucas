using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.model.Factory
{
    public class SpecialTileBehaviourFactory
    {
        private readonly Dictionary<string, Func<ISpecialTileBehaviour>> _specialTileCreators = new()
        {
            ["ice"] = () => new IceTileBehaviour(),
        };

        public ISpecialTileBehaviour Create(string type)
        {
            if (_specialTileCreators.TryGetValue(type.ToLower(), out var creator))
            {
                return creator();
            }
            throw new ArgumentException($"Invalid special tile type: {type}");
        }
    }
}
