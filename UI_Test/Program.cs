using SharpEngine;
using SharpEngine.UI;
using System;

namespace UI_Test {
    static class Program {
        public static Font Font { get; set; }

        public static Button Button { get; set; }
        public static ProgressBar ProgressBar { get; set; }
        public static TextBox TextBox { get; set; }

        static void Main(string[] args) {
            Engine.Init();

            Engine.Window = new(title: "Sharp Engine Test - UI Test");

            Engine.Ready += Engine_Ready;
            Engine.Draw += Engine_Draw;

            Engine.Start();
        }

        private static void Engine_Ready(object sender, EventArgs e) {
            Font = new(@"C:\Users\bjrus\Appdata\Local\Microsoft\Windows\Fonts\coolvetica rg.ttf", 14);

            Button = new(new(340, 60), new(50, 50), "Button", Font) {
                
            };

            Button.Clicked += Button_Clicked;

            ProgressBar = new(new(340, 20), new(100, 20)) {
                BarColour = new(0, 255, 0),
                BackgroundColour = new(255, 0, 0),
                BackgroundOutlineThickness = 2,
                BackgroundOutlineColour = new(200, 0, 0),
                BarOutlineThickness = 2,
                BarOutlineColour = new(0, 200, 0)
            };

            TextBox = new(new(20, 20), new(300, 18), Font) {
                //Text = "Hello World!",
                Placeholder = "Say Something!"
            };
        }

        private static void Button_Clicked(object sender, EventArgs e) {
            ProgressBar.Value += 0.1f;
        }

        private static void Engine_Draw(object sender, EventArgs e) {
            Button.Draw();
            ProgressBar.Draw();
            TextBox.Draw();
        }
    }
}
