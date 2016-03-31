using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    public class BBState
    {
        private List<int> citiesLeft;
        double cost;
        double[][] matrix;
        List<int> path;

        public BBState(double[][] matrix, List<int> path, List<int> citiesLeft, double cost)
        {
            this.path = path;
            this.matrix = matrix;
            this.cost = cost + reduce();
            this.citiesLeft = citiesLeft;
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
                double min = Double.PositiveInfinity;
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

        public double getCost()
        {
            return cost;
        }

        public bool extend()
        {
            if (citiesLeft.Count != 0)
            {
                foreach (int i in citiesLeft)
                {
                    double[][] newMatrix = this.matrix.Select(a => a.ToArray()).ToArray();
                    double initialCost = newMatrix[path.Last()][i] + cost;
                    List<int> newPath = path.Select(a => a).ToList();
                    newPath.Add(i);
                    List<int> newCitiesLeft = citiesLeft.Select(a => a).ToList();
                    newCitiesLeft.Remove(i);
                    BBState state = new BBState(newMatrix, newPath, newCitiesLeft, initialCost);
                    PriorityQueue.getInstance().insert(state);
                }
                return false;
            }
            else
            {
                cost = matrix[path.Last()][0] + cost;
                path.Add(0);
                return true;
            }
        }
    }
}
