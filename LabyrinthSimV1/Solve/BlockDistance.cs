namespace LabyrinthSim
{
    class BlockDistance
    {
        public int Distance { get; set; }

        public Block Block { get; private set; }

        public BlockDistance(Block block, int distance)
        {
            Block = block;
            Distance = distance;
        }
    }
}