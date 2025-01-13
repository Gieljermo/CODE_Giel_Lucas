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
    public class OrthogonalEnemyAdapter : IEnemy, IMovementObserver
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

        public bool isDead()
        {
            if (_adaptee.NumberOfLives > 0)
            {
                return false;
            }

            return true;

        }

        public void Move()
        {
            if (_adaptee != null)
            {
                _adaptee.Move();
            }
        }

        public void onMovementChanged()
        {
            this.Move();
        }
    }
}
