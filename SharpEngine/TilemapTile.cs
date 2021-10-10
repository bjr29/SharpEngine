namespace SharpEngine {
    /// <summary>
    /// A tile used within a tilemap.
    /// </summary>
    public struct TilemapTile {
        /// <summary>
        /// The index in the tileset.
        /// </summary>
        public int TilesetTile { get; set; }
        /// <summary>
        /// The position within the tilemap.
        /// </summary>
        public IntVector2 Position { get; set; }
        /// <summary>
        /// Flip the texture horizontally.
        /// </summary>
        public bool FlipX { get; set; }
        /// <summary>
        /// Flip the texture vertically.
        /// </summary>
        public bool FlipY { get; set; }

        /// <summary>
        /// Creates a tilemap tile.
        /// </summary>
        /// <param name="tilesetTile">The index in the tileset.</param>
        /// <param name="position">The position in the tilemap.</param>
        /// <param name="flipX">Flip the texture horizontally.</param>
        /// <param name="flipY">Flip the texture vertically.</param>
        public TilemapTile(int tilesetTile, IntVector2 position, bool flipX = false, bool flipY = false) {
            TilesetTile = tilesetTile;
            Position = position;
            FlipX = flipX;
            FlipY = flipY;
        }
    }
}
