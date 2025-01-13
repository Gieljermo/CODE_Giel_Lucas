using System.Text.Json;
using TempleOfDoom.controller;
using TempleOfDoom.model;
using TempleOfDoom.model.Interfaces;
using TempleOfDoom.model.Readers;

namespace TempleOfDoom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = "resources/TempleOfDoom_Extended_A.json";


            IFileReader fileReader = new JsonFileReader();

            GameController gameController = new GameController(fileReader, fileName);
        }
    }
}
