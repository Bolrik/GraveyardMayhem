using UnityEngine;

namespace PlayerControlls
{
    [CreateAssetMenu(fileName = "WeaponSpawnData", menuName = "Weapons/new Weapon Spawn Data")]
    public class WeaponSpawnData : ScriptableObject
    {
        [SerializeField] private WeaponData weapon;
        public WeaponData Weapon { get { return weapon; } }

        [SerializeField] private float chance;
        public float Chance { get { return chance; } }
    }
}
