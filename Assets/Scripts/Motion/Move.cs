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
        [Header("References")]
        [SerializeField] private IInput input;
        public IInput Input { get { return input; } private set { this.input = value; } }

        [SerializeField] private View view;
        public View View { get { return view; } }


        [Header("Settings")]
        [SerializeField] private MoveData data;
        public MoveData Data { get { return data; } }

        [Header("Info")]
        [SerializeField] private Vector3 velocity;
        public Vector3 Velocity { get { return velocity; } private set { velocity = value; } }


        public float Speed { get => this.Data.Speed; }

        private void Start()
        {
            this.Input = this.GetComponent<IInput>();
        }


        private void Update()
        {
            Vector3 velocity = this.Velocity;

            Vector2 moveInput = this.Input.Move.GetVector2();

            Vector3 forward = this.View.Yaw.forward * moveInput.y + this.View.Yaw.right * moveInput.x;
            Vector3 forwardProjected = Vector3.ProjectOnPlane(forward, Vector3.up);

            float speed = this.Speed * Mathf.Clamp01(this.Input.Move.DownTime * (1f / .1f));

            velocity += forwardProjected * speed * Time.deltaTime;

            this.Velocity = velocity;
        }

        private void FixedUpdate()
        {
            this.transform.position += this.Velocity;

            this.Velocity = Vector3.zero;
        }
    }
}