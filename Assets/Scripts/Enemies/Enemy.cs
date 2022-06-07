using Input;
using Misc;
using Misc.AssetVariables;
using Motion;
using PlayerControlls;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IHitObserver
    {
        [Header("References")]
        //[SerializeField] private Player player;
        //public Player Player { get { return player; } }

        [SerializeField] private PlayerReference playerInstance;
        public PlayerReference PlayerInstance { get { return playerInstance; } }

        [SerializeField] private EnemySwarmController swarmController;
        public EnemySwarmController SwarmController { get { return swarmController; } }

        [SerializeField] private EnemyAnimationUnit animationUnit;
        public EnemyAnimationUnit AnimationUnit { get { return animationUnit; } }

        [SerializeField] private ParticleSystem onHitParticleSystem;
        public ParticleSystem OnHitParticleSystem { get { return onHitParticleSystem; } }

        [SerializeField] private ParticleSystem onDieParticleSystem;
        public ParticleSystem OnDieParticleSystem { get { return onDieParticleSystem; } }

        [SerializeField] private ScoreManager scoreManager;
        public ScoreManager ScoreManager { get { return scoreManager; } }


        [Header("References > HitBox")]
        [SerializeField] private HitBox headHitBox;
        public HitBox HeadHitBox { get { return headHitBox; } }

        [SerializeField] private HitBox bodyHitBox;
        public HitBox BodyHitBox { get { return bodyHitBox; } }

        [SerializeField] private HitBox feetHitBox;
        public HitBox FeetHitBox { get { return feetHitBox; } }
        
        [Header("Data")]
        [SerializeField] private EnemyData data;
        public EnemyData Data { get { return data; } private set { this.data = value; } }

        public EnemyVisualData VisualData { get { return this.Data.Visuals; } }

        [Header("Values")]
        [SerializeField] private float hitpoints;
        public float Hitpoints { get { return hitpoints; } private set { this.hitpoints = value; } }

        public bool IsAlive { get => this.Hitpoints > 0; }


        public Player Player { get { return this.PlayerInstance.Value; } }
        Vector3 KnockbackForce { get; set; }
        float AttackCooldown { get; set; }

        EnemyDieAnimator DieAnimator { get; set; }

        // AnimationUnit

        bool PlayOnHit { get; set; }

        float HitPointsHead { get; set; }
        float HitPointsBody { get; set; }
        float HitPointsFeet { get; set; }


        public void SetData(EnemyData data)
        {
            this.Data = data;

            this.HitPointsHead = this.Data.HitPointsHead;
            this.HitPointsBody = this.Data.HitPointsBody;
            this.HitPointsFeet = this.Data.HitPointsFeet;

            this.HeadHitBox.Box.center = this.Data.HeadOffset;
            this.HeadHitBox.Box.size = this.Data.HeadScale;
            this.BodyHitBox.Box.center = this.Data.BodyOffset;
            this.BodyHitBox.Box.size = this.Data.BodyScale;
            this.FeetHitBox.Box.center = this.Data.FeetOffset;
            this.FeetHitBox.Box.size = this.Data.FeetScale;
        }


        private void Start()
        {
            this.DieAnimator = new EnemyDieAnimator(this);

            this.Hitpoints = this.Data.Hitpoints;

            this.GetPlayerLocation(out _, out float toPlayerAngleSigned, out _, out _);

            Vector3 rotation = this.transform.eulerAngles;
            rotation.y = rotation.y - toPlayerAngleSigned;
            this.transform.eulerAngles = rotation;

            this.AnimationUnit.SetHead(this.VisualData.GetRandomHead());
            this.AnimationUnit.SetBody(this.VisualData.GetRandomBody());
            this.AnimationUnit.SetFeet(this.VisualData.GetRandomFeet());
        }

        private void GetPlayerLocation(out float toPlayerAngle, out float toPlayerAngleSigned, out Vector3 toPlayerOnPlane, out Vector2 toPlayer2D)
        {
            Vector3 toPlayer = this.Player.transform.position - this.transform.position;
            Vector3 toPlayerNormalized = toPlayer.normalized;

            Vector3 forwardOnPlane = Vector3.ProjectOnPlane(this.transform.forward, Vector3.up).normalized;
            Vector2 forward = new Vector2(forwardOnPlane.x, forwardOnPlane.z);

            toPlayerOnPlane = Vector3.ProjectOnPlane(toPlayerNormalized, Vector3.up).normalized;
            toPlayer2D = new Vector2(toPlayerOnPlane.x, toPlayerOnPlane.z);

            toPlayerAngle = Vector3.Angle(forward, toPlayer2D);
            toPlayerAngleSigned = Vector2.SignedAngle(forward, toPlayer2D);
        }

        private void Update()
        {
            this.AttackCooldown = Mathf.Clamp(this.AttackCooldown - Time.deltaTime, 0, 1);

            if (this.DieAnimator.IsActive)
            {
                if (this.DieAnimator.Update())
                    GameObject.Destroy(this.gameObject);

                this.ResolveKnockback();
            }
            else
            {
                this.UpdateMovement();
                this.TryDamagePlayer();
            }
        }

        private void ResolveKnockback()
        {
            Vector3 knockback = this.KnockbackForce * Time.deltaTime * (1f / .1f);
            this.KnockbackForce -= knockback;
            this.transform.position += knockback;
        }

        private void UpdateMovement()
        {
            this.GetPlayerLocation(out float toPlayerAngle, out float toPlayerAngleSigned, out Vector3 toPlayerOnPlane, out Vector2 toPlayer2D);

            //Vector3 knockback = this.KnockbackForce * Time.deltaTime * (1f / .1f);
            //this.KnockbackForce -= knockback;
            this.ResolveKnockback();
            this.transform.position += toPlayerOnPlane * this.Data.Speed * Time.deltaTime; // + knockback;

            Vector3 rotation = this.transform.eulerAngles;
            rotation.y = Mathf.Lerp(rotation.y, rotation.y - toPlayerAngleSigned, (1f / .2f) * Time.deltaTime);
            this.transform.eulerAngles = rotation;

            this.SwarmController.Push();

            if (this.PlayOnHit)
            {
                this.OnHitParticleSystem.Play();
                this.PlayOnHit = false;
            }
        }


        private void DamageHead(HitObserverInfo info)
        {
            float damage = this.Damage(info, this.Data.DamageMultiplierHead);

            this.HitPointsHead -= damage;

            if (this.HitPointsHead <= 0)
            {
                this.HitPointsHead = this.Data.HitPointsHead;

                var enemyAnimationSet = this.AnimationUnit.GetCurrentHeadSet();
                this.AnimationUnit.SetHead(enemyAnimationSet.GetRandomSuccessor(out bool success));
            }
        }

        private void DamageBody(HitObserverInfo info)
        {
            float damage = this.Damage(info, this.Data.DamageMultiplierBody);

            this.HitPointsBody -= damage;

            if (this.HitPointsBody <= 0)
            {
                this.HitPointsBody = this.Data.HitPointsBody;

                var enemyAnimationSet = this.AnimationUnit.GetCurrentBodySet();
                this.AnimationUnit.SetBody(enemyAnimationSet.GetRandomSuccessor(out bool success));
            }
        }

        private void DamageFeet(HitObserverInfo info)
        {
            float damage = this.Damage(info, this.Data.DamageMultiplierFeet);

            this.HitPointsFeet -= damage;

            if (this.HitPointsFeet <= 0)
            {
                this.HitPointsFeet = this.Data.HitPointsFeet;

                var enemyAnimationSet = this.AnimationUnit.GetCurrentFeetSet();
                this.AnimationUnit.SetFeet(enemyAnimationSet.GetRandomSuccessor(out bool success));
            }
        }


        public void OnHit(HitObserverInfo info)
        {
            this.PlayOnHit = true;

            if (this.HeadHitBox == info.HitBox)
            {
                this.DamageHead(info);
            }
            else if (this.BodyHitBox == info.HitBox)
            {
                this.DamageBody(info);
            }
            else if (this.FeetHitBox == info.HitBox)
            {
                this.DamageFeet(info);
            }
        }

        public float Damage(HitObserverInfo info, float damageMultiplier)
        {
            float damage = info.CalculateDamage() * damageMultiplier;
            this.Hitpoints -= damage;

            this.Knockback(info, damage);

            if (this.Hitpoints <= 0)
            {
                this.Die();
            }

            return damage;
        }

        private void Knockback(HitObserverInfo info, float damage)
        {
            Vector3 avgOrigin = Vector3.zero;
            float avgDistance = 0;

            foreach (var shotInfo in info.ShotInfos)
            {
                avgOrigin += shotInfo.Origin;
                avgDistance += shotInfo.Hit.distance;
            }

            avgOrigin /= info.ShotInfos.Length;
            avgDistance /= info.ShotInfos.Length;

            this.Knockback(avgOrigin, avgDistance, damage, false);
        }

        public void Knockback(Vector3 origin, float distance, float force, bool ignoreReduction)
        {
            float finalForce = force / Mathf.Max(distance, 1);

            if (!ignoreReduction)
            {
                if (this.Data.KnockbackReduction > 0)
                    finalForce = finalForce / this.Data.KnockbackReduction;
                else
                    finalForce = finalForce * Mathf.Max(1, Mathf.Abs(this.Data.KnockbackReduction));
            }

            Vector3 finalKnockback = (this.transform.position - origin).normalized * finalForce;
            finalKnockback.y = 0;

            var resultKnockback = (this.KnockbackForce + finalKnockback);

            if (resultKnockback.magnitude > 20)
                resultKnockback = resultKnockback.normalized * 20;

            this.KnockbackForce = resultKnockback;
        }

        public void Die()
        {
            this.DieAnimator.IsActive = true;
            this.OnDieParticleSystem?.Play();

            this.ScoreManager.Add(this.Data);
        }

        private void TryDamagePlayer()
        {
            if (this.AttackCooldown > 0)
                return;

            if (this.KnockbackForce.magnitude > 1f)
                return;

            Vector3 delta = this.Player.transform.position - this.transform.position;
            delta.y = 0;

            if (delta.magnitude <= 1)
            {
                this.Player.Damage(this.Data.Damage);
                this.Knockback(this.Player.transform.position, 1, 6, true);

                this.AttackCooldown = 1;
            }
        }

        class EnemyDieAnimator
        {
            public bool IsActive { get; set; }

            float Time { get; set; }

            private Enemy Enemy { get; set; }


            public EnemyDieAnimator(Enemy enemy)
            {
                Enemy = enemy;
            }


            public bool Update()
            {
                this.Time += UnityEngine.Time.deltaTime;
                Vector3 angle = this.Enemy.transform.localEulerAngles;

                angle.x = Mathf.Lerp(0, -90, this.Time * this.Time);

                this.Enemy.transform.localEulerAngles = angle;

                //Vector3 position = this.Enemy.transform.position;
                //position.y = -this.Time;
                //this.Enemy.transform.position = position;

                return this.Time >= 1;
            }
        }
    }
}