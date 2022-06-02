using Input;
using Misc;
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
        [SerializeField] private Player player;
        public Player Player { get { return player; } }


        [SerializeField] private EnemySwarmController swarmController;
        public EnemySwarmController SwarmController { get { return swarmController; } }

        [SerializeField] private EnemyAnimationUnit animationUnit;
        public EnemyAnimationUnit AnimationUnit { get { return animationUnit; } }


        [SerializeField] private EnemyVisualData visualData;
        public EnemyVisualData VisualData { get { return visualData; } }

        
        [SerializeField] private HitBox headHitBox;
        public HitBox HeadHitBox { get { return headHitBox; } }

        [SerializeField] private HitBox bodyHitBox;
        public HitBox BodyHitBox { get { return bodyHitBox; } }

        [SerializeField] private HitBox feetHitBox;
        public HitBox FeetHitBox { get { return feetHitBox; } }




        // AnimationUnit

        private void Start()
        {
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

            this.transform.position += toPlayerOnPlane * Time.deltaTime;

            Vector3 rotation = this.transform.eulerAngles;
            rotation.y = Mathf.Lerp(rotation.y, rotation.y - toPlayerAngleSigned, (1f / .2f) * Time.deltaTime);
            this.transform.eulerAngles = rotation;

            this.SwarmController.Push();
        }

        private void DamageHead(WeaponData weapon)
        {
            var enemyAnimationSet = this.AnimationUnit.GetCurrentHeadSet();
            this.AnimationUnit.SetHead(enemyAnimationSet.GetRandomSuccessor(out bool success));
        }

        private void DamageBody(WeaponData weapon)
        {
            var enemyAnimationSet = this.AnimationUnit.GetCurrentBodySet();
            this.AnimationUnit.SetBody(enemyAnimationSet.GetRandomSuccessor(out bool success));
        }

        private void DamageFeet(WeaponData weapon)
        {
            var enemyAnimationSet = this.AnimationUnit.GetCurrentFeetSet();
            this.AnimationUnit.SetFeet(enemyAnimationSet.GetRandomSuccessor(out bool success));
        }


        public void OnHit(HitBox hitBox, WeaponData weaponData)
        {
            if (this.HeadHitBox == hitBox)
            {
                this.DamageHead(weaponData);
            }
            else if (this.BodyHitBox == hitBox)
            {
                this.DamageBody(weaponData);
            }
            else if (this.FeetHitBox == hitBox)
            {
                this.DamageFeet(weaponData);
            }
        }
    }
}