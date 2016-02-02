using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace _1_convex_hull
{
    public class Utils
    {
        
        public class PointFComparerBySlope : IComparer<PointF>
        {
            private PointF pivot;

            public PointFComparerBySlope(PointF pivot)
            {
                this.pivot = pivot;
            }

            public int Compare(PointF first, PointF second)
            {
                if (first.Equals(pivot))
                    return -1;
                else if (second.Equals(pivot))
                    return 1;
                if (calculateSlope(pivot, first) > calculateSlope(pivot, second))
                {
                    return -1;
                }
                return 1;
            }
        }

        public static float calculateSlope(PointF left, PointF right)
        {
            return -(right.Y - left.Y) / (right.X - left.X);
        }
    }
}
