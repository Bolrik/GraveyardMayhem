using UnityEngine;

namespace Decorations
{
    [CreateAssetMenu(fileName = "GroundTileManagerData", menuName = "Decorations/new Ground Tile Manager Data")]
    class GroundTileManagerData : ScriptableObject
    {
        [SerializeField] private Sprite[] sprites;
        public Sprite[] Sprites { get { return sprites; } }

        [SerializeField] private GroundTile groundTile;
        public GroundTile GroundTile { get { return groundTile; } }

        [SerializeField] private int width = 3;
        public int Width { get { return width; } }


    }
}