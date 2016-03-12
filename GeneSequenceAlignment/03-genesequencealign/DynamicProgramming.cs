using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeneticsLab
{
    class DynamicProgramming
    {
        public static Dictionary<Tuple<int,int>,DynamicProgramming> problems = 
            new Dictionary<Tuple<int,int>,DynamicProgramming>();
        private int indel_cost = 5;
        private int match_cost = -3;
        private int substitute_cost = 1;
        private string a;
        private string b;
        private bool banded;
        private int[][] cells;
        private direction[][] previous;
        private enum direction{ none, upleft, up, left };
        private const int banded_width = 3;

        public DynamicProgramming(Tuple<int,int> cell, string a, string b, bool banded)
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

            int columnBound = cells[0].Length;
            int rowBound = cells.Length;
            if(banded)
            {
                columnBound = banded_width+1;
                rowBound = banded_width+1;
            }
            //set indel cost and direction
            for (int i = 0; i < rowBound; i++)
            {
                cells[i][0] = i * indel_cost;
                previous[i][0] = direction.up;
            }
            for (int i = 0; i < columnBound; i++)
            {
                cells[0][i] = i * indel_cost;
                previous[0][i] = direction.left;
            }
            previous[0][0] = direction.none;
            fillTable();
            findPath();
            //problems.Add(cell,this);
        }

        string resultA = "";
        string resultB = "";
        //O(m+n) worst case if no matches or substitutes are performed
        private void findPath()
        {
            direction last = direction.left;
            int currentRow = cells.Length - 1;
            int currentColumn = cells[cells.Length - 1].Length - 1;
            resultA = "";
            resultB = "";
            last = previous[currentRow][currentColumn];
            //O(m+n) worst case if no matches or substitutes are performed
            while (last != direction.none)
            {
                switch (last)
                {
                    case direction.left:
                        resultA = "-" + resultA;
                        resultB = b[currentColumn - 1] + resultB;
                        currentColumn--;
                        break;
                    case direction.up:
                        resultA = a[currentRow - 1] + resultA;
                        resultB = "-" + resultB;
                        currentRow--;
                        break;
                    case direction.upleft:
                        resultA = a[currentRow - 1] + resultA;
                        resultB = b[currentColumn - 1] + resultB;
                        currentRow--;
                        currentColumn--;
                        break;
                    default:
                        return;
                }
                last = previous[currentRow][currentColumn];
            }
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
        
        //O(m*n) or O(m+n) if banded  O(m*n) space because it's an m by n table
        public void fillTable()
        {
            //fill in the table
            int currentRow = 1;
            int currentColumn = 1;
            //O(m*n) or O(m+n) if banded
            while (currentRow != cells.Length && currentColumn != cells[cells.Length-1].Length)
            {
                //O(1)
                setMinCost(currentRow, currentColumn);
                //this check makes it O(m+n) if banded because it skips those past the banded length by 
                //incrementing the row
                if(cells[currentRow-1][currentColumn] == Int32.MaxValue || 
                    currentColumn == cells[currentRow].Length-1)
                {
                    currentRow++;
                    if (banded)
                        currentColumn = Math.Max(currentRow - banded_width, 1);
                    else
                        currentColumn=1;
                }
                else
                {
                    currentColumn++;
                }
            }
        }
        //O(1)
        private void setMinCost(int row, int col)
        {
            int up=findDeleteCost(row,col);
            int left=findInsertCost(row,col);
            int upleft=findMatchCost(row,col);
            cells[row][col] = (left < upleft && left < up) ? left : (upleft < up ? upleft : up);
            previous[row][col] = (left < upleft && left < up) ? direction.left : 
                (upleft < up ? direction.upleft : direction.up);
        }
        //O(1)
        private int findDeleteCost(int row, int column)
        {
            if (row > 0 && cells[row-1][column] != Int32.MaxValue)
                return cells[row - 1][column] + indel_cost;
            return Int32.MaxValue;
        }
        //O(1)
        private int findInsertCost(int row, int column)
        {
            if(column > 0 && cells[row][column-1] != Int32.MaxValue)
                return cells[row][column-1]+indel_cost;
            return Int32.MaxValue;
        }
        //O(1)
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
