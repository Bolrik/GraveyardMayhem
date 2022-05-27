using Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Visuals
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] private Transform target;
        public Transform Target { get { return target; } }

        [SerializeField] private bool useTargetRotation;
        public bool UseTargetRotation { get { return useTargetRotation; } }


        private void LateUpdate()
        {
            if (this.UseTargetRotation)
            {
                this.transform.eulerAngles = this.Target.eulerAngles;
            }
            else
            {
                this.transform.LookAt(this.Target);
            }

            Vector3 rotation = this.transform.eulerAngles;

            rotation.x = 0;
            rotation.z = 0;
            this.transform.eulerAngles = rotation;
        }
    }
}
