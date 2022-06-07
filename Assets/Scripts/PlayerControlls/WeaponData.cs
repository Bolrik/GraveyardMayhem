using UnityEngine;

namespace PlayerControlls
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/new Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [Header("General")]
        [SerializeField] private LayerMask hitLayer;
        public LayerMask HitLayer { get { return hitLayer; } }

        [Header("Stats")]
        [SerializeField] private float spread;
        public float Spread { get { return spread; } }

        [SerializeField] private int bullets;
        public int Bullets { get { return bullets; } }

        [SerializeField] private float cooldown;
        public float Cooldown { get { return cooldown; } }

        [SerializeField] private float recoilTime;
        public float RecoilTime { get { return recoilTime; } }

        [SerializeField] private float distance;
        public float Distance { get { return distance; } }

        [SerializeField] private int ammo;
        public int Ammo { get { return ammo; } }

        [Header("Damage")]
        [SerializeField] private float bulletDamage;
        public float BulletDamage { get { return bulletDamage; } }

        [SerializeField] private float damageReductionPerDistance;
        public float DamageReductionPerDistance { get { return damageReductionPerDistance; } }

        [Header("Visuals")]
        [SerializeField] private Color spriteTint;
        public Color SpriteTint { get { return spriteTint; } }

        [SerializeField] private Vector3 bulletSpawnOffset;
        public Vector3 BulletSpawnOffset { get { return bulletSpawnOffset; } }

        [SerializeField] private Vector3 offset;
        public Vector3 Offset { get { return offset; } }

        [SerializeField] private Vector3 pivot;
        public Vector3 Pivot { get { return pivot; } }




        [Header("Animation")]
        [SerializeField] private Sprite[] frames;
        public Sprite[] Frames { get { return frames; } }

        [SerializeField] private float frameTime;
        public float FrameTime { get { return frameTime; } }

        [SerializeField] private Vector3 cooldownAngle;
        public Vector3 CooldownAngle { get { return cooldownAngle; } }

        [SerializeField] private Vector3 defaultAngle;
        public Vector3 DefaultAngle { get { return defaultAngle; } }
        
    }
}
