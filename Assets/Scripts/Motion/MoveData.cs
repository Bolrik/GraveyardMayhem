using UnityEngine;

namespace Motion
{
    [CreateAssetMenu(fileName = "MoveData", menuName = "General/new Move Data")]
    public class MoveData : ScriptableObject
    {
        [SerializeField] private float speed = 1;
        public float Speed { get { return speed; } }

    }
}