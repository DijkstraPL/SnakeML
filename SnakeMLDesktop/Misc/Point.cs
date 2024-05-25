using System.Collections.Generic;
using System;

namespace SnakeMLDesktop.Misc
{
    public class Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point Copy()
        {
            return new Point(X, Y);
        }

        public Dictionary<string, int> ToDict()
        {
            return new Dictionary<string, int>
        {
            { "x", X },
            { "y", Y }
        };
        }

        public static Point FromDict(Dictionary<string, int> dict)
        {
            return new Point(dict["x"], dict["y"]);
        }

        public override bool Equals(object obj)
        {
            if (obj is Tuple<int, int> tuple)
            {
                return tuple.Item1 == X && tuple.Item2 == Y;
            }

            if (obj is Point point)
            {
                return point.X == X && point.Y == Y;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point operator -(Tuple<int, int> tuple, Point p)
        {
            return new Point(tuple.Item1 - p.X, tuple.Item2 - p.Y);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
