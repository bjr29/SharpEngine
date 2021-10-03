namespace SharpEngine.UI {
    public static class DrawUI {
        public static List<IUIElement> RegisteredUI { get; set; } = new();

        internal static void Draw() {
            List<IUIElement> remaining = RegisteredUI.ToList();

            for (int i = 0; i < remaining.Count; i++) {
                IUIElement element = remaining[i];

                if (element.ZIndex != i) {
                    continue;
                }

                if (element.Show) {
                    element.DrawElement();
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
