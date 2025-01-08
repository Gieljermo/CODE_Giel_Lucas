using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Observers;

namespace TempleOfDoom.model.Interfaces
{
    public interface IObservable
    {
        public void RegisterObserverHealth(IHealthObserver healthobserver);
        public void RegisterObserverInventory(IInventoryObserver inventoryObserver);
        public void UnregisterObserverInventory(IInventoryObserver inventoryObserver);
        public void UnregisterObserverHealth(IHealthObserver healthObserver);
        public void NotifyHealthObservers();
        public void NotifyInventoryObservers();
    }
}
