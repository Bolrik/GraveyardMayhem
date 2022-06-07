using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "EnemySpawnData", menuName = "Enemies/new Enemy Spawn Data")]
    public class EnemySpawnData : ScriptableObject
    {
        [SerializeField] private EnemyData data;
        public EnemyData Data { get { return data; } }

        [SerializeField] private float ratio;
        public float Ratio { get { return ratio; } }

        [SerializeField] private float ratioChangePerWave;
        public float RatioChangePerWave { get { return ratioChangePerWave; } }

        [SerializeField] private int waveMin;
        public int WaveMin { get { return waveMin; } }

        [SerializeField] private int waveMax;
        public int WaveMax { get { return waveMax; } }

    }
}