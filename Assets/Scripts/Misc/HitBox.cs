using PlayerControlls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Misc
{
    public class HitBox : MonoBehaviour
    {
        [SerializeField, SerializeReference] private IHitObserverProxy hitObserver;
        public IHitObserverProxy HitObserver { get { return hitObserver; } }

        [SerializeField] private BoxCollider box;
        public BoxCollider Box
        {
            get
            {
                box = box ?? this.GetComponent<BoxCollider>();
                return box;
            }
            private set { this.box = value; }
        }

        private void Awake()
        {
            this.Box = this.GetComponent<BoxCollider>();
        }

        public void Hit(WeaponData weaponData, ShotInfo[] hits)
        {
            this.HitObserver.Interface.OnHit(new HitObserverInfo(this, weaponData, hits));
        }
    }

    public interface IHitObserver
    {
        void OnHit(HitObserverInfo info);
    }

    public struct HitObserverInfo
    {
        public HitBox HitBox { get; private set; }
        public WeaponData Data { get; private set; }
        public ShotInfo[] ShotInfos { get; private set; }

        public HitObserverInfo(HitBox hitBox, WeaponData data, ShotInfo[] shotInfos)
        {
            this.HitBox = hitBox;
            this.Data = data;
            this.ShotInfos = shotInfos;
        }

        public float CalculateDamage()
        {
            float toReturn = 0;

            foreach (var shotInfo in this.ShotInfos)
            {
                float damage = this.Data.BulletDamage - (this.Data.DamageReductionPerDistance * shotInfo.Hit.distance);
                damage = Mathf.Clamp(damage, 0, damage);

                toReturn += damage;
            }

            return toReturn;
        }
    }
}
//namespace Misc2
//    {
//        public class HitBox : MonoBehaviour
//        {
//            [SerializeField, SerializeReference] private IHitObserverProxy hitObserver;
//            public IHitObserverProxy HitObserver { get { return hitObserver; } }

//            public void Hit(WeaponData weaponData)
//            {
//                this.HitObserver.Interface.OnHit(this, weaponData);
//            }
//        }

//        public interface IHitObserver
//        {
//            void OnHit(HitObserverInfo info);
//        }

//        public struct HitObserverInfo
//        {
//            public HitBox HitBox { get; set; }
//            public WeaponData Data { get; private set; }
//            public float Damage { get; private set; }
//            public RaycastHit[] Hits { get; private set; }
//        }
//    }
//}