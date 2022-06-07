using Misc;
using Misc.Interfaces;
using PlayerControlls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Decorations
{
    public class Decoration : MonoBehaviour, IHitObserver, IDespawn<Decoration>
    {
        [Header("References")]
        [SerializeField] private SpriteRenderer renderer;
        public SpriteRenderer Renderer { get { return renderer; } }

        [SerializeField] private ParticleSystem onHitParticleSystem;
        public ParticleSystem OnHitParticleSystem { get { return onHitParticleSystem; } }

        [SerializeField] private HitBox hitBox;
        public HitBox HitBox { get { return hitBox; } }


        [SerializeField] private ScoreManager scoreManager;
        public ScoreManager ScoreManager { get { return scoreManager; } }


        [Header("Settings")]
        [SerializeField] private DecorationData data;
        public DecorationData Data { get { return data; } private set { this.data = value; } }

        [SerializeField] private int stage;
        public int Stage { get { return stage; } private set { stage = value; } }

        [Header("Info")]
        [SerializeField] private float hitPoints;
        public float HitPoints { get { return hitPoints; } private set { this.hitPoints = value; } }

        public Action<Decoration> OnDespawn { get; set; }

        bool IsDespawning { get; set; }
        float DespawnTime { get; set; }

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

            this.HitPoints -= damage;

            if (this.Data.StageHitpoints <= 0)
            {
                this.Stage++;
            }
            else
            {
                while (this.HitPoints <= 0)
                {
                    this.HitPoints += this.Data.StageHitpoints;
                    this.Stage++;

                    this.ScoreManager.Add(this.Data);
                }
            }

            this.UpdateStage();

            if (this.Stage >= this.Data.Stages.Length - 1)
                this.IsDespawning = true;
        }

        public void UpdateStage()
        {
            int currentStage = this.Stage;
            this.Stage = Mathf.Min(Mathf.Max(this.Stage, 0), Mathf.Max(this.Data.Stages.Length - 1, 0));

            var stage = this.Data.Stages[this.Stage];

            this.Renderer.sprite = stage.Sprite;
            this.HitBox.Box.center = stage.HitBoxOffset;
            this.HitBox.Box.size = stage.HitBoxScale;
            this.HitBox.Box.enabled = stage.HitBoxEnabled;
        }

        private void Update()
        {
            if (!this.IsDespawning)
                return;

            this.DespawnTime += Time.deltaTime;

            if (this.DespawnTime >= 10)
                this.Despawn();
        }

        public void Despawn()
        {
            GameObject.Destroy(this.gameObject);

            this.OnDespawn(this);
        }

        public void SetData(DecorationData data)
        {
            this.Data = data;
        }
    }
}