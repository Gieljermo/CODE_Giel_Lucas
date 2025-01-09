using CODE_TempleOfDoom_DownloadableContent;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;
using TempleOfDoom.model.Interfaces;

namespace Domain
{
    public class Field : IField
    {
        public IPosition Position { get; set; }
        public Room Room { get; set; }
        public IInteractiveFieldElement? InteractiveFieldElement { get; set; }
        public List<IDoor> Doors { get; set; }
        public bool IsWall { get; set; }
        public int IsConnection { get; set; }
        public Connection? Connection { get; set; }
        public bool CanEnter { get; }
        public IPlacable? Item { get; set; }

        public Field(Room room, IPosition position)
        {
            Room = room;
            Position = position;
        }

        public void RemoveItem()
        {
            this.InteractiveFieldElement = null;
        }

        public IField GetNeighbour(int direction)
        {
            return this;
        }
    }

}
