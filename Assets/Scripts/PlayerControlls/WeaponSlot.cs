using Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayerControlls
{
    [Serializable]
    public class WeaponSlot
    {
        [Header("References")]
        [SerializeField] private SpriteRenderer renderer;
        public SpriteRenderer Renderer { get { return renderer; } }

        [SerializeField] private Transform animationTransform;
        public Transform AnimationTransform { get { return animationTransform; } }

        [SerializeField] private Transform pivot;
        public Transform Pivot { get { return pivot; } }

        [Header("Settings")]
        [SerializeField] private WeaponData data;
        private WeaponData Data { get { return data; } set { data = value; } }

        float Cooldown { get; set; }

        WeaponSpriteAnimator SpriteAnimator { get; set; }
        WeaponTransformAnimator TransformAnimator { get; set; }


        public Vector3 BulletOrigin
        {
            get => this.Renderer.transform.position +
                this.Renderer.transform.right * this.Data.BulletSpawnOffset.x +
                this.Renderer.transform.up * this.Data.BulletSpawnOffset.y +
                this.Renderer.transform.forward * this.Data.BulletSpawnOffset.z;
        }

        public float Range { get => this.Data.Distance;  }


        public bool Fire(Transform view, out ShotInfo[] hits)
        {
            hits = new ShotInfo[0];

            if (this.Data == null)
            {
                return false;
            }

            if (this.Cooldown > 0)
            {
                this.PlayCooldownSound();
                return false;
            }

            List<ShotInfo> toReturn = new List<ShotInfo>();
            Dictionary<HitBox, List<ShotInfo>> hitInfo = new Dictionary<HitBox, List<ShotInfo>>();

            int bullets = this.Data.Bullets;
            for (int i = 0; i < bullets; i++)
            {
                float spreadX = UnityEngine.Random.Range(-this.Data.Spread, this.Data.Spread);
                float spreadY = UnityEngine.Random.Range(-this.Data.Spread, this.Data.Spread);

                Vector3 direction = view.forward + view.right * spreadX + view.up * spreadY;
                Vector3 origin = view.position;

                if (Physics.Raycast(view.position, direction, out RaycastHit hit, this.Data.Distance, this.Data.HitLayer))
                {
                    if (!(hit.collider.GetComponent<HitBox>() is HitBox hitBox))
                        continue;

                    if (!hitInfo.ContainsKey(hitBox))
                        hitInfo.Add(hitBox, new List<ShotInfo>());

                    ShotInfo shotInfo = new ShotInfo(hit, origin, direction, true);

                    hitInfo[hitBox].Add(shotInfo);
                    toReturn.Add(shotInfo);
                }
                else
                    toReturn.Add(new ShotInfo(hit, origin, direction, false));
            }

            this.ResolveHits(hitInfo);

            this.SpriteAnimator.ReStart();
            this.TransformAnimator.ReStart();

            this.Cooldown = this.Data.Cooldown;
            hits = toReturn.ToArray();
            return hits.Length > 0;
        }

        private void ResolveHits(Dictionary<HitBox, List<ShotInfo>> hits)
        {
            foreach (var hit in hits)
            {
                HitBox box = hit.Key;

                box.Hit(this.Data, hit.Value.ToArray());
            }
        }

        public void Update()
        {
            this.Cooldown = Mathf.Clamp(this.Cooldown - Time.deltaTime, 0, this.Cooldown);

            this.Pivot.transform.localPosition = this.data.Pivot;
            this.AnimationTransform.transform.localPosition = this.Data.Offset;

            this.Renderer.color = this.Data.SpriteTint;

            this.UpdateTransformAnimator();
            this.UpdateSpriteAnimator();
        }

        private void PlayCooldownSound()
        {
            Debug.Log("Cooldown");
        }

        void UpdateTransformAnimator()
        {
            if (this.TransformAnimator == null)
                this.TransformAnimator = new WeaponTransformAnimator(this.Data);

            if (this.Data != this.TransformAnimator.Data)
                this.TransformAnimator.SetData(this.Data);

            this.AnimationTransform.localEulerAngles = this.TransformAnimator.Update(this.AnimationTransform.localEulerAngles, this.Cooldown);
        }

        void UpdateSpriteAnimator()
        {
            if (this.SpriteAnimator == null)
                this.SpriteAnimator = new WeaponSpriteAnimator(this.Data);

            if (this.Data != this.SpriteAnimator.Data)
                this.SpriteAnimator.SetData(this.Data);

            this.Renderer.sprite = this.SpriteAnimator.Update();
        }
    }

    class WeaponSpriteAnimator
    {
        public Sprite[] Sprites { get; private set; }
        public WeaponData Data { get; private set; }

        float Time { get; set; }
        int Index { get; set; }
        bool IsActive { get; set; }

        public WeaponSpriteAnimator(WeaponData data)
        {
            this.SetData(data);
        }

        public void SetData(WeaponData data)
        {
            this.Data = data;
            this.Sprites = data.Frames;
        }

        public void ReStart()
        {
            this.IsActive = true;
            this.Index = 0;
            this.Time = 0;
        }

        public Sprite Update()
        {
            if (this.Data.FrameTime == 0)
                return null;

            if (!this.IsActive)
            {
                this.Index = 0;

                return this.Sprites[this.Index];
            }

            this.Time += UnityEngine.Time.deltaTime;
            while (this.Time >= this.Data.FrameTime)
            {
                this.NextFrame();
            }

            return this.Sprites[this.Index];
        }

        private void NextFrame()
        {
            this.Index = (this.Index + 1) % this.Sprites.Length;
            this.Time -= this.Data.FrameTime;

            if (this.Index == 0)
                this.IsActive = false;
        }
    }

    class WeaponTransformAnimator
    {
        public WeaponData Data { get; private set; }

        float Time { get; set; }
        bool Direction { get; set; }
        bool IsActive { get; set; }

        public WeaponTransformAnimator(WeaponData data)
        {
            this.SetData(data);
        }

        public void ReStart()
        {
            this.Direction = true;
            this.Time = 0;
            this.IsActive = true;
        }

        public Vector3 Update(Vector3 currentAngle, float cooldown)
        {
            if (!this.IsActive)
                return currentAngle;

            Vector3 toReturn = currentAngle;

            float cooldownPercent = cooldown / this.Data.Cooldown;

            Vector3 cooldownAngle = Vector3.Lerp(this.Data.DefaultAngle, this.Data.CooldownAngle, cooldownPercent);

            if (this.Direction)
            {
                this.Time += UnityEngine.Time.deltaTime * (1f / this.Data.RecoilTime);

                toReturn = Vector3.Lerp(currentAngle, cooldownAngle, this.Time);

                if (this.Time >= 1)
                {
                    this.Direction = false;
                    this.Time = 0;
                }
            }
            else
            {
                this.Time = cooldownPercent;

                toReturn = Vector3.Lerp(this.Data.DefaultAngle, cooldownAngle, this.Time);

                if (this.Time >= 1)
                {
                    this.IsActive = false;
                }
            }

            return toReturn;
        }

        public void SetData(WeaponData data)
        {
            this.Data = data;
        }
    }

    public struct ShotInfo
    {
        public RaycastHit Hit { get; set; }
        public Vector3 Origin { get; set; }
        public Vector3 Direction { get; set; }
        public bool IsHit { get; set; }

        public ShotInfo(RaycastHit hit, Vector3 origin, Vector3 direction, bool isHit)
        {
            this.Hit = hit;
            this.Origin = origin;
            this.Direction = direction;
            this.IsHit = isHit;
        }
    }
}
