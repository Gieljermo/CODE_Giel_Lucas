using System.Text.Json;
using TempleOfDoom.controller;
using TempleOfDoom.model;

namespace TempleOfDoom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string FileName = "resources/TempleOfDoom.json";
            TempleOfDoomGameJson gameData = JsonSerializer.Deserialize<TempleOfDoomGameJson>(File.ReadAllText(FileName));
            GameController gameController = new GameController(gameData);
            Console.WriteLine( gameController.TempleOfDoomGame);
        }
    }
}
