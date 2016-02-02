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
        int pauseTime = 0;
        ConvexHull neighbor;
        int topRight = -1;
        int topLeft = -1;
        int bottomRight = -1;
        int bottomLeft = -1;

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

        public int TopRight
        {
            get
            {
                return topRight;
            }

            set
            {
                topRight = value;
                clearGraphics();
                drawAll();
                refreshGraphics();
                pause(pauseTime);
            }
        }

        public int TopLeft
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

        public int BottomRight
        {
            get
            {
                return bottomRight;
            }

            set
            {
                bottomRight = value;
                clearGraphics();
                drawAll();
                refreshGraphics();
                pause(pauseTime);
            }
        }

        public int BottomLeft
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

        public ConvexHull(List<PointF> pointList)
        {
            PointF right = pointList.Last();
                        pointList.Sort(new Utils.PointFComparerBySlope(pointList.First()));
            points = new List<PointF>(pointList);

            if (points[0].X > points[1].X)
            {
                MessageBox.Show("Problem with x sort");
            }
            this.right = points.IndexOf(right);
            if (this.right == -1)
            {
                System.Console.WriteLine("error");
            }
        }

        public ConvexHull(List<PointF> pointList, int right)
        {
            if (pointList[0].X > pointList[1].X)
            {
                MessageBox.Show("Problem with x sort");
            }
            points = pointList;
            this.right = right;
            if(this.right == -1)
            {
                System.Console.WriteLine("error");
            }
        }

        public ConvexHull merge(ConvexHull second)
        {
            neighbor = second;
            //RESET CURRENT
            this.Current = this.right;
            second.Current = second.left;
            bool changed = true;
            bool changedfirst = false;
            bool changedsecond = false;
            drawAll();
            refreshGraphics();
            while(changed)
            {
                TopRight = findTopRight(this.current, second.current);
                if (second.points[TopRight].Equals(second.points[second.current]))
                {
                    changedfirst = false;
                }
                else
                {
                    changedfirst = true;
                    second.Current = TopRight;
                }
                drawAll();
                refreshGraphics();
                TopLeft = findTopLeft(this.current, second.current);
                if (points[TopLeft].Equals(points[this.current]))
                {
                    changedsecond = false;
                }
                else
                {
                    changedsecond = true;
                    this.Current = TopLeft;
                }
                drawAll();
                refreshGraphics();
                changed = changedfirst || changedsecond;
            }

            //RESET CURRENT
            this.Current = this.right;
            second.Current = second.left;
            changed = true;
            changedfirst = false;
            changedsecond = false;
            while (changed)
            {
                BottomRight = findBottomRight(this.current, second.current);
                if (second.points[BottomRight].Equals(second.points[second.current]))
                {
                    changedfirst = false;
                }
                else
                {
                    changedfirst = true;
                    second.Current = BottomRight;
                }
                BottomLeft = findBottomLeft(this.current, second.current);
                if (this.points[BottomLeft].Equals(this.points[this.current]))
                {
                    changedsecond = false;
                }
                else
                {
                    changedsecond = true;
                    this.Current = BottomLeft;
                }
                drawAll();
                refreshGraphics();
                changed = changedfirst || changedsecond;
            }

            //find values
            PointF right = second.points[second.right];
            List<PointF> combined = new List<PointF>();
            for(int i = 0; i <= topLeft; i++)
            {
                combined.Add(this.points[i]);
            }
            for(int i = TopRight; i != bottomRight; i = nextInt(second.points,i))
            {
                combined.Add(second.points[i]);
            }
            combined.Add(second.points[BottomRight]);
            for(int i = bottomLeft; i != 0; i = nextInt(this.points,i))
            {
                combined.Add(this.points[i]);
            }

            if (combined[0].X > combined[1].X)
            {
                MessageBox.Show("Problem with x sort");
            }
            return new ConvexHull(combined,combined.IndexOf(right));
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
            if (neighbor != null && neighbor.current != -1 && points != null)
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
            //drawHull();
            //drawNeighbor();
            //drawCurrentToCurrent();
            //drawTopBottom();
        }

        private void drawTopBottom()
        {
            if (topLeft != -1 && topRight != -1)
                ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Green), this.points[topLeft], neighbor.points[topRight]);
            if (bottomLeft != -1 && bottomRight != -1)
                ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Orange), this.points[bottomLeft], neighbor.points[bottomRight]);
        }

        private int findTopRight(int pivot, int right)
        {
            while(Utils.calculateSlope(this.points[pivot],next(neighbor.points,right)) > Utils.calculateSlope(this.points[pivot], neighbor.points[right]))
            {
                right = nextInt(neighbor.points, right);
            }
            return right;
        }
        private int findBottomRight(int pivot, int right)
        {
            while (Utils.calculateSlope(this.points[pivot], prev(neighbor.points,right)) < Utils.calculateSlope(points[pivot], neighbor.points[right]))
            {
                right = prevInt(neighbor.points,right);
            }
            return right;
        }
        private int findTopLeft(int left, int pivot)
        {
            while (Utils.calculateSlope(neighbor.points[pivot], prev(this.points, left)) < Utils.calculateSlope(neighbor.points[pivot], points[left]))
            {
                left = prevInt(this.points,left);
            }
            return left;
        }
        private int findBottomLeft(int left, int pivot)
        {
            while (Utils.calculateSlope(neighbor.points[pivot],next(this.points,left)) > Utils.calculateSlope(neighbor.points[pivot], points[left]))
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
