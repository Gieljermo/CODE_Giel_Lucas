using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Enums;
using TempleOfDoom.model.Interfaces;
using TempleOfDoom.model.Observers;

namespace TempleOfDoom.model.Adapter
{
    public class Player : Entity, IObservable
    {
        public int StartRoomId { get; set; }
        public int AmountOfStones { get; set; }
        public List<Item> Inventory { get; set; }

        private readonly List<IInventoryObserver> inventoryObservers;
        private readonly List<IHealthObserver> healthObservers;


        public Player(int startRoomId, int startX, int startY, int lives)
        {
            StartRoomId = startRoomId;
            Position = new Position(startX, startY);
            Lives = lives;
            Inventory = new List<Item>();
            inventoryObservers = new List<IInventoryObserver>();
            healthObservers = new List<IHealthObserver>();
        }

        public override void Move(IPosition position, Room room)
        {
            IPosition targetPosition = CalculateNewPosition(position);
            var fieldToMoveTo = room.Fields.FirstOrDefault(f => f.Position.Equals(targetPosition));

            if (fieldToMoveTo != null && !fieldToMoveTo.IsWall)
            {
                Position = fieldToMoveTo.Position;
            }
        }

        private IPosition CalculateNewPosition(IPosition position)
        {
            return new Position(Position.X + position.X, Position.Y + position.Y);
        }

        public override void TakeDamage(int amount)
        {
            Lives -= amount;
            NotifyHealthObservers();
        }

        public void AddItemToInvetory(Item item)
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
