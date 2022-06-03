using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/new Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private float knockbackReduction;
        public float KnockbackReduction { get { return knockbackReduction; } }

        [SerializeField] private float damageMultiplierHead = 1;
        public float DamageMultiplierHead { get { return damageMultiplierHead; } }
        
        [SerializeField] private float damageMultiplierBody = 1;
        public float DamageMultiplierBody { get { return damageMultiplierBody; } }
        
        [SerializeField] private float damageMultiplierFeet = 1;
        public float DamageMultiplierFeet { get { return damageMultiplierFeet; } }

        [SerializeField] private float hitpoints;
        public float Hitpoints { get { return hitpoints; } }


    }
}