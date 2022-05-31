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

            this.Input.GetMove(out Vector2 moveInput, out float time);

            Vector3 forward = this.GetForward(moveInput);
            Vector3 forwardProjected = Vector3.ProjectOnPlane(forward, Vector3.up);

            float speed = this.Speed * Mathf.Clamp01(time * (1f / .1f));

            velocity += forwardProjected * speed * Time.deltaTime;

            this.Velocity = velocity;
        }

        private Vector3 GetForward(Vector2 moveInput)
        {
            if (this.View == null)
                return this.transform.forward * moveInput.y + this.transform.right * moveInput.x;

            return this.View.Yaw.forward * moveInput.y + this.View.Yaw.right * moveInput.x;
        }

        private void FixedUpdate()
        {
            Vector3 finalPosition = this.transform.position + this.Velocity;
            finalPosition.x = Mathf.Clamp(finalPosition.x , - 27, 27);
            finalPosition.z = Mathf.Clamp(finalPosition.z , - 27, 27);

            this.transform.position = finalPosition;
            
            this.Velocity = Vector3.zero;
        }
    }
}