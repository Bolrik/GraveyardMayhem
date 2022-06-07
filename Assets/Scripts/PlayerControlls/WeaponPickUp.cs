using Misc;
using Misc.Interfaces;
using System;
using UnityEngine;

namespace PlayerControlls
{
    public class WeaponPickUp : MonoBehaviour, IDespawn<WeaponPickUp>
    {
        [Header("References")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }

        [SerializeField] private ParticleSystem particle;
        public ParticleSystem Particle { get { return particle; } }


        [Header("Settings")]
        [SerializeField] private WeaponData data;
        public WeaponData Data { get { return data; } private set { data = value; } }

        float Time { get; set; }

        public Action<WeaponPickUp> OnDespawn { get; set; }

        public void SetData(WeaponData data)
        {
            this.Data = data;
            this.SpriteRenderer.sprite = this.Data.Frames[0];
        }

        private void Update()
        {
            this.Time += UnityEngine.Time.deltaTime;

            Vector3 position = this.SpriteRenderer.transform.localPosition;
            position.y = .4f + Mathf.Sin(this.Time) * .2f;
            this.SpriteRenderer.transform.localPosition = position;


            Vector3 rotation = this.SpriteRenderer.transform.localEulerAngles;
            rotation.y = this.Time * 20f;
            this.SpriteRenderer.transform.localEulerAngles = rotation;

            if (this.Time >= 20)
            {
                if (this.Time % 2 > 1)
                {
                    if (this.Particle.isPlaying)
                        this.Particle.Stop();
                }
                else
                {
                    if (!this.Particle.isPlaying)
                        this.Particle.Play();
                }
            }

            if (this.Time >= 30)
                this.Despawn();
        }

        public void Despawn()
        {
            GameObject.Destroy(this.gameObject);

            this.OnDespawn(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!(other.transform.GetComponent<PickUpTrigger>() is PickUpTrigger player))
                return;

            player.Player.PickUp(this.Data);

            this.Despawn();
        }
    }
}
