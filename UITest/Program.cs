using SharpEngine;
using SharpEngine.UI;
using System;

namespace UITest {
    static class Program {
        public static Font Font { get; set; }

        public static Button Button { get; set; }
        public static ProgressBar ProgressBar { get; set; }
        public static TextBox TextBox { get; set; }
        public static Text Text { get; set; }

        static void Main(string[] args) {
            Engine.Init();

            Engine.Window = new(title: "Sharp Engine Test - UI Test");

            Engine.Ready += Engine_Ready;

            Engine.Start();
        }

        private static void Engine_Ready(object sender, EventArgs e) {
            Font = new(@"Fonts\OpenSans-SemiBold.ttf", 14);

            Button = new(new(340, 60), new(50, 50), Font, "Button");

            Button.Clicked += Button_Clicked;

            ProgressBar = new(new(340, 20), new(100, 20)) {
                ForegroundColour = new(0, 255, 0),
                BackgroundColour = new(255, 0, 0),
                BackgroundOutlineThickness = 2,
                BackgroundOutlineColour = new(200, 0, 0),
                BarOutlineThickness = 2,
                BarOutlineColour = new(0, 200, 0)
            };

            TextBox = new(new(20, 20), new(300, 18), Font, text: "Bruh", placeholder: "Type Something");
            TextBox.Text.ForegroundColour = new(255);
            TextBox.PlaceholderText.ForegroundColour = new(200);

            Text = new(new(280, 60), "Text", Font) {
                ForegroundColour = new(255),
                Font = Font
            };
        }

        private static void Button_Clicked(object sender, MouseButtonEventArgs e) {
            if (e.MouseButton == Input.MouseButton.Left) {
                ProgressBar.Value += 0.1f;

            } else if (e.MouseButton == Input.MouseButton.Right) {
                ProgressBar.Value -= 0.1f;
            }
        }
    }
}
