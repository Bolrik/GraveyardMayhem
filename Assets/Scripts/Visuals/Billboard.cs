using Input;
using Misc.AssetVariables;
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
        [SerializeField] private TransformReference target;
        public TransformReference Target { get { return target; } }

        [SerializeField] private bool useTargetRotation;
        public bool UseTargetRotation { get { return useTargetRotation; } }


        private void LateUpdate()
        {
            if (this.UseTargetRotation)
            {
                this.transform.eulerAngles = this.Target.Value.eulerAngles;
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
