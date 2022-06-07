using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "EnemyVisualData", menuName = "Visuals/new Enemy Visual Data")]
    public class EnemyVisualData : ScriptableObject
    {
        [SerializeField] private EnemyAnimationSet[] head_AnimationSets;
        public EnemyAnimationSet[] Head_AnimationSets { get { return head_AnimationSets; } }

        [SerializeField] private EnemyAnimationSet[] body_AnimationSets;
        public EnemyAnimationSet[] Body_AnimationSets { get { return body_AnimationSets; } }

        [SerializeField] private EnemyAnimationSet[] feet_AnimationSets;
        public EnemyAnimationSet[] Feet_AnimationSets { get { return feet_AnimationSets; } }


        [SerializeField] private float headOffset;
        public float HeadOffset { get { return headOffset; } }
        
        [SerializeField] private float bodyOffset;
        public float BodyOffset { get { return bodyOffset; } }
        
        [SerializeField] private float feetOffset;
        public float FeetOffset { get { return feetOffset; } }


        public EnemyAnimationSet GetRandomHead()
        {
            int index = UnityEngine.Random.Range(0, this.Head_AnimationSets.Length);
            return this.Head_AnimationSets[index];
        }

        public EnemyAnimationSet GetRandomBody()
        {
            int index = UnityEngine.Random.Range(0, this.Body_AnimationSets.Length);
            return this.Body_AnimationSets[index];
        }

        public EnemyAnimationSet GetRandomFeet()
        {
            int index = UnityEngine.Random.Range(0, this.Feet_AnimationSets.Length);
            return this.Feet_AnimationSets[index];
        }
    }
}