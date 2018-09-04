using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthSim
{
    class Labyrinth
    {
        private Block[,] blocks;

        public Block this[int x, int y] { get { return blocks[x, y]; } }

        public Blockrelation this[double x, double y]
        {
            get { return GetAllRelations().OrderBy(r => Distance(x, y, r)).First(); }
        }

        public Block Start { get; private set; }

        public SquareTarget Target { get; set; }

        public int Width { get { return blocks.GetLength(0); } }

        public int Height { get { return blocks.GetLength(1); } }

        private Labyrinth(int width, int height, int targetX, int targetY)
        {
            blocks = Block.GetBlocks(width, height);
            Start = blocks[0, 0];
            Target = new SquareTarget(blocks[targetX, targetY]);
        }

        private double Distance(double x, double y, Blockrelation relation)
        {
            if (relation.Block2 == null) return double.MaxValue;

            double relationX = (relation.Block1.X + relation.Block2.X) / 2.0;
            double relationY = (relation.Block1.Y + relation.Block2.Y) / 2.0;

            return Math.Sqrt(Math.Pow(relationX - x, 2) + Math.Pow(relationY - y, 2));
        }

        public void GetPosition(Block block, out int x, out int y)
        {
            for (x = 0; x < Width; x++)
            {
                for (y = 0; y < Height; y++)
                {
                    if (this[x, y] == block) return;
                }
            }

            x = -1;
            y = -1;
        }

        private static Random ran = new Random();

        public static Labyrinth GetActual(int width, int height)
        {
            Labyrinth labyrinth = new Labyrinth(width, height, width / 2, height / 2);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (i + 1 < width) labyrinth[i, j].Right.Open();
                    if (j + 1 < height) labyrinth[i, j].Bottom.Open();
                }
            }

            return labyrinth;
        }

        #region Generate Random Labyrinth (does not work)
        //public static Labyrinth GetActual(int width, int height, int minRouteLength)
        //{
        //    System.Diagnostics.Debug.WriteLine("GetActual");
        //    Labyrinth labyrinth = new Labyrinth(width, height, width / 2, height / 2);

        //    do
        //    {
        //        for (int i = 0; i < width; i++)
        //        {
        //            for (int j = 0; j < height; j++)
        //            {
        //                if (i + 1 < width) labyrinth[i, j].Right.Open();
        //                if (j + 1 < height) labyrinth[i, j].Bottom.Open();
        //            }
        //        }

        //        labyrinth.CloseTarget();

        //    } while (!labyrinth.AddWalls(minRouteLength));

        //    System.Diagnostics.Debug.WriteLine("RemoveJustCorners");
        //    labyrinth.RemoveJustCorners();
        //    labyrinth.OpenWalls(minRouteLength, 50);

        //    return labyrinth;
        //}

        //public void CloseTarget()
        //{
        //    IEnumerable<BlockRelation> targetRelations = GetAllRelations().
        //        Where(r => Target.Is(r.Block1) ^ Target.Is(r.Block2));

        //    foreach (BlockRelation relation in targetRelations) relation.Close();

        //    targetRelations.ElementAt(ran.Next(targetRelations.Count())).Open();
        //}

        //public bool AddWalls(int minRouteLength)
        //{
        //    List<BlockRelation> relations = GetAddableRelation().ToList();
        //    Route currentRoute = null;

        //    while (relations.Count > 0)
        //    {
        //        int index = ran.Next(relations.Count);
        //        BlockRelation previousChangedRelation = relations[index];
        //        previousChangedRelation.Close();
        //        relations.RemoveAt(index);

        //        currentRoute = new Route(Width * Height);

        //        if (!Start.TryGetPossibleRoute(ref currentRoute, Target))
        //        {
        //            previousChangedRelation.Open();
        //        }
        //        else if (currentRoute.CurrentLength > minRouteLength) return true;
        //    }

        //    System.Diagnostics.Debug.WriteLine("AddedWalls: " + currentRoute.CurrentLength);
        //    return false;
        //}

        //public void RemoveJustCorners()
        //{
        //    Route currentRoute = new Route(Width * Height);
        //    Start.TryGetPossibleRoute(ref currentRoute, Target);

        //    BlockRelation[] relations = GetAddableRelation().
        //        Where(ar => !currentRoute.GetRelations().Any(rr => ar == rr)).ToArray();
        //    IEnumerable<BlockRelation> corners = relations.Where(IsAroundJustCorner);

        //    while (true)
        //    {
        //        int count = corners.Count();

        //        if (count == 0) break;

        //        corners.ElementAt(ran.Next(count)).Close();
        //    }
        //}

        //public bool IsAroundJustCorner(BlockRelation relation)
        //{
        //    if (relation.Relation != RelationType.Open) return false;
        //    if (relation.Block2 == null) return false;

        //    if (relation.Block1.GetTopBlock() == relation.Block2)
        //    {
        //        if (relation.Block1.GetLeftBlock()?.Top.Relation == RelationType.Open) return true;
        //        if (relation.Block1.GetRightBlock()?.Top.Relation == RelationType.Open) return true;
        //    }
        //    else if (relation.Block1.GetBottomBlock() == relation.Block2)
        //    {
        //        if (relation.Block1.GetLeftBlock()?.Bottom.Relation == RelationType.Open) return true;
        //        if (relation.Block1.GetRightBlock()?.Bottom.Relation == RelationType.Open) return true;
        //    }
        //    else if (relation.Block1.GetRightBlock() == relation.Block2)
        //    {
        //        if (relation.Block1.GetTopBlock()?.Right.Relation == RelationType.Open) return true;
        //        if (relation.Block1.GetBottomBlock()?.Right.Relation == RelationType.Open) return true;
        //    }
        //    else if (relation.Block1.GetLeftBlock() == relation.Block2)
        //    {
        //        if (relation.Block1.GetTopBlock()?.Right.Relation == RelationType.Open) return true;
        //        if (relation.Block1.GetBottomBlock()?.Right.Relation == RelationType.Open) return true;
        //    }

        //    return false;
        //}

        //public void OpenWalls(int minRouteLength, int maxClosedWalls)
        //{
        //    Random ran = new Random();
        //    List<BlockRelation> relations = GetAllRelations().
        //        Where(r => r.Block2 != null && r.Relation == RelationType.Close).ToList();

        //    while (relations.Count > maxClosedWalls)
        //    {
        //        int index = ran.Next(relations.Count);
        //        BlockRelation previousChangedRelation = relations[index];
        //        previousChangedRelation.Open();
        //        relations.RemoveAt(index);

        //        Route currentRoute = new Route(Width * Height);

        //        Start.TryGetPossibleRoute(ref currentRoute, Target);

        //        if (currentRoute.CurrentLength < minRouteLength) previousChangedRelation.Close();
        //    }
        //}

        //private IEnumerable<BlockRelation> GetAddableRelation()
        //{
        //    return GetAllRelations().
        //        Where(r => r.Relation == RelationType.Open && (!Target.Is(r.Block1) && !Target.Is(r.Block2)));
        //}
        #endregion

        public IEnumerable<Blockrelation> GetAllRelations()
        {
            return GetAbsoluteEveryRelations().Distinct().Where(r => r.Block2 != null);
        }

        private IEnumerable<Blockrelation> GetAbsoluteEveryRelations()
        {
            foreach (Block block in blocks)
            {
                yield return block.Top;
                yield return block.Bottom;
                yield return block.Right;
                yield return block.Left;
            }
        }

        public static Labyrinth GetLern(Labyrinth labyrinth)
        {
            int targetX, targetY;
            labyrinth.GetPosition(labyrinth.Target.TopLeft, out targetX, out targetY);

            return new Labyrinth(labyrinth.Width, labyrinth.Height, targetX, targetY);
        }
    }
}
