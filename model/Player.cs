using CODE_TempleOfDoom_DownloadableContent;
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
    public class Player : IEntity
    {
        public const int DAMAGE = 1;
        public int StartRoomId { get; set; }

        private List<IInventoryObserver> inventoryObservers = new List<IInventoryObserver>();
        private List<IHealthObserver> healthObservers = new List<IHealthObserver>();
        private List<IMovementObserver> moveObservers = new List<IMovementObserver>();

        public int AmountOfLives { get; private set; }
        public int AmountOfStones { get; set; }

        public List<IItem> Inventory { get; set; }



        public int X {  get; set; }

        public int Y { get; set; }

        public Player(int startRoomId, int startX, int startY, int lives)
        {
            this.StartRoomId = startRoomId;
            this.X = startX;
            this.Y = startY;
            this.AmountOfLives = lives;
            Inventory = new List<IItem>();
        }

        private static readonly Dictionary<int, (int dx, int dy)> DirectionOffsets = new()
        {
            { 0, (0, -1) }, // Up
            { 1, (1, 0) },  // Right
            { 2, (0, 1) },  // Down
            { 3, (-1, 0) }  // Left
        };

        public void move(int x, int y, Room room)
        {
            foreach (var enemy in moveObservers)
            {
                enemy.onMovementChanged(room);
            }

            Field fieldToMoveTo = room.Fields.Where(f => f.Y == this.Y + y).FirstOrDefault(f => f.X == this.X + x);

            if (fieldToMoveTo != null && !fieldToMoveTo.IsWall)
            {
                var enemyOnField = room.Enemies.FirstOrDefault(e => e.X == fieldToMoveTo.X && e.Y == fieldToMoveTo.Y);

                if (enemyOnField != null)
                {
                    takeDamage(1);
                }

                this.X += x;
                this.Y += y;

                if (fieldToMoveTo.SpecialTileBehaviour != null)
                {
                    // Determine the moving direction by matching the (x,y) offset with DirectionOffsets
                    int direction = DirectionOffsets.FirstOrDefault(pair => pair.Value.dx == x && pair.Value.dy == y).Key;
                    fieldToMoveTo.SpecialTileBehaviour.OnEnter(this, direction, room);
                }
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
            AmountOfLives -= amount;
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

        public bool isDead()
        {
            if (AmountOfLives > 0) return false;

            return true;
        }
    }
}
