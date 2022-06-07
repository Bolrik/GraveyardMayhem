using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "EnemyAnimationSet", menuName = "Visuals/new Enemy Animation Set")]
    public class EnemyAnimationSet : ScriptableObject
    {
        [SerializeField] private AnimationSet animationSet;
        public AnimationSet AnimationSet { get { return animationSet; } }

        [SerializeField] private EnemyAnimationSet[] successors;
        public EnemyAnimationSet[] Successors { get { return successors; } }

        public EnemyAnimationSet GetRandomSuccessor(out bool success)
        {
            success = false;

            if (this.Successors.Length == 0)
                return this;

            success = true;
            return this.Successors[UnityEngine.Random.Range(0, this.Successors.Length)];
        }
    }
}