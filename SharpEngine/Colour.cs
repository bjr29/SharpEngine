using System;
using System.Linq;
using static SDL2.SDL;

namespace SharpEngine {
    /// <summary>
    /// A colour that be applied to any of the drawing methods.
    /// </summary>
    public struct Colour {
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

        public Colour(string hex) {
            R = 255;
            G = 255;
            B = 255;
            A = 255;

            byte[] values = ParseHexValue(hex);

            switch (values.Length) {
                case 1:
                    R = (byte) (values[0] * 16 + values[0]);
                    G = (byte) (values[0] * 16 + values[0]);
                    B = (byte) (values[0] * 16 + values[0]);

                    break;

                case 2:
                    R = (byte) (values[0] * 16 + values[1]);
                    G = (byte) (values[0] * 16 + values[1]);
                    B = (byte) (values[0] * 16 + values[1]);

                    break;

                case 3:
                    R = (byte) (values[0] * 16 + values[0]);
                    G = (byte) (values[1] * 16 + values[1]);
                    B = (byte) (values[2] * 16 + values[2]);

                    break;

                case 6:
                    R = (byte) (values[0] * 16 + values[1]);
                    G = (byte) (values[2] * 16 + values[3]);
                    B = (byte) (values[4] * 16 + values[5]);

                    break;

                case 8:
                    R = (byte) (values[0] * 16 + values[1]);
                    G = (byte) (values[2] * 16 + values[3]);
                    B = (byte) (values[4] * 16 + values[5]);
                    A = (byte) (values[6] * 16 + values[7]);

                    break;

                default:
                    throw new Exception("Invalid hex value.");
            }
        }

        public static byte ParseHexSymbol(char hexSymbol) {
            bool hasParsedInt = byte.TryParse(hexSymbol.ToString(), out byte intParse);

            if (hasParsedInt) {
                return intParse;
            }

            return (byte) Enum.Parse<HexColour>(hexSymbol.ToString());
        }

        public static byte[] ParseHexValue(string hexValue) {
            if (hexValue[0] == '#') {
                hexValue = hexValue[1..];
            }

            if (!new int[] { 1, 2, 3, 6, 8 }.Contains(hexValue.Length)) {
                throw new Exception("Invalid hex format. Hex values can only be 1, 2, 3, 6, 8 characters long (excludes '#').");
            }

            byte[] values = new byte[hexValue.Length];

            for (int i = 0; i < values.Length; i++) {
                values[i] = ParseHexSymbol(hexValue[i]);
            }

            return values;
        }

        internal SDL_Color ToSDL_Color() => new() { r = R, g = G, b = B, a = A };

        private enum HexColour {
            A = 10,
            B = 11,
            C = 12,
            D = 13,
            E = 14,
            F = 15
        }
    }
}
