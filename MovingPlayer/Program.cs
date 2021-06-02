using System;
using SharpEngine;

namespace MovingPlayer {
    class Program {
        private const int MOVEMENT_SPEED = 100;

        private static Vector2 Position { get; set; }
        private static Texture PlayerTexture { get; set; }

        static void Main(string[] args) {
            Engine.Init();

            Engine.Window = new();

            Engine.Ready += Engine_Ready;
            Engine.Draw += Engine_Draw;

            Engine.Start();
        }

        private static void Engine_Ready(object sender, EventArgs e) {
            Engine.EnforceFPSCap = true;
            Engine.MaxFPS = 60;
            Position = (Engine.Window.WindowSize / 2) - 100;
            PlayerTexture = new(@"C:\Users\bjrus\Desktop\Development\C#\SharpEngine\MovingPlayer\bin\Debug\net5.0\Images\Arrow.png");
        }

        private static void Engine_Draw(object sender, EventArgs e) {
            if (Input.IsKeyDown("W"))
                Position -= Vector2.Up * MOVEMENT_SPEED * Engine.DeltaTime;

            if (Input.IsKeyDown("A"))
                Position += Vector2.Left * MOVEMENT_SPEED * Engine.DeltaTime;

            if (Input.IsKeyDown("S"))
                Position -= Vector2.Down * MOVEMENT_SPEED * Engine.DeltaTime;

            if (Input.IsKeyDown("D"))
                Position += Vector2.Right * MOVEMENT_SPEED * Engine.DeltaTime;

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
