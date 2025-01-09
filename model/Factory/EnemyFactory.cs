using CODE_TempleOfDoom_DownloadableContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Interfaces;

namespace TempleOfDoom.model.Factory
{
    public static class EnemyFactory
    {
        private const int AMOUNT_OF_LIVES = 1;

        public static Enemy CreateEnemy(string type, IPosition position, int minX, int maxX, int minY, int maxY)
        {
            return type switch
            {
                "horizontal" => new HorizontallyMovingEnemy(AMOUNT_OF_LIVES, position.X, position.Y, minX, maxX),
                "vertical" => new VerticallyMovingEnemy(AMOUNT_OF_LIVES, position.X, position.Y, minY, maxY),
                _ => throw new ArgumentException($"Invalid enemy type: {type}")
            };
        }
    }
}
