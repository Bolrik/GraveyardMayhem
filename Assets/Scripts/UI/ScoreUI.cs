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
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private FloatReference score;
        public FloatReference Score { get { return score; } }

        Label ScoreLabel { get; set; }

        public void SetRoot(VisualElement visualElement)
        {
            this.ScoreLabel = visualElement.Q<Label>("ScoreText");
        }

        private void Update()
        {
            if (this.ScoreLabel == null)
                return;

            this.ScoreLabel.text = $"{this.Score.Value}";
        }
    }
}
