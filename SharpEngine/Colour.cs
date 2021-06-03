using SDL2;
using System;
using static SDL2.SDL;

namespace SharpEngine {
    public struct Colour {
        #region Properties
        /// <summary>
        /// Red
        /// </summary>
        public byte R { get; set; }
        /// <summary>
        /// Green
        /// </summary>
        public byte G { get; set; }
        /// <summary>
        /// Blue
        /// </summary>
        public byte B { get; set; }
        /// <summary>
        /// Alpha (opacity)
        /// </summary>
        public byte A { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new colour.
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        /// <param name="a">Alpha (opacity)</param>
        public Colour(byte r, byte g, byte b, byte a = 255) {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Short-hand for creating a new colour.
        /// </summary>
        /// <param name="all">The combination of all red, green and blue.</param>
        /// <param name="a">Alpha (opacity)</param>
        public Colour(byte all, byte a = 255) {
            R = all;
            G = all;
            B = all;
            A = a;
        }

        internal SDL_Color ToSDL_Color() => new() { r = R, g = G, b = B, a = A };
        #endregion
    }
}