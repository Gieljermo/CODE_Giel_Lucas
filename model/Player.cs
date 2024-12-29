using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Interfaces;
using TempleOfDoom.model.Observers;

namespace TempleOfDoom.model
{
    public class Player : IObservable
    {
        public int StartRoomId { get; set; }
        private int xPosition;
        private List<IInventoryObserver> inventoryObservers = new List<IInventoryObserver>();
        private List<IHealthObserver> healthObservers = new List<IHealthObserver>();
        public int XPositon
        {
            get { return xPosition; }
            set { xPosition = value; }
        }
        private int yPosition;
        public int YPositon
        {
            get { return yPosition; }
            set { yPosition = value; }
        }
        public int AmountOfLives { get; set; }
        public int AmountOfStones { get; set; }

        public List<IItem> Inventory { get; set; }

        public Player(int startRoomId, int startX, int startY, int lives)
        {
            this.StartRoomId = startRoomId;
            this.xPosition = startX;
            this.yPosition = startY;
            this.AmountOfLives = lives;
            Inventory = new List<IItem>();
        }

        public void Move(int x, int y, Room room)
        {
            Field fieldToMoveTo = room.Fields.Where(f => f.Y == this.YPositon + y).FirstOrDefault(f => f.X == this.XPositon + x);
            if (fieldToMoveTo != null && !fieldToMoveTo.IsWall)
            {
                this.xPosition += x;
                this.yPosition += y;
            }
        }

        public void takeDamage(int amount)
        {
            this.AmountOfLives -= amount;
            NotifyHealthObservers();
        }


        internal void AddItemToInvetory(IItem item)
        {
            this.Inventory.Add(item);
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
                observer.OnHealthChanged(this.AmountOfLives);
            }
        }
        public void NotifyInventoryObservers()
        { 
            foreach (var observer in inventoryObservers)
            {
                observer.onInventoryChange(this.Inventory);
            }
        }
    }
}
