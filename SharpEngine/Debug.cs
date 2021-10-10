using static SDL2.SDL;
using static SDL2.SDL_image;

namespace SharpEngine {
    /// <summary>
    /// Used for debuging your game.
    /// </summary>
    public static class Debug {
        #region Properties
        /// <summary>
        /// The default background colour of the console.
        /// </summary>
        public static ConsoleColor DefaultBackground { get; set; } = Console.BackgroundColor;
        /// <summary>
        /// The default foreground colour of the console.
        /// </summary>
        public static ConsoleColor DefaultForeground { get; set; } = Console.ForegroundColor;
        /// <summary>
        /// The background colour of the console message type - [LOG].
        /// </summary>
        public static ConsoleColor LogNotificationBackground { get; set; } = ConsoleColor.White;
        /// <summary>
        /// The foreground colour of the console message type - [LOG].
        /// </summary>
        public static ConsoleColor LogNotificationForeground { get; set; } = ConsoleColor.Black;
        /// <summary>
        /// The background colour of the console message type - [ERR].
        /// </summary>
        public static ConsoleColor WarningNotificationBackground { get; set; } = ConsoleColor.Red;
        /// <summary>
        /// The foreground colour of the console message type - [ERR].
        /// </summary>
        public static ConsoleColor WarningNotificationForeground { get; set; } = ConsoleColor.White;
        /// <summary>
        /// The background colour of the console when an error has occured.
        /// </summary>
        public static ConsoleColor WarningMessageBackground { get; set; } = ConsoleColor.Blue;
        /// <summary>
        /// The foreground colour of the console when an error has occured.
        /// </summary>
        public static ConsoleColor WarningMessageForeground { get; set; } = DefaultBackground;
        /// <summary>
        /// The background colour of the console message type - [ERR].
        /// </summary>
        public static ConsoleColor ErrorNotificationBackground { get; set; } = ConsoleColor.Red;
        /// <summary>
        /// The foreground colour of the console message type - [ERR].
        /// </summary>
        public static ConsoleColor ErrorNotificationForeground { get; set; } = ConsoleColor.White;
        /// <summary>
        /// The background colour of the console when an error has occured.
        /// </summary>
        public static ConsoleColor ErrorMessageBackground { get; set; } = ConsoleColor.Red;
        /// <summary>
        /// The foreground colour of the console when an error has occured.
        /// </summary>
        public static ConsoleColor ErrorMessageForeground { get; set; } = DefaultBackground;
        /// <summary>
        /// The background colour of the console message type - [SDL_ERR].
        /// </summary>
        public static ConsoleColor SDL_ErrorNotificationBackground { get; set; } = ConsoleColor.DarkRed;
        /// <summary>
        /// The foreground colour of the console message type - [SDL_ERR].
        /// </summary>
        public static ConsoleColor SDL_ErrorNotificationForeground { get; set; } = ConsoleColor.White;
        /// <summary>
        /// The background colour of the console when an sdl error has occured.
        /// </summary>
        public static ConsoleColor SDL_ErrorMessageBackground { get; set; } = SDL_ErrorNotificationBackground;
        /// <summary>
        /// The foreground colour of the console when an sdl error has occured.
        /// </summary>
        public static ConsoleColor SDL_ErrorMessageForeground { get; set; } = SDL_ErrorNotificationForeground;

        /// <summary>
        /// The interval between reporting the FPS to the console if ReportFPS is true.
        /// </summary>
        public static float FPS_ReportInterval { get; set; } = 1;
        /// <summary>
        /// Whether to report the fps at the specified interval from FPS_ReportInterval.
        /// </summary>
        public static bool ReportFPS { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Writes to the console.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="end">What to be write at the end of the message.</param>
        /// <param name="includeTimestampAndNotification">Should it include the timestamp.</param>
        public static void Log(object message, string end = "\n", bool includeTimestampAndNotification = true) {
            string displayedMessage = message.ToString();

            if (includeTimestampAndNotification) {
                Console.Write(DateTime.Now.ToLongTimeString());
                Console.BackgroundColor = LogNotificationBackground;
                Console.ForegroundColor = LogNotificationForeground;
                Console.Write(" [LOG] ");
            }

            Console.BackgroundColor = DefaultBackground;
            Console.ForegroundColor = DefaultForeground;
            Console.Write($"{displayedMessage}{end}");
        }
        /// <summary>
        /// Writes a warning to the console.
        /// </summary>
        /// <param name="message">The messsage to write.</param>
        /// <param name="end">What to write at the end of the string.</param>
        /// <param name="includeTimestampAndNotification">Should it include the timestamp.</param>
        public static void Warn(object message, string end = "\n", bool includeTimestampAndNotification = true) {
            string displayedMessage = message.ToString();

            if (includeTimestampAndNotification) {
                Console.Write(DateTime.Now.ToLongTimeString());
                Console.BackgroundColor = WarningNotificationBackground;
                Console.ForegroundColor = WarningNotificationForeground;
                Console.Write(" [WRN] ");
            }

            Console.BackgroundColor = DefaultBackground;
            Console.ForegroundColor = DefaultForeground;
            Console.Write($"{displayedMessage}{end}");
        }
        /// <summary>
        /// Writes an error to the console.
        /// </summary>
        /// <param name="message">The messsage to write.</param>
        /// <param name="end">What to write at the end of the string.</param>
        /// <param name="includeTimestampAndNotification">Should it include the timestamp.</param>
        public static void Error(object message, string end = "\n", bool includeTimestampAndNotification = true) {
            string displayedMessage = message.ToString();

            if (includeTimestampAndNotification) {
                Console.Write(DateTime.Now.ToLongTimeString());
                Console.BackgroundColor = ErrorNotificationBackground;
                Console.ForegroundColor = ErrorNotificationForeground;
                Console.Write(" [ERR] ");
            }

            Console.BackgroundColor = DefaultBackground;
            Console.ForegroundColor = DefaultForeground;
            Console.Write($"{displayedMessage}{end}");
        }

        internal static void ErrorCheckSDL(bool throwException = true) {
            string error = SDL_GetError();
            string imgError = IMG_GetError();

            if (error == "") {
                if (imgError == "") {
                    return;

                } else {
                    error = imgError;
                }
            }

            Console.Write(DateTime.Now.ToLongTimeString());
            Console.BackgroundColor = SDL_ErrorNotificationBackground;
            Console.ForegroundColor = SDL_ErrorNotificationForeground;
            Console.Write(" [SDL_ERR] ");
            Console.BackgroundColor = SDL_ErrorMessageBackground;
            Console.ForegroundColor = SDL_ErrorMessageForeground;
            Console.WriteLine(error);
            Console.BackgroundColor = DefaultBackground;
            Console.ForegroundColor = DefaultForeground;

            if (throwException) {
                throw new SDL_Exception(error);
            }
        }
        #endregion
    }
}
