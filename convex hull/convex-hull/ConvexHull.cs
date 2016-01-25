using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;

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
        private LinkedListNode<PointF> prev(LinkedListNode<PointF> node)
        {
            return node.Previous ?? node.List.Last;
        }

        public ConvexHull merge(ConvexHull second)
        {
            this.current = this.right;
            second.current = second.left;
            bool check1 = false;
            bool check2 = false;
            while (check1 || check2) //while atleast 1 of them has changed
            {
                check1 = findNextSuccessful(this, second);
                check2 = findNextSuccessful(second, this);
            }
            return new ConvexHull(new List<PointF>());
        }

        private bool findNextSuccessful(ConvexHull pivot, ConvexHull temp)
        {
            LinkedListNode<PointF> initial = temp.current;
            while (isValidHullPoint(pivot.current,temp.current))
            {
                temp.current = next(temp.current);
            }
            temp.current = prev(temp.current);
            return initial != temp.current;
        }

        private bool isValidHullPoint(LinkedListNode<PointF> pivot, LinkedListNode<PointF> temp)
        {
            Vector pivotVector = new Vector(pivot.Value.X, pivot.Value.Y);
            Vector tempVector = new Vector(temp.Value.X, temp.Value.Y);
            double result = Vector.CrossProduct(pivotVector, tempVector);
            return result > 0;
        }
    }
}
