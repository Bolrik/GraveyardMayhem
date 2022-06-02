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
        [SerializeField] private Vector2 currentView;
        private Vector2 CurrentView { get { return this.currentView; } set { this.currentView = value; } }

        [SerializeField] private Vector2 currentViewSmooth;
        private Vector2 CurrentViewSmooth { get { return this.currentViewSmooth; } set { this.currentViewSmooth = value; } }

        [Header("Settings")]
        [SerializeField] private ViewData data;
        public ViewData Data { get { return data; } }

        [SerializeField] private Vector2 targetView;
        private Vector2 TargetView { get { return this.targetView; } set { this.targetView = value; } }

        [SerializeField] private Vector2 targetViewDirection;
        private Vector2 TargetViewDirection { get { return this.targetViewDirection; } set { this.targetViewDirection = value; } }

        [Header("References")]
        [SerializeField] private Transform pitch;
        public Transform Pitch { get { return this.pitch; } private set { this.pitch = value; } }

        [SerializeField] private Transform yaw;
        public Transform Yaw { get { return this.yaw; } private set { this.yaw = value; } }

        public Transform FinalView { get => this.Yaw; }

        [SerializeField] private IInput input;
        public IInput Input { get { return input; } private set { this.input = value; } }


        private Vector2 Sensitivit { get => this.Data.Sensitivit; }
        private Vector2 Smoothing { get => this.Data.Smoothing; }
        private Vector2 ViewClamp { get => this.Data.ViewClamp; }
        private Vector2 ViewClampOffset { get => this.Data.ViewClampOffset; }

        private void Start()
        {
            this.Input = this.GetComponent<IInput>();

            // Set target direction to the camera's initial orientation.
            if (this.Pitch != null)
                this.TargetView = this.Pitch.localRotation.eulerAngles;

            // Set target direction for the character body to its inital state.
            if (this.Yaw != null)
                this.TargetViewDirection = this.Yaw.localRotation.eulerAngles;
        }

        void LateUpdate()
        {
            var targetOrientation = Quaternion.Euler(this.TargetView);
            var targetCharacterOrientation = Quaternion.Euler(this.TargetViewDirection);

            this.Input.GetViewDelta(out Vector2 viewDelta);

            viewDelta = Vector2.Scale(viewDelta,
                new Vector2(
                    this.Sensitivit.x * this.Smoothing.x,
                this.Sensitivit.y * this.Smoothing.y));

            Vector2 mousePositionSmooth = this.CurrentViewSmooth;
            mousePositionSmooth.x = Mathf.Lerp(mousePositionSmooth.x, viewDelta.x, 1f / this.Smoothing.x);
            mousePositionSmooth.y = Mathf.Lerp(mousePositionSmooth.y, viewDelta.y, 1f / this.Smoothing.y);

            Vector2 mousePosition = this.CurrentView;
            mousePosition += mousePositionSmooth;

            if (this.ViewClamp.x < 360)
                mousePosition.x = Mathf.Clamp(mousePosition.x, -this.ViewClamp.x * 0.5f + this.ViewClampOffset.x, this.ViewClamp.x * 0.5f + this.ViewClampOffset.x);

            if (this.ViewClamp.y < 360)
                mousePosition.y = Mathf.Clamp(mousePosition.y, -this.ViewClamp.y * 0.5f + this.ViewClampOffset.y, this.ViewClamp.y * 0.5f + this.ViewClampOffset.y);

            if (this.Pitch != null)
            {
                var rotation = Quaternion.AngleAxis(-mousePosition.y, targetOrientation * Vector3.right) * targetOrientation;
                this.Pitch.localRotation = rotation * targetCharacterOrientation;
            }

            if (this.Yaw != null)
            {
                var rotation = Quaternion.AngleAxis(mousePosition.x, Vector3.up);
                this.Yaw.localRotation = rotation * targetCharacterOrientation;
            }

            this.CurrentView = mousePosition;
            this.CurrentViewSmooth = mousePositionSmooth;
        }
    }
}