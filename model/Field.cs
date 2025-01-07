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
        public IInteractiveFieldElement InteractiveFieldElement { get; set; }
        public IDoor Door { get; set; }
        public bool IsWall { get; set; }
        public int IsConnection { get; set; }
        public Connection? Connection { get; set; }

        public bool CanEnter { get; }
        public IPlacable Item { get; set; }

        public Field()
        {
        }

        public void RemoveItem()
        {
            this.InteractiveFieldElement = null;
        }

        public IField GetNeighbour(int direction)
        {
            // Verkrijg de X en Y coördinaten van de huidige Position
            int x = Position.X;
            int y = Position.Y;

            // Bereken het indexnummer van het veld op basis van de huidige X en Y
            int index = y * Room.Width + x;

            // Bereken het nieuwe indexnummer op basis van de richting
            int neighbourIndex = -1;

            if (direction == 0) // Boven (North)
            {
                neighbourIndex = (y - 1) * Room.Width + x;
            }
            else if (direction == 1) // Rechts (East)
            {
                neighbourIndex = y * Room.Width + (x + 1);
            }
            else if (direction == 2) // Onder (South)
            {
                neighbourIndex = (y + 1) * Room.Width + x;
            }
            else if (direction == 3) // Links (West)
            {
                neighbourIndex = y * Room.Width + (x - 1);
            }

            // Zorg ervoor dat de index geldig is en binnen de grenzen van de room valt
            if (neighbourIndex >= 0 && neighbourIndex < Room.Fields.Count)
            {
                return Room.Fields[neighbourIndex];
            }
            else
            {
                // Indien de index buiten de grenzen valt, return het eerste veld of een ander fallback-veld
                return Room.Fields[0];
            }
        }
    }

}
