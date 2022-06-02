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

        public void Hit(WeaponData weaponData)
        {
            this.HitObserver.Interface.OnHit(this, weaponData);
        }
    }

    public interface IHitObserver
    {
        void OnHit(HitBox hitBox, WeaponData weaponData);
    }
}