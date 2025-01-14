using Domain.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Interfaces;
using CODE_TempleOfDoom_DownloadableContent;
using TempleOfDoom.model.adapter;

namespace TempleOfDoom.model.Factory
{
    public class EnemyFactory
    {
        private readonly Dictionary<string, Func<EnemyJson, IEntity>> _enemyCreators = new()
        {
            ["horizontal"] = enemyJson => new OrthogonalEnemyAdapter(
                new HorizontallyMovingEnemy(1, enemyJson.x, enemyJson.y, enemyJson.minX, enemyJson.maxX)
            ),
            ["vertical"] = enemyJson => new OrthogonalEnemyAdapter(
                new VerticallyMovingEnemy(1, enemyJson.x, enemyJson.y, enemyJson.minY, enemyJson.maxY)
            )
        };

        public IEntity CreateEnemy(EnemyJson enemy)
        {
            if (_enemyCreators.TryGetValue(enemy.type, out var creator))
            {
                return creator(enemy);
            }
            throw new ArgumentException($"Unknown enemy type: {enemy.type}");
        }
    }
}

