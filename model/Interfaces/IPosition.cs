using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.model.Interfaces
{
    public interface IPosition
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool Equals(object obj);
    }
}
