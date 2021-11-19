using System.Collections.Generic;
using System.Linq;

namespace SharpEngine {
    /// <summary>
    /// The class that draws all renderable objects that impliment IRenderable.
    /// </summary>
    public static class DrawRenderables {
        /// <summary>
        /// Objects within the list will be rendered.
        /// </summary>
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
