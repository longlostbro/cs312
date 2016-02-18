using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NetworkRouting
{
    public class PathCalculator
    {
        
        public static List<int> findShortestByArray(List<PointF> points, List<HashSet<int>> connections)
        {
            return new List<int>();
        
        }


        public static List<int> findShortestByHeap(List<PointF> points, List<HashSet<int>> connections)
        {
            return new List<int>();
        }

        private void dijkstra(int startingIndex, List<PointF> points, List<HashSet<int>> connections)
        {
            List<int> prev = new List<int>();
            List<double> dist = new List<double>();
            foreach(PointF point in points)
            {
                dist.Add(Double.MaxValue);
                prev.Add(-1);
            }
            dist[startingIndex] = 0;
        }
    }
}
