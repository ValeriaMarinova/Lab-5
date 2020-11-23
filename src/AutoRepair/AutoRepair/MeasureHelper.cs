using System;
using System.Diagnostics;

namespace AutoRepair
{
    public static class MeasureHelper
    {
        public static readonly Stopwatch _stopwatch = new Stopwatch();

        public static long MeasureMilliseconds(Action action, int times = 5)
        {
            _stopwatch.Start();

            for (int i = 0; i < times; i++)
            {
                action();
            }

            _stopwatch.Stop();

            long averageTime = _stopwatch.ElapsedMilliseconds / times;
            return averageTime;
        }
    }
}
