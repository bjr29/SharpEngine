using System;
using SharpEngine;

namespace MovingPlayer {
    static class Program {
        private const int MovementSpeed = 100;

        private static Vector2 Position { get; set; }
        private static Texture PlayerTexture { get; set; }

        static void Main(string[] args) {
            Engine.Init();

            Engine.Window = new(title: "Sharp Engine Test - Moving Player");

            Engine.Ready += Engine_Ready;
            Engine.Draw += Engine_Draw;

            Engine.Start();
        }

        private static void Engine_Ready(object sender, EventArgs e) {
            Engine.EnforceFPSCap = true;
            Engine.MaxFPS = 60;
            Position = (Engine.Window.WindowSize / 2) - 100;
            PlayerTexture = new(@"Images\Arrow.png");
        }

        private static void Engine_Draw(object sender, EventArgs e) {
            if (Input.IsKeyDown("W"))
                Position -= Vector2.Up * MovementSpeed * Engine.DeltaTime;

            if (Input.IsKeyDown("A"))
                Position += Vector2.Left * MovementSpeed * Engine.DeltaTime;

            if (Input.IsKeyDown("S"))
                Position -= Vector2.Down * MovementSpeed * Engine.DeltaTime;

            if (Input.IsKeyDown("D"))
                Position += Vector2.Right * MovementSpeed * Engine.DeltaTime;

            Drawing.DrawTexture(
                PlayerTexture,
                Position,
                new(200, 200),
                -(float)((Math.Atan2(Input.MousePosition.X - (Position.X + 100),
                                     Input.MousePosition.Y - (Position.Y + 100)
                                    ) * (180 / Math.PI)) - 90)
            );
        }
    }
}
