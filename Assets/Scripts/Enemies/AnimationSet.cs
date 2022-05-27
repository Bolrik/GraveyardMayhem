using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "AnimationSet", menuName = "Visuals/new Animation Set")]
    class AnimationSet : ScriptableObject
    {
        [SerializeField] private Sprite[] frames;
        public Sprite[] Frames { get { return frames; } }
    }
}