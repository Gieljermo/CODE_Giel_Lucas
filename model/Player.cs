using CODE_TempleOfDoom_DownloadableContent;
using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Observers;

namespace TempleOfDoom.model
{
    public class Player
    {
        public const int DAMAGE = 1;
        public int StartRoomId { get; set; }
        private int xPosition;
        private List<IInventoryObserver> inventoryObservers = new List<IInventoryObserver>();
        private List<IHealthObserver> healthObservers = new List<IHealthObserver>();
        private List<IMovementObserver> moveObservers = new List<IMovementObserver>();
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
            foreach (var enemy in moveObservers)
            {
                enemy.onMovementChanged();
            }

            Field fieldToMoveTo = room.Fields.Where(f => f.Y == this.YPositon + y).FirstOrDefault(f => f.X == this.XPositon + x);

            if (fieldToMoveTo != null && !fieldToMoveTo.IsWall)
            {
                var enemyOnField = room.Enemies.FirstOrDefault(e => e.X == fieldToMoveTo.X && e.Y == fieldToMoveTo.Y);

                if (enemyOnField != null)
                {
                    takeDamage(1);
                }

                this.xPosition += x;
                this.yPosition += y;
            }
        }

        public void AddInventoryObserver(IInventoryObserver observer)
        {
            inventoryObservers.Add(observer);
        }

        public void RemoveInvetoryObserver(IInventoryObserver observer)
        {
            inventoryObservers.Remove(observer);
        }

        public void AddHealthObserver(IHealthObserver observer)
        {
            healthObservers.Add(observer);
        }

        public void RemoveHealthObserver(IHealthObserver observer)
        {
            healthObservers.Remove(observer);
        }

        public void AddMovementObserver(IMovementObserver observer)
        {
            moveObservers.Add(observer);
        }

        public void RemoveMovementObserver(IMovementObserver observer)
        {
            moveObservers.Remove(observer);
        }

        public void takeDamage(int amount)
        {
            this.AmountOfLives -= amount;
            changeHealth();
        }

        private void changeHealth()
        {
            foreach(var observer in healthObservers)
            {
                observer.OnHealthChanged(this.AmountOfLives);
            }
        }

        public void changeInventory()
        {
            foreach (var observer in inventoryObservers) { 
                observer.onInventoryChange(this.Inventory);
            }
        }

        internal void AddItemToInvetory(IItem item)
        {
            this.Inventory.Add(item);
            changeInventory();
        }
    }
}
