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
        LinkedListNode<PointF> topLeft;
        LinkedListNode<PointF> topright;
        LinkedListNode<PointF> bottomLeft;
        LinkedListNode<PointF> bottomright;
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

        public LinkedListNode<PointF> TopLeft
        {
            get
            {
                return topLeft;
            }

            set
            {
                topLeft = value;
                clearGraphics();
                drawAll();
                refreshGraphics();
                pause(pauseTime);
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
                clearGraphics();
                drawAll();
                refreshGraphics();
                pause(pauseTime);
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
                clearGraphics();
                drawAll();
                refreshGraphics();
                pause(pauseTime);
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
            neighbor = second;
            this.Current = this.right;
            second.Current = second.left;
            bool changed = true;
            while(changed)
            {
                bool changedRight = true;
                while(changedRight)
                    changedRight = findRight(this, second);
                bool changedLeft = true;
                while(changedLeft)
                    changedLeft =  findLeft(this, second);
                changed = changedRight || changedLeft;
            }

            TopLeft = this.Current;
            Topright = second.Current;

            this.Current = this.right;
            second.Current = second.left;
            changed = true;
            while (changed)
            {
                bool changedRight = true;
                while (changedRight)
                    changedRight = findRight(second, this);
                bool changedLeft = true;
                while (changedLeft)
                    changedLeft = findLeft(second, this);
                changed = changedRight || changedLeft;
            }
            BottomLeft = this.Current;
            Bottomright = second.Current;

            //find values
            List<PointF> combined = new List<PointF>();
            combined.Sort(new PointFComparer());
            LinkedListNode<PointF> temp = bottomLeft;
            while(!temp.Value.Equals(next(topLeft).Value))
            {
                combined.Add(temp.Value);
                temp = next(temp);
            }
            temp = topright;
            while (!temp.Value.Equals(next(bottomright).Value))
            {
                combined.Add(temp.Value);
                temp = next(temp);
            }

            //List<PointF> combined = new List<PointF>();
            //combined.AddRange(points.TakeWhile(x => !x.Equals(topLeft.Value)).Reverse().TakeWhile(x => !x.Equals(bottomLeft.Value)));
            //combined.AddRange(second.points.SkipWhile(x=>!x.Equals(topright)).TakeWhile(x=>!x.Equals(Bottomright)));
            //combined.Add(topLeft.Value);
            //combined.Add(bottomLeft.Value);
            //combined.Add(bottomright.Value);
            //combined.Add(Topright.Value);
            //HashSet<PointF> set = new HashSet<PointF>(combined);
            //return new ConvexHull(new LinkedList<PointF>(combined), second.right.Value);
            return new ConvexHull(combined);
        }

        public bool findRight(ConvexHull counter, ConvexHull clock)
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
            float prevslope = getSlope(clock.Current, prev(counter.Current));
            while (prevslope > currentslope)
            {
                changed = true;
                counter.Current = prev(counter.Current);
                currentslope = prevslope;
                prevslope = getSlope(clock.Current, prev(counter.Current)); ;
            }
            return changed;
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
            if(topLeft != null && topright != null)
                ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Green), topLeft.Value, topright.Value);
            if(bottomLeft != null && bottomright != null)
                ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Orange), bottomLeft.Value, bottomright.Value);
        }

    }
}
