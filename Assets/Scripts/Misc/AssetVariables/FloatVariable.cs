using UnityEngine;

namespace Misc.AssetVariables
{
    [CreateAssetMenu(fileName = "Float", menuName = "Asset Variables/new Float")]
    public class FloatVariable : ScriptableObject
    {
        [SerializeField] private float value;
        public float Value { get { return value; } set { this.value = value; } }

        public static implicit operator float(FloatVariable variable) => variable.Value;
    }

    [System.Serializable]
    public class FloatReference
    {
        [SerializeField] private float constant;
        public float Constant { get { return constant; } set { constant = value; } }

        [SerializeField] private bool useConstant;
        public bool UseConstant { get { return useConstant; } set { useConstant = value; } }

        [SerializeField] private FloatVariable variable;
        public FloatVariable Variable { get { return variable; } set { variable = value; } }

        public float Value { get => this.UseConstant ? this.Constant : this.Variable; }

        public FloatReference()
        { }

        public FloatReference(float value)
        {
            this.UseConstant = true;
            this.Constant = value;
        }

        public static implicit operator float(FloatReference reference)
        {
            return reference.Value;
        }
    }
}
