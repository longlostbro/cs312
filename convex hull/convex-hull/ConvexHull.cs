using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace _1_convex_hull
{
    class ConvexHull
    {
        LinkedList<PointF> points;
        LinkedListNode<PointF> current;
        LinkedListNode<PointF> left;
        LinkedListNode<PointF> right;
        public ConvexHull(List<PointF> pointList)
        {
            setCircularlyOrderedPoints(pointList);
            left = points.First;
            right = points.Last;
        }

        private void setCircularlyOrderedPoints(List<PointF> pointList)
        {
            SortedDictionary<int, PointF> sortedPoints= new SortedDictionary<int, PointF>();
            foreach(PointF point in pointList.Skip(1))
            {
                int slope = (int)((point.Y - points.First.Value.Y) / (point.X - points.First.Value.X));
                sortedPoints.Add(slope, point);
            }
            points = new LinkedList<PointF>(sortedPoints.Values);
            sortedPoints.Reverse();
            points.AddFirst(pointList[0]);
        }

        private LinkedListNode<PointF> next(LinkedListNode<PointF> node)
        {
            return node.Next ?? node.List.First;
        }

        private ConvexHull merge(ConvexHull second)
        {
            this.current = this.right;
            second.current = second.left;
            findNextSuccessful();
            return new ConvexHull(new List<PointF>());
        }

        private void findNextSuccessful()
        {
            throw new NotImplementedException();
        }
    }
}
