using System;
using static SDL2.SDL_ttf;

namespace SharpEngine {
    /// <summary>
    /// A font that can be applied to text.
    /// </summary>
    public class Font : IDisposable {
        /// <summary>
        /// The size of the font.
        /// </summary>
        public int FontSize {
            get => _FontSize;
            set {
                _FontSize = value;

                TTF_CloseFont(FontPtr);
                TTF_OpenFont(FontPath, value);

                Debug.ErrorCheckSDL();
            }
        }
        /// <summary>
        /// The path to the font.
        /// </summary>
        public string FontPath { get; private set; }
        /// <summary>
        /// The applied font styles.
        /// </summary>
        public FontStyle FontStyles {
            get => (FontStyle)TTF_GetFontStyle(FontPtr);
            set => TTF_SetFontStyle(FontPtr, (int)value);
        }

        private int _FontSize { get; set; }

        internal IntPtr FontPtr { get; set; }

        /// <summary>
        /// The font styles that can be applied to text.
        /// </summary>
        public enum FontStyle {
            /// <summary>
            /// Regular text.
            /// </summary>
            Normal = TTF_STYLE_NORMAL,
            /// <summary>
            /// Bold text.
            /// </summary>
            Bold = TTF_STYLE_BOLD,
            /// <summary>
            /// Italic text.
            /// </summary>
            Italic = TTF_STYLE_ITALIC,
            /// <summary>
            /// Underlined text.
            /// </summary>
            Underline = TTF_STYLE_UNDERLINE,
            /// <summary>
            /// Strikethrough text.
            /// </summary>
            Strikethrough = TTF_STYLE_STRIKETHROUGH
        }

        /// <summary>
        /// Creates the font from the path.
        /// </summary>
        /// <param name="path">The path to the font file.</param>
        /// <param name="fontSize">The size of the font.</param>
        public Font(string path, int fontSize = 12) {
            FontPtr = TTF_OpenFont(path, fontSize);
            FontPath = path;

            Debug.ErrorCheckSDL(false);
        }

        private bool DisposedValue { get; set; }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">Is the object being disposed.</param>
        protected virtual void Dispose(bool disposing) {
            if (!DisposedValue) {
                TTF_CloseFont(FontPtr);

                DisposedValue = true;
            }
        }

        ~Font() {
            Dispose(disposing: false);
        }

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
