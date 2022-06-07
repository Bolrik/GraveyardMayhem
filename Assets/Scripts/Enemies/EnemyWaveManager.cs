using Input;
using Misc.AssetVariables;
using Motion;
using PlayerControlls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Enemies
{
    public class EnemyWaveManager : MonoBehaviour
    {
        [SerializeField] private EnemyWaveManagerData data;
        public EnemyWaveManagerData Data { get { return data; } }


        [SerializeField] private bool isActive;
        public bool IsActive { get { return isActive; } private set { isActive = value; } }

        [SerializeField] private int wave;
        public int Wave { get { return wave; } private set { wave = value; } }

        [SerializeField] private FloatVariable countdown;
        public FloatVariable Countdown { get { return countdown; } }

        [SerializeField] private PlayerReference player;
        public PlayerReference Player { get { return player; } }



        List<Enemy> ActiveEnemies { get; set; } = new List<Enemy>();


        private void Start()
        {
            this.Countdown.Value = 3;
        }

        private void Update()
        {
            if (!this.IsActive)
                return;

            this.Countdown.Value -= Time.deltaTime;

            for (int i = this.ActiveEnemies.Count - 1; i >= 0; i--)
            {
                Enemy enemy = this.ActiveEnemies[i];

                if (enemy.IsAlive)
                    continue;

                this.ActiveEnemies.RemoveAt(i);
            }

            if (this.ActiveEnemies.Count <= 0)
            {
                this.Countdown.Value = Mathf.Min(this.Countdown, 3);
            }

            if (this.Countdown.Value <= 0)
            {
                this.Spawn();
            }
        }

        private void Spawn()
        {
            Dictionary<EnemySpawnData, int> spawns = this.GetSpawns();

            foreach (var toSpawn in spawns)
            {
                Vector3 position = new Vector2();
                for (int i = 0; i < toSpawn.Value; i++)
                {
                    do
                    {
                        position.x = UnityEngine.Random.Range(-30, 30);
                        position.z = UnityEngine.Random.Range(-30, 30);

                    } while ((this.Player.Value.transform.position - position).magnitude < 8);

                    Enemy enemy = GameObject.Instantiate(this.Data.Prefab);
                    enemy.transform.position = position;
                    enemy.SetData(toSpawn.Key.Data);
                    this.ActiveEnemies.Add(enemy);
                }
            }

            this.Wave++;
            this.Countdown.Value = this.Data.Countdown;
        }

        private Dictionary<EnemySpawnData, int> GetSpawns()
        {
            int spawnCount = this.Data.SpawnCount;
            spawnCount += this.Wave / 4;

            Dictionary<EnemySpawnData, int> toReturn = new Dictionary<EnemySpawnData, int>();
            Dictionary<EnemySpawnData, float> ratios = new Dictionary<EnemySpawnData, float>();

            foreach (var enemy in this.Data.Enemies)
            {
                if (this.Wave < enemy.WaveMin ||
                    (enemy.WaveMax > 0 && this.Wave > enemy.WaveMax))
                    continue;

                float ratio = enemy.Ratio;
                ratio += (this.Wave - enemy.WaveMin) * enemy.RatioChangePerWave;

                ratios.Add(enemy, ratio);
            }

            float sum = ratios.Values.Sum();

            for (int c = 0; c < spawnCount; c++)
            {
                float random = UnityEngine.Random.value * sum;

                foreach (var spawnData in ratios)
                {
                    if (random <= spawnData.Value)
                    {
                        if (!toReturn.ContainsKey(spawnData.Key))
                            toReturn[spawnData.Key] = 0;

                        toReturn[spawnData.Key]++;
                        break;
                    }

                    random -= spawnData.Value;
                }
            }

            return toReturn;
        }
    }
}