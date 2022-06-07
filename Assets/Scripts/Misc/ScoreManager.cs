using Enemies;
using Misc.AssetVariables;
using PlayerControlls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Misc
{
    [CreateAssetMenu(fileName = "ScoreManager", menuName = "Manager/new Score Manager")]
    public class ScoreManager : ScriptableObject
    {
        [SerializeField] private FloatVariable score;
        public FloatVariable Score { get { return score; } }

        [SerializeField] private FloatReference waveCountdown;
        public FloatReference WaveCountdown { get { return waveCountdown; } }


        public void Add(IScoreItem scoreItem)
        {
            float value = scoreItem.ScoreValue;// Mathf.Max(scoreItem.ScoreValue * this.WaveCountdown.Value, scoreItem.ScoreValue);

            this.Score.Value += value;
        }
    }

    public interface IScoreItem
    {
        float ScoreValue { get; }
    }
}