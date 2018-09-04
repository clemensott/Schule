using System;
using System.Threading;
using MV;
using System.Drawing;
using System.Linq;

namespace RobotWorld
{
    partial class RobotProg
    {
        #region Usefull Functions

        void DriveDistance(double aPow, double aDist)
        {
            rb.SetV(aPow); // Motoren ein
            Vect2D start = rb.Pos;
            while (true)
            {
                // Warten bis die Simulation eine neue Pos berchnet hat
                WaitForUpdate();
                if (start.DistBetweenPoints(rb.Pos) > aDist)
                    break;
            }
            rb.SetV(0); // Motoren aus
        }

        // Turn aPhi from current Heading
        void TurnRelAngle(double aPhi, double aRotSpeed)
        {
            double turnedPhi = 0;
            double previousPhi = GetAngleInRange(rb.GyroAngle);

            while (!SetTurnRelAngle(aPhi, aRotSpeed, ref turnedPhi, ref previousPhi)) WaitForUpdate();
        }

        bool SetTurnRelAngle(double relPhi, double aRotSpeed, ref double turnedPhi, ref double previousPhi)
        {
            double currentPhi = GetAngleInRange(rb.GyroAngle);

            if (relPhi > 0)
            {
                if (currentPhi < previousPhi) previousPhi -= 360;

                turnedPhi += currentPhi - previousPhi;
                previousPhi = currentPhi;

                if (turnedPhi < relPhi)
                {
                    rb.Set_dPhi(aRotSpeed);
                    return false;
                }
                else
                {
                    rb.Set_dPhi(0);
                    return true;
                }
            }
            else if (relPhi < 0)
            {
                if (currentPhi > previousPhi) previousPhi += 360;

                turnedPhi += currentPhi - previousPhi;
                previousPhi = currentPhi;

                if (turnedPhi > relPhi)
                {
                    rb.Set_dPhi(-aRotSpeed);
                    return false;
                }
                else
                {
                    rb.Set_dPhi(0);
                    return true;
                }
            }
            else
            {
                rb.Set_dPhi(0);
                previousPhi = currentPhi;
            }

            return true;
        }

        // aPhi must be within +/- 180°
        void TurnAbsAngle(double aPhi, double aRotSpeed)
        {
            TurnRelAngle(GetRelAngle(aPhi), aRotSpeed);
        }

        double GetRelAngle(double targetPhi)
        {
            double relPhi1 = targetPhi - ((rb.GyroAngle % 360) + 360) % 360;
            double relPhi2 = targetPhi - ((rb.GyroAngle % 360) + 360) % 360 + 360;

            return Math.Abs(relPhi1) < Math.Abs(relPhi2) ? relPhi1 : relPhi2;
        }

        void SetFollow(Vect2D target, double keepDistance, double speedFactor)
        {
            Vect2D vect = GetVectorToTarget(target, keepDistance);

            double dPhi = GetRelAngle(vect.GetPhiGrad()) * speedFactor * 2;
            double v = vect.VectLength() * speedFactor;
            if (v < speedFactor * 1.1) dPhi = v = 0;

            rb.Set_dPhi(dPhi);
            rb.SetV(v);
        }

        Vect2D GetVectorToTarget(Vect2D target, double keepDistance)
        {
            double deltaX = target.X - rb.Pos.X;
            double deltaY = target.Y - rb.Pos.Y;
            double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            double factor = (distance - keepDistance) / distance;

            return new Vect2D(deltaX * factor, deltaY * factor, false);
        }

        double GetAngleInRange(double aPhi)
        {
            return ((aPhi + 180) % 360 + 360) % 360 - 180;
        }

        double DistanceTo(Robot otherRb)
        {
            double distance = (otherRb.Pos - rb.Pos).VectLength();
            return (otherRb.Pos - rb.Pos).VectLength();
        }

        bool DrivingTowardsOther(Robot otherRb)
        {
            if (otherRb == rb) return false;

            double currentDistance = (otherRb.Pos - rb.Pos).VectLength();
            double nextDistance = (otherRb.Pos - (rb.Pos + rb.V)).VectLength();

            return currentDistance >= nextDistance;
        }

        bool DrivingTowardsThis(Robot otherRb)
        {
            if (otherRb == rb) return false;

            double currentDistance = (otherRb.Pos - rb.Pos).VectLength();
            double nextDistance = (otherRb.Pos + otherRb.V - rb.Pos).VectLength();

            return currentDistance >= nextDistance;
        }
        #endregion

        #region Programms

        void PrgAngleTest()
        {
            while (true)
            {
                TurnAbsAngle(0, 5);
                Thread.Sleep(100);
                TurnAbsAngle(90, 5);
                Thread.Sleep(1000);
            }
        }

        void PrgDistTest()
        {
            while (true)
            {
                DriveDistance(5, 100);
                DriveDistance(-5, 100);
            }
        }

        void PrgRectangle()
        {
            while (true)
            {
                DriveDistance(5, 100);
                TurnRelAngle(90, 5);
                WaitForUpdate();
            }
        }

        void PrgReflectBorder()
        {
            Random ran = new Random();

            TurnRelAngle(ran.Next(-180, 180), 5);

            while (true)
            {
                if (rb.Pos.X < DblBuffForm.XMin())
                {
                    DriveDistance(-5, 20);
                    TurnRelAngle(rb.GyroAngle > 0 ? -90 : 90, 5);
                }
                else if (rb.Pos.X > DblBuffForm.XMax())
                {
                    DriveDistance(-5, 20);
                    TurnRelAngle(rb.GyroAngle > 0 ? 90 : -90, 5);
                }
                else if (rb.Pos.Y < DblBuffForm.YMin())
                {
                    DriveDistance(-5, 20);
                    TurnRelAngle(rb.GyroAngle + 90 > 0 ? 90 : -90, 5);
                }
                else if (rb.Pos.Y > DblBuffForm.YMax())
                {
                    DriveDistance(-5, 20);
                    double phi = rb.GyroAngle;
                    TurnRelAngle(rb.GyroAngle - 90 > 0 ? 90 : -90, 5);
                }

                rb.SetV(5);
                WaitForUpdate();
            }
        }

        void PrgFollowMouse()
        {
            while (true)
            {
                SetFollow(mpos, 0, 0.1);
                WaitForUpdate();
            }
        }

        void ProgTrain()
        {
            Robot followingRobot = RobotMgr.GetNearest(rb);

            while (true)
            {
                if (followingRobot != null) SetFollow(followingRobot.Pos, 50, 0.1);
                else SetFollow(mpos, 0, 0.1);

                WaitForUpdate();
            }
        }

        void ProgObstacle()
        {
            while (true)
            {
                for (int i = 0; i < Omgr.Count; i++)
                {
                    double distance;
                    Obstacle obstacle = Omgr.At(i);

                    Vect2D vect = GetVectorToTarget(obstacle.pos, 0);
                    rb.SetV(0);
                    TurnAbsAngle(vect.GetPhiGrad(), 5);

                    do
                    {
                        double dPhi = GetRelAngle(vect.GetPhiGrad()) / 10;
                        rb.Set_dPhi(dPhi);
                        rb.SetV(5);

                        WaitForUpdate();

                        distance = vect.VectLength();
                        vect = GetVectorToTarget(obstacle.pos, 0);
                    }
                    while (vect.VectLength() < distance);
                }

                WaitForUpdate();
            }
        }

        enum DogeState { Forward, ForwardClockwise, ForwardAnticlockwise, BackwardClockwise, BackwardAnticlockwise, Turn }

        void ProgDoge()
        {
            const double speed = 5;
            Random ran = new Random();
            DogeState state = DogeState.Turn;
            double vFactor = 1;
            double v = 0;
            double turnedPhi = 0;
            double relPhi = 0;

            TurnAbsAngle(ran.Next(-180, 180), 5);
            double previousPhi = GetAngleInRange(rb.GyroAngle);

            while (true)
            {
                double phi = GetAngleInRange(rb.GyroAngle);

                if (rb.Pos.X < DblBuffForm.XMin())
                {
                    if (phi >= -180 && phi <= -90 && v > 0 && state != DogeState.BackwardClockwise)
                    {
                        state = DogeState.BackwardClockwise;
                        relPhi = 45;
                        turnedPhi = 0;
                    }
                    else if (phi >= 90 && phi <= 180 && v > 0 && state != DogeState.BackwardAnticlockwise)
                    {
                        state = DogeState.BackwardAnticlockwise;
                        relPhi = -45;
                        turnedPhi = 0;
                    }
                    else if (phi >= 0 && phi <= 90 && v < 0 && state != DogeState.ForwardClockwise)
                    {
                        state = DogeState.ForwardClockwise;
                        relPhi = 45;
                        turnedPhi = 0;
                    }
                    else if (phi >= -90 && phi <= 0 && v < 0 && state != DogeState.ForwardAnticlockwise)
                    {
                        state = DogeState.ForwardAnticlockwise;
                        relPhi = -45;
                        turnedPhi = 0;
                    }
                }
                else if (rb.Pos.X > DblBuffForm.XMax())
                {
                    if (phi >= 0 && phi <= 90 && v > 0 && state != DogeState.BackwardClockwise)
                    {
                        state = DogeState.BackwardClockwise;
                        relPhi = 45;
                        turnedPhi = 0;
                    }
                    else if (phi >= -90 && phi <= 0 && v > 0 && state != DogeState.BackwardAnticlockwise)
                    {
                        state = DogeState.BackwardAnticlockwise;
                        relPhi = -45;
                        turnedPhi = 0;
                    }
                    else if (phi >= -180 && phi <= -90 && v < 0 && state != DogeState.ForwardClockwise)
                    {
                        state = DogeState.ForwardClockwise;
                        relPhi = 45;
                        turnedPhi = 0;
                    }
                    else if (phi >= 90 && phi <= 180 && v < 0 && state != DogeState.ForwardAnticlockwise)
                    {
                        state = DogeState.ForwardAnticlockwise;
                        relPhi = -45;
                        turnedPhi = 0;
                    }
                }
                else if (rb.Pos.Y < DblBuffForm.YMin())
                {
                    if (phi >= -90 && phi <= 0 && v > 0 && state != DogeState.BackwardClockwise)
                    {
                        state = DogeState.BackwardClockwise;
                        relPhi = 45;
                        turnedPhi = 0;
                    }
                    else if (phi >= -180 && phi <= -90 && v > 0 && state != DogeState.BackwardAnticlockwise)
                    {
                        state = DogeState.BackwardAnticlockwise;
                        relPhi = -45;
                        turnedPhi = 0;
                    }
                    else if (phi >= 90 && phi <= 180 && v < 0 && state != DogeState.ForwardClockwise)
                    {
                        state = DogeState.ForwardClockwise;
                        relPhi = 45;
                        turnedPhi = 0;
                    }
                    else if (phi >= 0 && phi <= 90 && v < 0 && state != DogeState.ForwardAnticlockwise)
                    {
                        state = DogeState.ForwardAnticlockwise;
                        relPhi = -45;
                        turnedPhi = 0;
                    }
                }
                else if (rb.Pos.Y > DblBuffForm.YMax())
                {
                    if (phi >= 90 && phi <= 180 && v > 0 && state != DogeState.BackwardClockwise)
                    {
                        state = DogeState.BackwardClockwise;
                        relPhi = 45;
                        turnedPhi = 0;
                    }
                    else if (phi >= 0 && phi <= 90 && v > 0 && state != DogeState.BackwardAnticlockwise)
                    {
                        state = DogeState.BackwardAnticlockwise;
                        relPhi = -45;
                        turnedPhi = 0;
                    }
                    else if (phi >= -90 && phi <= 0 && v < 0 && state != DogeState.ForwardClockwise)
                    {
                        state = DogeState.ForwardClockwise;
                        relPhi = 45;
                        turnedPhi = 0;
                    }
                    else if (phi >= -180 && phi <= -90 && v < 0 && state != DogeState.ForwardAnticlockwise)
                    {
                        state = DogeState.ForwardAnticlockwise;
                        relPhi = -45;
                        turnedPhi = 0;
                    }
                }
                else
                {
                    Robot[] nearRobots = RobotMgr.GetRobots().Where(r => r != rb && DistanceTo(r) < 50).
                        OrderBy(r => DistanceTo(r)).ToArray();
                    int toNearRobotsCount = nearRobots.Count(r => DrivingTowardsOther(r));

                    if (toNearRobotsCount > 0)
                    {
                        state = DogeState.Turn;

                        double toRobotPhi = (nearRobots.First().Pos - rb.Pos).GetPhiGrad();
                        double relToRobotPhi = GetRelAngle(toRobotPhi);

                        relPhi = relToRobotPhi < 0 ? 180 : -180;
                        turnedPhi = 0;
                    }
                    else if (toNearRobotsCount == 0 && state == DogeState.Turn)
                    {
                        state = DogeState.Forward;
                        relPhi = 0;
                        turnedPhi = 0;
                    }

                    vFactor = nearRobots.Length > 0 ? Math.Pow(DistanceTo(nearRobots.First()) / 50, 3) : 1;
                }

                switch (state)
                {
                    case DogeState.Forward:
                        rb.SetV(v = speed * vFactor);
                        break;

                    case DogeState.ForwardClockwise:
                        rb.SetV(v = speed * vFactor);
                        break;

                    case DogeState.ForwardAnticlockwise:
                        rb.SetV(v = speed * vFactor);
                        break;

                    case DogeState.BackwardClockwise:
                        rb.SetV(v = -speed * vFactor);
                        break;

                    case DogeState.BackwardAnticlockwise:
                        rb.SetV(v = -speed * vFactor);
                        break;

                    case DogeState.Turn:
                        rb.SetV(v = 0);
                        break;
                }

                if (SetTurnRelAngle(relPhi, 5, ref turnedPhi, ref previousPhi))
                {
                    switch (state)
                    {
                        case DogeState.ForwardClockwise:
                            state = DogeState.Forward;
                            relPhi = 0;
                            turnedPhi = 0;
                            break;

                        case DogeState.ForwardAnticlockwise:
                            state = DogeState.Forward;
                            relPhi = 0;
                            turnedPhi = 0;
                            break;

                        case DogeState.BackwardClockwise:
                            state = DogeState.ForwardAnticlockwise;
                            relPhi = 45;
                            turnedPhi = 0;
                            break;

                        case DogeState.BackwardAnticlockwise:
                            state = DogeState.ForwardClockwise;
                            relPhi = -45;
                            turnedPhi = 0;
                            break;

                        case DogeState.Turn:
                            state = DogeState.Forward;
                            relPhi = 0;
                            turnedPhi = 0;
                            break;
                    }
                }

                WaitForUpdate();
            }
        }
        #endregion
    }
}
