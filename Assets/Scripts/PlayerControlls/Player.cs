using Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PlayerControlls
{
    public class Player : MonoBehaviour, IInput
    {
        [SerializeField] private InputController input;
        public InputController Input { get { return input; } }


        #region IInput
        public void GetAction(out bool value)
        {
            value = this.Input.LeftClick.IsPressed;
        }

        public void GetMove(out Vector2 value, out float pressTime)
        {
            value = this.Input.Move.GetVector2();
            pressTime = this.Input.Move.DownTime;
        }

        public void GetViewDelta(out Vector2 value)
        {
            value = this.Input.ViewDelta.GetVector2();
        }


        #endregion
    }
}
