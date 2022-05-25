using Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Motion
{
    public class Move : MonoBehaviour
    {
        [SerializeField] private InputController input;
        public InputController Input { get { return input; } }

        [SerializeField] private View view;
        public View View { get { return view; } }


        [SerializeField] private Vector3 velocity;
        public Vector3 Velocity { get { return velocity; } private set { velocity = value; } }

        private void Update()
        {
            Vector3 velocity = this.Velocity;

            // velocity += Vector3.down * 9.81f * Time.deltaTime;
            Vector2 moveInput = this.Input.Move.GetVector2();

            velocity += new Vector3(moveInput.x, 0, moveInput.y);
            this.View.

            this.Velocity = velocity;
        }

        private void FixedUpdate()
        {
            this.transform.position += this.Velocity * Time.fixedDeltaTime;
        }
    }
}