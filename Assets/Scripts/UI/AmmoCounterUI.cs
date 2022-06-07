using Misc.AssetVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class AmmoCounterUI : MonoBehaviour
    {
        [SerializeField] private IntReference ammo;
        public IntReference Ammo { get { return ammo; } }

        Label AmmoLabel { get; set; }

        public void SetRoot(VisualElement visualElement)
        {
            this.AmmoLabel = visualElement.Q<Label>("AmmoText");
        }

        private void Update()
        {
            if (this.AmmoLabel == null)
                return;

            if (this.Ammo > 0)
                this.AmmoLabel.text = $"{this.Ammo.Value}";
            else
                this.AmmoLabel.text = $"-";
        }
    }
}
