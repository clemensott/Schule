using System;

namespace LabrinthDraw
{
    struct Position
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int? Index { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
            Index = null;
        }

        public static Position SubAbs(Position pos1, Position pos2)
        {
            return new Position(Math.Abs(pos1.X - pos2.X), Math.Abs(pos1.Y - pos2.Y));
        }
    }
}