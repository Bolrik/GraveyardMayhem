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

        InputButton IInput.Move => this.Input.Move;
        InputState IInput.ViewDelta => this.Input.ViewDelta;
        InputButton IInput.LeftClick => this.Input.LeftClick;

        #endregion
    }
}
