using static SDL2.SDL;

namespace SharpEngine {
    /// <summary>
    /// Displays a texture.
    /// </summary>
    public class Sprite : IRenderable {
        /// <summary>
        /// The position of the object (top left point).
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// The size of the object.
        /// </summary>
        public Vector2 Size { get; set; }
        /// <summary>
        /// The angle of the sprite.
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// The texture displayed by the sprite.
        /// </summary>
        public Texture Texture { get; set; }

        /// <summary>
        /// Should be rendered by DrawRenderables.
        /// </summary>
        public bool Show { get; set; } = true;
        /// <summary>
        /// The order to be rendered in, only applied by DrawRenderables.
        /// </summary>
        public int ZIndex { get; set; }

        /// <summary>
        /// Flip the texture horizontally.
        /// </summary>
        public bool FlipX { get; set; }
        /// <summary>
        /// Flip the texture vertically.
        /// </summary>
        public bool FlipY { get; set; }

        /// <summary>
        /// Creates a sprite.
        /// </summary>
        /// <param name="position">The position of the object (top left point).</param>
        /// <param name="size">The size of the object.</param>
        /// <param name="texture">The texture displayed by the sprite.</param>
        /// <param name="rotation">The angle of the sprite.</param>
        /// <param name="zIndex">The index to be rendered on, only applied by DrawRenderables.</param>
        public Sprite(Vector2 position, Vector2 size, Texture texture, int rotation = 0, int zIndex = 0) {
            Position = position;
            Size = size;
            Texture = texture;
            Rotation = rotation;
            ZIndex = zIndex;

            DrawRenderables.RegisteredRenderables.Add(this);
        }

        /// <summary>
        /// The render method.
        /// </summary>
        public void Draw() {
            if (Position.X > Engine.Window.WindowSize.X && Position.Y > Engine.Window.WindowSize.Y 
                && Position.X + Size.X > 0 && Position.Y + Size.Y > 0) {

                return;
            }

            SDL_FRect rect = Vector2.ToSDL_FRect(Position, Size);

            SDL_RendererFlip flips = SDL_RendererFlip.SDL_FLIP_NONE;

            if (FlipX) {
                flips |= SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
            }

            if (FlipY) {
                flips |= SDL_RendererFlip.SDL_FLIP_VERTICAL;
            }

            _ = SDL_RenderCopyExF(
                Engine.Window.RendererPtr,
                Texture.TexturePtr,
                IntPtr.Zero,
                ref rect,
                Rotation,
                IntPtr.Zero,
                flips
            );

            Debug.ErrorCheckSDL();
        }
    }
}
