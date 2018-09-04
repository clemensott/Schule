using System;

namespace LabyrinthSim
{
    class Labyrinth
    {
        public int this[double x, double y]
        {
            get
            {
                RelationArray array;
                int i, j;

                GetRelation(x, y, out array, out i, out j);

                return array[i, j];
            }

            set
            {
                RelationArray array;
                int i, j;

                GetRelation(x, y, out array, out i, out j);

                array[i, j] = value;

                HasChanges = true;
            }
        }

        public int this[Block block1, Block block2]
        {
            get
            {
                if (block1.X < block2.X) return H[block1];
                if (block1.X > block2.X) return H[block2];
                if (block1.Y < block2.Y) return V[block1];

                return V[block2];
            }

            set
            {
                if (block1.X < block2.X) H[block1] = value;
                else if (block1.X > block2.X) H[block2] = value;
                else if (block1.Y < block2.Y) V[block1] = value;
                else V[block2] = value;

                HasChanges = true;
            }
        }

        public SquareTarget Target { get; set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public RelationArray H { get; set; }

        public RelationArray V { get; set; }

        public bool HasChanges { get; set; }

        public Labyrinth(int width, int height, int targetX, int targetY, int startValue = 2)
        {
            Width = width;
            Height = height;

            Target = new SquareTarget(new Block(targetX, targetY));

            H = new RelationArray(width - 1, height, startValue);
            V = new RelationArray(width, height - 1, startValue);
        }

        private void GetRelation(double x, double y, out RelationArray array, out int i, out int j)
        {
            double hX = Math.Round(x);
            double hY = y - y % 1 + 0.5;
            double vX = x - x % 1 + 0.5;
            double vY = Math.Round(y);

            if (Distance(x, y, hX, hY) < Distance(x, y, vX, vY))
            {
                array = V;
                i = (int)hX;
                j = (int)(hY - 0.5);

                if (i >= Width) i = Width - 1;
                if (j + 1 >= Height) i = Height - 2;
            }
            else
            {
                array = H;
                i = (int)(vX - 0.5);
                j = (int)vY;

                if (i + 1 >= Width) i = Width - 2;
                if (j >= Height) i = Height - 1;
            }

            if (i < 0) i = 0;
            if (j < 0) j = 0;
        }

        private double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }



        public static Labyrinth GetActual(int width, int height)
        {
            Labyrinth labyrinth = new Labyrinth(width, height, width / 2, height / 2);

            SetRelationValue(labyrinth, 2);

            return labyrinth;
        }

        private static void SetRelationValue(Labyrinth labyrinth, int value)
        {
            for (int i = 0; i < labyrinth.H.Width; i++)
            {
                for (int j = 0; j < labyrinth.H.Height; j++)
                {
                    labyrinth.H[i, j] = value;
                }
            }

            for (int i = 0; i < labyrinth.V.Width; i++)
            {
                for (int j = 0; j < labyrinth.V.Height; j++)
                {
                    labyrinth.V[i, j] = value;
                }
            }
        }

        #region Generate Random Labyrinth (does not work)
        //private static Random ran = new Random();

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

        public static Labyrinth GetLern(Labyrinth labyrinth)
        {
            int targetX = labyrinth.Target.TopLeft.X, targetY = labyrinth.Target.TopLeft.Y;

            return new Labyrinth(labyrinth.Width, labyrinth.Height, targetX, targetY);
        }
    }
}
