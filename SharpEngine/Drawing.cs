using System;
using static SDL2.SDL;
using static SDL2.SDL_ttf;

namespace SharpEngine {
    /// <summary>
    /// Allows a window to be rendered to.
    /// </summary>
    public static class Drawing {
        #region Properties
        private static IntPtr RendererPtr { get => Engine.Window.RendererPtr; }
        #endregion

        #region Methods
        /// <summary>
        /// Draws a rectangle.
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
        /// Draws a line.
        /// </summary>
        /// <param name="point1">The first point of the line.</param>
        /// <param name="point2">The second point of the line.</param>
        /// <param name="colour">The colour of the line.</param>
        public static void DrawLine(Vector2 point1, Vector2 point2, Colour colour) {
           SetDrawColour(colour);
            _ = SDL_RenderDrawLineF(RendererPtr, point1.X, point1.Y, point2.X, point2.Y);
        }

        /// <summary>
        /// Draws a pixel.
        /// </summary>
        /// <param name="position">The position of the pixel.</param>
        /// <param name="colour">The colour of the pixel.</param>
        public static void DrawPixel(Vector2 position, Colour colour) {
            SetDrawColour(colour);
            _ = SDL_RenderDrawPointF(RendererPtr, position.X, position.Y);
        }

        /// <summary>
        /// Draws a texture.
        /// </summary>
        /// <param name="texture">The texture to show.</param>
        /// <param name="position">The top left of the texture.</param>
        /// <param name="size">The size of the texture.</param>
        public static void DrawTexture(Texture texture, Vector2 position, Vector2 size, float rotation = 0,
                bool flipHorizontal = false, bool flipVertical = false) {
            SDL_FRect rect = Vector2.ToSDL_FRect(position, size);

            SDL_RendererFlip flips = SDL_RendererFlip.SDL_FLIP_NONE;

            if (flipHorizontal)
                flips |= SDL_RendererFlip.SDL_FLIP_HORIZONTAL;

            if (flipVertical)
                flips |= SDL_RendererFlip.SDL_FLIP_VERTICAL;

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
        /// Draws text to the screen.
        /// </summary>
        /// <param name="position">The top left point of the text.</param>
        /// <param name="text">The text to be displayed.</param>
        /// <param name="font">The font to be used.</param>
        /// <param name="colour">The colour of the text.</param>
        /// <param name="angle">The angle of the text.</param>
        /// <param name="flipHorizontal">If the text should be flipped horizontally.</param>
        /// <param name="flipVertical">If the text should be flipped vertically.</param>
        public static void DrawText(Vector2 position, string text, Font font, Colour colour, float angle = 0,
                bool flipHorizontal = false, bool flipVertical = false) {
            IntPtr surface = TTF_RenderText_Solid(font.FontPtr, text, colour.ToSDL_Color());
            IntPtr texture = SDL_CreateTextureFromSurface(RendererPtr, surface);

            _ = TTF_SizeText(font.FontPtr, text, out int x, out int y);
            SDL_FRect rect = new() { x = position.X, y = position.Y, w = x, h = y };

            SDL_RendererFlip flips = SDL_RendererFlip.SDL_FLIP_NONE;

            if (flipHorizontal)
                flips |= SDL_RendererFlip.SDL_FLIP_HORIZONTAL;

            if (flipVertical)
                flips |= SDL_RendererFlip.SDL_FLIP_VERTICAL;

            _ = SDL_RenderCopyExF(RendererPtr, texture, IntPtr.Zero, ref rect, angle, IntPtr.Zero, flips);

            Debug.ErrorCheckSDL();

            SDL_FreeSurface(surface);
            SDL_DestroyTexture(texture);
        }

        internal static void SetDrawColour(Colour colour) {
            _ = SDL_SetRenderDrawColor(RendererPtr, colour.R, colour.G, colour.B, colour.A);
        }
        #endregion
    }
}
