using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;
using TempleOfDoom.model.Adapter;

namespace Domain.Interfaces
{
    public interface IDoor
    {
        public bool IsOpen { get; set; }
        public void OpenDoor(Player player, Room room);
    }
}
