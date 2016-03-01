using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetworkRouting
{
    public class PathCalculator
    {
        //calculates the shorted path
        public static void shortest(TextBox cost, TextBox arrayTimeBox, TextBox heapTimeBox, TextBox xspeedupBox,int startingIndex, List<PointF> points
            ,List<HashSet<int>> connections, int stopIndex, Graphics graphics, bool compare)
        {
            DijkstraResult heapResult = dijkstra(startingIndex, points, connections, PriorityQueueFactory.QueueType.Heap);
            //Grabbing the results of dijkstra
            List<int> prev = heapResult.getPrev();
            List<double> dist = heapResult.getDist();
            int currentIndex = stopIndex;
            bool isPath = true;
            //iterating through the result to make sure a complete path exists and to draw the path
            while (currentIndex != startingIndex)
            {
                //if the path doesn't exist then there is no reason to continue, and the cost is unreachable
                if (currentIndex != startingIndex && prev[currentIndex] == -1)
                {
                    isPath = false;
                    break;
                }
                PointF previousNode = points[prev[currentIndex]];
                PointF currentNode = points[currentIndex];
                graphics.DrawLine(Pens.Black, currentNode, previousNode);
                double distance = dist[currentIndex] - dist[prev[currentIndex]];
                graphics.DrawString(String.Format("{0}", (int)distance), SystemFonts.DefaultFont, Brushes.Black, midpoint(currentNode,previousNode));
                currentIndex = prev[currentIndex];
            }
            if(isPath)
            {
                //if the user wants to compare the array to the binary heap times we have to do dijkstras with the array
                if (compare)
                {
                    DijkstraResult arrayResult = dijkstra(startingIndex, points, connections, PriorityQueueFactory.QueueType.Array);
                    arrayTimeBox.Text = String.Format("{0}",arrayResult.getTime());
                    xspeedupBox.Text = String.Format("{0}", arrayResult.getTime()/ heapResult.getTime());
                }
                cost.Text = String.Format("{0}",(int)dist.Last());
                heapTimeBox.Text = String.Format("{0}", heapResult.getTime());
            }
            else
            {
                cost.Text = "Unreachable";
            }
        }

        public static PointF midpoint(PointF point1, PointF point2)
        {
            return new PointF((point1.X + point2.X) / 2, (point1.Y + point2.Y) / 2);
        }

        private static DijkstraResult dijkstra(int startingIndex, List<PointF> points, List<HashSet<int>> connections, PriorityQueueFactory.QueueType type)
        {
            //start timer to see how long dijkstra's takes
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<int> prev = new List<int>();
            List<double> dist = new List<double>();
            //O(|v|)
            foreach (PointF point in points)
            {
                //set all initial values to MaxValue for distances since we can't set it to infinity
                dist.Add(Double.MaxValue);
                //set all initial values to -1 for previous since we can't set it to infinity
                prev.Add(-1);
            }
            dist[startingIndex] = 0;
            //create the queue This factory allows for creating a queue, and initializes the queue with the distances.
            //O depends on type of queue
            IPriorityQueue queue = PriorityQueueFactory.makeQueue(type,dist);
            //while the queue is not empty we still have work to do
            //O depends on type of queue
            while (!queue.isEmpty())
            {
                //we need to pop the minimum off of the queue and process it.
                //O depends on type of queue
                int current = queue.deletemin();
                //iterate through connections and store the distances if shorter than previously recorded
                foreach (int index in connections[current])
                {
                    double distance = dist[current] + distanceBetweenPoints(points[current], points[index]);
                    if (dist[index] > distance)
                    {
                        dist[index] = distance;
                        prev[index] = current;
                        //update the queue
                        //O depends on type of queue
                        queue.decreaseKey(index, distance);
                    }
                }
            }
            stopwatch.Stop();
            //return the list of distances and previous, and the time it took
            return new DijkstraResult(prev, dist, stopwatch.Elapsed.TotalSeconds);
        }
        private static double distanceBetweenPoints(PointF point1, PointF point2)
        {
            return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }

        private class DijkstraResult
        {
            private List<double> dist;
            private List<int> prev;
            private double time;

            public DijkstraResult(List<int> prev, List<double> dist, double time)
            {
                this.prev = prev;
                this.dist = dist;
                this.time = time;
            }

            internal List<double> getDist()
            {
                return dist;
            }

            internal List<int> getPrev()
            {
                return prev;
            }

            internal double getTime()
            {
                return time;
            }
        }

    }
}
