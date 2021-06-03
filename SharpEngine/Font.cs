using System;
using static SDL2.SDL_ttf;

namespace SharpEngine {
    public class Font : IDisposable {
        #region Properties
        public IntPtr FontPtr { get; set; }
        public int FontSize {
            get => _FontSize;
            set {
                _FontSize = value;
                _ = TTF_SetFontSize(FontPtr, value);
            }
        }
        public string FontPath { get; private set; }
        public FontStyle FontStyles {
            get => (FontStyle)TTF_GetFontStyle(FontPtr);
            set => TTF_SetFontStyle(FontPtr, (int)value);
        }

        private int _FontSize { get; set; }
        #endregion

        #region Enums
        public enum FontStyle {
            Normal = TTF_STYLE_NORMAL,
            Bold = TTF_STYLE_BOLD,
            Italic = TTF_STYLE_ITALIC,
            Underline = TTF_STYLE_UNDERLINE,
            Strikethrough = TTF_STYLE_STRIKETHROUGH
        }
        #endregion

        #region Constructors
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
