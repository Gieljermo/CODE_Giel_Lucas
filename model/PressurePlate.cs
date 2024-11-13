using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using TempleOfDoom.model;

namespace Domain
{
    public class PressurePlate : IItem
    {
        private string _type;
        public string Type { get => _type; set => _type = value; }

        public void Interact(Player player, Field field)
        {
            Console.WriteLine("Sorry de pressure plate werkt niet omdat er ook geen deuren zijn :(");
        }
    }
}
