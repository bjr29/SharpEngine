using System;
using static SDL2.SDL;

namespace SharpEngine {
    /// <summary>
    /// Handles the main internals of the engine.
    /// </summary>
    public static class Engine {
        #region Properties
        /// <summary>
        /// The currently active window.
        /// </summary>
        public static Window Window { get; set; }
        /// <summary>
        /// FPS limiter.
        /// </summary>
        public static int MaxFPS { get; set; }
        /// <summary>
        /// Caps the FPS to MaxFPS.
        /// </summary>
        public static bool EnforceFPSCap { get; set; }
        /// <summary>
        /// The current FPS.
        /// </summary>
        public static int FPS { get; private set; }
        /// <summary>
        /// The lasts frame's delta in seconds.
        /// </summary>
        public static float DeltaTime { get; private set; }
        #endregion

        #region Events
        /// <summary>
        /// Invoked when the engine has started the main loop.
        /// </summary>
        public static event EventHandler Ready;
        /// <summary>
        /// When the engine is ready to process game logic and draw to the window.
        /// </summary>
        public static event EventHandler Draw;
        /// <summary>
        /// When the engine is stopping as a window has been closed.
        /// </summary>
        public static event EventHandler Stopping;
        #endregion

        #region Enums
        private enum HandleEventReturn {
            None,
            Quit
        }
        #endregion

        #region Methods
        /// <summary>
        /// Run this method first! Initialises the engine.
        /// </summary>
        public static void Init() {
            _ = SDL_Init(SDL_INIT_EVERYTHING);

            Debug.ErrorCheckSDL();
        }

        /// <summary>
        /// Starts the main loop for the engine.
        /// </summary>
        public static void Start() {
            Ready?.Invoke(null, new());

            DateTime frameStart = DateTime.Now;

            while (true) {
                if (!EnforceFPSCap || FPS <= MaxFPS) {
                    frameStart = DateTime.Now;

                    if (HandleEvents() == HandleEventReturn.Quit)
                        break;

                    PreDraw();
                    Draw?.Invoke(null, new());
                    PostDraw();
                }

                DateTime frameEnd = DateTime.Now;
                DeltaTime = (float)(frameEnd - frameStart).TotalSeconds;
                FPS = (int)(1 / DeltaTime);
            }

            Close();
        }

        private static void Close() {
            Stopping?.Invoke(null, new());

            Window.Dispose();
            SDL_Quit();
        }

        private static void PreDraw() {
            _ = SDL_SetRenderDrawColor(
                Window.RendererPtr,
                Window.BackgroundColour.R,
                Window.BackgroundColour.G,
                Window.BackgroundColour.B,
                Window.BackgroundColour.A
            );

            _ = SDL_RenderClear(Window.RendererPtr);
        }

        private static void PostDraw() {
            SDL_RenderPresent(Window.RendererPtr);
        }

        private static HandleEventReturn HandleEvents() {
            while (SDL_PollEvent(out SDL_Event @event) > 0) {
                switch (@event.type) {
                    case SDL_EventType.SDL_QUIT:
                        return HandleEventReturn.Quit;

                    default:
                        Input.HandleInputEvent(@event);
                        break;
                }
            }

            return HandleEventReturn.None;
        }
        #endregion
    }
}
