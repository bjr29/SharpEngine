namespace SharpEngine.UI {
    public class TextBox : IUIElement {
        #region Properties
        /// <summary>
        /// The position of the textbox.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// The size of the textbox.
        /// </summary>
        public Vector2 Size { get; set; }
        /// <summary>
        /// The colour of the background.
        /// </summary>
        public Colour BackgroundColour {
            get {
                if (Selected)
                    return SelectedBackgroundColour;
                return _BackgroundColour;
            }
            set => _BackgroundColour = value;
        }
        [Obsolete("Not used")] public Colour ForegroundColour { get; set; }
        /// <summary>
        /// The colour of the background while selected.
        /// </summary>
        public Colour SelectedBackgroundColour { get; set; } = new(60);
        /// <summary>
        /// The text to be displayed unless blank then it shows the placeholder.
        /// </summary>
        public Text Text {
            get {
                if (string.IsNullOrEmpty(_Text.Content))
                    return PlaceholderText;
                return _Text;
            }
            set => _Text = value;
        }
        /// <summary>
        /// The text displayed if Text.Content is blank.
        /// </summary>
        public Text PlaceholderText { get; set; }

        /// <summary>
        /// Whether the textbox can be selected.
        /// </summary>
        public bool Selectable { get; set; } = true;

        /// <summary>
        /// The position of the caret.
        /// </summary>
        public int CaretPosition { get; set; }

        /// <summary>
        /// Has the textbox been selected.
        /// </summary>
        public bool Selected { get; set; }

        public bool Show { get; set; } = true;
        public int ZIndex { get; set; }

        private Text _Text { get; set; }
        private Colour _BackgroundColour { get; set; } = new(60);
        private Colour _TextColour { get; set; } = new(255);

        private DateTime LastDraw { get; set; }
        private DateTime LastEdit { get; set; }
        #endregion

        #region Events
        /// <summary>
        /// Invoked by the textbox being clicked while it's selectable.
        /// </summary>
        public event EventHandler Clicked;
        public event EventHandler Typed;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates the textbox.
        /// </summary>
        /// <param name="position">The top left point of the textbox.</param>
        /// <param name="size">The size of the textbox.</param>
        /// <param name="font">The font used in the textbox.</param>
        /// <param name="text">The text to be already be in the textbox.</param>
        /// <param name="placeholder">The text that shows up when it is empty.</param>
        public TextBox(Vector2 position, Vector2 size, Font font, string text = "", string placeholder = "") {
            Position = position;
            Size = size;

            _Text = new(Position, text, font) {
                Crop = new(new(), size),
                Cropped = true
            };

            PlaceholderText = new(Position, placeholder, font) {
                Crop = new(new(), size),
                Cropped = true
            };

            Input.MouseButtonDown += Input_MouseButtonDown;
            Input.KeyDown += Input_KeyDown;

            DrawRenderables.RegisteredRenderable.Add(this);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders the textbox to the screen.
        /// </summary>
        public void Draw() {
            LastDraw = DateTime.Now;

            Drawing.DrawRect(Position, Size, BackgroundColour);

            if (Selected && (LastEdit.AddSeconds(1) >= DateTime.Now || DateTime.Now.Second % 2 == 0)) {
                Vector2 size = Text.GetTextSize(
                    _Text.Content[..Math.Clamp(CaretPosition, 0, _Text.Content.Length)],
                    Text.Font
                );

                Drawing.DrawLine(Position + new Vector2(size.X, 0), Position + size, new(255));
            }
        }

        private void Input_MouseButtonDown(object sender, MouseButtonEventArgs e) {
            Selected = Selectable && Rect.HasOverlapped(new(Position, Size), Input.MousePosition);

            if (Selected) {
                Clicked?.Invoke(this, e);
            }
        }

        private void Input_KeyDown(object sender, KeyPressedEventArgs e) {
            if (Selected) {
                bool invokeTyped = true;

                LastEdit = DateTime.Now;

                if (e.Key.Length == 1) {
                    string key = e.Key;
                    if (!(Input.IsKeyDown("Left Shift") || Input.IsKeyDown("Right Shift")))
                        key = key.ToLower();

                    _Text.Content = _Text.Content.Insert(CaretPosition, key);
                    CaretPosition = Math.Clamp(CaretPosition + 1, 0, _Text.Content.Length);
                    return;
                }

                try {
                    switch (e.Key) {
                        case "Backspace":
                            _Text.Content = _Text.Content.Remove(CaretPosition - 1, 1);
                            CaretPosition = Math.Clamp(CaretPosition - 1, 0, _Text.Content.Length);
                            break;

                        case "Delete":
                            _Text.Content = _Text.Content.Remove(CaretPosition, 1);
                            break;

                        case "Space":
                            _Text.Content = _Text.Content.Insert(CaretPosition, " ");
                            CaretPosition = Math.Clamp(CaretPosition + 1, 0, _Text.Content.Length);
                            break;

                        case "Left":
                            CaretPosition = Math.Clamp(CaretPosition - 1, 0, _Text.Content.Length);
                            invokeTyped = false;
                            break;

                        case "Right":
                            CaretPosition = Math.Clamp(CaretPosition + 1, 0, _Text.Content.Length);
                            invokeTyped = false;
                            break;
                    }

                } catch { }

                if (invokeTyped) {
                    Typed?.Invoke(this, e);
                }
            }
        }
        #endregion
    }
}
