using Enemies;
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
    public class WaveInfoUI : MonoBehaviour
    {
        [SerializeField] private FloatReference waveCountdownValue;
        public FloatReference WaveCountdownValue { get { return waveCountdownValue; } }

        [SerializeField] private EnemyWaveManagerData waveData;
        public EnemyWaveManagerData WaveData { get { return waveData; } }


        VisualElement ProgressContent { get; set; }
        Label WaveCountdown { get; set; }

        public void SetRoot(VisualElement visualElement)
        {
            this.ProgressContent = visualElement.Q<VisualElement>("ProgressContent");
            this.WaveCountdown = visualElement.Q<Label>("WaveCountdownText");
        }

        private void Update()
        {
            if (this.ProgressContent == null || this.WaveCountdown == null)
                return;

            this.WaveCountdown.text = $"{this.WaveCountdownValue.Value:N2}";
            this.ProgressContent.style.width = new StyleLength(new Length(this.WaveCountdownValue.Value / this.WaveData.Countdown * 100, LengthUnit.Percent));
        }
    }
}
