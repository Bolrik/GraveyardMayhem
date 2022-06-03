using UnityEngine;

namespace Misc
{
    [CreateAssetMenu(fileName = "MiscManager", menuName = "Manager/new Misc Manager")]
    public class MiscManager : ScriptableObject
    {
        [SerializeField] private BulletTrail bulletTrail;
        BulletTrail BulletTrail { get { return bulletTrail; } }

        public void CreateTrail(Vector3 origin, Vector3 target)
        {
            BulletTrail trail = GameObject.Instantiate(this.BulletTrail);
            trail.Set(origin, target);
        }

    }
}