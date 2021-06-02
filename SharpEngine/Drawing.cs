using System;
using static SDL2.SDL;
using static SDL2.SDL_image;

namespace SharpEngine {
    public static class Drawing {
        /// <summary>
        /// Draws a rectangle.
        /// </summary>
        /// <param name="position">The top left of the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        /// <param name="colour">The colour of the rectangle.</param>
        /// <param name="filled">If the rectangle should be filled.</param>
        public static void DrawRect(Vector2 position, Vector2 size, Colour colour, bool filled = true) {
            SDL_FRect rect = Vector2.ToSDL_FRect(position, size);

            _ = SDL_SetRenderDrawColor(
                Engine.Window.RendererPtr,
                colour.R,
                colour.G,
                colour.B,
                colour.A
            );

            if (!filled) {
                _ = SDL_RenderDrawRectF(Engine.Window.RendererPtr, ref rect);

                return;
            }

            _ = SDL_RenderFillRectF(Engine.Window.RendererPtr, ref rect);

            Debug.ErrorCheckSDL();
        }

        /// <summary>
        /// Draws a line.
        /// </summary>
        /// <param name="point1">The first point of the line.</param>
        /// <param name="point2">The second point of the line.</param>
        /// <param name="colour">The colour of the line.</param>
        public static void DrawLine(Vector2 point1, Vector2 point2, Colour colour) {
            _ = SDL_SetRenderDrawColor(Engine.Window.RendererPtr, colour.R, colour.G, colour.B, colour.A);
            _ = SDL_RenderDrawLineF(Engine.Window.RendererPtr, point1.X, point1.Y, point2.X, point2.Y);
        }

        /// <summary>
        /// Draws a pixel.
        /// </summary>
        /// <param name="position">The position of the pixel.</param>
        /// <param name="colour">The colour of the pixel.</param>
        public static void DrawPixel(Vector2 position, Colour colour) {
            _ = SDL_SetRenderDrawColor(Engine.Window.RendererPtr, colour.R, colour.B, colour.G, colour.A);
            _ = SDL_RenderDrawPointF(Engine.Window.RendererPtr, position.X, position.Y);
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
                Engine.Window.RendererPtr,
                texture.TexturePtr,
                IntPtr.Zero,
                ref rect,
                rotation,
                IntPtr.Zero,
                flips
            );

            /*_ = SDL_RenderCopyF(
                Engine.Window.RendererPtr,
                texture.TexturePtr,
                IntPtr.Zero,
                ref rect
            );*/

            Debug.ErrorCheckSDL();
        }
    }
}
