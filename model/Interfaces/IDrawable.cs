using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.model.Interfaces
{
    public interface IDrawable
    {
        public string GetColor();
        public char? GetSymbol();
    }
}
