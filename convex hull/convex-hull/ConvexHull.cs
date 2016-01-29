using _2_convex_hull;
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
        LinkedListNode<PointF> topLeft;
        LinkedListNode<PointF> topright;
        LinkedListNode<PointF> bottomLeft;
        LinkedListNode<PointF> bottomright;
        ConvexHull neighbor;

        public LinkedListNode<PointF> Current
        {
            get
            {
                return current;
            }

            set
            {
                current = value;
                if(neighbor != null && neighbor.current != null)
                {
                    ConvexHullSolver._instance.graphic.Clear(Color.White);
                    LinkedList<PointF> tempPoints = new LinkedList<PointF>(points);
                    tempPoints.AddLast(points.First.Value);
                    ConvexHullSolver._instance.graphic.DrawLines(new Pen(Brushes.Blue), tempPoints.ToArray());

                    LinkedList<PointF> tempPoints2 = new LinkedList<PointF>(neighbor.points);
                    tempPoints2.AddLast(neighbor.points.First.Value);
                    ConvexHullSolver._instance.graphic.DrawLines(new Pen(Brushes.Blue), tempPoints2.ToArray());

                    ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Red), this.current.Value, neighbor.current.Value);
                    ConvexHullSolver._instance.Refresh();
                    ConvexHullSolver._instance.Pause(250);
                }
            }
        }

        public LinkedListNode<PointF> TopLeft
        {
            get
            {
                return topLeft;
            }

            set
            {
                topLeft = value;
                if (neighbor != null && bottomLeft != null && bottomright != null && topLeft != null && topright != null)
                {
                    ConvexHullSolver._instance.graphic.Clear(Color.White);
                    LinkedList<PointF> tempPoints = new LinkedList<PointF>(points);
                    tempPoints.AddLast(points.First.Value);
                    ConvexHullSolver._instance.graphic.DrawLines(new Pen(Brushes.Blue), tempPoints.ToArray());

                    LinkedList<PointF> tempPoints2 = new LinkedList<PointF>(neighbor.points);
                    tempPoints2.AddLast(neighbor.points.First.Value);
                    ConvexHullSolver._instance.graphic.DrawLines(new Pen(Brushes.Blue), tempPoints2.ToArray());

                    ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Green), topLeft.Value, topright.Value);
                    ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Orange), bottomLeft.Value, bottomright.Value);
                    ConvexHullSolver._instance.Refresh();
                    ConvexHullSolver._instance.Pause(250);
                }
            }
        }

        public LinkedListNode<PointF> Topright
        {
            get
            {
                return topright;
            }

            set
            {
                topright = value;
                if (neighbor != null && bottomLeft != null && bottomright != null && topLeft != null && topright != null)
                {
                    ConvexHullSolver._instance.graphic.Clear(Color.White);
                    LinkedList<PointF> tempPoints = new LinkedList<PointF>(points);
                    tempPoints.AddLast(points.First.Value);
                    ConvexHullSolver._instance.graphic.DrawLines(new Pen(Brushes.Blue), tempPoints.ToArray());

                    LinkedList<PointF> tempPoints2 = new LinkedList<PointF>(neighbor.points);
                    tempPoints2.AddLast(neighbor.points.First.Value);
                    ConvexHullSolver._instance.graphic.DrawLines(new Pen(Brushes.Blue), tempPoints2.ToArray());

                    ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Green), topLeft.Value, topright.Value);
                    ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Orange), bottomLeft.Value, bottomright.Value);
                    ConvexHullSolver._instance.Refresh();
                    ConvexHullSolver._instance.Pause(250);
                }
            }
        }

        public LinkedListNode<PointF> BottomLeft
        {
            get
            {
                return bottomLeft;
            }

            set
            {
                bottomLeft = value;
                if (neighbor != null && bottomLeft != null && bottomright != null && topLeft != null && topright != null)
                {
                    ConvexHullSolver._instance.graphic.Clear(Color.White);
                    LinkedList<PointF> tempPoints = new LinkedList<PointF>(points);
                    tempPoints.AddLast(points.First.Value);
                    ConvexHullSolver._instance.graphic.DrawLines(new Pen(Brushes.Blue), tempPoints.ToArray());

                    LinkedList<PointF> tempPoints2 = new LinkedList<PointF>(neighbor.points);
                    tempPoints2.AddLast(neighbor.points.First.Value);
                    ConvexHullSolver._instance.graphic.DrawLines(new Pen(Brushes.Blue), tempPoints2.ToArray());

                    ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Orange), bottomLeft.Value, bottomright.Value);
                    ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Green), topLeft.Value, topright.Value);
                    ConvexHullSolver._instance.Refresh();
                    ConvexHullSolver._instance.Pause(250);
                }
            }
        }

        public LinkedListNode<PointF> Bottomright
        {
            get
            {
                return bottomright;
            }

            set
            {
                bottomright = value;
                if (neighbor != null && bottomLeft != null && bottomright != null && topLeft != null && topright != null)
                {
                    ConvexHullSolver._instance.graphic.Clear(Color.White);
                    LinkedList<PointF> tempPoints = new LinkedList<PointF>(points);
                    tempPoints.AddLast(points.First.Value);
                    ConvexHullSolver._instance.graphic.DrawLines(new Pen(Brushes.Blue), tempPoints.ToArray());

                    LinkedList<PointF> tempPoints2 = new LinkedList<PointF>(neighbor.points);
                    tempPoints2.AddLast(neighbor.points.First.Value);
                    ConvexHullSolver._instance.graphic.DrawLines(new Pen(Brushes.Blue), tempPoints2.ToArray());

                    ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Orange), bottomLeft.Value, bottomright.Value);
                    ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Green), topLeft.Value, topright.Value);
                    ConvexHullSolver._instance.Refresh();
                    ConvexHullSolver._instance.Pause(250);
                }
            }
        }

        public ConvexHull(List<PointF> pointList)
        {
            List<PointF> drawingList = new List<PointF>(pointList);
            drawingList.Add(drawingList.First());
            setCircularlyOrderedPoints(pointList);
        }

        public ConvexHull(LinkedList<PointF> pointList, PointF value)
        {
            points = pointList;
            this.left = pointList.First;
            if (this.left == null)
            {
                points.Find(pointList.Last());
            }
            this.right = points.Find(value);
            if (this.right == null)
            {
                points.Find(pointList.Last());
            }
        }

        public ConvexHull(LinkedListNode<PointF> topLeft)
        {
            this.topLeft = topLeft;
        }

        private void setCircularlyOrderedPoints(List<PointF> pointList)
        {
            SortedDictionary<float, PointF> sortedPoints= new SortedDictionary<float, PointF>();
            foreach(PointF point in pointList.Skip(1))
            {
                float slope = ((point.Y - pointList[0].Y) / (point.X - pointList[0].X));
                sortedPoints.Add(slope, point);
            }
            points = new LinkedList<PointF>(sortedPoints.Values);
            sortedPoints.Reverse();
            points.AddFirst(pointList[0]);
            this.left = points.First;
            if (this.left == null)
            {
                points.Find(pointList.Last());
            }
            this.right = points.Find(pointList.Last());
            if(this.right == null)
            {
                points.Find(pointList.Last());
            }
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
            neighbor = second;
            this.Current = this.right;
            second.Current = second.left;
            bool changed = true;
            while(changed)
            {
                changed = findRight(this, second) || findLeft(this, second);
            }
            TopLeft = this.Current;
            Topright = second.Current;

            this.Current = this.right;
            second.Current = second.left;
            changed = true;
            while (changed)
            {
                changed = findRight(second, this) || findLeft(second, this);
            }
            BottomLeft = this.Current;
            Bottomright = second.Current;

            //find values
            LinkedList<PointF> combinedList = new LinkedList<PointF>();
            LinkedListNode<PointF> temp = this.left;
            while(!temp.Value.Equals(next(TopLeft).Value))
            {
                combinedList.AddLast(temp.Value);
                temp = next(temp);
            }
            temp = Topright;
            while (!temp.Value.Equals(next(Bottomright).Value))
            {
                combinedList.AddLast(temp.Value);
                temp = next(temp);
            }
            temp = BottomLeft;
            while (!temp.Value.Equals(this.left.Value))
            {
                combinedList.AddLast(temp.Value);
                temp = next(temp);
            }
            return new ConvexHull(combinedList, second.right.Value);
        }

        private bool findRight(ConvexHull counter, ConvexHull clock)
        {
            bool changed = false;
            float currentslope = getSlope(counter.Current, clock.Current);
            float nextslope = getSlope(counter.Current, next(clock.Current));
            while(nextslope < currentslope)
            {
                changed = true;
                clock.Current = next(clock.Current);
                currentslope = nextslope;
                nextslope = getSlope(counter.Current, next(clock.Current)); ;
            }
            return changed;
        }
        private bool findLeft(ConvexHull counter, ConvexHull clock)
        {
            bool changed = false;
            float currentslope = getSlope(clock.Current, counter.Current);
            float nextslope = getSlope(clock.Current, prev(counter.Current));
            while (nextslope > currentslope)
            {
                changed = true;
                counter.Current = next(counter.Current);
                currentslope = nextslope;
                nextslope = getSlope(clock.Current, prev(counter.Current)); ;
            }
            return changed;
        }

        private float getSlope(LinkedListNode<PointF> left, LinkedListNode<PointF> right)
        {
            PointF a = left.Value;
            PointF b = right.Value;
            return (b.Y - a.Y) / (b.X - b.Y);
        }
    }
}
