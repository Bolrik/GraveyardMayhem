using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Decorations
{
    class GroundTile : MonoBehaviour
    {

        [SerializeField] private SpriteRenderer renderer;
        public SpriteRenderer Renderer { get { return renderer; } }

        public void SetSprite(Sprite sprite)
        {
            this.Renderer.sprite = sprite;
        }

    }
}