using System;

namespace SharpEngine.UI {
    public class Button {
        #region Properties
        /// <summary>
        /// The position of the button.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// The size of the button.
        /// </summary>
        public Vector2 Size { get; set; }
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
        /// The colour of the background while selected.
        /// </summary>
        public Colour ToggledBackgroundColour { get; set; } = new(60);
        /// <summary>
        /// The colour of the text.
        /// </summary>
        public Colour TextColour {
            get {
                if (Toggled)
                    return ToggledTextColour;
                return _TextColour;
            }
            set => _TextColour = value;
        }
        /// <summary>
        /// The colour of the text while selected.
        /// </summary>
        public Colour ToggledTextColour { get; set; } = new(200);
        /// <summary>
        /// The font to use on the text.
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// The text to be displayed.
        /// </summary>
        public string Text { get; set; }

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

        private Colour _BackgroundColour { get; set; } = new(60);
        private Colour _TextColour { get; set; } = new(255);

        private bool _Toggleable { get; set; }
        #endregion

        #region Events
        /// <summary>
        /// Invoked by the button being clicked while active.
        /// </summary>
        public event EventHandler Clicked;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a button.
        /// </summary>
        /// <param name="position">The top left point of the button.</param>
        /// <param name="size">The size of the button.</param>
        /// <param name="text">The text contained within the button.</param>
        /// <param name="font">The font to be applied to the text.</param>
        public Button(Vector2 position, Vector2 size, string text = null, Font font = null) {
            Position = position;
            Size = size;
            Text = text;
            Font = font;

            Input.MouseButtonDown += Input_MouseButtonDown;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders the button.
        /// </summary>
        public void Draw() {
            Drawing.DrawRect(Position, Size, BackgroundColour);

            if (Text is not null || Font is not null)
                Drawing.DrawText(Position + (Size / 2) - (Drawing.GetTextSize(Text, Font) / 2), Text, Font, TextColour);
        }

        private void Input_MouseButtonDown(object sender, MouseButtonEventArgs e) {
            if (Rect.HasOverlapped(new(Position, Size), Input.MousePosition)) {
                Clicked?.Invoke(this, e);
                Toggled = Toggleable && !Toggled;
            }
        }
        #endregion
    }
}
