using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetworkRouting
{
    public class PathCalculator
    {

        public static void findShortestByArray(TextBox cost, TextBox time,int startingIndex, List<PointF> points, List<HashSet<int>> connections, int stopIndex, Graphics graphics)
        {
            DijkstraResult result = dijkstra(startingIndex, points, connections, PriorityQueueFactory.QueueType.Array);
            List<int> prev = result.getPrev();
            List<double> dist = result.getDist();
            int currentIndex = stopIndex;
            bool isPath = true;
            while(currentIndex != startingIndex)
            {
                if (currentIndex != startingIndex && prev[currentIndex] == -1)
                {
                    isPath = false;
                    break;
                }
                PointF previousNode = points[prev[currentIndex]];
                PointF currentNode = points[currentIndex];
                graphics.DrawLine(Pens.Black, currentNode, previousNode);
                double distance = dist[currentIndex] - dist[prev[currentIndex]];//distanceBetweenPoints(currentNode, previousNode);
                graphics.DrawString(String.Format("{0}", (int)distance), SystemFonts.DefaultFont, Brushes.Black, midpoint(currentNode,previousNode));
                currentIndex = prev[currentIndex];
            }
            if(isPath)
            {
                cost.Text = String.Format("{0}",(int)dist.Last());
            }
            else
            {
                cost.Text = "No path";
            }
        }

        public static PointF midpoint(PointF point1, PointF point2)
        {
            return new PointF((point1.X + point2.X) / 2, (point1.Y + point2.Y) / 2);
        }


        public static List<int> findShortestByHeap(List<PointF> points, List<HashSet<int>> connections)
        {
            return new List<int>();
        }

        private static DijkstraResult dijkstra(int startingIndex, List<PointF> points, List<HashSet<int>> connections, PriorityQueueFactory.QueueType type)
        {
            List<int> prev = new List<int>();
            List<double> dist = new List<double>();
            foreach (PointF point in points)
            {
                dist.Add(Double.MaxValue);
                prev.Add(-1);
            }
            dist[startingIndex] = 0;
            IPriorityQueue queue = PriorityQueueFactory.makeQueue(type,dist);
            while (!queue.isEmpty())
            {
                int current = queue.deletemin();
                foreach (int index in connections[current])
                {
                    double distance = dist[current] + distanceBetweenPoints(points[current], points[index]);
                    if (dist[index] > distance)
                    {
                        dist[index] = distance;
                        prev[index] = current;
                        queue.decreaseKey(index, distance);
                    }
                }
            }
            return new DijkstraResult(prev, dist);
        }
        private static double distanceBetweenPoints(PointF point1, PointF point2)
        {
            return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }

        private class DijkstraResult
        {
            private List<double> dist;
            private List<int> prev;

            public DijkstraResult(List<int> prev, List<double> dist)
            {
                this.prev = prev;
                this.dist = dist;
            }

            internal List<double> getDist()
            {
                return dist;
            }

            internal List<int> getPrev()
            {
                return prev;
            }
        }

    }
}
