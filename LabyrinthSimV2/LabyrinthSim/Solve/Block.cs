namespace LabyrinthSim
{
    struct Block
    {
        public static readonly Block Origin = new Block(0, 0), None = new Block(-1, -1);

        public int X { get; private set; }

        public int Y { get; private set; }

        public Block Left { get { return new Block(X - 1, Y); } }

        public Block Right { get { return new Block(X + 1, Y); } }

        public Block Top { get { return new Block(X, Y - 1); } }

        public Block Bottom { get { return new Block(X, Y + 1); } }

        public Block(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return X.ToString() + " x " + Y.ToString();
        }

        public static bool operator ==(Block b1, Block b2)
        {
            return b1.X == b2.X && b1.Y == b2.Y;
        }

        public static bool operator !=(Block b1, Block b2)
        {
            return b1.X != b2.X || b1.Y != b2.Y;
        }
    }
}
