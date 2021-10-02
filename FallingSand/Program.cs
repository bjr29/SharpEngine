using SharpEngine;

namespace FallingSand {
    public class Program {
        static void Main(string[] args) {
            Engine.Init();

            Engine.Window = new();

            Engine.Ready += Engine_Ready;
            Engine.Draw += Engine_Draw;

            Engine.Start();
        }

        private static void Engine_Ready(object? sender, EventArgs e) {

        }

        private static void Engine_Draw(object? sender, EventArgs e) {

        }
    }
}
