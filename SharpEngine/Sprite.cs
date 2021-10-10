using static SDL2.SDL;

namespace SharpEngine {
    public class Sprite : IRenderable {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public float Rotation { get; set; }

        public Texture Texture { get; set; }

        public bool Show { get; set; } = true;
        public int ZIndex { get; set; }

        public bool FlipX { get; set; }
        public bool FlipY { get; set; }

        public Sprite(Vector2 position, Vector2 size, Texture texture, int rotation = 0, int zIndex = 0) {
            Position = position;
            Size = size;
            Texture = texture;
            Rotation = rotation;
            ZIndex = zIndex;

            DrawRenderables.RegisteredRenderables.Add(this);
        }

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
