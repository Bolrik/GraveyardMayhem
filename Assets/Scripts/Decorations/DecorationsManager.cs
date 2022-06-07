using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Decorations
{
    public class DecorationsManager : MonoBehaviour
    {
        [SerializeField] private DecorationsManagerData data;
        public DecorationsManagerData Data { get { return data; } }

    }

    [CreateAssetMenu(fileName = "DecorationsManagerData", menuName = "Decorations/new Decorations Manager Data")]
    public class DecorationsManagerData : ScriptableObject
    {

    }
}