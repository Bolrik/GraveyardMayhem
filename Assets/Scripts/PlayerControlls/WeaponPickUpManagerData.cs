using System.Linq;
using UnityEngine;

namespace PlayerControlls
{
    [CreateAssetMenu(fileName = "WeaponPickUpManagerData", menuName = "Weapons/ new Weapon Pick Up Manager Data")]
    public class WeaponPickUpManagerData : ScriptableObject
    {
        [SerializeField] private WeaponSpawnData[] weapons;
        public WeaponSpawnData[] Weapons { get { return weapons; } }

        [SerializeField, Tooltip("The Prefab")] private WeaponPickUp weapon;
        public WeaponPickUp Weapon { get { return weapon; } }

        [SerializeField] private float spawnTime;
        public float SpawnTime { get { return spawnTime; } }


        public WeaponData GetWeaponData()
        {
            int idx = 0;

            float sum = this.Weapons.Sum(weapon => weapon.Chance);
            float rng = Random.value * sum;

            for (int i = 0; i < this.Weapons.Length; i++)
            {
                idx = i;
                if (rng <= this.Weapons[i].Chance)
                    return this.Weapons[i].Weapon;
            }

            return this.GetWeaponData(idx);
        }

        public WeaponData GetWeaponData(int idx)
        {
            return this.Weapons[idx].Weapon;
        }
    }
}
