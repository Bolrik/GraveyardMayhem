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
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private UIDocument document;
        public UIDocument Document { get { return document; } }


        [SerializeField] private AmmoCounterUI ammoCounter;
        public AmmoCounterUI AmmoCounter { get { return ammoCounter; } }

        [SerializeField] private WaveInfoUI waveInfo;
        public WaveInfoUI WaveInfo { get { return waveInfo; } }

        [SerializeField] private HealthUI health;
        public HealthUI Health { get { return health; } }


        [SerializeField] private ScoreUI score;
        public ScoreUI Score { get { return score; } }



        private void Awake()
        {
            this.AmmoCounter.SetRoot(this.Document.rootVisualElement.Q("AmmoCounter"));
            this.WaveInfo.SetRoot(this.Document.rootVisualElement.Q("WaveInfo"));
            this.Health.SetRoot(this.Document.rootVisualElement.Q("HealthUI"));
            this.Score.SetRoot(this.Document.rootVisualElement.Q("ScoreUI"));
        }
    }
}
