using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "EnemyWaveManagerData", menuName = "Manager/new Enemy Wave Manager Data")]
    public class EnemyWaveManagerData : ScriptableObject
    {
        [SerializeField] private EnemySpawnData[] enemies;
        public EnemySpawnData[] Enemies { get { return enemies; } }

        [SerializeField] private Enemy prefab;
        public Enemy Prefab { get { return prefab; } }

        [SerializeField] private float countdown;
        public float Countdown { get { return countdown; } }

        [SerializeField] private int spawnCount;
        public int SpawnCount { get { return spawnCount; } }
    }
}