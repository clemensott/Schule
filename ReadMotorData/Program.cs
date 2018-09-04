using MathNet.Numerics;
using MathNet.Numerics.LinearRegression;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

namespace ReadMotorData
{
    class Program
    {
        private static object lockObj = new object();

        static void Main(string[] args)
        {
            // TestRegression();
            Mouse m = new Mouse();
            XmlSerializer serializer = new XmlSerializer(m.GetType());

            SerialPort serial = new SerialPort("COM3", 9600);
            serial.DataReceived += Serial_DataReceived;

            serial.Open();

            BinaryReader reader = new BinaryReader(serial.BaseStream);
            Stopwatch sw = Stopwatch.StartNew();

            while (true)
            {
                lock (lockObj)
                {
                    while (serial.BytesToRead < 10) Monitor.Wait(lockObj);
                }

                while (serial.BytesToRead > 10)
                {
                    byte id1 = reader.ReadByte();
                    int value1 = reader.ReadInt32();

                    if (id1 > 0 && id1 <= 4) continue;

                    byte id2 = reader.ReadByte();
                    int value2 = reader.ReadInt32();

                    if (id2 != id1 + 4) continue;

                    Encoder e;
                    if (id1 == 1) e = m.Left.A;
                    else if (id1 == 2) e = m.Left.B:
                    else if (id1 == 3) e = m.Right.A;
                    else e = m.Right.B;

                    e.Values.ElementAtOrDefault(value1)?.Add(value2);
                }

                if (sw.ElapsedMilliseconds > 10000)
                {
                    TextWriter writer = new StringWriter();
                    serializer.Serialize(writer, m);

                    File.WriteAllText("Data.txt", writer.ToString());

                    sw = Stopwatch.StartNew();
                }

                if (Console.KeyAvailable) break;
            }

            serial.Close();
        }

        private static void TestRegression()
        {
            double[] x = new double[100];
            double[] y = new double[100];

            for (int i = 0; i < 100; i++)
            {
                x[i] = i + 1;
                y[i] = 200 + Math.Exp((i + 1) / -20.0);
            }

            double[] p = Exponential(x, y, DirectRegressionMethod.Svd); // a=1.017, r=0.687
            double[] yh = Generate.Map(x, k => p[0] * Math.Exp(p[1] * k)); // 2.02, 4.02, 7.98
        }

        static double[] Exponential(double[] x, double[] y, DirectRegressionMethod method = DirectRegressionMethod.QR)
        {
            double[] y_hat = Generate.Map(y, Math.Log);
            double[] p_hat = Fit.LinearCombination(x, y_hat, method, t => 1.0, t => t);
            return new[] { Math.Exp(p_hat[0]), p_hat[1] };
        }

        private static void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            lock (lockObj)
            {
                Monitor.Pulse(lockObj);
            }
        }
    }
}
