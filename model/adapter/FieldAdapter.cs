using CODE_TempleOfDoom_DownloadableContent;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.model.adapter
{
    public class FieldAdapter : IField
    {
        private readonly Field _adaptee;

        public FieldAdapter(Field adaptee)
        {
            _adaptee = adaptee;
        }


        public bool CanEnter => _adaptee.IsWall;

        public IPlacable Item { get; set; }

        public IField? GetNeighbour(int direction)
        {
            var neighbor = _adaptee.GetNeighbor(direction);

            return neighbor != null ? new FieldAdapter(neighbor) : null;
        }

    }
}
