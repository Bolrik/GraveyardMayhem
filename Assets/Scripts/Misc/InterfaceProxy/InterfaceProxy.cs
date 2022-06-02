using UnityEngine;

namespace Misc
{
    [System.Serializable]
    public class InterfaceProxy : MonoBehaviour
    {
        [SerializeField] protected GameObject provider;
    }

    public class InterfaceProxy<T> : InterfaceProxy
        where T : class
    {
        public T Interface
        {
            get
            {
                T toReturn = provider.GetComponent<T>();
                return toReturn;
            }
        }
    }
}