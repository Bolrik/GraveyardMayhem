using Misc.AssetVariables;
using PlayerControlls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private FloatReference hitPoints;
        public FloatReference HitPoints { get { return hitPoints; } }

        [SerializeField] private PlayerData playerData;
        public PlayerData PlayerData { get { return playerData; } }


        Label HitPointLabel { get; set; }

        public void SetRoot(VisualElement visualElement)
        {
            this.HitPointLabel = visualElement.Q<Label>("HitPointText");
        }

        private void Update()
        {
            if (this.HitPointLabel == null)
                return;

            if (this.HitPoints.Value > 0)
            {
                float realValue = this.HitPoints.Value;
                float fakeValue = realValue / this.PlayerData.Health;
                fakeValue = Mathf.Pow(fakeValue, 1.73671148372952f);
                fakeValue = Mathf.Clamp(fakeValue * this.PlayerData.Health, 1, this.PlayerData.Health);

                this.HitPointLabel.text = $"{fakeValue:N0}";
            }
            else
            {
                this.HitPointLabel.text = $"{0:N0}";
            }
        }
    }
}
