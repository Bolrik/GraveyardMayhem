using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Decorations
{
    class GroundTileManager : MonoBehaviour
    {
        [SerializeField] private GroundTileManagerData data;
        public GroundTileManagerData Data { get { return data; } }

        GameObject Root { get; set; }

        private void Start()
        {
            this.Root = new GameObject("Ground Tiles");

            for (int x = 0; x < 30; x++)
            {
                int width = this.Data.Width / 2;

                for (int y = -width; y <= width; y++)
                {
                    this.Create(x, y);

                    if (x > 0)
                        this.Create(-x, y);

                    if (x > width)
                    {
                        this.Create(y, x);
                        this.Create(y, -x);
                    }
                }
            }
        }

        private GroundTile Create(int x, int y)
        {
            GroundTile toReturn = GameObject.Instantiate(this.Data.GroundTile);
            toReturn.SetSprite(this.Data.Sprites[Random.Range(0, this.Data.Sprites.Length)]);

            toReturn.transform.SetParent(this.Root.transform, false);
            toReturn.transform.position = new Vector3(x, 0, y);

            return toReturn;
        }
    }
}