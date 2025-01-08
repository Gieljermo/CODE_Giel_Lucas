using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.model
{
    public class Portal
    {
        public int RoomId { get; set; }
        public IPosition Position { get; set; }

        public Portal(int roomId, IPosition position) 
        {
            this.RoomId = roomId;
            this.Position = position;
        }

    }
}
