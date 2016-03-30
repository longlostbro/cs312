using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    public class BBState
    {
        double cost;
        StateMatrix matrix;

        public BBState(double[][] matrixArray, List<int> path, double cost)
        {
            this.matrix = new StateMatrix(matrixArray, path);
            this.cost = cost + matrix.reduce();
        }

        
    }
    public class StateMatrix
    {
        double[][] matrix;
        List<int> path;
        public StateMatrix(double[][] matrix, List<int> path)
        {
            this.matrix = matrix;
            this.path = path;
        }

        public double reduce()
        {
            double cost = 0;
            for(int i = 0; i < matrix.Length; i++)
            {
                if (!matrix[i].Contains(0))
                {
                    double min = matrix[i].Min();
                    cost += min;
                    for (int j = 0; j < matrix.Length; j++)
                    {
                        matrix[i][j] = matrix[i][j]- min;
                    }
                }
            }
            for (int i = 0; i < matrix.Length; i++)
            {
                double min = 0;
                for (int j = 0; j < matrix.Length; j++)
                {
                    min = min > matrix[j][i] ? matrix[j][i] : min;
                }
                if (min != 0)
                {
                    cost += min;
                    for (int j = 0; j < matrix.Length; j++)
                    {
                        matrix[j][i] = matrix[j][i] - min;

                    }
                }
            }
            return cost;
        }
    }
}
