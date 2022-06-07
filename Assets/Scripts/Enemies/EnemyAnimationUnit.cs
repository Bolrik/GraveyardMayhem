using UnityEngine;

namespace Enemies
{
    public class EnemyAnimationUnit : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer head;
        public SpriteRenderer Head { get { return head; } }

        [SerializeField] private SpriteRenderer body;
        public SpriteRenderer Body { get { return body; } }

        [SerializeField] private SpriteRenderer feet;
        public SpriteRenderer Feet { get { return feet; } }


        [SerializeField] private Enemy owner;
        public Enemy Owner { get { return owner; } }

        int Frame { get; set; }
        float FrameTime { get; set; }


        private EnemyAnimationSet[] AnimationSets { get; set; } = new EnemyAnimationSet[3];

        public void UpdateHead(EnemyAnimationSet animationSet)
        {
            this.AnimationSets[0] = animationSet;
            this.UpdateHead();
        }

        public void UpdateBody(EnemyAnimationSet animationSet)
        {
            this.AnimationSets[1] = animationSet;
            this.UpdateBody();
        }
        
        public void UpdateFeet(EnemyAnimationSet animationSet)
        {
            this.AnimationSets[2] = animationSet;
            this.UpdateFeet();
        }

        public void SetHead(EnemyVisualData visualData)
        {
            Vector3 localPosition = this.Head.transform.localPosition;
            localPosition.y = visualData.HeadOffset;
            this.Head.transform.localPosition = localPosition;

            this.UpdateHead(visualData.GetRandomHead());
        }

        public void SetBody(EnemyVisualData visualData)
        {
            Vector3 localPosition = this.Body.transform.localPosition;
            localPosition.y = visualData.BodyOffset;
            this.Body.transform.localPosition = localPosition;

            this.UpdateBody(visualData.GetRandomBody());
        }

        public void SetFeet(EnemyVisualData visualData)
        {
            Vector3 localPosition = this.Feet.transform.localPosition;
            localPosition.y = visualData.FeetOffset;
            this.Feet.transform.localPosition = localPosition;

            this.UpdateFeet(visualData.GetRandomFeet());
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
            this.FrameTime += Time.deltaTime;

            if (this.FrameTime > .2f)
            {
                this.Frame++;
                this.FrameTime = 0;
            }

            this.UpdateHead();
            this.UpdateBody();
            this.UpdateFeet();
        }

        void UpdateHead()
        {
            this.Head.sprite = this.GetSprite(this.AnimationSets[0]);
        }

        void UpdateBody()
        {
            this.Body.sprite = this.GetSprite(this.AnimationSets[1]);
        }

        void UpdateFeet()
        {
            this.Feet.sprite = this.GetSprite(this.AnimationSets[2]);
        }

        Sprite GetSprite(EnemyAnimationSet set)
        {
            int length = set.AnimationSet.Frames.Length;
            return set.AnimationSet.Frames[this.Frame % length];
        }
    }
}