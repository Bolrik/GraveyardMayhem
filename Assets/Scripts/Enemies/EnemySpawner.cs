using Input;
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
    class EnemySpawn : MonoBehaviour
    {
        [SerializeField] private EnemySpawnData data;
        public EnemySpawnData Data { get { return data; } }


        float Cooldown { get; set; }

        private void Update()
        {
            this.Cooldown -= Time.deltaTime;

            if (this.Cooldown <= 0)
                this.Spawn();
        }

        private void Spawn()
        {


            this.Cooldown = this.Data.Cooldown;
        }
    }

    [CreateAssetMenu(fileName = "EnemySpawnData", menuName = "Enemies/new Enemy Spawn Data")]
    class EnemySpawnData : ScriptableObject
    {
        [SerializeField] private Enemy prefab;
        public Enemy Prefab { get { return prefab; } }

        [SerializeField] private float cooldown;
        public float Cooldown { get { return cooldown; } }

    }
}