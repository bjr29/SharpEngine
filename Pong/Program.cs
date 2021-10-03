using SharpEngine;
using SharpEngine.UI;
using System;

namespace Pong {
    static class Program {
        private static readonly Vector2 PaddleSize = new(20, 100);
        private static readonly Vector2 BallSize = new(20, 20);
        private static readonly Vector2 PaddleRange = new(30, 370 - PaddleSize.Y);
        private static readonly int PaddleMovementSpeed = 150;
        private static readonly int BallMovementSpeed = 200;

        private static Vector2 BallPosition { get; set; }
        private static Vector2 LeftPaddlePosition { get; set; }
        private static Vector2 RightPaddlePosition { get; set; }

        private static Vector2 DefaultBallPosition { get; set; }
        private static Vector2 DefaultLeftPaddlePosition { get; set; }
        private static Vector2 DefaultRightPaddlePosition { get; set; }

        private static Vector2 BallDirection { get; set; }

        private static bool Paused { get; set; } = true;
        private static int PausedMultiplier { 
            get {
                if (Paused)
                    return 0;

                return 1;
            }
        }
        private static float StartCountdown { get; set; }

        private static int Score1 { get; set; }
        private static int Score2 { get; set; }

        private static Font Font { get; set; }
        private static Text Countdown { get; set; }
        private static Text Score1Text { get; set; }
        private static Text Score2Text { get; set; }

        private static Random Random { get; set; } = new();

        static void Main(string[] args) {
            Engine.Init();

            Engine.Window = new(title: "Sharp Engine Test - Pong");

            Engine.Ready += Engine_Ready;
            Engine.Draw += Engine_Draw;

            Engine.Start();
        }

        private static void Engine_Ready(object sender, EventArgs e) {
            Engine.MaxFPS = 60;
            Engine.EnforceFPSCap = true;

            LeftPaddlePosition = new(
                20,
                (Engine.Window.WindowSize.Y / 2) - (PaddleSize.Y / 2)
            );

            RightPaddlePosition = new(
                Engine.Window.WindowSize.X - 40,
                (Engine.Window.WindowSize.Y / 2) - (PaddleSize.Y / 2)
            );

            DefaultLeftPaddlePosition = LeftPaddlePosition;
            DefaultRightPaddlePosition = RightPaddlePosition;

            BallPosition = (Engine.Window.WindowSize / 2) - (BallSize / 2);
            DefaultBallPosition = BallPosition;

            Font = new(@"Fonts\OpenSans-SemiBold.ttf", 50);

            Countdown = new(Engine.Window.WindowSize / 2 - new Vector2(10, 75), StartCountdown.ToString(), Font);

            Score1Text = new(new(LeftPaddlePosition.X + PaddleSize.X + 50, 10), Score2.ToString(), Font) {
                ForegroundColour = new(255, 150)
            };

            Score2Text = new(new(RightPaddlePosition.X - PaddleSize.X - 50, 10), Score2.ToString(), Font) {
                ForegroundColour = new(255, 150)
            };

            Reset();
        }

        private static void Engine_Draw(object sender, EventArgs e) {
            // Game Logic
            if (StartCountdown > 0) {
                StartCountdown -= Engine.DeltaTime;
                Countdown.Content = Math.Ceiling(StartCountdown).ToString();

            } else {
                Countdown.Show = false;
                Paused = false;
            }

            if (BallPosition.X < LeftPaddlePosition.X) {
                Score1 += 1;
                Reset();

            } else if (BallPosition.X > RightPaddlePosition.X + PaddleSize.X) {
                Score2 += 1;
                Reset();
            }

            // Ball Physics
            if (HasHitPaddle()) {
                float y = (float)Random.NextDouble();
                if (Random.Next(0, 2) == 0)
                    y *= -1;

                BallDirection += new Vector2(BallDirection.X * -2, y);
            }

            if (HasHitBounds()) {
                BallDirection *= new Vector2(1, -1);
            }

            BallPosition += BallDirection * BallMovementSpeed * Engine.DeltaTime * PausedMultiplier;

            // Input
            if (Input.IsKeyDown("W")) {
                LeftPaddlePosition = new Vector2(DefaultLeftPaddlePosition.X, 0) + Vector2.Up
                    * Math.Clamp(LeftPaddlePosition.Y - (PaddleMovementSpeed * Engine.DeltaTime * PausedMultiplier),
                    PaddleRange.X, PaddleRange.Y);
            }

            if (Input.IsKeyDown("S")) {
                LeftPaddlePosition = new Vector2(DefaultLeftPaddlePosition.X, 0) + Vector2.Up
                    * Math.Clamp(LeftPaddlePosition.Y + (PaddleMovementSpeed * Engine.DeltaTime * PausedMultiplier),
                    PaddleRange.X, PaddleRange.Y);
            }

            if (Input.IsKeyDown("Up")) {
                RightPaddlePosition = new Vector2(DefaultRightPaddlePosition.X, 0) + Vector2.Up
                    * Math.Clamp(RightPaddlePosition.Y - (PaddleMovementSpeed * Engine.DeltaTime * PausedMultiplier),
                    PaddleRange.X, PaddleRange.Y);
            }

            if (Input.IsKeyDown("Down")) {
                RightPaddlePosition = new Vector2(DefaultRightPaddlePosition.X, 0) + Vector2.Up
                    * Math.Clamp(RightPaddlePosition.Y + (PaddleMovementSpeed * Engine.DeltaTime * PausedMultiplier),
                    PaddleRange.X, PaddleRange.Y);
            }

            // Drawing
            Drawing.DrawRect(LeftPaddlePosition, PaddleSize, new(255));
            Drawing.DrawRect(RightPaddlePosition, PaddleSize, new(255));

            Drawing.DrawRect(BallPosition, BallSize, new(255));

            Score1Text.Content = Score1.ToString();
            Score2Text.Content = Score2.ToString();
        }

        private static bool HasHitPaddle() {
            if (
                    (LeftPaddlePosition.X + PaddleSize.X >= BallPosition.X
                    && LeftPaddlePosition.Y <= BallPosition.Y + BallSize.Y
                    && LeftPaddlePosition.Y + PaddleSize.Y >= BallPosition.Y)
                    ||
                    (RightPaddlePosition.X <= BallPosition.X + BallSize.X
                    && RightPaddlePosition.Y <= BallPosition.Y + BallSize.Y
                    && RightPaddlePosition.Y + PaddleSize.Y >= BallPosition.Y)) {
                return true;
            }

            return false;
        }

        private static bool HasHitBounds() {
            if (BallPosition.Y <= 30 || BallPosition.Y + BallSize.Y >= 370) {
                return true;
            }

            return false;
        }

        private static void Reset() {
            float x = 1;
            if (Random.Next(0, 2) == 0)
                x = -1;

            float y = (float) Random.NextDouble();
            if (Random.Next(0, 2) == 0)
                y *= -1;

            BallDirection = new Vector2(x, y);

            BallPosition = DefaultBallPosition;
            LeftPaddlePosition = DefaultLeftPaddlePosition;
            RightPaddlePosition = DefaultRightPaddlePosition;
            StartCountdown = 3;
            Paused = true;
        }
    }
}
