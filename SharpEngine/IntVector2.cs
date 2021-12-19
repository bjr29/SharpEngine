using System;
using static SDL2.SDL;

namespace SharpEngine {
    /// <summary>
    /// Used to show a point in a 2D space using int values.
    /// </summary>
    public struct IntVector2 : IEquatable<IntVector2> {
        /// <summary>
        /// 0, 1
        /// </summary>
        public static readonly IntVector2 Up = new(y: 1);
        /// <summary>
        /// -1, 0
        /// </summary>
        public static readonly IntVector2 Left = new(x: -1);
        /// <summary>
        /// 0, -1
        /// </summary>
        public static readonly IntVector2 Down = new(y: -1);
        /// <summary>
        /// 1, 0
        /// </summary>
        public static readonly IntVector2 Right = new(x: 1);
        /// <summary>
        /// 0, 0
        /// </summary>
        public static readonly IntVector2 Zero = new();

        /// <summary>
        /// The x position of the vector.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// The y position of the vector.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Creates a vector.
        /// </summary>
        /// <param name="x">The x position of the vector.</param>
        /// <param name="y">The y position of the vector.</param>
        public IntVector2(int x = 0, int y = 0) {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Add the values of 2 vectors.
        /// </summary>
        public static IntVector2 operator +(IntVector2 a, IntVector2 b) => new(a.X + b.X, a.Y + b.Y);
        /// <summary>
        /// Subtracts the values of 2 vectors.
        /// </summary>
        public static IntVector2 operator -(IntVector2 a, IntVector2 b) => new(a.X - b.X, a.Y - b.Y);
        /// <summary>
        /// Divides the values of 2 vectors.
        /// </summary>
        public static IntVector2 operator /(IntVector2 a, IntVector2 b) => new(a.X / b.X, a.Y / b.Y);
        /// <summary>
        /// Multiplies the values of 2 vectors.
        /// </summary>
        public static IntVector2 operator *(IntVector2 a, IntVector2 b) => new(a.X * b.X, a.Y * b.Y);

        /// <summary>
        /// Adds both values of the vector.
        /// </summary>
        public static IntVector2 operator +(IntVector2 a, int b) => new(a.X + b, a.Y + b);
        /// <summary>
        /// Subtracts both values of the vector.
        /// </summary>
        public static IntVector2 operator -(IntVector2 a, int b) => new(a.X - b, a.Y - b);
        /// <summary>
        /// Divides both values of the vector.
        /// </summary>
        public static IntVector2 operator /(IntVector2 a, int b) => new(a.X / b, a.Y / b);
        /// <summary>
        /// Multiplies both values of the vector.
        /// </summary>
        public static IntVector2 operator *(IntVector2 a, int b) => new(a.X * b, a.Y * b);

        /// <summary>
        /// Adds both values of the vector.
        /// </summary>
        public static IntVector2 operator +(int a, IntVector2 b) => new(a + b.X, a + b.Y);
        /// <summary>
        /// Subtracts both values of the vector.
        /// </summary>
        public static IntVector2 operator -(int a, IntVector2 b) => new(a - b.X, a - b.Y);
        /// <summary>
        /// Divides both values of the vector.
        /// </summary>
        public static IntVector2 operator /(int a, IntVector2 b) => new(a / b.X, a / b.Y);
        /// <summary>
        /// Multiplies both values of the vector.
        /// </summary>
        public static IntVector2 operator *(int a, IntVector2 b) => new(a * b.X, a * b.Y);

        /// <summary>
        /// Adds both values of the vector.
        /// </summary>
        public static IntVector2 operator +(IntVector2 a, float b) => new(a.X + (int) b, a.Y + (int) b);
        /// <summary>
        /// Subtracts both values of the vector.
        /// </summary>
        public static IntVector2 operator -(IntVector2 a, float b) => new(a.X - (int) b, a.Y - (int) b);
        /// <summary>
        /// Divides both values of the vector.
        /// </summary>
        public static IntVector2 operator /(IntVector2 a, float b) => new(a.X / (int) b, a.Y / (int) b);
        /// <summary>
        /// Multiplies both values of the vector.
        /// </summary>
        public static IntVector2 operator *(IntVector2 a, float b) => new(a.X * (int) b, a.Y * (int) b);

        /// <summary>
        /// Adds both values of the vector.
        /// </summary>
        public static IntVector2 operator +(float a, IntVector2 b) => new((int) a + b.X, (int) a + b.Y);
        /// <summary>
        /// Subtracts both values of the vector.
        /// </summary>
        public static IntVector2 operator -(float a, IntVector2 b) => new((int) a - b.X, (int) a - b.Y);
        /// <summary>
        /// Divides both values of the vector.
        /// </summary>
        public static IntVector2 operator /(float a, IntVector2 b) => new((int) a / b.X, (int) a / b.Y);
        /// <summary>
        /// Multiplies both values of the vector.
        /// </summary>
        public static IntVector2 operator *(float a, IntVector2 b) => new((int) a * b.X, (int) a * b.Y);

        /// <summary>
        /// Adds both values of the vector.
        /// </summary>
        public static IntVector2 operator +(IntVector2 a, double b) => new(a.X + (int) b, a.Y + (int) b);
        /// <summary>
        /// Subtracts both values of the vector.
        /// </summary>
        public static IntVector2 operator -(IntVector2 a, double b) => new(a.X - (int) b, a.Y - (int) b);
        /// <summary>
        /// Divides both values of the vector.
        /// </summary>
        public static IntVector2 operator /(IntVector2 a, double b) => new(a.X / (int) b, a.Y / (int) b);
        /// <summary>
        /// Multiplies both values of the vector.
        /// </summary>
        public static IntVector2 operator *(IntVector2 a, double b) => new(a.X * (int) b, a.Y * (int) b);

        /// <summary>
        /// Adds both values of the vector.
        /// </summary>
        public static IntVector2 operator +(double a, IntVector2 b) => new((int) a + b.X, (int) a + b.Y);
        /// <summary>
        /// Subtracts both values of the vector.
        /// </summary>
        public static IntVector2 operator -(double a, IntVector2 b) => new((int) a - b.X, (int) a - b.Y);
        /// <summary>
        /// Divides both values of the vector.
        /// </summary>
        public static IntVector2 operator /(double a, IntVector2 b) => new((int) a / b.X, (int) a / b.Y);
        /// <summary>
        /// Multiplies both values of the vector.
        /// </summary>
        public static IntVector2 operator *(double a, IntVector2 b) => new((int) a * b.X, (int) a * b.Y);

        /// <summary>
        /// Checks if the vectors hold the same values.
        /// </summary>
        public static bool operator ==(IntVector2 a, IntVector2 b) => Equals(a, b);
        /// <summary>
        /// Checks if the vectors don't hold the same values.
        /// </summary>
        public static bool operator !=(IntVector2 a, IntVector2 b) => !Equals(a, b);

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator Vector2(IntVector2 vector2) => new(vector2.X, vector2.Y);

        /// <summary>
        /// Converts the vector to a string.
        /// </summary>
        public override string ToString() => $"{X},{Y}";

        internal static SDL_Rect ToSDL_Rect(IntVector2 point1, IntVector2 point2) =>
            new() { x = point1.X, y = point1.Y, w = point2.X, h = point2.Y};

        /// <summary>
        /// Checks if the vectors hold the same values.
        /// </summary>
        public bool Equals(IntVector2 other) {
            return X == other.X && Y == other.Y;
        }

        /// <summary>
        /// Checks if the vectors hold the same values.
        /// </summary>
        public bool Equals(Vector2 other) {
            return X == other.X && Y == other.Y;
        }

        /// <summary>
        /// Checks if the vectors hold the same values.
        /// </summary>
        public override bool Equals(object obj) {
            return obj is Vector2 vector && Equals(vector);
        }

        /// <summary>
        /// Gets the hashcode.
        /// </summary>
        public override int GetHashCode() {
            return HashCode.Combine(X, Y);
        }
    }
}
