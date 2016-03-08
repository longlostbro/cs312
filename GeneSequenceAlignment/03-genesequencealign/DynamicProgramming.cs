using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeneticsLab
{
    class DynamicProgramming
    {
        public static List<DynamicProgramming> problems = new List<DynamicProgramming>();
        private int indel_cost = 5;
        private int match_cost = -3;
        private int substitute_cost = 1;
        private string a;
        private string b;
        private bool banded;
        private int[][] cells;
        private direction[][] previous;
        private enum direction{ none, upleft, up, left };

        public DynamicProgramming(string a, string b, bool banded)
        {
            this.a = a;
            this.b = b;
            this.banded = banded;
            //initialize cells to maxvalue
            cells = new int[a.Length + 1][];
            previous = new direction[a.Length + 1][];
            for (int i = 0; i < a.Length + 1; i++)
            {
                cells[i] = new int[b.Length + 1];
                previous[i] = new direction[b.Length + 1];

                for (int j = 0; j < b.Length + 1; j++)
                {
                    cells[i][j] = Int32.MaxValue;
                }
            }
            //set indel cost and direction
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i][0] = i * indel_cost;
                previous[i][0] = direction.up;
            }
            for (int i = 0; i < cells[0].Length; i++)
            {
                cells[0][i] = i * indel_cost;
                previous[0][i] = direction.left;
            }
            previous[0][0] = direction.none;
            unbanded();
            getPrevious(cells.Length - 1, cells[cells.Length-1].Length - 1);
            problems.Add(this);
        }

        public string getStringA()
        {
            return a;
        }
        public string getStringB()
        {
            return b;
        }
        public int getScore()
        {
            return cells[cells.Length - 1][cells[cells.Length - 1].Length - 1];
        }

        public string getResultA()
        {
            return resultA;
        }
        public string getResultB()
        {
            return resultB;
        }
        string resultA = "";
        string resultB = "";
        private void getPrevious(int i, int j)
        {
            switch(previous[i][j])
            {
                case direction.left:
                    getPrevious(i, j - 1);
                    resultA += "-";
                    resultB += b[j - 1];
                    break;
                case direction.up:
                    getPrevious(i-1, j);
                    resultA += a[i - 1];
                    resultB += "-";
                    break;
                case direction.upleft:
                    getPrevious(i-1, j - 1);
                    resultA += a[i - 1];
                    resultB += b[j - 1];
                    break;
                default:
                    break;
            }
        }

        public void unbanded()
        {
            //fill in the table
            for (int i = 1; i < cells.Length; i++)
            {
                for (int j = 1; j < cells[i].Length; j++)
                {
                    setMinCost(i, j);
                }
            }
        }

        private void setMinCost(int row, int col)
        {
            int up=findDeleteCost(row,col);
            int left=findInsertCost(row,col);
            int upleft=findMatchCost(row,col);
            cells[row][col] = (left < upleft && left < up) ? left : (upleft < up ? upleft : left);
            previous[row][col] = (left < upleft && left < up) ? direction.left : (upleft < up ? direction.upleft : direction.up);
        }

        private int findDeleteCost(int row, int column)
        {
            if (row > 0 && cells[row-1][column] != Int32.MaxValue)
                return cells[row - 1][column] + indel_cost;
            return Int32.MaxValue;
        }
        private int findInsertCost(int row, int column)
        {
            if(column > 0 && cells[row][column-1] != Int32.MaxValue)
                return cells[row][column-1]+indel_cost;
            return Int32.MaxValue;
        }
        private int findMatchCost(int row, int column)
        {
            if(row > 0 && column > 0)
            {
                if (a[row-1] == b[column-1])
                    return cells[row - 1][column - 1] + match_cost;
                else
                    return cells[row - 1][column - 1] + substitute_cost;
            }
            return Int32.MaxValue;
        }

        internal int[][] getCells()
        {
            return cells;
        }
    }
}
