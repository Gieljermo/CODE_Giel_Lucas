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
        void RegisterObserverHealth(IHealthObserver healthobserver);
        void RegisterObserverInventory(IInventoryObserver inventoryObserver);
        void UnregisterObserverInventory(IInventoryObserver inventoryObserver);
        void UnregisterObserverHealth(IHealthObserver healthObserver);
        void NotifyHealthObservers();
        void NotifyInventoryObservers();
    }
}
