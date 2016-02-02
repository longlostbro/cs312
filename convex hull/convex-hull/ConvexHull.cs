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
        //int pauseTime = 0;
        ConvexHull neighbor;
        int topRight = -1;
        int topLeft = -1;
        int bottomRight = -1;
        int bottomLeft = -1;

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
            neighbor = second;
            //RESET current
            this.current = this.right;
            second.current = second.left;
            bool changedfirst = false;
            bool changedsecond = false;
            bool firstRunRight = true;
            bool firstRunLeft = true;
            while(changedfirst || changedsecond || firstRunLeft || firstRunRight)
            {
                if (firstRunRight || changedsecond)
                {
                    firstRunRight = false;
                    topRight = findtopRight(this.current, second.current);
                    if (second.points[topRight].Equals(second.points[second.current]))
                    {
                        changedfirst = false;
                        changedsecond = false;
                    }
                    else
                    {
                        changedfirst = true;
                        second.current = topRight;
                    }
                }
                if (firstRunLeft || changedfirst)
                {
                    firstRunLeft = false;
                    topLeft = findtopLeft(this.current, second.current);
                    if (points[topLeft].Equals(points[this.current]))
                    {
                        changedsecond = false;
                        changedfirst = false;
                    }
                    else
                    {
                        changedsecond = true;
                        this.current = topLeft;
                    }
                }
            }

            //RESET current
            this.current = this.right;
            second.current = second.left;
            changedfirst = false;
            changedsecond = false;
            firstRunLeft = true;
            firstRunRight = true;
            while (changedfirst || changedsecond || firstRunLeft || firstRunRight)
            {
                if (firstRunRight || changedsecond)
                {
                    firstRunRight = false;
                    bottomRight = findbottomRight(this.current, second.current);
                    if (second.points[bottomRight].Equals(second.points[second.current]))
                    {
                        changedfirst = false;
                        changedsecond = false;
                    }
                    else
                    {
                        changedfirst = true;
                        second.current = bottomRight;
                    }
                }
                if (firstRunLeft || changedsecond)
                {
                    firstRunLeft = false;
                    bottomLeft = findbottomLeft(this.current, second.current);
                    if (this.points[bottomLeft].Equals(this.points[this.current]))
                    {
                        changedsecond = false;
                        changedfirst = false;
                    }
                    else
                    {
                        changedsecond = true;
                        this.current = bottomLeft;
                    }
                }
            }

            //find values
            PointF right = second.points[second.right];
            List<PointF> combined = new List<PointF>();
            for(int i = 0; i <= topLeft; i++)
            {
                combined.Add(this.points[i]);
            }
            for(int i = topRight; i != bottomRight; i = nextInt(second.points.Count,i))
            {
                combined.Add(second.points[i]);
            }
            combined.Add(second.points[bottomRight]);
            for(int i = bottomLeft; i != 0; i = nextInt(this.points.Count,i))
            {
                combined.Add(this.points[i]);
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
            }
        }

        private void drawcurrentTocurrent()
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

        private void drawTopBottom()
        {
            if (topLeft != -1 && topRight != -1)
                ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Green), this.points[topLeft], neighbor.points[topRight]);
            if (bottomLeft != -1 && bottomRight != -1)
                ConvexHullSolver._instance.graphic.DrawLine(new Pen(Brushes.Orange), this.points[bottomLeft], neighbor.points[bottomRight]);
        }

        private int findtopRight(int pivot, int right)
        {
            while(Utils.calculateSlope(this.points[pivot],neighbor.points[nextInt(neighbor.points.Count,right)]) > Utils.calculateSlope(this.points[pivot], neighbor.points[right]))
            {
                right = nextInt(neighbor.points.Count, right);
            }
            return right;
        }
        private int findbottomRight(int pivot, int right)
        {
            while (Utils.calculateSlope(this.points[pivot], neighbor.points[prevInt(neighbor.points.Count,right)]) < Utils.calculateSlope(points[pivot], neighbor.points[right]))
            {
                right = prevInt(neighbor.points.Count,right);
            }
            return right;
        }
        private int findtopLeft(int left, int pivot)
        {
            while (Utils.calculateSlope(neighbor.points[pivot], this.points[prevInt(this.points.Count, left)]) < Utils.calculateSlope(neighbor.points[pivot], points[left]))
            {
                left = prevInt(this.points.Count,left);
            }
            return left;
        }
        private int findbottomLeft(int left, int pivot)
        {
            while (Utils.calculateSlope(neighbor.points[pivot],this.points[nextInt(this.points.Count,left)]) > Utils.calculateSlope(neighbor.points[pivot], points[left]))
            {
                left = nextInt(this.points.Count,left);
            }
            return left;
        }
        private int prevInt(int count, int pos)
        {
            if (pos == 0)
                return count - 1;
            return pos - 1;
        }
        
        private int nextInt(int count, int pos)
        {
            if (pos == count - 1)
                return 0;
            return pos + 1;
        }
    }
}
