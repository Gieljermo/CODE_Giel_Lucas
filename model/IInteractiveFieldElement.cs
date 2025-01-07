using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.model
{
    public interface IInteractiveFieldElement
    {
        string Type { get; set; }
        public IPosition Position { get; set; }
        void Interact(IEntity entity, Field field, Room room);
    }
}
