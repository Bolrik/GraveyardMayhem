using Misc.AssetVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Misc
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private FloatVariable score;
        public FloatVariable Score { get { return score; } }

        [SerializeField] private IntVariable ammo;
        public IntVariable Ammo { get { return ammo; } }

        [SerializeField] private ScoreManager scoreManager;
        public ScoreManager ScoreManager { get { return scoreManager; } }


        private void Awake()
        {
            this.Score.Value = 0;
            this.Ammo.Value = 0;
            this.ScoreManager.IsActive = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
