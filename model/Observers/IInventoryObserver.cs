using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.model.Observers
{
    public interface IInventoryObserver
    {
        public void onInventoryChange(List<IItem> inventory);
    }
}
