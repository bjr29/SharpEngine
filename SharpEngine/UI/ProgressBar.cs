using System;

namespace SharpEngine.UI {
    /// <summary>
    /// Shows the progress of an action or the value of something.
    /// </summary>
    public class ProgressBar : IUIElement {
        #region Properties
        /// <summary>
        /// The position of the whole progress bar.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// The size of the whole progress bar.
        /// </summary>
        public Vector2 Size { get; set; }
        /// <summary>
        /// The colour of the background.
        /// </summary>
        public Colour BackgroundColour { get; set; } = new(60);
        /// <summary>
        /// The colour of the bar.
        /// </summary>
        public Colour ForegroundColour { get; set; } = new(255);

        /// <summary>
        /// The background's outline colour.
        /// </summary>
        public Colour BackgroundOutlineColour { get; set; }
        /// <summary>
        /// The bar's outline colour.
        /// </summary>
        public Colour BarOutlineColour { get; set; }
        /// <summary>
        /// The size of the background's outline.
        /// </summary>
        public int BackgroundOutlineThickness { get; set; }
        /// <summary>
        /// The size of the bar's outline.
        /// </summary>
        public int BarOutlineThickness { get; set; }

        /// <summary>
        /// The value of the progress bar between the values of 0 and 1.
        /// </summary>
        public float Value { get; set; }

        public bool Show { get; set; } = true;
        public int ZIndex { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates the progress bar.
        /// </summary>
        /// <param name="position">The top left of the whole progress bar.</param>
        /// <param name="size">The size of the whole progress bar.</param>
        /// <param name="value">The value of the progress bar between the values of 0 and 1.</param>
        public ProgressBar(Vector2 position, Vector2 size, float value = 0) {
            Position = position;
            Size = size;
            Value = value;

            DrawRenderables.RegisteredRenderable.Add(this);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders the progress bar to the screen.
        /// </summary>
        public void Draw() {
            // Background Outline
            Drawing.DrawRect(
                Position - BackgroundOutlineThickness,
                Size + (BackgroundOutlineThickness * 2),
                BackgroundOutlineColour
            );

            // Background
            Drawing.DrawRect(
                Position,
                Size,
                BackgroundColour
            );

            if (Value > 0) {
                // Bar Outline
                Drawing.DrawRect(
                    Position - BarOutlineThickness,
                    new Vector2(Size.X * Math.Clamp(Value, 0, 1), Size.Y) + (BarOutlineThickness * 2),
                    BarOutlineColour
                );

                // Bar
                Drawing.DrawRect(
                    Position,
                    new(Size.X * Math.Clamp(Value, 0, 1), Size.Y),
                    ForegroundColour
                );
            }
        }
        #endregion
    }
}
