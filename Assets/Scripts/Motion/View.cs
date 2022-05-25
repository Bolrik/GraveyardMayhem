using Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Motion
{
    public class View : MonoBehaviour
    {
        [Header("Info")]
        [SerializeField] private Vector2 mousePosition;
        private Vector2 MousePosition { get { return this.mousePosition; } set { this.mousePosition = value; } }

        [SerializeField] private Vector2 mousePositionSmooth;
        private Vector2 MousePositionSmooth { get { return this.mousePositionSmooth; } set { this.mousePositionSmooth = value; } }

        [Header("Settings")]
        [SerializeField] private ViewData data;
        public ViewData Data { get { return data; } }

        [SerializeField] private Vector2 targetDirection;
        private Vector2 TargetDirection { get { return this.targetDirection; } set { this.targetDirection = value; } }

        [SerializeField] private Vector2 targetCharacterDirection;
        private Vector2 TargetCharacterDirection { get { return this.targetCharacterDirection; } set { this.targetCharacterDirection = value; } }

        [Header("References")]
        [SerializeField] private Transform verticalTargetTransform;
        private Transform VerticalTargetTransform { get { return this.verticalTargetTransform; } set { this.verticalTargetTransform = value; } }

        [SerializeField] private Transform horizontalTargetTransform;
        private Transform HorizontalTargetTransform { get { return this.horizontalTargetTransform; } set { this.horizontalTargetTransform = value; } }

        [SerializeField] private InputController input;
        public InputController Input { get { return input; } private set { this.input = value; } }


        private Vector2 Sensitivit { get => this.Data.Sensitivit; }
        private Vector2 Smoothing { get => this.Data.Smoothing; }
        private Vector2 ViewClamp { get => this.Data.ViewClamp; }
        private Vector2 ViewClampOffset { get => this.Data.ViewClampOffset; }

        private void Start()
        {
            // Set target direction to the camera's initial orientation.
            if (this.VerticalTargetTransform != null)
                this.targetDirection = this.VerticalTargetTransform.localRotation.eulerAngles;

            // Set target direction for the character body to its inital state.
            if (this.HorizontalTargetTransform != null)
                this.targetCharacterDirection = this.HorizontalTargetTransform.localRotation.eulerAngles;

            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
        }

        void LateUpdate()
        {
            //if (!GameSettings.Instance.ShowCursor)
            //{
            //    Cursor.lockState = CursorLockMode.Locked;
            //    Cursor.visible
            //}

            var targetOrientation = Quaternion.Euler(this.TargetDirection);
            var targetCharacterOrientation = Quaternion.Euler(this.TargetCharacterDirection);

            var mouseDelta = this.Input.ViewDelta.GetVector2();

            mouseDelta = Vector2.Scale(mouseDelta,
                new Vector2(
                    this.Sensitivit.x * this.Smoothing.x,
                this.Sensitivit.y * this.Smoothing.y));

            Vector2 mousePositionSmooth = this.MousePositionSmooth;
            mousePositionSmooth.x = Mathf.Lerp(mousePositionSmooth.x, mouseDelta.x, 1f / this.Smoothing.x);
            mousePositionSmooth.y = Mathf.Lerp(mousePositionSmooth.y, mouseDelta.y, 1f / this.Smoothing.y);

            Vector2 mousePosition = this.MousePosition;
            mousePosition += mousePositionSmooth;

            if (this.ViewClamp.x < 360)
                mousePosition.x = Mathf.Clamp(mousePosition.x, -this.ViewClamp.x * 0.5f + this.ViewClampOffset.x, this.ViewClamp.x * 0.5f + this.ViewClampOffset.x);

            if (this.ViewClamp.y < 360)
                mousePosition.y = Mathf.Clamp(mousePosition.y, -this.ViewClamp.y * 0.5f + this.ViewClampOffset.y, this.ViewClamp.y * 0.5f + this.ViewClampOffset.y);

            if (this.VerticalTargetTransform != null)
            {
                var rotation = Quaternion.AngleAxis(-mousePosition.y, targetOrientation * Vector3.right) * targetOrientation;
                this.VerticalTargetTransform.localRotation = rotation * targetCharacterOrientation;
            }

            if (this.HorizontalTargetTransform != null)
            {
                var rotation = Quaternion.AngleAxis(mousePosition.x, Vector3.up);
                this.HorizontalTargetTransform.localRotation = rotation * targetCharacterOrientation;
            }

            this.MousePosition = mousePosition;
            this.MousePositionSmooth = mousePositionSmooth;
        }
    }
}