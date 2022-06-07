using UnityEngine;

namespace Misc.AssetVariables
{
    [CreateAssetMenu(fileName = "Integer", menuName = "Asset Variables/new Integer")]
    public class IntVariable : ScriptableObject
    {
        [SerializeField] private int value;
        public int Value { get { return value; } set { this.value = value; } }

        public static implicit operator int(IntVariable variable) => variable.Value;
    }

    [System.Serializable]
    public class IntReference
    {
        [SerializeField] private int constant;
        public int Constant { get { return constant; } set { constant = value; } }

        [SerializeField] private bool useConstant;
        public bool UseConstant { get { return useConstant; } set { useConstant = value; } }

        [SerializeField] private IntVariable variable;
        public IntVariable Variable { get { return variable; } set { variable = value; } }

        public int Value { get => this.UseConstant ? this.Constant : this.Variable; }

        public IntReference()
        { }

        public IntReference(int value)
        {
            this.UseConstant = true;
            this.Constant = value;
        }

        public static implicit operator int(IntReference reference)
        {
            return reference.Value;
        }
    }
}
