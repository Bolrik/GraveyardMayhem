using PlayerControlls;
using UnityEngine;

namespace Misc.AssetVariables
{
    [CreateAssetMenu(fileName = "Player", menuName = "Asset Variables/new Player")]
    public class PlayerVariable : ScriptableObject
    {
        [SerializeField] private Player value;
        public Player Value { get { return value; } set { this.value = value; } }

        public static implicit operator Player(PlayerVariable variable) => variable.Value;
    }

    [System.Serializable]
    public class PlayerReference
    {
        [SerializeField] private Player constant;
        public Player Constant { get { return constant; } set { constant = value; } }

        [SerializeField] private bool useConstant;
        public bool UseConstant { get { return useConstant; } set { useConstant = value; } }

        [SerializeField] private PlayerVariable variable;
        public PlayerVariable Variable { get { return variable; } set { variable = value; } }

        public Player Value { get => this.UseConstant ? this.Constant : this.Variable; }

        public PlayerReference()
        { }

        public PlayerReference(Player value)
        {
            this.UseConstant = true;
            this.Constant = value;
        }

        public static implicit operator Player(PlayerReference reference)
        {
            return reference.Value;
        }
    }
}
