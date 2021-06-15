using System;

namespace SharpEngine.UI {
    public class TextBox {
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
        /// <summary>
        /// The colour of the background while selected.
        /// </summary>
        public Colour SelectedBackgroundColour { get; set; } = new(60);
        /// <summary>
        /// The colour of the text.
        /// </summary>
        public Colour TextColour {
            get {
                if (string.IsNullOrEmpty(_Text))
                    return PlaceholderTextColour;
                return _TextColour;
            }
            set => _TextColour = value;
        }
        /// <summary>
        /// The colour of the placeholder text.
        /// </summary>
        public Colour PlaceholderTextColour { get; set; } = new(200);
        /// <summary>
        /// The font to use on the text.
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// The text to be displayed unless blank then it shows the placeholder.
        /// </summary>
        public string Text {
            get {
                if (string.IsNullOrEmpty(_Text))
                    return Placeholder;
                return _Text;
            }
            set => _Text = value;
        }
        /// <summary>
        /// The text displayed if the property text is blank.
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// Whether the textbox can be selected.
        /// </summary>
        public bool Selectable { get; set; } = true;

        /// <summary>
        /// The position of the caret.
        /// </summary>
        public int CaretPosition { get; set; }

        /*/// <summary>
        /// Wraps the text if true.
        /// </summary>
        public bool Wrapped { get; set; }*/
        /// <summary>
        /// Has the textbox been selected.
        /// </summary>
        public bool Selected { get; set; }

        private string _Text { get; set; } = "";
        private Colour _BackgroundColour { get; set; } = new(60);
        private Colour _TextColour { get; set; } = new(255);

        private DateTime LastDraw { get; set; }
        private DateTime LastEdit { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates the textbox.
        /// </summary>
        /// <param name="position">The top left point of the textbox.</param>
        /// <param name="size">The size of the textbox.</param>
        /// <param name="font">The font used in the textbox.</param>
        public TextBox(Vector2 position, Vector2 size, Font font) {
            Position = position;
            Size = size;
            Font = font;

            Input.MouseButtonDown += Input_MouseButtonDown;
            Input.KeyDown += Input_KeyDown;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders the textbox to the screen.
        /// </summary>
        public void Draw() {
            LastDraw = DateTime.Now;

            Drawing.DrawRect(Position, Size, BackgroundColour);

            Drawing.DrawCroppedText(Position, new(), Size, Text, Font, TextColour);

            if (Selected && (LastEdit.AddSeconds(1) >= DateTime.Now || DateTime.Now.Second % 2 == 0)) {
                Vector2 size = Drawing.GetTextSize(_Text[..Math.Clamp(CaretPosition, 0, _Text.Length)], Font);
                Drawing.DrawLine(Position + new Vector2(size.X, 0), Position + size, new(255));
            }
        }

        private void Input_MouseButtonDown(object sender, MouseButtonEventArgs e) =>
            Selected = Selectable && Rect.HasOverlapped(new(Position, Size), Input.MousePosition);

        private void Input_KeyDown(object sender, KeyPressedEventArgs e) {
            if (Selected) {
                LastEdit = DateTime.Now;

                if (e.Key.Length == 1) {
                    string key = e.Key;
                    if (!(Input.IsKeyDown("Left Shift") || Input.IsKeyDown("Right Shift")))
                        key = key.ToLower();

                    _Text = _Text.Insert(CaretPosition, key);
                    CaretPosition = Math.Clamp(CaretPosition + 1, 0, _Text.Length);
                    return;
                }

                try {
                    switch (e.Key) {
                        case "Backspace":
                            _Text = _Text.Remove(CaretPosition - 1, 1);
                            CaretPosition = Math.Clamp(CaretPosition - 1, 0, _Text.Length);;
                            break;

                        case "Delete":
                            _Text = _Text.Remove(CaretPosition, 1);
                            break;

                        case "Space":
                            _Text = _Text.Insert(CaretPosition, " ");
                            CaretPosition = Math.Clamp(CaretPosition + 1, 0, _Text.Length);;
                            break;

                        case "Left":
                            CaretPosition = Math.Clamp(CaretPosition - 1, 0, _Text.Length);;
                            break;

                        case "Right":
                            CaretPosition = Math.Clamp(CaretPosition + 1, 0, _Text.Length);;
                            break;
                    }

                } catch { }
            }
        }
        #endregion
    }
}
