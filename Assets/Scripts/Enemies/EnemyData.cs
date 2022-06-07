using Misc;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/new Enemy Data")]
    public class EnemyData : ScriptableObject, IScoreItem
    {
        [SerializeField] private float knockbackReduction;
        public float KnockbackReduction { get { return knockbackReduction; } }

        [SerializeField] private float damageMultiplierHead = 1;
        public float DamageMultiplierHead { get { return damageMultiplierHead; } }
        
        [SerializeField] private float damageMultiplierBody = 1;
        public float DamageMultiplierBody { get { return damageMultiplierBody; } }
        
        [SerializeField] private float damageMultiplierFeet = 1;
        public float DamageMultiplierFeet { get { return damageMultiplierFeet; } }

        [Header("Stats")]
        [SerializeField] private float hitpoints;
        public float Hitpoints { get { return hitpoints; } }

        [SerializeField] private float hitPointsHead;
        public float HitPointsHead { get { return hitPointsHead; } }

        [SerializeField] private float hitPointsBody;
        public float HitPointsBody { get { return hitPointsBody; } }

        [SerializeField] private float hitPointsFeet;
        public float HitPointsFeet { get { return hitPointsFeet; } }


        [SerializeField] private float speed = 1;
        public float Speed { get { return speed; } }

        [SerializeField] private float speedPerWave;
        public float SpeedPerWave { get { return speedPerWave; } }


        [SerializeField] private float damage;
        public float Damage { get { return damage; } }

        [SerializeField] private EnemyVisualData visuals;
        public EnemyVisualData Visuals { get { return visuals; } }


        [SerializeField] private float scoreValue;
        public float ScoreValue { get { return scoreValue; } }

        [SerializeField] private AudioClip[] clips;
        public AudioClip[] Clips { get { return clips; } }




        [Header("Colliders")]
        [SerializeField] private Vector3 headOffset;
        public Vector3 HeadOffset { get { return headOffset; } }

        [SerializeField] private Vector3 headScale;
        public Vector3 HeadScale { get { return headScale; } }


        [SerializeField] private Vector3 bodyOffset;
        public Vector3 BodyOffset { get { return bodyOffset; } }

        [SerializeField] private Vector3 bodyScale;
        public Vector3 BodyScale { get { return bodyScale; } }


        [SerializeField] private Vector3 feetOffset;
        public Vector3 FeetOffset { get { return feetOffset; } }

        [SerializeField] private Vector3 feetScale;
        public Vector3 FeetScale { get { return feetScale; } }
    }
}