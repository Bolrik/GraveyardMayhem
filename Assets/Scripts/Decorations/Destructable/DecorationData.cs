using Misc;
using UnityEngine;

namespace Decorations
{
    [CreateAssetMenu(fileName = "DecorationData", menuName = "Decorations/new Decoration Data")]
    public class DecorationData : ScriptableObject, IScoreItem
    {
        [SerializeField] private DecorationSpriteStage[] stages;
        public DecorationSpriteStage[] Stages { get { return stages; } }

        [SerializeField] private float stageHitpoints;
        public float StageHitpoints { get { return stageHitpoints; } }

        [SerializeField] private float scoreValue;
        public float ScoreValue{ get { return scoreValue; } }

        [SerializeField] private Vector3 visualOffset;
        public Vector3 VisualOffset { get { return visualOffset; } }

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