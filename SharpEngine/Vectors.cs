using System;
using static SDL2.SDL;

namespace SharpEngine {
    public struct Vector2 : IEquatable<Vector2>, IEquatable<IntVector2> {
        #region Constants/ readonlys
        /// <summary>
        /// 0, 1
        /// </summary>
        public static readonly Vector2 Up = new(0, 1);
        /// <summary>
        /// -1, 0
        /// </summary>
        public static readonly Vector2 Left = new(-1, 0);
        /// <summary>
        /// 0, -1
        /// </summary>
        public static readonly Vector2 Down = new(0, -1);
        /// <summary>
        /// 1, 0
        /// </summary>
        public static readonly Vector2 Right = new(1, 0);
        #endregion
        
        #region Properties
        /// <summary>
        /// The x position of the vector.
        /// </summary>
        public float X { get; set; }
        /// <summary>
        /// The y position of the vector.
        /// </summary>
        public float Y { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a vector.
        /// </summary>
        /// <param name="x">The x position of the vector.</param>
        /// <param name="y">The y position of the vector.</param>
        public Vector2(float x = 0, float y = 0) {
            X = x;
            Y = y;
        }
        #endregion

        #region Operators
        public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator /(Vector2 a, Vector2 b) => new(a.X / b.X, a.Y / b.Y);
        public static Vector2 operator *(Vector2 a, Vector2 b) => new(a.X * b.X, a.Y * b.Y);

        public static Vector2 operator +(Vector2 a, IntVector2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, IntVector2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator /(Vector2 a, IntVector2 b) => new(a.X / b.X, a.Y / b.Y);
        public static Vector2 operator *(Vector2 a, IntVector2 b) => new(a.X * b.X, a.Y * b.Y);

        public static Vector2 operator +(IntVector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(IntVector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator /(IntVector2 a, Vector2 b) => new(a.X / b.X, a.Y / b.Y);
        public static Vector2 operator *(IntVector2 a, Vector2 b) => new(a.X * b.X, a.Y * b.Y);

        public static Vector2 operator +(Vector2 a, int b) => new(a.X + b, a.Y + b);
        public static Vector2 operator -(Vector2 a, int b) => new(a.X - b, a.Y - b);
        public static Vector2 operator /(Vector2 a, int b) => new(a.X / b, a.Y / b);
        public static Vector2 operator *(Vector2 a, int b) => new(a.X * b, a.Y * b);

        public static Vector2 operator +(int a, Vector2 b) => new(a + b.X, a + b.Y);
        public static Vector2 operator -(int a, Vector2 b) => new(a - b.X, a - b.Y);
        public static Vector2 operator /(int a, Vector2 b) => new(a / b.X, a / b.Y);
        public static Vector2 operator *(int a, Vector2 b) => new(a * b.X, a * b.Y);

        public static Vector2 operator +(Vector2 a, float b) => new(a.X + b, a.Y + b);
        public static Vector2 operator -(Vector2 a, float b) => new(a.X - b, a.Y - b);
        public static Vector2 operator /(Vector2 a, float b) => new(a.X / b, a.Y / b);
        public static Vector2 operator *(Vector2 a, float b) => new(a.X * b, a.Y * b);

        public static Vector2 operator +(float a, Vector2 b) => new(a + b.X, a + b.Y);
        public static Vector2 operator -(float a, Vector2 b) => new(a - b.X, a - b.Y);
        public static Vector2 operator /(float a, Vector2 b) => new(a / b.X, a / b.Y);
        public static Vector2 operator *(float a, Vector2 b) => new(a * b.X, a * b.Y);

        public static Vector2 operator +(Vector2 a, double b) => new(a.X + (float)b, a.Y + (float)b);
        public static Vector2 operator -(Vector2 a, double b) => new(a.X - (float)b, a.Y - (float)b);
        public static Vector2 operator /(Vector2 a, double b) => new(a.X / (float)b, a.Y / (float)b);
        public static Vector2 operator *(Vector2 a, double b) => new(a.X * (float)b, a.Y * (float)b);

        public static Vector2 operator +(double a, Vector2 b) => new((float)a + b.X, (float)a + b.Y);
        public static Vector2 operator -(double a, Vector2 b) => new((float)a - b.X, (float)a - b.Y);
        public static Vector2 operator /(double a, Vector2 b) => new((float)a / b.X, (float)a / b.Y);
        public static Vector2 operator *(double a, Vector2 b) => new((float)a * b.X, (float)a * b.Y);
        #endregion

        #region Methods
        public static implicit operator IntVector2(Vector2 vector2) => new((int)vector2.X, (int)vector2.Y);

        public override string ToString() => $"{X},{Y}";

        internal static SDL_FRect ToSDL_FRect(Vector2 position, Vector2 size) =>
            new() { x = (int)position.X, y = (int)position.Y, w = (int)size.X, h = (int)size.Y };
        #endregion

        #region Implemented Methods
        public bool Equals(Vector2 other) {
            if (other.X == X && other.Y == Y)
                return true;

            return false;
        }

        public bool Equals(IntVector2 other) {
            if (other.X == X && other.Y == Y)
                return true;

            return false;
        }
        #endregion
    }

    public struct IntVector2 : IEquatable<Vector2>, IEquatable<IntVector2> {
        #region Constants/ readonlys
        /// <summary>
        /// 0, 1
        /// </summary>
        public static readonly IntVector2 Up = new(0, 1);
        /// <summary>
        /// -1, 0
        /// </summary>
        public static readonly IntVector2 Left = new(-1, 0);
        /// <summary>
        /// 0, -1
        /// </summary>
        public static readonly IntVector2 Down = new(0, -1);
        /// <summary>
        /// 1, 0
        /// </summary>
        public static readonly IntVector2 Right = new(1, 0);
        #endregion

        #region Properties
        public int X { get; set; }
        /// <summary>
        /// The x position of the vector.
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// The y position of the vector.
        /// </summary>
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a vector.
        /// </summary>
        /// <param name="x">The x position of the vector.</param>
        /// <param name="y">The y position of the vector.</param>
        public IntVector2(int x = 0, int y = 0) {
            X = x;
            Y = y;
        }
        #endregion

        #region Operators
        public static IntVector2 operator +(IntVector2 a, IntVector2 b) => new(a.X + b.X, a.Y + b.Y);
        public static IntVector2 operator -(IntVector2 a, IntVector2 b) => new(a.X - b.X, a.Y - b.Y);
        public static IntVector2 operator /(IntVector2 a, IntVector2 b) => new(a.X / b.X, a.Y / b.Y);
        public static IntVector2 operator *(IntVector2 a, IntVector2 b) => new(a.X * b.X, a.Y * b.Y);

        public static IntVector2 operator +(IntVector2 a, int b) => new(a.X + b, a.Y + b);
        public static IntVector2 operator -(IntVector2 a, int b) => new(a.X - b, a.Y - b);
        public static IntVector2 operator /(IntVector2 a, int b) => new(a.X / b, a.Y / b);
        public static IntVector2 operator *(IntVector2 a, int b) => new(a.X * b, a.Y * b);

        public static IntVector2 operator +(int a, IntVector2 b) => new(a + b.X, a + b.Y);
        public static IntVector2 operator -(int a, IntVector2 b) => new(a - b.X, a - b.Y);
        public static IntVector2 operator /(int a, IntVector2 b) => new(a / b.X, a / b.Y);
        public static IntVector2 operator *(int a, IntVector2 b) => new(a * b.X, a * b.Y);

        public static IntVector2 operator +(IntVector2 a, float b) => new(a.X + (int)b, a.Y + (int)b);
        public static IntVector2 operator -(IntVector2 a, float b) => new(a.X - (int)b, a.Y - (int)b);
        public static IntVector2 operator /(IntVector2 a, float b) => new(a.X / (int)b, a.Y / (int)b);
        public static IntVector2 operator *(IntVector2 a, float b) => new(a.X * (int)b, a.Y * (int)b);

        public static IntVector2 operator +(float a, IntVector2 b) => new((int)a + b.X, (int)a + b.Y);
        public static IntVector2 operator -(float a, IntVector2 b) => new((int)a - b.X, (int)a - b.Y);
        public static IntVector2 operator /(float a, IntVector2 b) => new((int)a / b.X, (int)a / b.Y);
        public static IntVector2 operator *(float a, IntVector2 b) => new((int)a * b.X, (int)a * b.Y);

        public static IntVector2 operator +(IntVector2 a, double b) => new(a.X + (int)b, a.Y + (int)b);
        public static IntVector2 operator -(IntVector2 a, double b) => new(a.X - (int)b, a.Y - (int)b);
        public static IntVector2 operator /(IntVector2 a, double b) => new(a.X / (int)b, a.Y / (int)b);
        public static IntVector2 operator *(IntVector2 a, double b) => new(a.X * (int)b, a.Y * (int)b);

        public static IntVector2 operator +(double a, IntVector2 b) => new((int)a + b.X, (int)a + b.Y);
        public static IntVector2 operator -(double a, IntVector2 b) => new((int)a - b.X, (int)a - b.Y);
        public static IntVector2 operator /(double a, IntVector2 b) => new((int)a / b.X, (int)a / b.Y);
        public static IntVector2 operator *(double a, IntVector2 b) => new((int)a * b.X, (int)a * b.Y);
        #endregion

        #region Methods
        public static implicit operator Vector2(IntVector2 vector2) => new(vector2.X, vector2.Y);

        public override string ToString() => $"{X},{Y}";

        internal static SDL_Rect ToSDL_Rect(IntVector2 point1, IntVector2 point2) =>
            new() { x = point1.X, y = point1.Y, w = point2.X, h = point2.Y};
        #endregion

        #region Implemented Methods
        public bool Equals(Vector2 other) {
            if (other.X == X && other.Y == Y)
                return true;

            return false;
        }

        public bool Equals(IntVector2 other) {
            if (other.X == X && other.Y == Y)
                return true;

            return false;
        }
        #endregion
    }
}
