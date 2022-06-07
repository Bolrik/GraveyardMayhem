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

        List<Decoration> Decorations { get; set; }

        Transform DecorationsRoot { get; set; }

        private void Awake()
        {
            this.DecorationsRoot = new GameObject("Decorations").transform;

            this.Decorations = new List<Decoration>();
        }

        private void Update()
        {
            // Create one per Frame if needed
            if (this.Decorations.Count <= 20)
            {
                DecorationData data = this.Data.GetDecorationData();

                Decoration decoration = GameObject.Instantiate(this.Data.Decoration);
                decoration.SetData(data);
                decoration.transform.SetParent(this.DecorationsRoot, false);

                Vector3 position = new Vector3();
                position.x = Random.Range(3, 28) * Mathf.Sign(Random.value - .5f);
                position.z = Random.Range(3, 28) * Mathf.Sign(Random.value - .5f);
                decoration.transform.position = position;
                this.Decorations.Add(decoration);

                decoration.OnDespawn += this.Decoration_OnDespawn;
            }
        }

        void Decoration_OnDespawn(Decoration decoration)
        {
            this.Decorations.Remove(decoration);
        }
    }
}