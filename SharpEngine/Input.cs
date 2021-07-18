using System;
using System.Collections.Generic;
using static SDL2.SDL;

namespace SharpEngine {
    /// <summary>
    /// Handles the input from the user.
    /// </summary>
    public static class Input {
        #region Properties
        /// <summary>
        /// The string of the last key down.
        /// </summary>
        public static string LastKeyDown { get; private set; }
        /// <summary>
        /// The string of the last key up.
        /// </summary>
        public static string LastKeyUp { get; private set; }
        /// <summary>
        /// The last mouse button down.
        /// </summary>
        public static MouseButton LastMouseButtonDown { get; private set; }
        /// <summary>
        /// The last mouse button up.
        /// </summary>
        public static MouseButton LastMouseButtonUp { get; private set; }
        /// <summary>
        /// The cursors's position within the window.
        /// </summary>
        public static IntVector2 MousePosition { 
            get {
                _ = SDL_GetMouseState(out int x, out int y);
                return new(x, y);
            }
            set {
                SDL_WarpMouseInWindow(Engine.Window.WindowPtr, value.X, value.Y);
            }
        }
        /// <summary>
        /// The velocity of the cursor.
        /// </summary>
        public static IntVector2 MouseMotion { get; private set; }
        /// <summary>
        /// The cursor's position on the screen.
        /// </summary>
        public static IntVector2 GlobalMousePosition {
            get {
                _ = SDL_GetGlobalMouseState(out int x, out int y);
                return new(x, y);
            }
            set {
                _ = SDL_WarpMouseGlobal(value.X, value.Y);
            }
        }
        /// <summary>
        /// The velocity of the mouse's scroll wheel.
        /// </summary>
        public static int ScrollWheel { get; private set; }

        private static Dictionary<MouseButton, bool> MouseButtonsDown { get; set; } = new();
        private static Dictionary<string, bool> KeysDown { get; set; } = new();
        #endregion

        #region Events
        /// <summary>
        /// Invoked when a keyboard's key has been pushed down.
        /// </summary>
        public static event EventHandler<KeyPressedEventArgs> KeyDown;
        /// <summary>
        /// Invoked when a keyboard's key has been released.
        /// </summary>
        public static event EventHandler<KeyPressedEventArgs> KeyUp;
        /// <summary>
        /// Invoked when the mouse is moved.
        /// </summary>
        public static event EventHandler<MouseMotionEventArgs> MouseMoved;
        /// <summary>
        /// Invoked when a button on the mouse is pushed down.
        /// </summary>
        public static event EventHandler<MouseButtonEventArgs> MouseButtonDown;
        /// <summary>
        /// Invoked when a button on the mouse is released.
        /// </summary>
        public static event EventHandler<MouseButtonEventArgs> MouseButtonUp;
        /// <summary>
        /// When the mouse wheel has been scrolled.
        /// </summary>
        public static event EventHandler<MouseWheelEventArgs> MouseWheelScrolled;
        #endregion

        #region Enums
        /// <summary>
        /// The buttons of the mouse.
        /// </summary>
        public enum MouseButton {
            Left = 1,
            Middle = 2,
            Right = 3,
            X1 = 4 ,
            X2 = 5
        }
        #endregion

        #region Methods
        /// <summary>
        /// If the key is currently down.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>Returns true if the key is down.</returns>
        public static bool IsKeyDown(string key) {
            if (KeysDown.TryGetValue(key, out bool value)) {
                return value;
            }

            return false;
        }

        /// <summary>
        /// If the mouse button is currently down.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>Returns true if the button is down.</returns>
        public static bool IsMouseButtonDown(MouseButton button) {
            if (MouseButtonsDown.TryGetValue(button, out bool value)) {
                return value;
            }

            return false;
        }

        internal static void HandleInputEvent(SDL_Event @event) {
            switch (@event.type) {
                case SDL_EventType.SDL_KEYDOWN:
                    LastKeyDown = SDL_GetKeyName(@event.key.keysym.sym);

                    if (KeysDown.ContainsKey(LastKeyDown)) {
                        KeysDown[LastKeyDown] = true;

                    } else {
                        KeysDown.Add(LastKeyDown, true);
                    }

                    KeyDown?.Invoke(null, new KeyPressedEventArgs(LastKeyDown));

                    break;

                case SDL_EventType.SDL_KEYUP:
                    LastKeyUp = SDL_GetKeyName(@event.key.keysym.sym);
                    KeysDown[LastKeyUp] = false;

                    KeyUp?.Invoke(null, new KeyPressedEventArgs(LastKeyUp));

                    break;

                case SDL_EventType.SDL_MOUSEMOTION:
                    IntVector2 motion = new(@event.motion.xrel, @event.motion.yrel);
                    MouseMotion = motion;

                    MouseMoved?.Invoke(null, new MouseMotionEventArgs(motion, MousePosition, GlobalMousePosition));

                    break;

                case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                    LastMouseButtonDown = (MouseButton)@event.button.button;
                    MouseButtonsDown[LastMouseButtonDown] = true;

                    if (MouseButtonsDown.ContainsKey(LastMouseButtonDown)) {
                        MouseButtonsDown[LastMouseButtonDown] = true;
                    } else {
                        MouseButtonsDown.Add(LastMouseButtonDown, true);
                    }

                    MouseButtonDown?.Invoke(null, new(LastMouseButtonDown));

                    break;

                case SDL_EventType.SDL_MOUSEBUTTONUP:
                    LastMouseButtonUp = (MouseButton)@event.button.button;
                    MouseButtonsDown[LastMouseButtonDown] = false;

                    MouseButtonUp?.Invoke(null, new MouseButtonEventArgs(LastMouseButtonUp));

                    break;

                case SDL_EventType.SDL_MOUSEWHEEL:
                    ScrollWheel = @event.wheel.y;

                    MouseWheelScrolled?.Invoke(null, new MouseWheelEventArgs(ScrollWheel));

                    break;
            }
        }
        #endregion
    }
}
