using static SDL2.SDL;

namespace SharpEngine {
    /// <summary>
    /// A window which can be rendered to.
    /// </summary>
    public class Window : IDisposable {
        #region Properties
        /// <summary>
        /// The size of the window.
        /// </summary>
        public IntVector2 WindowSize {
            get {
                SDL_GetWindowSize(WindowPtr, out int x, out int y);
                return new(x, y);
            }
            set => SDL_SetWindowSize(WindowPtr, value.X, value.Y);
        }
        /// <summary>
        /// The window's background.
        /// </summary>
        public Colour BackgroundColour { get; set; } = new();
        /// <summary>
        /// Gets the monitors' resolutions.
        /// </summary>
        public static IntVector2[] MonitorResolutions {
            get {
                IntVector2[] screenResolutions = new IntVector2[SDL_GetNumVideoDisplays()];

                for (int i = 0; i < screenResolutions.Length; i++) {
                    _ = SDL_GetCurrentDisplayMode(0, out SDL_DisplayMode displayMode);
                    screenResolutions[i] = new(displayMode.w, displayMode.h);

                    Debug.ErrorCheckSDL();
                }

                return screenResolutions;
            }
        }

        internal IntPtr WindowPtr { get; set; }
        internal IntPtr RendererPtr { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a window.
        /// </summary>
        /// <param name="size">The size of the window.</param>
        /// <param name="title">The window's displayed title.</param>
        /// <param name="fullscreen">Should the window be started in fullscreen.</param>
        /// <param name="maximised">Should the window be started maximised.</param>
        public Window(IntVector2 size, string title = "Sharp Engine Game", bool fullscreen = false, bool maximised = false) =>
            CreateWindow(size.X, size.Y, title, fullscreen, maximised);

        /// <summary>
        /// Creates a window.
        /// </summary>
        /// <param name="width">The width of the window.</param>
        /// <param name="height">The height of the window.</param>
        /// <param name="title">The window's displayed title.</param>
        /// <param name="fullscreen">Should the window be started in fullscreen.</param>
        /// <param name="maximised">Should the window be started maximised.</param>
        public Window(int width = 600, int height = 400, string title = "Sharp Engine Game", bool fullscreen = false, bool maximised = false) =>
            CreateWindow(width, height, title, fullscreen, maximised);
        #endregion

        #region Methods
        private void CreateWindow(int x, int y, string title, bool fullscreen, bool maximised) {
            SDL_WindowFlags flags = SDL_WindowFlags.SDL_WINDOW_RESIZABLE;

            if (fullscreen)
                flags |= SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;

            if (maximised)
                flags |= SDL_WindowFlags.SDL_WINDOW_MAXIMIZED;

            WindowPtr = SDL_CreateWindow(
                title,
                SDL_WINDOWPOS_CENTERED,
                SDL_WINDOWPOS_CENTERED,
                x,
                y,
                flags
            );

            Debug.ErrorCheckSDL();

            RendererPtr = SDL_CreateRenderer(
                WindowPtr,
                0,
                SDL_RendererFlags.SDL_RENDERER_ACCELERATED
            );

            Debug.ErrorCheckSDL();
        }
        #endregion

        #region Inherited Properties
        private bool DisposedValue { get; set; }
        #endregion

        #region Inherited Methods
        ~Window() {
            Dispose(disposing: true);
        }

        protected virtual void Dispose(bool disposing) {
            if (!DisposedValue) {
                SDL_DestroyRenderer(RendererPtr);
                SDL_DestroyWindow(WindowPtr);

                DisposedValue = true;
            }
        }

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
