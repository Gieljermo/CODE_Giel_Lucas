using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;

namespace Domain.Interfaces
{
    public interface IDoor
    {
        bool IsOpen { get; set; }
        void OpenDoor(Player player);
    }
}
