using System;
using static SDL2.SDL;

namespace SharpEngine {
    /// <summary>
    /// Allows a window to be rendered to.
    /// </summary>
    public static class Drawing {
        #region Properties
        /// <summary>
        /// The texture that is selected to be rendered to.
        /// </summary>
        public static Texture RenderingTo { get; private set; }

        private static IntPtr RendererPtr { get => Engine.Window.RendererPtr; }
        #endregion

        #region Methods
        /// <summary>
        /// Renders a rectangle.
        /// </summary>
        /// <param name="position">The top left of the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        /// <param name="colour">The colour of the rectangle.</param>
        /// <param name="filled">If the rectangle should be filled.</param>
        public static void DrawRect(Vector2 position, Vector2 size, Colour colour, bool filled = true) {
            SDL_FRect rect = Vector2.ToSDL_FRect(position, size);

            SetDrawColour(colour);

            if (!filled) {
                _ = SDL_RenderDrawRectF(RendererPtr, ref rect);

                return;
            }

            _ = SDL_RenderFillRectF(RendererPtr, ref rect);

            Debug.ErrorCheckSDL();
        }

        /// <summary>
        /// Renders a rectangle.
        /// </summary>
        /// <param name="rect">The rectangle.</param>
        /// <param name="colour">The colour of the rectangle.</param>
        /// <param name="filled">If the rectangle should be filled.</param>
        public static void DrawRect(Rect rect, Colour colour, bool filled = true) {
            DrawRect(rect.Position, rect.Size, colour, filled);
        }

        /// <summary>
        /// Renders a line.
        /// </summary>
        /// <param name="point1">The first point of the line.</param>
        /// <param name="point2">The second point of the line.</param>
        /// <param name="colour">The colour of the line.</param>
        public static void DrawLine(Vector2 point1, Vector2 point2, Colour colour) {
           SetDrawColour(colour);
            _ = SDL_RenderDrawLineF(RendererPtr, point1.X, point1.Y, point2.X, point2.Y);
        }

        /// <summary>
        /// Renders a pixel.
        /// </summary>
        /// <param name="position">The position of the pixel.</param>
        /// <param name="colour">The colour of the pixel.</param>
        public static void DrawPixel(Vector2 position, Colour colour) {
            SetDrawColour(colour);
            _ = SDL_RenderDrawPointF(RendererPtr, position.X, position.Y);
        }

        /// <summary>
        /// Renders a circle.
        /// </summary>
        /// <param name="position">The centre position of the circle.</param>
        /// <param name="colour">The colour of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="filled">Is the circle filled?</param>
        public static void DrawCircle(Vector2 position, Colour colour, float radius, bool filled = true) {
            SetDrawColour(colour);

            float startRadius = radius;

            while (radius > 0) {
                if (!filled && radius != startRadius) {
                    return;
                }

                for (float angle = 0; angle <= Math.PI * 2; angle += 1.0f / radius) {
                    _ = SDL_RenderDrawPointF(RendererPtr,
                        (float)(position.X + radius * Math.Cos(angle)),
                        (float)(position.Y + radius * Math.Sin(angle))
                    );
                }

                radius -= 0.5f;
            }
        }

        /// <summary>
        /// Renders a texture.
        /// </summary>
        /// <param name="texture">The texture to show.</param>
        /// <param name="position">The top left of the texture.</param>
        /// <param name="size">The size of the texture.</param>
        /// <param name="rotation">The rotation of the texture.</param>
        /// <param name="flipHorizontal">Flips the texture horizontally.</param>
        /// <param name="flipVertical">Flips the texture vertically.</param>
        [Obsolete("Use sprites")] public static void DrawTexture(Texture texture, Vector2 position, Vector2 size, float rotation = 0,
                bool flipHorizontal = false, bool flipVertical = false) {
            SDL_FRect rect = Vector2.ToSDL_FRect(position, size);

            SDL_RendererFlip flips = SDL_RendererFlip.SDL_FLIP_NONE;

            if (flipHorizontal) {
                flips |= SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
            }

            if (flipVertical) {
                flips |= SDL_RendererFlip.SDL_FLIP_VERTICAL;
            }

            _ = SDL_RenderCopyExF(
                RendererPtr,
                texture.TexturePtr,
                IntPtr.Zero,
                ref rect,
                rotation,
                IntPtr.Zero,
                flips
            );

            Debug.ErrorCheckSDL();
        }

        /// <summary>
        /// Sets all of the drawing methods to affect the chosen texture.
        /// </summary>
        /// <param name="texture">The texture to render to.</param>
        public static void RenderToTarget(Texture texture) {
            _ = SDL_SetRenderTarget(RendererPtr, texture.TexturePtr);
            RenderingTo = texture;
        }

        /// <summary>
        /// Sets all of the drawing methods to draw to the screen.
        /// </summary>
        public static void RenderToScreen() {
            _ = SDL_SetRenderTarget(RendererPtr, IntPtr.Zero);
            RenderingTo = null;
        }

        internal static void SetDrawColour(Colour colour) {
            _ = SDL_SetRenderDrawColor(RendererPtr, colour.R, colour.G, colour.B, colour.A);
        }
        #endregion
    }
}
