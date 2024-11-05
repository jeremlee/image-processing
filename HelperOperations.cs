using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP_Activity
{
    internal class HelperOperations
    {
        public static void swap<T>(ref T a, ref T b)
        {
            (b, a) = (a, b);
        }

        public static int applyContrast(int color, float factor)
        {
            return (int)(Math.Max(Math.Min(1, (color / 255.0f - 0.5f) * factor + 0.5f), 0) * 255);
        }
    }
}
