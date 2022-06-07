using UnityEngine;

namespace Decorations
{
    [CreateAssetMenu(fileName = "DecorationsManagerData", menuName = "Decorations/new Decorations Manager Data")]
    public class DecorationsManagerData : ScriptableObject
    {
        [SerializeField] private DecorationData[] datas;
        public DecorationData[] Datas { get { return datas; } }

        [SerializeField, Tooltip("The Decoration Prefab")] private Decoration decoration;
        public Decoration Decoration { get { return decoration; } }


        public DecorationData GetDecorationData()
        {
            int idx = Random.Range(0, this.Datas.Length);
            return this.GetDecorationData(idx);
        }

        public DecorationData GetDecorationData(int idx)
        {
            return this.Datas[idx];
        }
    }
}