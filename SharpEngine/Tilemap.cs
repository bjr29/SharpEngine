namespace SharpEngine {
    public class Tilemap : IRenderable {
        public Vector2 Position { get; set; }
        /// <summary>
        /// Size per cell.
        /// </summary>
        public Vector2 Size { get; set; } = new(32, 32);

        public List<Texture> Tileset { get; set; }
        public List<Tuple<int, IntVector2>> Tiles { get; set; }

        public bool Show { get; set; } = true;
        public int ZIndex { get; set; } = -1;

        public Tilemap() {
            DrawRenderables.RegisteredRenderables.Add(this);
        }

        public Tilemap(Vector2 position, Vector2 size, List<Texture> tileset = null, List<Tuple<int, IntVector2>> tiles = null) {
            Position = position;
            Size = size;
            Tileset = tileset;
            Tiles = tiles;

            DrawRenderables.RegisteredRenderables.Add(this);
        }

        public void Draw() {
            Sprite tileSprite = new(new(), Size, null) {
                Show = false
            };

            foreach (Tuple<int, IntVector2> tile in Tiles) {
                tileSprite.Position = tile.Item2 * Size;
                tileSprite.Texture = Tileset[tile.Item1];

                tileSprite.Draw();
            }

            DrawRenderables.RegisteredRenderables.Remove(tileSprite);
        }
    }
}
