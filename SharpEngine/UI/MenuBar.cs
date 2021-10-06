using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEngine.UI {
    public class MenuBar : IUIElement {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Colour BackgroundColour { get; set; }
        public Colour ForegroundColour { get; set; }

        public bool Show { get; set; } = true;
        public int ZIndex { get; set; }

        public MenuBar() {
            DrawTextures.RegisteredTextures.Add(this);
        }

        public void Draw() {

        }
    }
}
