using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemySwarmController : MonoBehaviour
    {
        [SerializeField] private Enemy owner;
        public Enemy Owner { get { return owner; } }

        [SerializeField] private SphereCollider trigger;
        public SphereCollider Trigger { get { return trigger; } }

        public List<Enemy> Nearby { get; private set; } = new List<Enemy>();

        private void OnTriggerEnter(Collider other)
        {
            if (!(other.GetComponent<EnemySwarmController>() is EnemySwarmController enemy))
                return;

            this.Nearby.Add(enemy.Owner);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!(other.GetComponent<EnemySwarmController>() is EnemySwarmController enemy))
                return;

            this.Nearby.Remove(enemy.Owner);
        }

        public void Push()
        {
            foreach (var other in this.Nearby)
            {
                Vector3 delta = other.transform.position - this.Owner.transform.position;
                delta.y = 0;

                float distance = delta.magnitude;
                float force = Mathf.Clamp01(distance / (this.Trigger.radius * 2));
                force *= force;

                other.transform.position = this.Owner.transform.position + (delta * (2 - force));
            }
        }
    }
}