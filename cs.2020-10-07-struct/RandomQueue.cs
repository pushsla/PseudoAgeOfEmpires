using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace cs._2020_10_07_struct
{
    public static class RandomQueue
    {
        public static readonly int MAX_POOL_SIZE = 100000;
            
        private static Random __rnd_external = new Random();
        private static Random __rnd = new Random(__rnd_external.Next(1000));
        private static Timer __timer = new Timer();
        private static Queue<int> __queue = new Queue<int>();
        private static int __previous;

        public static int CurrentPoolSize => __queue.Count;

        static RandomQueue()
        {
            __timer.Interval = 20;
            __timer.Tick += onTimerTick;
            __timer.Enabled = true;
            __timer.Start();
        }

        public static int Next()
        {
            int result = __rnd_external.Next();
            
            if (__previous != result)
            {
                __previous = result;
                return result;
            }
            else
            {
                try
                {
                    return __queue.Dequeue();
                }
                catch (InvalidOperationException)
                {
                    return __rnd.Next();
                }
            }
        }

        public static int Next(int excluded_top)
        {
            return Next() % excluded_top;
        }

        public static int Next(int included_bottom, int excluded_top)
        {
            return Next() % (excluded_top - included_bottom) + included_bottom;
        }

        private static void onTimerTick(object sende, EventArgs e)
        {
            if (__queue.Count <= MAX_POOL_SIZE)
            {
                __queue.Enqueue(__rnd.Next());
            }
        }
    }
}