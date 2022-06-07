using UnityEngine;

namespace Misc.AssetVariables
{
    [CreateAssetMenu(fileName = "Transform", menuName = "Asset Variables/new Transform")]
    public class TransformVariable : ScriptableObject
    {
        [SerializeField] private Transform value;
        public Transform Value { get { return value; } set { this.value = value; } }

        public static implicit operator Transform(TransformVariable variable) => variable.Value;
    }

    [System.Serializable]
    public class TransformReference
    {
        [SerializeField] private Transform constant;
        public Transform Constant { get { return constant; } set { constant = value; } }

        [SerializeField] private bool useConstant;
        public bool UseConstant { get { return useConstant; } set { useConstant = value; } }

        [SerializeField] private TransformVariable variable;
        public TransformVariable Variable { get { return variable; } set { variable = value; } }

        public Transform Value { get => this.UseConstant ? this.Constant : this.Variable; }

        public TransformReference()
        { }

        public TransformReference(Transform value)
        {
            this.UseConstant = true;
            this.Constant = value;
        }

        public static implicit operator Transform(TransformReference reference)
        {
            return reference.Value;
        }
    }
}
