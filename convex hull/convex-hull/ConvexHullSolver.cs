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
            List<PointF> sortedPoints = pointList.OrderBy(p => p.X).ToList(); //O(nlogn)
            ConvexHull hull = generateHull(sortedPoints);
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
                //splitting O(logn)
                List<PointF> firstArray = new List<PointF>(sortedPoints.Take(sortedPoints.Count / 2));
                List<PointF> secondArray = new List<PointF>(sortedPoints.Skip(sortedPoints.Count / 2));
                ConvexHull first = generateHull(firstArray);
                ConvexHull second = generateHull(secondArray);
                return first.merge(second);
            }
        }
    }
}

