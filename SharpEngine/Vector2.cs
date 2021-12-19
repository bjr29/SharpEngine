using System;
using static SDL2.SDL;

namespace SharpEngine {
    /// <summary>
    /// Used to show a point in a 2D space using float values.
    /// </summary>
    public struct Vector2 : IEquatable<Vector2>, IEquatable<IntVector2> {
        /// <summary>
        /// 0, 1
        /// </summary>
        public static readonly Vector2 Up = new(y: 1);
        /// <summary>
        /// -1, 0
        /// </summary>
        public static readonly Vector2 Left = new(x: -1);
        /// <summary>
        /// 0, -1
        /// </summary>
        public static readonly Vector2 Down = new(y: -1);
        /// <summary>
        /// 1, 0
        /// </summary>
        public static readonly Vector2 Right = new(x: 1);
        /// <summary>
        /// 0, 0
        /// </summary>
        public static readonly Vector2 Zero = new();
        
        /// <summary>
        /// The x position of the vector.
        /// </summary>
        public float X { get; set; }
        /// <summary>
        /// The y position of the vector.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Creates a vector.
        /// </summary>
        /// <param name="x">The x position of the vector.</param>
        /// <param name="y">The y position of the vector.</param>
        public Vector2(float x = 0, float y = 0) {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Add the values of 2 vectors.
        /// </summary>
        public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
        /// <summary>
        /// Subtracts the values of 2 vectors.
        /// </summary>
        public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
        /// <summary>
        /// Divides the values of 2 vectors.
        /// </summary>
        public static Vector2 operator /(Vector2 a, Vector2 b) => new(a.X / b.X, a.Y / b.Y);
        /// <summary>
        /// Multiplies the values of 2 vectors.
        /// </summary>
        public static Vector2 operator *(Vector2 a, Vector2 b) => new(a.X * b.X, a.Y * b.Y);

        /// <summary>
        /// Add the values of 2 vectors.
        /// </summary>
        public static Vector2 operator +(Vector2 a, IntVector2 b) => new(a.X + b.X, a.Y + b.Y);
        /// <summary>
        /// Subtracts the values of 2 vectors.
        /// </summary>
        public static Vector2 operator -(Vector2 a, IntVector2 b) => new(a.X - b.X, a.Y - b.Y);
        /// <summary>
        /// Divides the values of 2 vectors.
        /// </summary>
        public static Vector2 operator /(Vector2 a, IntVector2 b) => new(a.X / b.X, a.Y / b.Y);
        /// <summary>
        /// Multiplies the values of 2 vectors.
        /// </summary>
        public static Vector2 operator *(Vector2 a, IntVector2 b) => new(a.X * b.X, a.Y * b.Y);

        /// <summary>
        /// Add the values of 2 vectors.
        /// </summary>
        public static Vector2 operator +(IntVector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
        /// <summary>
        /// Substracts the values of 2 vectors.
        /// </summary>
        public static Vector2 operator -(IntVector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
        /// <summary>
        /// Divides the values of 2 vectors.
        /// </summary>
        public static Vector2 operator /(IntVector2 a, Vector2 b) => new(a.X / b.X, a.Y / b.Y);
        /// <summary>
        /// Multiplies the values of 2 vectors.
        /// </summary>
        public static Vector2 operator *(IntVector2 a, Vector2 b) => new(a.X * b.X, a.Y * b.Y);

        /// <summary>
        /// Adds both values of the vector.
        /// </summary>
        public static Vector2 operator +(Vector2 a, int b) => new(a.X + b, a.Y + b);
        /// <summary>
        /// Subtracts both values of the vector.
        /// </summary>
        public static Vector2 operator -(Vector2 a, int b) => new(a.X - b, a.Y - b);
        /// <summary>
        /// Divides both values of the vector.
        /// </summary>
        public static Vector2 operator /(Vector2 a, int b) => new(a.X / b, a.Y / b);
        /// <summary>
        /// Multiplies both values of the vector.
        /// </summary>
        public static Vector2 operator *(Vector2 a, int b) => new(a.X * b, a.Y * b);

        /// <summary>
        /// Adds both values of the vector.
        /// </summary>
        public static Vector2 operator +(int a, Vector2 b) => new(a + b.X, a + b.Y);
        /// <summary>
        /// Subtracts both values of the vector.
        /// </summary>
        public static Vector2 operator -(int a, Vector2 b) => new(a - b.X, a - b.Y);
        /// <summary>
        /// Divides both values of the vector.
        /// </summary>
        public static Vector2 operator /(int a, Vector2 b) => new(a / b.X, a / b.Y);
        /// <summary>
        /// Multiplies both values of the vector.
        /// </summary>
        public static Vector2 operator *(int a, Vector2 b) => new(a * b.X, a * b.Y);

        /// <summary>
        /// Adds both values of the vector.
        /// </summary>
        public static Vector2 operator +(Vector2 a, float b) => new(a.X + b, a.Y + b);
        /// <summary>
        /// Subtracts both values of the vector.
        /// </summary>
        public static Vector2 operator -(Vector2 a, float b) => new(a.X - b, a.Y - b);
        /// <summary>
        /// Divides both values of the vector.
        /// </summary>
        public static Vector2 operator /(Vector2 a, float b) => new(a.X / b, a.Y / b);
        /// <summary>
        /// Multiplies both values of the vector.
        /// </summary>
        public static Vector2 operator *(Vector2 a, float b) => new(a.X * b, a.Y * b);

        /// <summary>
        /// Adds both values of the vector.
        /// </summary>
        public static Vector2 operator +(float a, Vector2 b) => new(a + b.X, a + b.Y);
        /// <summary>
        /// Subtracts both values of the vector.
        /// </summary>
        public static Vector2 operator -(float a, Vector2 b) => new(a - b.X, a - b.Y);
        /// <summary>
        /// Divides both values of the vector.
        /// </summary>
        public static Vector2 operator /(float a, Vector2 b) => new(a / b.X, a / b.Y);
        /// <summary>
        /// Multiplies both values of the vector.
        /// </summary>
        public static Vector2 operator *(float a, Vector2 b) => new(a * b.X, a * b.Y);

        /// <summary>
        /// Adds both values of the vector.
        /// </summary>
        public static Vector2 operator +(Vector2 a, double b) => new(a.X + (float)b, a.Y + (float)b);
        /// <summary>
        /// Subtracts both values of the vector.
        /// </summary>
        public static Vector2 operator -(Vector2 a, double b) => new(a.X - (float)b, a.Y - (float)b);
        /// <summary>
        /// Divides both values of the vector.
        /// </summary>
        public static Vector2 operator /(Vector2 a, double b) => new(a.X / (float)b, a.Y / (float)b);
        /// <summary>
        /// Multiplies both values of the vector.
        /// </summary>
        public static Vector2 operator *(Vector2 a, double b) => new(a.X * (float)b, a.Y * (float)b);

        /// <summary>
        /// Adds both values of the vector.
        /// </summary>
        public static Vector2 operator +(double a, Vector2 b) => new((float)a + b.X, (float)a + b.Y);
        /// <summary>
        /// Subtracts both values of the vector.
        /// </summary>
        public static Vector2 operator -(double a, Vector2 b) => new((float)a - b.X, (float)a - b.Y);
        /// <summary>
        /// Divides both values of the vector.
        /// </summary>
        public static Vector2 operator /(double a, Vector2 b) => new((float)a / b.X, (float)a / b.Y);
        /// <summary>
        /// Multiplies both values of the vector.
        /// </summary>
        public static Vector2 operator *(double a, Vector2 b) => new((float)a * b.X, (float)a * b.Y);

        /// <summary>
        /// Checks if the vectors hold the same values.
        /// </summary>
        public static bool operator ==(Vector2 a, Vector2 b) => Equals(a, b);
        /// <summary>
        /// Checks if the vectors don't hold the same values.
        /// </summary>
        public static bool operator !=(Vector2 a, Vector2 b) => !Equals(a, b);

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator IntVector2(Vector2 vector2) => new((int)vector2.X, (int)vector2.Y);

        /// <summary>
        /// Converts the vector to a string.
        /// </summary>
        public override string ToString() => $"{X},{Y}";

        internal static SDL_FRect ToSDL_FRect(Vector2 position, Vector2 size) =>
            new() { x = (int)position.X, y = (int)position.Y, w = (int)size.X, h = (int)size.Y };

        /// <summary>
        /// Checks if the vectors hold the same values.
        /// </summary>
        public bool Equals(Vector2 other) {
            return X == other.X && Y == other.Y;
        }

        /// <summary>
        /// Checks if the vectors hold the same values.
        /// </summary>
        public bool Equals(IntVector2 other) {
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
