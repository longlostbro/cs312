using _2_convex_hull;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _1_convex_hull
{
    class ConvexHull
    {
        LinkedList<PointF> points;
        LinkedListNode<PointF> current;
        LinkedListNode<PointF> left;
        LinkedListNode<PointF> right;
        int pauseTime = 500;
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
                clearGraphics();
                drawAll();
                refreshGraphics();
                pause(pauseTime);
            }
        }

        public ConvexHull(List<PointF> pointList)
        {
            if(pointList.Count > 2)
                orderPointsClockwise(pointList);
            else if(pointList.Count == 0)
            {
                MessageBox.Show("EMPTY HULL");
            }
            else
            {
                points = new LinkedList<PointF>(pointList);
                this.left = points.First;
                this.right = points.Last;
            }
        }

        private void setPoints(LinkedList<PointF> points)
        {
            this.points = points;
        }

        public ConvexHull(LinkedList<PointF> pointList, PointF value)
        {
            setPoints(pointList);
            setLeft(pointList.First);
            setRight(value);
        }

        private void setRight(PointF point)
        {
            this.right = points.Find(point);
            if(this.right == null)
            {
                MessageBox.Show("Can't find right point of hull");
            }
        }

        private void setLeft(LinkedListNode<PointF> point)
        {
            this.left = point;
            if (this.left == null)
            {
                MessageBox.Show("Left point of hull is null");
            }
        }

        private void orderPointsClockwise(List<PointF> pointList)
        {
            SortedDictionary<float, PointF> sortedPoints= new SortedDictionary<float, PointF>();
            foreach(PointF point in pointList.Skip(1))
            {
                float slope = ((point.Y - pointList[0].Y) / (point.X - pointList[0].X));
                sortedPoints.Add(slope, point);
            }
            sortedPoints.Reverse();
            points = new LinkedList<PointF>(sortedPoints.Values);
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
            LinkedListNode<PointF> topRight = null;
            LinkedListNode<PointF> topLeft = null;
            LinkedListNode<PointF> bottomRight = null;
            LinkedListNode<PointF> bottomLeft = null;
            neighbor = second;
            //RESET CURRENT
            this.Current = this.right;
            second.Current = second.left;
            bool changed = true;
            while(changed)
            {
                topRight = findTopRight(this.current, second.current);
                if (topRight.Value.Equals(second.current.Value))
                {
                    changed = false;
                }
                else
                {
                    second.points.Remove(this.current.Value);
                    second.Current = topRight;
                }
                topLeft = findTopLeft(this.current, second.current);
                if (topRight.Value.Equals(this.current.Value))
                {
                    changed = false;
                }
                else
                {
                    this.points.Remove(this.current.Value);
                    this.Current = topLeft;
                }
            }

            //RESET CURRENT
            this.Current = this.right;
            second.Current = second.left;
            changed = true;
            while (changed)
            {
                bottomRight = findBottomRight(this.current, second.current);
                if (bottomRight.Value.Equals(second.current.Value))
                {
                    changed = false;
                }
                else
                {
                    second.points.Remove(second.current.Value);
                    second.Current = bottomRight;
                }
                bottomLeft = findBottomLeft(this.current, second.current);
                if (bottomLeft.Value.Equals(this.current.Value))
                {
                    changed = false;
                }
                else
                {
                    this.points.Remove(this.current.Value);
                    this.Current = bottomLeft;
                }
            }

            //find values
            for (int i = 0; i < second.points.Count; i++)
            {
                this.points.AddAfter(this.current, next(second.current).Value);
            }
            return new ConvexHull(this.points.ToList());
        }

        private float getSlope(LinkedListNode<PointF> left, LinkedListNode<PointF> right)
        {
            PointF a = left.Value;
            PointF b = right.Value;
            return (b.Y - a.Y) / (b.X - b.Y);
        }


        private void clearGraphics()
        {
            ConvexHullSolver._instance.graphic.Clear(Color.White);
        }

        private void refreshGraphics()
        {
            ConvexHullSolver._instance.Refresh();
        }

        private void pause(int milliseconds)
        {
            ConvexHullSolver._instance.Pause(milliseconds);
        }

        public void drawHull()
        {
            if (points != null && points.Count > 0)
            {
                LinkedList<PointF> tempPoints = new LinkedList<PointF>(points);
                tempPoints.AddLast(points.First.Value);
                ConvexHullSolver._instance.graphic.DrawLines(new Pen(Brushes.Blue), tempPoints.ToArray());
                labelPoints();
            }
        }

        private void drawCurrentToCurrent()
        {
            if (neighbor != null && neighbor.current != null)
            {
                ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Red), this.current.Value, neighbor.current.Value);
            }
        }

        public void drawNeighbor()
        {
            if (neighbor != null && neighbor.points != null)
            {
                LinkedList<PointF> tempPoints2 = new LinkedList<PointF>(neighbor.points);
                tempPoints2.AddLast(neighbor.points.First.Value);
                ConvexHullSolver._instance.graphic.DrawLines(new Pen(Brushes.Blue), tempPoints2.ToArray());
            }
        }

        public void labelPoints()
        {
            int i = 0;
            if(points != null)
            foreach(PointF point in points)
            {
                ConvexHullSolver._instance.graphic.DrawString(String.Format("{0}", i++),new Font("Tahoma",8),Brushes.Black, point);
            }
            i = 0;
            if(neighbor != null && neighbor.points != null)
            foreach (PointF point in neighbor.points)
            {
                ConvexHullSolver._instance.graphic.DrawString(String.Format("{0}", i++), new Font("Tahoma", 8), Brushes.Black, point);
            }
        }

        public void drawAll()
        {
            drawHull();
            drawNeighbor();
            drawCurrentToCurrent();
            drawTopBottom();
        }

        private void drawTopBottom()
        {
            //if(topLeft != null && topright != null)
            //    ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Green), topLeft.Value, topright.Value);
            //if(bottomLeft != null && bottomright != null)
            //    ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Orange), bottomLeft.Value, bottomright.Value);
        }

        private LinkedListNode<PointF> findTopRight(LinkedListNode<PointF> left, LinkedListNode<PointF> right)
        {
            LinkedListNode<PointF> temp = right;
            while(getSlope(left,next(temp)) > getSlope(left, temp))
            {
                temp = next(temp);
            }
            return temp;
        }
        private LinkedListNode<PointF> findBottomRight(LinkedListNode<PointF> left, LinkedListNode<PointF> right)
        {
            LinkedListNode<PointF> temp = right;
            while (getSlope(left, prev(temp)) < getSlope(left, temp))
            {
                temp = prev(temp);
            }
            return temp;
        }
        private LinkedListNode<PointF> findTopLeft(LinkedListNode<PointF> left, LinkedListNode<PointF> right)
        {
            LinkedListNode<PointF> temp = left;
            while (getSlope(prev(temp), right) < getSlope(temp, right))
            {
                temp = prev(temp);
            }
            return temp;
        }
        private LinkedListNode<PointF> findBottomLeft(LinkedListNode<PointF> left, LinkedListNode<PointF> right)
        {
            LinkedListNode<PointF> temp = left;
            while (getSlope(next(temp), right) > getSlope(temp, right))
            {
                temp = next(temp);
            }
            return temp;
        }

    }
}
