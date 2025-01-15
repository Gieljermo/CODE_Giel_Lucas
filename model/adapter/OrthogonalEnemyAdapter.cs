using CODE_TempleOfDoom_DownloadableContent;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Interfaces;
using TempleOfDoom.model.Observers;

namespace TempleOfDoom.model.adapter
{
    public class OrthogonalEnemyAdapter : IMovementObserver, IEntity
    {
        private readonly Enemy _adaptee;
        private FieldAdapter _fieldAdapter;

        public OrthogonalEnemyAdapter(Enemy adaptee)
        {
            this._adaptee = adaptee;
        }

        public void setCurrentField(Field field)
        {
            _fieldAdapter = new FieldAdapter(field);
            _adaptee.CurrentField = _fieldAdapter;
        }

        public int X => _adaptee.CurrentXLocation;

        public int Y => _adaptee.CurrentYLocation;

        public int AmountOfLives => _adaptee.NumberOfLives;

        public bool isDead()
        {
            if (_adaptee.NumberOfLives > 0)
            {
                return false;
            }

            return true;

        }

        public void move(int x, int y, Room room)
        {
            if(room.Enemies.Count > 0)
            {
                if (_adaptee != null)
                {
                    _adaptee.Move();
                }
            }

            if(room.SpecialFloorTiles != null)
            {
                Field currentNewField = room.Fields.Where(f => f.Y == this.Y).FirstOrDefault(f => f.X == this.X);
                if (currentNewField.SpecialTileBehaviour != null)
                {
                    //direction is 0 because it is set in the move method of the enemy
                    currentNewField.SpecialTileBehaviour.OnEnter(this, 0, room);
                }
            }
        }

        public void onMovementChanged(Room room)
        {
            this.move(0, 0, room);
        }

        public void takeDamage(int damage)
        {
            _adaptee.DoDamage(damage);
        }
    }
}
