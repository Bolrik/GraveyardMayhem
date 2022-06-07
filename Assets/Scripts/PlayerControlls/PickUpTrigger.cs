using UnityEngine;

namespace PlayerControlls
{
    public class PickUpTrigger : MonoBehaviour
    {
        [SerializeField] private Player player;
        public Player Player { get { return player; } }
    }
}
