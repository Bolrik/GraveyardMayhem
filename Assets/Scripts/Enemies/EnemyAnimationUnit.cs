using UnityEngine;

namespace Enemies
{
    class EnemyAnimationUnit : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer head;
        public SpriteRenderer Head { get { return head; } }

        [SerializeField] private SpriteRenderer body;
        public SpriteRenderer Body { get { return body; } }

        [SerializeField] private SpriteRenderer feet;
        public SpriteRenderer Feet { get { return feet; } }

        [SerializeField] private Enemy owner;
        public Enemy Owner { get { return owner; } }


        private EnemyAnimationSet[] AnimationSets { get; set; } = new EnemyAnimationSet[3];

        public void SetHead(EnemyAnimationSet animationSet)
        {
            this.AnimationSets[0] = animationSet;
            this.UpdateHead();
        }

        public void SetBody(EnemyAnimationSet animationSet)
        {
            this.AnimationSets[1] = animationSet;
            this.UpdateBody();
        }
        
        public void SetFeet(EnemyAnimationSet animationSet)
        {
            this.AnimationSets[2] = animationSet;
            this.UpdateFeet();
        }


        public EnemyAnimationSet GetCurrentHeadSet()
        {
            return this.AnimationSets[0];
        }

        public EnemyAnimationSet GetCurrentBodySet()
        {
            return this.AnimationSets[1];
        }

        public EnemyAnimationSet GetCurrentFeetSet()
        {
            return this.AnimationSets[2];
        }


        private void LateUpdate()
        {
            this.UpdateHead();
            this.UpdateBody();
            this.UpdateFeet();
        }

        void UpdateHead()
        {
            this.Head.sprite = this.AnimationSets[0].AnimationSet.Frames[0];
        }

        void UpdateBody()
        {
            this.Body.sprite = this.AnimationSets[1].AnimationSet.Frames[0];
        }

        void UpdateFeet()
        {
            this.Feet.sprite = this.AnimationSets[2].AnimationSet.Frames[0];
        }
    }
}