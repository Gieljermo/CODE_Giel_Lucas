using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Interfaces;
using TempleOfDoom.model.Observers;

namespace TempleOfDoom.model.Adapter
{
    public class Player : Entity, IObservable
    {
        public int StartRoomId { get; set; }

        private List<IInventoryObserver> inventoryObservers = new List<IInventoryObserver>();
        private List<IHealthObserver> healthObservers = new List<IHealthObserver>();

        public List<Item> Inventory { get; set; }

        public Player(int startRoomId, int startX, int startY, int lives)
        {
            StartRoomId = startRoomId;
            Position = new Position(startX, startY);
            Lives = lives;
            Inventory = new List<Item>();
        }

        public override void Move(IPosition position, Room room)
        {
            Field fieldToMoveTo = room.Fields.Where(f => f.Position.Y == Position.Y + position.Y && f.Position.X == Position.X + position.X).FirstOrDefault();
            if (fieldToMoveTo != null && !fieldToMoveTo.IsWall)
            {
                Position = fieldToMoveTo.Position;
            }
        }


        public override void TakeDamage(int amount)
        {
            Lives -= amount;
            NotifyHealthObservers();
        }

        public override void AddItemToInvetory(Item item)
        {
            Inventory.Add(item);
            NotifyInventoryObservers();
        }

        public void RegisterObserverHealth(IHealthObserver healthobserver)
        {
            healthObservers.Add(healthobserver);
        }

        public void RegisterObserverInventory(IInventoryObserver inventoryObserver)
        {
            inventoryObservers.Add(inventoryObserver);
        }

        public void UnregisterObserverInventory(IInventoryObserver inventoryObserver)
        {
            inventoryObservers.Remove(inventoryObserver);
        }

        public void UnregisterObserverHealth(IHealthObserver healthObserver)
        {
            healthObservers.Remove(healthObserver);
        }

        public void NotifyHealthObservers()
        {
            foreach (var observer in healthObservers)
            {
                observer.OnHealthChanged(Lives);
            }
        }
        public void NotifyInventoryObservers()
        {
            foreach (var observer in inventoryObservers)
            {
                observer.OnInventoryChange(Inventory);
            }
        }


    }
}
