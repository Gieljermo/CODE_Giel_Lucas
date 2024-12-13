using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using TempleOfDoom.model;

namespace Domain
{
    public class Key : IItem
    {
        private string _type;
        public string Type { get => _type; set => _type = value; }
        public string Color { get; set; } 
        public void Interact(Player player, Field field, Room room)
        {
            player.AddItemToInvetory(this);
            field.RemoveItem();
        }
    }
}
