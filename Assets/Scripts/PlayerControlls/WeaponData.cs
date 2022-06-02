using UnityEngine;

namespace PlayerControlls
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Settings/new Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private LayerMask hitLayer;
        public LayerMask HitLayer { get { return hitLayer; } }

        [SerializeField] private float spread;
        public float Spread { get { return spread; } }

        [SerializeField] private int bullets;
        public int Bullets { get { return bullets; } }

        [SerializeField] private float distance;
        public float Distance { get { return distance; } }

        [SerializeField] private float cooldown;
        public float Cooldown { get { return cooldown; } }

        [SerializeField] private float recoilTime;
        public float RecoilTime { get { return recoilTime; } }

        [SerializeField] private Color spriteTint;
        public Color SpriteTint { get { return spriteTint; } }



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
