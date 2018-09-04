using System;
using System.Threading;
using MV;

namespace RobotWorld
{
    partial class RobotProg
    {
        #region MemberVariables
        public string progName; // Name of executed Robot-Programm
        public Robot rb = new Robot();
        public Thread thr;
        public static object signal = new object();
        #endregion

        public static Vect2D mpos; // MousePosition

        public RobotProg(int aPrgNum)
        {
            switch (aPrgNum)
            {
                case 1:
                    { progName = "Snake"; thr = new Thread(PrgSnake); }
                    break;
                case 2:
                    { progName = "DistTest"; thr = new Thread(PrgDistTest); }
                    break;
                case 3:
                    { progName = "Angle"; thr = new Thread(PrgAngleTest); }
                    break;
                case 4:
                    { progName = "Rect"; thr = new Thread(PrgRectangle); }
                    break;
                case 5:
                    { progName = "Reflect"; thr = new Thread(PrgReflectBorder); }
                    break;
                case 6:
                    { progName = "FollowMouse"; thr = new Thread(PrgFollowMouse); }
                    break;
                case 7:
                    { progName = "TrainFollowMouse"; thr = new Thread(ProgTrain); }
                    break;
                case 8:
                    { progName = "Obstacle"; thr = new Thread(ProgObstacle); }
                    break;
                case 9:
                    { progName = "Doge"; thr = new Thread(ProgDoge); }
                    break;
                default:
                    { progName = "Nothing"; thr = new Thread(WaitForUpdate); }
                    break;
            }
            thr.Start();
        }

        public static void SignalRobots()
        {
            lock (signal)
            {
                Monitor.PulseAll(signal);
            }
        }

        protected void WaitForUpdate()
        {
            lock (signal)
            { Monitor.Wait(signal); }
        }

        void PrgSnake()
        {
            rb.SetV(2); // 2Pix pro Frame vorw.
            while (true)
            {
                WaitForUpdate(); // warten bis es bei der RobotSim was neues gibt
                rb.Set_dPhi(2); // 
                Thread.Sleep(2000);
                WaitForUpdate();
                rb.Set_dPhi(-2);
                Thread.Sleep(2000);
            }
        }
    }
}
