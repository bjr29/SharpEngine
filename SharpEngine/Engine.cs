using static SDL2.SDL;
using static SDL2.SDL_image;
using static SDL2.SDL_ttf;
using static SDL2.SDL_mixer;
using System;

namespace SharpEngine {
    /// <summary>
    /// Handles the main internals of the engine.
    /// </summary>
    public static class Engine {
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
        public static float FPS { get; private set; }
        /// <summary>
        /// The lasts frame's delta in seconds.
        /// </summary>
        public static float DeltaTime { get; private set; }
        /// <summary>
        /// The native resolutions of the connected screens.
        /// </summary>
        public static Vector2[] NativeScreenResolutions { 
            get {
                Vector2[] resolutions = new Vector2[SDL_GetNumVideoDisplays()];

                for (int i = 0; i < resolutions.Length; i++) {
                    _ = SDL_GetCurrentDisplayMode(i, out SDL_DisplayMode displayMode);
                    resolutions[i] = new(displayMode.w, displayMode.h);
                }

                return resolutions;
            }
        }

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

        private enum HandleEventReturn {
            None,
            Quit
        }

        /// <summary>
        /// Run this method first! Initialises the engine.
        /// </summary>
        public static void Init() {
            _ = SDL_Init(SDL_INIT_EVERYTHING);
            _ = IMG_Init(IMG_InitFlags.IMG_INIT_JPG | IMG_InitFlags.IMG_INIT_PNG);
            _ = TTF_Init();
            _ = Mix_Init(MIX_InitFlags.MIX_INIT_MP3 | MIX_InitFlags.MIX_INIT_OGG);

            Debug.ErrorCheckSDL();

            _ = Mix_OpenAudio(22050, AUDIO_S16SYS, 2, 4096);

            Debug.ErrorCheckSDL();
        }

        /// <summary>
        /// Starts the main loop for the engine.
        /// </summary>
        public static void Start() {
            Ready?.Invoke(null, new());

            DateTime frameStart = DateTime.Now;
            DateTime nextDebugInterval = DateTime.Now.AddSeconds(Debug.FPS_ReportInterval);

            while (true) {
                if (!EnforceFPSCap || FPS <= MaxFPS) {
                    if (Debug.ReportFPS && DateTime.Now > nextDebugInterval) {
                        nextDebugInterval = DateTime.Now.AddSeconds(Debug.FPS_ReportInterval);
                        Debug.Log($"{Math.Round(FPS)} FPS");
                    }

                    frameStart = DateTime.Now;

                    if (HandleEvents() == HandleEventReturn.Quit) {
                        break;
                    }

                    PreDraw();
                    DrawRenderables.Draw();
                    Draw?.Invoke(null, new());
                    PostDraw();
                }

                DateTime frameEnd = DateTime.Now;
                DeltaTime = (float)(frameEnd - frameStart).TotalSeconds;
                FPS = 1 / DeltaTime;
            }

            Close();
        }

        private static void Close() {
            Stopping?.Invoke(null, new());

            Window.Dispose();

            Mix_CloseAudio();

            IMG_Quit();
            TTF_Quit();
            Mix_Quit();
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
    }
}
