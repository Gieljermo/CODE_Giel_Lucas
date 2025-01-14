using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.model.Interfaces
{
    public interface IFileReader
    {
        public TempleOfDoomGameJson ReadFile(string file);
    }
}
