using static SDL2.SDL;
using static SDL2.SDL_ttf;

namespace SharpEngine.UI {
    /// <summary>
    /// Renders text to window.
    /// </summary>
    public class Text : IUIElement {
        #region Properties
        /// <summary>
        /// The top left point of the text.
        /// </summary>
        public Vector2 Position {
            get => _Position;
            set {
                _Position = value;
                Move();
            }
        }
        [Obsolete("Not Used")] public Vector2 Size { get; set; }
        /// <summary>
        /// How much the text is cropped by while Cropped is true.
        /// </summary>
        public Rect Crop {
            get => _Crop;
            set {
                _Crop = value;
                CropText();
            }
        }
        /// <summary>
        /// The text to render.
        /// </summary>
        public string Content {
            get => _Content;
            set {
                _Content = value;
                Update();
            }
        }
        /// <summary>
        /// The rotation of the text.
        /// </summary>
        public float Rotation { get; set; }
        /// <summary>
        /// The font to be applied to the text.
        /// </summary>
        public Font Font {
            get => _Font;
            set {
                _Font = value;
                Update();
            }
        }
        /// <summary>
        /// The colour of the text.
        /// </summary>
        public Colour ForegroundColour {
            get => _Colour;
            set {
                _Colour = value;
                Update();
            }
        }
        [Obsolete("Not Used")] public Colour BackgroundColour { get; set; }

        /// <summary>
        /// Whether the text is anti-aliased.
        /// </summary>
        public bool AntiAliased {
            get => _AntiAliased;
            set {
                _AntiAliased = value;
                Update();
            }
        }
        /// <summary>
        /// Whether the text is cropped, the amount decided by Crop.
        /// </summary>
        public bool Cropped {
            get => _Cropped;
            set {
                _Cropped = value;
                Update();
            }
        }

        public bool Show { get; set; } = true;
        public int ZIndex { get; set; }

        private Vector2 _Position { get; set; }
        private Rect _Crop { get; set; }
        private string _Content { get; set; }
        private Font _Font { get; set; }
        private Colour _Colour { get; set; } = new(255);
        private bool _AntiAliased { get; set; } = true;
        private bool _Cropped { get; set; }

        private readonly IntPtr Renderer = Engine.Window.RendererPtr;

        private IntPtr Texture { get; set; }
        private SDL_FRect Rect;
        private SDL_Rect CropRect;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates the text.
        /// </summary>
        /// <param name="position">The top left point of the text.</param>
        /// <param name="content">The text to render.</param>
        /// <param name="font">The font to be applied to the text.</param>
        /// <param name="rotation">The rotation of the text.</param>
        public Text(Vector2 position, string content, Font font, float rotation = 0) {
            _Font = font;
            _Position = position;
            _Content = content;
            Rotation = rotation;

            Update();

            DrawUI.RegisteredUI.Add(this);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders the text to the screen.
        /// </summary>
        public void DrawElement() {
            SDL_Rect crop = CropRect;

            if (!Cropped) {
                crop = new SDL_Rect() {
                    x = 0,
                    y = 0,
                    w = (int) Rect.w,
                    h = (int) Rect.h
                };
            }

            _ = SDL_RenderCopyExF(
                Renderer,
                Texture,
                ref crop,
                ref Rect,
                Rotation,
                IntPtr.Zero,
                SDL_RendererFlip.SDL_FLIP_NONE
            );

            Debug.ErrorCheckSDL();
        }

        private void Update() {
            if (string.IsNullOrEmpty(Content)) {
                return;
            }

            IntPtr surface;

            if (AntiAliased) {
                surface = TTF_RenderText_Blended(Font.FontPtr, Content, ForegroundColour.ToSDL_Color());

            } else {
                surface = TTF_RenderText_Solid(Font.FontPtr, Content, ForegroundColour.ToSDL_Color());
            }

            if (Texture != IntPtr.Zero) {
                SDL_DestroyTexture(Texture);
            }

            Texture = SDL_CreateTextureFromSurface(Renderer, surface);

            SDL_FreeSurface(surface);

            _ = TTF_SizeText(Font.FontPtr, Content, out int x, out int y);

            Rect = new() {
                x = Position.X,
                y = Position.Y,
                w = x,
                h = y
            };

            if (Cropped) {
                Rect = new() {
                    x = Position.X,
                    y = Position.Y,
                    w = Math.Clamp(x, 0, Crop.Size.X),
                    h = Math.Clamp(y, 0, Crop.Size.Y)
                };
            }

            Debug.ErrorCheckSDL();
        }

        private void Move() {
            Rect = new() {
                x = Position.X,
                y = Position.Y,
                w = Rect.w,
                h = Rect.h
            };
        }

        private void CropText() {
            CropRect = new() {
                x = (int) Crop.Position.X,
                y = (int) Crop.Position.Y,
                w = (int) Crop.Size.X,
                h = (int) Crop.Size.Y
            };
        }

        /// <summary>
        /// Gets the space, in pixels, the string would take up using the selected font.
        /// </summary>
        /// <param name="text">The string.</param>
        /// <param name="font">The font.</param>
        /// <returns>The space, in pixels, taken up by the text.</returns>
        public static Vector2 GetTextSize(string text, Font font) {
            _ = TTF_SizeText(font.FontPtr, text, out int x, out int y);
            return new(x, y);
        }
        #endregion
    }
}
