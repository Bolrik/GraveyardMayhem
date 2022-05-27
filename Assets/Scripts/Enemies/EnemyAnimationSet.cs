using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "EnemyAnimationSet", menuName = "Visuals/new Enemy Animation Set")]
    class EnemyAnimationSet : ScriptableObject
    {
        [SerializeField] private AnimationSet animationSet;
        public AnimationSet AnimationSet { get { return animationSet; } }

        [SerializeField] private EnemyAnimationSet[] successors;
        public EnemyAnimationSet[] Successors { get { return successors; } }

    }
}