using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Adapter;

namespace TempleOfDoom.model.Interfaces
{
    public interface IInteractiveFieldElement
    {
        public string Type { get; set; }
        public IPosition Position { get; set; }
        public void Interact(IEntity entity, Field field);
    }
}
