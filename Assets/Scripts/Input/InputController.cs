using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputController : MonoBehaviour
    {
        private InputUnit Input { get; set; }

        public InputButton Move { get; private set; }
        public InputState ViewDelta { get; private set; }
        public InputButton Action { get; private set; }

        GameInputUpdate InputUpdate { get; set; }

        private void Awake()
        {
            this.Input = new InputUnit();
            this.Input.Enable();

            this.Move = this.AddButton(this.Input.Player.Move);
            this.Action = this.AddButton(this.Input.Player.Action);

            this.ViewDelta = this.Add(this.Input.Player.ViewDelta);
        }

        private InputState Add(InputAction inputAction)
        {
            GameInputUpdate gameInputUpdate = this.InputUpdate;

            InputState toReturn = new InputState(inputAction, ref gameInputUpdate);

            this.InputUpdate = gameInputUpdate;

            return toReturn;
        }

        private InputButton AddButton(InputAction inputAction)
        {
            GameInputUpdate gameInputUpdate = this.InputUpdate;

            InputButton toReturn = new InputButton(inputAction, ref gameInputUpdate);

            this.InputUpdate = gameInputUpdate;

            return toReturn;
        }

        private void Update()
        {
            this.InputUpdate?.Invoke();
        }
    }

    public delegate void GameInputUpdate();


    public class InputState
    {
        public bool WasPressed { get => this.InputAction.WasPressedThisFrame(); }
        public bool WasReleased { get => this.InputAction.WasReleasedThisFrame(); }
        public bool IsPressed { get => this.InputAction.IsPressed(); }

        protected InputAction InputAction { get; private set; }

        public InputState(InputAction input, ref GameInputUpdate action)
        {
            this.InputAction = input;
        }

        public Vector2 GetVector2() => this.InputAction.ReadValue<Vector2>();

        public static implicit operator float(InputState inputState) => inputState.InputAction.ReadValue<float>();
    }

    public class InputButton : InputState
    {
        public float DownTime { get; private set; }
        public float UpTime { get; private set; }

        public InputButton(InputAction input, ref GameInputUpdate action)
            : base(input, ref action)
        {
            action += this.Update;
        }


        void Update()
        {
            if (this.IsPressed)
            {
                this.DownTime += Time.deltaTime;
                this.UpTime = 0;
            }
            else
            {
                this.DownTime = 0;
                this.UpTime += Time.deltaTime;
            }
        }
    }
}