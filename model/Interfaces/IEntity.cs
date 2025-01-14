using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempleOfDoom.model.Interfaces
{
    public interface IEntity
    {
        int X { get; }
        int Y { get; }
        int AmountOfLives { get;}
        bool isDead();
        void takeDamage(int damage);

        void move(int x, int y, Room room);
    }
}
