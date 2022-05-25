using UnityEngine;

namespace Motion
{
    [CreateAssetMenu(fileName = "newViewData", menuName = "General/new View Data")]
    public class ViewData : ScriptableObject
    {
        [SerializeField] private Vector2 viewClamp = new Vector2(360, 180);
        public Vector2 ViewClamp { get { return this.viewClamp; } }
        
        [SerializeField] private Vector2 viewClampOffset = new Vector2(0, 0);
        public Vector2 ViewClampOffset { get { return this.viewClampOffset; } }

        [SerializeField] private Vector2 sensitivit = new Vector2(2, 2);
        public Vector2 Sensitivit { get { return this.sensitivit; } }

        [SerializeField] private Vector2 smoothing = new Vector2(3, 3);
        public Vector2 Smoothing { get { return this.smoothing; } private set { this.smoothing = value; } }


    }
}