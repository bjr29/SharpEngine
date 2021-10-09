using System.Linq;

namespace SharpEngine {
    public static class DrawRenderables {
        public static List<IRenderable> RegisteredRenderables { get; set; } = new();

        private static IOrderedEnumerable<IRenderable> OrderedRenderables { get; set; }

        internal static void Draw() {
            if (RegisteredRenderables.Count == 0) {
                return;
            }

            OrderedRenderables = RegisteredRenderables.OrderBy(x => x.ZIndex);

            foreach (IRenderable renderable in OrderedRenderables) {
                if (renderable.Show) {
                    renderable.Draw();
                }
            }
        }
    }
}
