using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Misc
{
    public class BulletTrail : MonoBehaviour
    {
        public Vector3 Target { get; private set; }
        public Vector3 Origin { get; private set; }

        public float Done { get; set; }

        public void Set(Vector3 origin, Vector3 target)
        {
            this.Target = target;
            this.Origin = origin;

            this.transform.position = this.Origin;
        }

        private void Update()
        {
            this.Done += Time.deltaTime * (1f / .1f);
            this.transform.position = Vector3.Lerp(this.Origin, this.Target, this.Done);

            if (this.Done >= 1)
                GameObject.Destroy(this.gameObject);
        }
    }
}