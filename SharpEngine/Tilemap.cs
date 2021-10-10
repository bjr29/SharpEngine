namespace SharpEngine {
    /// <summary>
    /// A collection of sprites used as tiles.
    /// </summary>
    public class Tilemap : IRenderable {
        /// <summary>
        /// The position of the object (center).
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Size per cell.
        /// </summary>
        public Vector2 Size { get; set; } = new(32, 32);

        /// <summary>
        /// Sprites to use as tiles.
        /// </summary>
        public List<Texture> Tileset { get; set; }
        /// <summary>
        /// The placement of tiles.
        /// </summary>
        public List<TilemapTile> Tiles { get; set; }

        /// <summary>
        /// Should be rendered by DrawRenderables.
        /// </summary>
        public bool Show { get; set; } = true;
        /// <summary>
        /// The order to be rendered in, only applied by DrawRenderables.
        /// </summary>
        public int ZIndex { get; set; } = -1;

        /// <summary>
        /// Creates the tilemap.
        /// </summary>
        public Tilemap() {
            DrawRenderables.RegisteredRenderables.Add(this);
        }

        /// <summary>
        /// Creates the tilemap.
        /// </summary>
        public Tilemap(Vector2 position, Vector2 size, List<Texture> tileset = null, List<TilemapTile> tiles = null) {
            Position = position;
            Size = size;
            Tileset = tileset;
            Tiles = tiles;

            DrawRenderables.RegisteredRenderables.Add(this);
        }

        /// <summary>
        /// The render method.
        /// </summary>
        public void Draw() {
            Sprite tileSprite = new(new(), Size, null) {
                Show = false
            };

            foreach (TilemapTile tile in Tiles) {
                tileSprite.Position = tile.Position * Size;
                tileSprite.FlipX = tile.FlipX;
                tileSprite.FlipY = tile.FlipY;
                tileSprite.Texture = Tileset[tile.TilesetTile];

                tileSprite.Draw();
            }

            DrawRenderables.RegisteredRenderables.Remove(tileSprite);
        }
    }
}
