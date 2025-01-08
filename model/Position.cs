using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.model
{
    public class Position : IPosition
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
        public bool Equals(IPosition other)
        {
            if (other == null) return false;
            return this.X == other.X && this.Y == other.Y;
        }

        //handle comparisons with other objects.
        public override bool Equals(object? obj)
        {
            if (obj is IPosition other)
            {
                return Equals(other);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
