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

        [SerializeField] private IntReference wave;
        public IntReference Wave { get { return wave; } }



        VisualElement ProgressContent { get; set; }
        Label WaveCountdown { get; set; }
        Label WaveInfo { get; set; }

        public void SetRoot(VisualElement visualElement)
        {
            this.ProgressContent = visualElement.Q<VisualElement>("ProgressContent");
            this.WaveCountdown = visualElement.Q<Label>("WaveCountdownText");
            this.WaveInfo = visualElement.Q<Label>("WaveInfoValue");
        }

        private void Update()
        {
            if (this.ProgressContent == null || this.WaveCountdown == null || this.WaveInfo == null)
                return;

            this.WaveCountdown.text = $"{this.WaveCountdownValue.Value:N1}";
            this.WaveInfo.text = $"({this.Wave.Value}):";
            this.ProgressContent.style.width = new StyleLength(new Length(this.WaveCountdownValue.Value / this.WaveData.Countdown * 100, LengthUnit.Percent));
        }
    }
}
