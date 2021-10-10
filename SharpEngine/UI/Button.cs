namespace SharpEngine.UI {
    /// <summary>
    /// A clickable UI element.
    /// </summary>
    public class Button : IUIElement {
        #region Properties
        /// <summary>
        /// The position of the button.
        /// </summary>
        public Vector2 Position {
            get => _Position;
            set {
                _Position = value;
                SetTextPosition(_Text);
                SetTextPosition(_ToggledText);
            }
        }
        /// <summary>
        /// The size of the button.
        /// </summary>
        public Vector2 Size {
            get => _Size;
            set {
                _Size = value;
                SetTextPosition(_Text);
                SetTextPosition(_ToggledText);
            }
        }
        /// <summary>
        /// The colour of the background.
        /// </summary>
        public Colour BackgroundColour {
            get {
                if (Toggled)
                    return ToggledBackgroundColour;
                return _BackgroundColour;
            }
            set => _BackgroundColour = value;
        }
        /// <summary>
        /// The text's colour.
        /// </summary>
        public Colour ForegroundColour { get; set; }
        /// <summary>
        /// The colour of the background while toggled.
        /// </summary>
        public Colour ToggledBackgroundColour { get; set; } = new(80);
        /// <summary>
        /// The text's colour while toggled.
        /// </summary>
        public Colour ToggledForegroundColour { get; set; }

        /// <summary>
        /// The text to be displayed.
        /// </summary>
        public Text Text {
            get => _Text;
            set {
                _Text = value;
                SetTextPosition(_Text);
            }
        }
        /// <summary>
        /// The text to be displayed while the button is toggled.
        /// </summary>
        public Text ToggledText {
            get => _ToggledText;
            set {
                _ToggledText = value;
                SetTextPosition(_ToggledText);
            }
        }

        /// <summary>
        /// Whether the button can be interacted with.
        /// </summary>
        public bool Active { get; set; } = true;
        /// <summary>
        /// Whether the button can be toggled.
        /// </summary>
        public bool Toggleable {
            get => _Toggleable;
            set {
                _Toggleable = value;
                Toggled = false;
            }
        }

        /// <summary>
        /// Has the button been toggled.
        /// </summary>
        public bool Toggled { get; set; }

        /// <summary>
        /// Should be rendered by DrawRenderables.
        /// </summary>
        public bool Show { get; set; } = true;
        /// <summary>
        /// The order to be rendered in, only applied by DrawRenderables.
        /// </summary>
        public int ZIndex { get; set; }

        private Vector2 _Position { get; set; }
        private Vector2 _Size { get; set; }
        private Colour _BackgroundColour { get; set; } = new(60);
        private Text _Text { get; set; }
        private Text _ToggledText { get; set; }
        private bool _Toggleable { get; set; }
        #endregion

        #region Events
        /// <summary>
        /// Invoked by the button being clicked while active.
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> Clicked;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a button.
        /// </summary>
        /// <param name="position">The top left point of the button.</param>
        /// <param name="size">The size of the button.</param>
        /// <param name="text">The text contained within the button.</param>
        /// <param name="font">The font to be applied to the text.</param>
        public Button(Vector2 position, Vector2 size, Font font, string text = "") {
            _Position = position;
            _Size = size;

            Text = new(new(), text, font);
            Text.Position = SetTextPosition(Text);

            Input.MouseButtonDown += Input_MouseButtonDown;

            DrawRenderables.RegisteredRenderables.Add(this);
        }
        #endregion

        #region Methods
        /// <summary>
        /// The render method.
        /// </summary>
        public void Draw() {
            Drawing.DrawRect(Position, Size, BackgroundColour);

            Text.ZIndex = ZIndex + 1;
        }

        private void Input_MouseButtonDown(object sender, MouseButtonEventArgs e) {
            if (Rect.HasOverlapped(new(Position, Size), Input.MousePosition)) {
                Clicked?.Invoke(this, e);
                Toggled = Toggleable && !Toggled;
            }
        }

        private Vector2 SetTextPosition(Text text) =>
            _Position + (Size / 2) - (Text.GetTextSize(text.Content, text.Font) / 2);
        #endregion
    }
}
