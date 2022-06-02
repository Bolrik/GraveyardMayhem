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
        [SerializeField] private SpriteRenderer renderer;
        public SpriteRenderer Renderer { get { return renderer; } }

        [SerializeField] private WeaponData data;
        private WeaponData Data { get { return data; } set { data = value; } }

        float Cooldown { get; set; }

        WeaponSpriteAnimator SpriteAnimator { get; set; }
        WeaponTransformAnimator TransformAnimator { get; set; }

        public bool Fire(Transform view, out RaycastHit[] hits)
        {
            hits = new RaycastHit[0];

            if (this.Data == null)
            {
                return false;
            }

            if (this.Cooldown > 0)
            {
                this.PlayCooldownSound();
                return false;
            }

            List<RaycastHit> hitInfo = new List<RaycastHit>();

            int bullets = this.Data.Bullets;
            for (int i = 0; i < bullets; i++)
            {
                float spread = UnityEngine.Random.Range(-this.Data.Spread, this.Data.Spread);
                Vector3 direction = view.forward + view.right * spread + view.up * spread;
                
                if (Physics.Raycast(view.position, direction, out RaycastHit hit, this.Data.Distance, this.Data.HitLayer))
                {
                    if (!(hit.collider.GetComponent<HitBox>() is HitBox hitBox))
                        continue;

                    hitBox.Hit(this.Data);
                    hitInfo.Add(hit);
                }
            }

            this.SpriteAnimator.ReStart();
            this.TransformAnimator.ReStart();

            this.Cooldown = this.Data.Cooldown;
            hits = hitInfo.ToArray();
            return hits.Length > 0;
        }

        public void Update()
        {
            this.Cooldown = Mathf.Clamp(this.Cooldown - Time.deltaTime, 0, this.Cooldown);

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

            this.Renderer.transform.localEulerAngles = this.TransformAnimator.Update(this.Renderer.transform.localEulerAngles, this.Cooldown);
        }

        void UpdateSpriteAnimator()
        {
            if (this.SpriteAnimator == null)
                this.SpriteAnimator = new WeaponSpriteAnimator(this.Data);

            this.Renderer.sprite = this.SpriteAnimator.Update();
        }
    }

    class WeaponSpriteAnimator
    {
        public Sprite[] Sprites { get; private set; }
        WeaponData Data { get; set; }

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

            List<Sprite> frames = new List<Sprite>();
            frames.AddRange(data.Frames);
            frames.Reverse();
            frames.InsertRange(0, data.Frames.Take(data.Frames.Length - 1));
            this.Sprites = frames.ToArray();
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

                Debug.Log(this.Index);
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
        WeaponData Data { get; set; }

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
            Debug.Log("CD");

            Vector3 cooldownAngle = Vector3.Lerp(this.Data.DefaultAngle, this.Data.CooldownAngle, cooldownPercent);

            if (this.Direction)
            {
                this.Time += UnityEngine.Time.deltaTime * (1f / this.Data.RecoilTime);
                Debug.Log($"A: {this.Time}");
                toReturn = Vector3.Lerp(currentAngle, cooldownAngle, this.Time);

                if (this.Time >= 1)
                {
                    this.Direction = false;
                    this.Time = 0;
                }

                Debug.Log("A");
            }
            else
            {
                this.Time = cooldownPercent;
                Debug.Log($"B: {this.Time}");

                toReturn = Vector3.Lerp(this.Data.DefaultAngle, cooldownAngle, this.Time);

                if (this.Time >= 1)
                {
                    this.IsActive = false;
                    Debug.Log("~B");
                }
                Debug.Log("B");
            }

            return toReturn;
        }

        public void SetData(WeaponData data)
        {
            this.Data = data;
        }
    }
}
