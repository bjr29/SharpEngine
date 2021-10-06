namespace SharpEngine {
    public static class DrawRenderables {
        public static List<IRenderable> RegisteredRenderable { get; set; } = new();

        internal static void Draw() {
            List<IRenderable> remaining = RegisteredRenderable.ToList();

            for (int i = 0; i < remaining.Count; i++) {
                IRenderable element = remaining[i];

                if (element.ZIndex != i) {
                    continue;
                }

                if (element.Show) {
                    element.Draw();
                }

                remaining.Remove(element);
                i--;

                if (i == remaining.Count - 1) {
                    i = 0;
                }
            }
        }
    }
}
