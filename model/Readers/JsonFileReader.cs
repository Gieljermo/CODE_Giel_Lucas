using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.model.Readers
{
    public class JsonFileReader : IFileReader
    {
        public TempleOfDoomGameJson ReadFile(string file)
        {
            string fileContent = File.ReadAllText(file);
            return JsonSerializer.Deserialize<TempleOfDoomGameJson>(fileContent);
        }
    }
}
