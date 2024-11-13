using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.model
{
    public class Door : IDoor
    {
        private bool _isOpen;
        public string Type { get; set; }
        public string? Color { get; set; }
        public bool IsOpen { get => _isOpen; set => _isOpen = value; }
        public int NumberOfStones { get; set; }

        public Door(string type, string? color, int no_of_stones)
        {
            this.Type = type;
            this.Color = color;
            this.NumberOfStones = no_of_stones;
        }

        public void OpenDoor(Player player)
        {
            this.IsOpen = true;
        }
    }
}
