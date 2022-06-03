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
    class Enemy : MonoBehaviour, IHitObserver
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



        [Header("References > HitBox")]
        [SerializeField] private HitBox headHitBox;
        public HitBox HeadHitBox { get { return headHitBox; } }

        [SerializeField] private HitBox bodyHitBox;
        public HitBox BodyHitBox { get { return bodyHitBox; } }

        [SerializeField] private HitBox feetHitBox;
        public HitBox FeetHitBox { get { return feetHitBox; } }

        [Header("Data")]
        [SerializeField] private EnemyVisualData visualData;
        public EnemyVisualData VisualData { get { return visualData; } }

        [SerializeField] private EnemyData data;
        public EnemyData Data { get { return data; } }

        [Header("Values")]
        [SerializeField] private float hitpoints;
        public float Hitpoints { get { return hitpoints; } private set { this.hitpoints = value; } }



        public Player Player { get { return this.PlayerInstance.Value; } }
        Vector3 KnockbackForce { get; set; }

        // AnimationUnit

        bool PlayOnHit { get; set; }

        private void Start()
        {
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
            this.GetPlayerLocation(out float toPlayerAngle, out float toPlayerAngleSigned, out Vector3 toPlayerOnPlane, out Vector2 toPlayer2D);

            Vector3 knockback = this.KnockbackForce * Time.deltaTime * (1f / .1f);
            this.KnockbackForce -= knockback;
            this.transform.position += toPlayerOnPlane * Time.deltaTime + knockback;

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
            var enemyAnimationSet = this.AnimationUnit.GetCurrentHeadSet();
            this.AnimationUnit.SetHead(enemyAnimationSet.GetRandomSuccessor(out bool success));

            this.Damage(info, this.Data.DamageMultiplierHead);

        }

        private void DamageBody(HitObserverInfo info)
        {
            var enemyAnimationSet = this.AnimationUnit.GetCurrentBodySet();
            this.AnimationUnit.SetBody(enemyAnimationSet.GetRandomSuccessor(out bool success));

            this.Damage(info, this.Data.DamageMultiplierBody);

        }

        private void DamageFeet(HitObserverInfo info)
        {
            var enemyAnimationSet = this.AnimationUnit.GetCurrentFeetSet();
            this.AnimationUnit.SetFeet(enemyAnimationSet.GetRandomSuccessor(out bool success));

            this.Damage(info, this.Data.DamageMultiplierFeet);
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

        public void Damage(HitObserverInfo info, float damageMultiplier)
        {
            float damage = info.CalculateDamage() * damageMultiplier;
            this.Hitpoints -= damage;

            this.Knockback(info, damage);

            if (this.Hitpoints <= 0)
            {
                this.Die();
            }
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

            this.Knockback(avgOrigin, avgDistance, damage);
        }

        public void Knockback(Vector3 origin, float distance, float force)
        {
            float finalForce = force / Mathf.Max(distance, 1);

            Vector3 finalKnockback = (this.transform.position - origin).normalized * finalForce;
            finalKnockback.y = 0;

            this.KnockbackForce += finalKnockback;
        }

        public void Die()
        {

        }
    }
}