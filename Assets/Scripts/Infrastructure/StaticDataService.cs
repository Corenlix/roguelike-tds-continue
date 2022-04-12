using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure
{
    public class StaticDataService : IStaticDataService
    {
        private const string EnemiesDataPath = "Static Data/Enemies";
        private Dictionary<EnemyId, EnemyStaticData> _enemies;


        public StaticDataService()
        {
            Load();
        }

        public void Load()
        {
            _enemies = Resources
                .LoadAll<EnemyStaticData>(EnemiesDataPath)
                .ToDictionary(x => x.Id, x => x);
        }

        public EnemyStaticData ForEnemy(EnemyId id) => _enemies[id];
    }

    public enum EnemyId
    {
        Test,
    }
}