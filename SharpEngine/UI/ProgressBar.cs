using System;

namespace SharpEngine.UI {
    public class ProgressBar {
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
        public Colour BarColour { get; set; } = new(255);

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

        public ProgressBar(Vector2 position, Vector2 size, float value = 0) {
            Position = position;
            Size = size;
            Value = value;
        }

        public void Draw() {
            // Background Outline
            Drawing.DrawRect(
                Position - BackgroundOutlineThickness,
                Size + BackgroundOutlineThickness * 2,
                BackgroundOutlineColour
            );

            // Background
            Drawing.DrawRect(
                Position,
                Size,
                BackgroundColour
            );

            // Bar Outline
            Drawing.DrawRect(
                Position - BarOutlineThickness, 
                new Vector2(Size.X * Math.Clamp(Value, 0, 1), Size.Y) * 2,
                BarColour
            );

            // Bar
            Drawing.DrawRect(
                Position, 
                new(Size.X * Math.Clamp(Value, 0, 1), Size.Y),
                BarColour
            );
        }
    }
}
