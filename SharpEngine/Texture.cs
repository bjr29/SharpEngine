using SDL2;
using System;
using static SDL2.SDL;
using static SDL2.SDL_image;

namespace SharpEngine {
    /// <summary>
    /// Used to show images.
    /// </summary>
    public class Texture : IDisposable {
        /// <summary>
        /// The path to the image
        /// </summary>
        public string Path {
            get => _Path;
            set {
                _Path = value;

                TexturePtr = IMG_LoadTexture(RendererPtr, _Path);
                Debug.ErrorCheckSDL();
            }
        }

        /// <summary>
        /// Modifies the colours of the texture.
        /// </summary>
        public Colour ColourMod {
            get {
                _ = SDL_GetTextureColorMod(TexturePtr, out byte r, out byte g, out byte b);
                _ = SDL_GetTextureAlphaMod(TexturePtr, out byte a);

                Debug.ErrorCheckSDL();

                return new(r, g, b, a);
            }
            set {
                _ = SDL_SetTextureColorMod(TexturePtr, value.R, value.G, value.B);
                _ = SDL_SetTextureAlphaMod(TexturePtr, value.A);

                Debug.ErrorCheckSDL();
            }
        }

        /// <summary>
        /// Gets the texture's size.
        /// </summary>
        public IntVector2 Size {
            get {
                _ = SDL_QueryTexture(TexturePtr, out _, out _, out int width, out int height);
                return new(width, height);
            }
        }

        private string _Path { get; set; }

        internal IntPtr TexturePtr { get; set; } = IntPtr.Zero;

        internal static IntPtr RendererPtr { get => Engine.Window.RendererPtr; }

        /// <summary>
        /// Creates the texture from the path.
        /// </summary>
        /// <param name="path">The texture's image path.</param>
        public Texture(string path) {
            Path = path;
        }

        /// <summary>
        /// Creates an empty texture.
        /// </summary>
        public Texture(IntVector2 size, Colour colour, bool setRenderTarget = false) {
            TexturePtr = SDL_CreateTexture(
                RendererPtr,
                SDL_PIXELFORMAT_RGBA8888,
                (int) SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET,
                size.X,
                size.Y
            );

            Colour originalColour = Engine.Window.BackgroundColour;

            Drawing.RenderToTarget(this);

            Drawing.SetDrawColour(colour);
            Drawing.DrawRect(new(), size, colour);

            if (!setRenderTarget) {
                Drawing.RenderToScreen();
            }

            Drawing.SetDrawColour(originalColour);
        }

        private bool DisposedValue { get; set; }

        /// <summary>
        /// Disposes the texture.
        /// </summary>
        protected virtual void Dispose(bool disposing) {
            if (!DisposedValue) {
                DisposedValue = true;

                SDL_DestroyTexture(TexturePtr);
            }
        }

        /// <summary>
        /// Disposes the texture.
        /// </summary>
        ~Texture() {
            Dispose(disposing: false);
        }

        /// <summary>
        /// Disposes the texture.
        /// </summary>
        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
