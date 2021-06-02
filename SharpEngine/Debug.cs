using System;
using static SDL2.SDL;
using static SDL2.SDL_image;

namespace SharpEngine {
    /// <summary>
    /// Used for debuging your game.
    /// </summary>
    public static class Debug {
        #region Properties
        public static ConsoleColor DefaultBackground { get; set; } = Console.BackgroundColor;
        public static ConsoleColor DefaultForeground { get; set; } = Console.ForegroundColor;
        public static ConsoleColor LogNotificationBackground { get; set; } = ConsoleColor.White;
        public static ConsoleColor LogNotificationForeground { get; set; } = ConsoleColor.Black;
        public static ConsoleColor ErrorNotificationBackground { get; set; } = ConsoleColor.Red;
        public static ConsoleColor ErrorNotificationForeground { get; set; } = ConsoleColor.White;
        public static ConsoleColor ErrorMessageBackground { get; set; } = ConsoleColor.Red;
        public static ConsoleColor ErrorMessageForeground { get; set; } = DefaultBackground;
        public static ConsoleColor SDL_ErrorNotificationBackground { get; set; } = ConsoleColor.DarkRed;
        public static ConsoleColor SDL_ErrorNotificationForeground { get; set; } = ConsoleColor.White;
        public static ConsoleColor SDL_ErrorMessageBackground { get; set; } = SDL_ErrorNotificationBackground;
        public static ConsoleColor SDL_ErrorMessageForeground { get; set; } = SDL_ErrorNotificationForeground;
        #endregion

        #region Methods
        /// <summary>
        /// Writes to the console.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="end">What to be put at the end of the message.</param>
        /// <param name="includeTimestampAndNotification">If to include the text to the left of the message.</param>
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

        internal static void ErrorCheckSDL(bool throwException = true) {
            string error = SDL_GetError();
            string imgError = IMG_GetError();

            if (error == "")
                if (imgError == "")
                    return;
                else
                    error = imgError;

            Console.Write(DateTime.Now.ToLongTimeString());
            Console.BackgroundColor = SDL_ErrorNotificationBackground;
            Console.ForegroundColor = SDL_ErrorNotificationForeground;
            Console.Write(" [SDL_ERR] ");
            Console.BackgroundColor = SDL_ErrorMessageBackground;
            Console.ForegroundColor = SDL_ErrorMessageForeground;
            Console.WriteLine(error);
            Console.BackgroundColor = DefaultBackground;
            Console.ForegroundColor = DefaultForeground;

            if (throwException)
                throw new SDL_Exception(error);
        }
        #endregion
    }
}
