using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using _1_convex_hull;
using System.Linq;

namespace _2_convex_hull
{
    class ConvexHullSolver
    {
        public System.Drawing.Graphics graphic;
        private System.Windows.Forms.PictureBox pictureBoxView;
        public static ConvexHullSolver _instance;

        public ConvexHullSolver(System.Drawing.Graphics g, System.Windows.Forms.PictureBox pictureBoxView)
        {
            _instance = this;
            this.graphic = g;
            this.pictureBoxView = pictureBoxView;
        }

        public void Refresh()
        {
            // Use this especially for debugging and whenever you want to see what you have drawn so far
            pictureBoxView.Refresh();
        }

        public void Pause(int milliseconds)
        {
            // Use this especially for debugging and to animate your algorithm slowly
            pictureBoxView.Refresh();
            System.Threading.Thread.Sleep(milliseconds);
        }

        public void Solve(List<System.Drawing.PointF> pointList)
        {
            pointList.Sort(new PointFComparer());
            List<PointF> sortedPoints = new List<PointF>(pointList);
            ConvexHull hull = generateHull(sortedPoints);
            graphic.Clear(Color.White);
            hull.drawHull();
        }

        private ConvexHull generateHull(List<PointF> sortedPoints)
        {
            if(sortedPoints.Count <= 3)
            {
                return new ConvexHull(sortedPoints);
            }
            else
            {
                List<PointF> firstArray = new List<PointF>(sortedPoints.Take(sortedPoints.Count / 2));
                List<PointF> secondArray = new List<PointF>(sortedPoints.Skip(sortedPoints.Count / 2));
                ConvexHull first = generateHull(firstArray);
                ConvexHull second = generateHull(secondArray);
                return first.merge(second);
            }

        }

        public string ListToString(List<PointF> points)
        {
            StringBuilder str = new StringBuilder();
            points.ForEach(o => str.Append(o + ","));
            return str.ToString().Substring(0, str.Length - 2);
        }

    }


    public class PointFComparer : IComparer<PointF>
    {

        public int Compare(PointF first, PointF second)
        {
            return (int)(first.X - second.X);
        }
    }
}
//public int Compare(PointF first, PointF second)
//{
//    if (first.X == second.X)
//    {
//        return (int)(first.Y - second.Y);
//    }
//    else
//    {
//        return (int)(first.X - second.X);
//    }

//}
