using UnityEngine;

namespace PlayerControlls
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Player/new Player Data")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private float health;
        public float Health { get { return health; } }

    }
}
