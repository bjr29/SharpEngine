using System;
using SharpEngine;

namespace SharpEngineGOL {
    class Program {
        private static bool[,] World { get; set; } = new bool[256, 256];
        private static bool Paused { get; set; }
        private static int CellSize { get; set; } = 8;
        //private static IntVector2 Pan = new();

        static void Main(string[] args) {
            Engine.Init();

            Engine.Window = new(title: "Sharp Engine Test - GOL");

            Engine.Ready += Ready;
            Engine.Draw += Draw;
            Input.MouseButtonDown += MouseButtonDown;
            Input.KeyDown += KeyDown;
            Input.MouseWheelScrolled += ScrollWheel;

            Engine.Start();
        }

        private static void Ready(object sender, EventArgs e) {
            Engine.MaxFPS = 10;
        }

        private static void Draw(object sender, EventArgs e) {
            int cellSize = CellSize;

            bool[,] world = World;

            for (int y = 0; y < World.GetLength(1); y++) {
                for (int x = 0; x < World.GetLength(0); x++) {
                    if (!Paused)
                        if (!World[x, y] && SurroundingCells(x, y) == 3)
                            world[x, y] = true;

                        else if (World[x, y] && !(SurroundingCells(x, y) == 2 || SurroundingCells(x, y) == 3))
                            world[x, y] = false;

                    if (World[x, y])
                        Drawing.DrawRect(new IntVector2(x, y) * cellSize, new IntVector2(1, 1) * cellSize, new(255));
                }
            }

            World = world;
        }

        private static void ScrollWheel(object sender, EventArgs e) {
            CellSize = Math.Clamp(CellSize + Input.ScrollWheel, 1, 32);
        }

        private static void KeyDown(object sender, EventArgs e) {
            if (Input.LastKeyDown == "Space")
                Paused = !Paused;
        }

        private static void MouseButtonDown(object sender, EventArgs e) {
            IntVector2 position = Input.MousePosition / CellSize;

            if (Input.LastMouseButtonDown == Input.MouseButton.Left)
                World[position.X, position.Y] = true;

            else if (Input.LastMouseButtonDown == Input.MouseButton.Right)
                World[position.X, position.Y] = false;
        }

        private static int SurroundingCells(int x, int y) {
            int i = 0;

            if (x > 0 && y > 0 && World[x - 1, y - 1])
                i++;    // Top Left

            if (y > 0 && World[x, y - 1])
                i++;    // Top Middle

            if (x < World.GetLength(0) - 1 && y > 0 && World[x + 1, y - 1])
                i++;    // Top Right

            if (x < World.GetLength(0) - 1 && World[x + 1, y])
                i++;    // Middle Right

            if (x < World.GetLength(0) - 1 && y < World.GetLength(1) - 1 && World[x + 1, y + 1])
                i++;    // Bottom Right

            if (y < World.GetLength(1) - 1 && World[x, y + 1])
                i++;    // Bottom Middle

            if (x > 0 && y < World.GetLength(1) - 1 && World[x - 1, y + 1])
                i++;    // Bottom Left

            if (x > 0 && World[x - 1, y])
                i++;    // Middle Left

            return i;
        }
    }
}
