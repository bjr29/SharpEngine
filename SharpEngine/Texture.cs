using System;
using static SDL2.SDL;
using static SDL2.SDL_image;

namespace SharpEngine {
    /// <summary>
    /// Used to show images.
    /// </summary>
    public class Texture : IDisposable {
        #region Properties
        /// <summary>
        /// The path to the image
        /// </summary>
        public string Path {
            get => _Path;
            set {
                _Path = value;

                TexturePtr = IMG_LoadTexture(Engine.Window.RendererPtr, _Path);
                Debug.ErrorCheckSDL();
            }
        }

        private string _Path { get; set; }

        internal IntPtr TexturePtr { get; set; } = IntPtr.Zero;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates the texture from the path.
        /// </summary>
        /// <param name="path">The texture's image path.</param>
        public Texture(string path) {
            Path = path;
        }
        #endregion

        #region Inherited Properties
        private bool DisposedValue { get; set; }
        #endregion

        #region Inherited Methods
        protected virtual void Dispose(bool disposing) {
            if (!DisposedValue) {
                DisposedValue = true;

                SDL_DestroyTexture(TexturePtr);
            }
        }

        ~Texture() {
            Dispose(disposing: false);
        }

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
