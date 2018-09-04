using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;

namespace RobotWorld
{
    class RobotMgr
    {
        static List<RobotProg> _list = new List<RobotProg>();

        public static void CreateRobot(int aPrgNum, Point aMp)
        {
            RobotProg prg = new RobotProg(aPrgNum);
            prg.rb.showPath = Par.showPath;
            prg.rb.Pos.SetXY(aMp.X, aMp.Y);
            _list.Add(prg);
        }

        public static void Paint(Graphics gr)
        {
            foreach (RobotProg prg in _list)
                prg.rb.Paint(gr);
        }

        public static void CalcNextPos()
        {
            foreach (RobotProg prg in _list)
                prg.rb.CalcNextPos();
        }

        public static void UpdatePath()
        {
            foreach (RobotProg prg in _list)
                prg.rb.UpdatePath();
        }

        public static void CleanUp()
        {
            foreach (RobotProg prg in _list)
                prg.thr.Abort();
        }

        public static string GetInfo(Point aMp)
        {
            foreach (RobotProg prg in _list)
            {
                if (prg.rb.HitInRadius(aMp))
                    return prg.progName;
            }
            return "";
        }

        private static List<Robot> _usedRobots = new List<Robot>();

        public static Robot GetNearest(Robot robot)
        {
            Robot nearestRobot = _list.Select(p => p.rb).Except(_usedRobots).Where(r => r != robot).
                OrderBy(r => r.Pos.DistBetweenPoints(robot.Pos)).FirstOrDefault();
            _usedRobots.Add(nearestRobot);

            return nearestRobot;
        }

        public static IEnumerable<Robot> GetRobots()
        {
            return _list.Select(p => p.rb);
        }
    }
}
