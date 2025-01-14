using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.model.Interfaces
{
    public interface ISpecialTileBehaviour
    {
        void OnEnter(IEntity slider, int direction, Room room);
    }
}
