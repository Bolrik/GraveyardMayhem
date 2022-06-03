using UnityEngine;

namespace Decorations
{
    [CreateAssetMenu(fileName = "DecorationData", menuName = "Decorations/new Decoration Data")]
    public class DecorationData : ScriptableObject
    {
        [SerializeField] private DecorationSpriteStage[] stages;
        public DecorationSpriteStage[] Stages { get { return stages; } }

        [SerializeField] private float stageHitpoints;
        public float StageHitpoints { get { return stageHitpoints; } }
    }

    [System.Serializable]
    public class DecorationSpriteStage
    {
        [SerializeField] private Sprite sprite;
        public Sprite Sprite { get { return sprite; } }

        [SerializeField] private Vector3 hitBoxOffset;
        public Vector3 HitBoxOffset { get { return hitBoxOffset; } set { this.hitBoxOffset = value; } }

        [SerializeField] private Vector3 hitBoxScale;
        public Vector3 HitBoxScale { get { return hitBoxScale; } set { this.hitBoxScale = value; } }

        [SerializeField] private bool hitBoxEnabled = true;
        public bool HitBoxEnabled { get { return hitBoxEnabled; } }

    }
}