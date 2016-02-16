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

        private void dijkstra()
        {
            List<int> shortestPath;
            int distance = Int32.MaxValue;
            
            


            if(tempDistance < distance)
            {
                shortestPath = tempPoints;
                distance = tempDistance;
            }
        }
    }
}
