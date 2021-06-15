using System;
using static SDL2.SDL_ttf;

namespace SharpEngine {
    /// <summary>
    /// A font that can be applied to text.
    /// </summary>
    public class Font : IDisposable {
        #region Properties
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
        #endregion

        #region Enums
        /// <summary>
        /// The font styles that can be applied to text.
        /// </summary>
        public enum FontStyle {
            Normal = TTF_STYLE_NORMAL,
            Bold = TTF_STYLE_BOLD,
            Italic = TTF_STYLE_ITALIC,
            Underline = TTF_STYLE_UNDERLINE,
            Strikethrough = TTF_STYLE_STRIKETHROUGH
        }
        #endregion

        #region Constructors
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
        #endregion

        #region Inherited Properties
        private bool DisposedValue { get; set; }
        #endregion

        #region Inherited Methods
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
        #endregion
    }
}
