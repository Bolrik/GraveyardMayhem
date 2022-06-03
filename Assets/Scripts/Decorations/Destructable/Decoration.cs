using Misc;
using PlayerControlls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Decorations
{
    public class Decoration : MonoBehaviour, IHitObserver
    {
        [Header("References")]
        [SerializeField] private SpriteRenderer renderer;
        public SpriteRenderer Renderer { get { return renderer; } }

        [SerializeField] private ParticleSystem onHitParticleSystem;
        public ParticleSystem OnHitParticleSystem { get { return onHitParticleSystem; } }


        [SerializeField] private HitBox hitBox;
        public HitBox HitBox { get { return hitBox; } }

        [Header("Settings")]
        [SerializeField] private DecorationData data;
        public DecorationData Data { get { return data; } }

        [SerializeField] private int stage;
        public int Stage { get { return stage; } private set { stage = value; } }

        [Header("Info")]
        [SerializeField] private float hitpoints;
        public float Hitpoints { get { return hitpoints; } private set { this.hitpoints = value; } }


        private void Awake()
        {
            this.UpdateStage();
        }

        public void OnHit(HitObserverInfo info)
        {
            float damage = info.CalculateDamage();

            this.Damage(damage);

            this.OnHitParticleSystem?.Play();
        }

        public void Damage(float damage)
        {
            damage = Mathf.Clamp(damage, 0, damage);

            this.Hitpoints -= damage;

            if (this.Data.StageHitpoints <= 0)
            {
                this.Stage++;
            }
            else
            {
                while (this.Hitpoints <= 0)
                {
                    this.Hitpoints += this.Data.StageHitpoints;
                    this.Stage++;
                }
            }

            this.UpdateStage();
        }

        public void UpdateStage()
        {
            int currentStage = this.Stage;
            this.Stage = Mathf.Min(Mathf.Max(this.Stage, 0), Mathf.Max(this.Data.Stages.Length - 1, 0));

            if (this.Stage != currentStage)
                Debug.Log(this.Stage);

            var stage = this.Data.Stages[this.Stage];

            this.Renderer.sprite = stage.Sprite;
            this.HitBox.Box.center = stage.HitBoxOffset;
            this.HitBox.Box.size = stage.HitBoxScale;
            this.HitBox.Box.enabled = stage.HitBoxEnabled;
        }
    }
}