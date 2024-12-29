using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;

namespace Domain
{
    public class Field
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Room { get; set; }
        public IItem Item { get; set; }
        public IDoor Door { get; set; }
        public bool IsWall { get; set; }
        public int IsConnection { get; set; }
        public Connection? Connection { get; set; }

        public Field()
        {
        }

        public void RemoveItem()
        {
            this.Item = null;
        }


    }
}
