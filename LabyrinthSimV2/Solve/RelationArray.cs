namespace LabyrinthSim
{
    //  0...open; 1...closed; 2...unkownButOpen; 3...unkownButClosed
    class RelationArray
    {
        public int this[Block block]
        {
            get { return this[block.X, block.Y]; }
            set { this[block.X, block.Y] = value; }
        }

        public int this[int x, int y]
        {
            get { return IsIn(x, y) ? Array[x, y] : 1; }
            set { if (IsIn(x, y)) Array[x, y] = value; }
        }

        public int[,] Array { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public RelationArray(int width, int height, int startValue)
        {
            Width = width;
            Height = height;

            Array = new int[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Array[i, j] = startValue;
                }
            }
        }

        private bool IsIn(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }
    }
}
