using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "AnimationSet", menuName = "Visuals/new Animation Set")]
    public class AnimationSet : ScriptableObject
    {
        [SerializeField] private Sprite[] frames;
        public Sprite[] Frames { get { return frames; } }
    }
}