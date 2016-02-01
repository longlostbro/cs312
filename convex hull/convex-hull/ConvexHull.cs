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
        List<PointF> points;
        int current = -1;
        int left = 0;
        int right = -1;
        int pauseTime = 500;
        ConvexHull neighbor;

        public int Current
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
            PointF right = pointList.Last();
            pointList.Sort(new Utils.PointFComparerBySlope(pointList.First()));
            points = new List<PointF>(pointList);
            this.right = points.IndexOf(right);
        }

        public ConvexHull(List<PointF> pointList, int right)
        {
            points = pointList;
            this.right = right;
        }

        public ConvexHull merge(ConvexHull second)
        {
            int topRight = -1;
            int topLeft = -1;
            int bottomRight = -1;
            int bottomLeft = -1;
            neighbor = second;
            //RESET CURRENT
            this.Current = this.right;
            second.Current = second.left;
            bool changed = true;
            drawAll();
            refreshGraphics();
            while(changed)
            {
                topRight = findTopRight(this.current, second.current);
                if (second.points[topRight].Equals(second.points[second.current]))
                {
                    changed = false;
                }
                else
                {
                    if (second.right > second.current)
                    {
                        second.right--;
                    }
                    if (topRight > this.current)
                    {
                        topRight--;
                    }
                    second.points.Remove(second.points[second.current]);//TODO
                    second.Current = topRight;
                }
                drawAll();
                refreshGraphics();
                topLeft = findTopLeft(this.current, second.current);
                if (points[topLeft].Equals(points[this.current]))
                {
                    changed = false;
                }
                else
                {
                    changed = true;
                    if (this.right > this.current)
                    {
                        this.right--;
                    }
                    if (topLeft> this.current)
                    {
                        topLeft--;
                    }
                    this.points.Remove(points[this.current]);//TODO
                    this.Current = topLeft;
                }
                drawAll();
                refreshGraphics();
            }

            //RESET CURRENT
            this.Current = this.right;
            second.Current = second.left;
            changed = true;
            while (changed)
            {
                bottomRight = findBottomRight(this.current, second.current);
                if (second.points[bottomRight].Equals(second.points[second.current]))
                {
                    changed = false;
                }
                else
                {
                    if (second.right > second.current)
                    {
                        second.right--;
                    }
                    if (bottomRight > second.current)
                    {
                        bottomRight--;
                    }
                    second.points.Remove(second.points[second.current]);//TODO
                    second.Current = bottomRight;
                }
                bottomLeft = findBottomLeft(this.current, second.current);
                if (this.points[bottomLeft].Equals(this.points[this.current]))
                {
                    changed = false;
                }
                else
                {
                    if (this.right > this.current)
                    {
                        this.right--;
                    }
                    if (bottomLeft > second.current)
                    {
                        bottomLeft--;
                    }
                    this.points.Remove(points[this.current]);//TODO
                    this.Current = bottomLeft;
                }
            }

            //find values
            PointF right = second.points[second.right];
            for (int i = 0; i < second.points.Count; i++)
            {
                this.points.Insert(topLeft + 1, second.points[i]);
                    //  .AddAfter(this.current, next(second.current).Value);
            }
            return new ConvexHull(this.points,this.points.IndexOf(right));
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
                List<PointF> tempPoints = new List<PointF>(points);
                tempPoints.Add(points.First());
                ConvexHullSolver._instance.graphic.DrawLines(new Pen(Brushes.Blue), tempPoints.ToArray());
                labelPoints();
            }
        }

        private void drawCurrentToCurrent()
        {
            if (neighbor != null && neighbor.current != -1)
            {
                ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Red), points[this.current], neighbor.points[neighbor.current]);
            }
        }

        public void drawNeighbor()
        {
            if (neighbor != null && neighbor.points != null)
            {
                List<PointF> tempPoints2 = new List<PointF>(neighbor.points);
                tempPoints2.Add(neighbor.points.First());
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

        private int findTopRight(int left, int right)
        {
            while(Utils.calculateSlope(this.points[left],next(neighbor.points,right)) > Utils.calculateSlope(this.points[left], neighbor.points[right]))
            {
                right = nextInt(neighbor.points, right);
            }
            return right;
        }
        private int findBottomRight(int left, int right)
        {
            while (Utils.calculateSlope(this.points[left], prev(neighbor.points,right)) < Utils.calculateSlope(points[left], neighbor.points[right]))
            {
                right = prevInt(neighbor.points,right);
            }
            return right;
        }
        private int findTopLeft(int left, int right)
        {
            while (Utils.calculateSlope(prev(this.points,left), neighbor.points[right]) < Utils.calculateSlope(points[left], neighbor.points[right]))
            {
                left = prevInt(this.points,left);
            }
            return left;
        }
        private int findBottomLeft(int left, int right)
        {
            while (Utils.calculateSlope(next(this.points,left), neighbor.points[right]) > Utils.calculateSlope(points[left], neighbor.points[right]))
            {
                left = nextInt(this.points,left);
            }
            return left;
        }

        private PointF prev(List<PointF> points, int pos)
        {
            if (pos == 0)
                return points[points.Count-1];
            return points[pos-1];
        }
        private int prevInt(List<PointF> points, int pos)
        {
            if (pos == 0)
                return points.Count - 1;
            return pos - 1;
        }

        private PointF next(List<PointF> points, int pos)
        {
            if (pos == points.Count - 1)
                return points[0];
            return points[pos+1];
        }
        private int nextInt(List<PointF> points, int pos)
        {
            if (pos == points.Count - 1)
                return 0;
            return pos + 1;
        }
    }
}
