using SharpEngine;
using System;

namespace MovingPlayer {
    static class Program {
        private const int MovementSpeed = 100;

        private static Texture PlayerTexture { get; set; }
        private static Sprite Player { get; set; }

        static void Main(string[] args) {
            Engine.Ready += Engine_Ready;
            Engine.Draw += Engine_Draw;

            Engine.Init();
        }

        private static void Engine_Ready(object sender, EventArgs e) {
            Engine.Window = new(title: "Sharp Engine Test - Moving Player");

            Engine.EnforceFPSCap = true;
            Engine.MaxFPS = 60;

            PlayerTexture = new(@"Images\Arrow.png");

            Player = new((Engine.Window.WindowSize / 2) - 100, new(200, 200), PlayerTexture);
        }

        private static void Engine_Draw(object sender, EventArgs e) {
            if (Input.IsKeyDown("W")) {
                Player.Position -= Vector2.Up * MovementSpeed * Engine.DeltaTime;
            }

            if (Input.IsKeyDown("A")) {
                Player.Position += Vector2.Left * MovementSpeed * Engine.DeltaTime;
            }

            if (Input.IsKeyDown("S")) {
                Player.Position -= Vector2.Down * MovementSpeed * Engine.DeltaTime;
            }

            if (Input.IsKeyDown("D")) {
                Player.Position += Vector2.Right * MovementSpeed * Engine.DeltaTime;
            }

            Player.Rotation = -(float) ((Math.Atan2(
                Input.MousePosition.X - (Player.Position.X + 100),
                Input.MousePosition.Y - (Player.Position.Y + 100)) * (180 / Math.PI)) - 90);
        }
    }
}
