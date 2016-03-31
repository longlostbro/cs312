using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{

    public class BBState
    {
        public static List<System.Collections.Generic.KeyValuePair<int, int>> order = new List<System.Collections.Generic.KeyValuePair<int, int>>();
        public List<int> citiesLeft;
        public double cost;
        public double[][] matrix;
        public List<int> path;

        public BBState(double[][] matrix, List<int> path, List<int> citiesLeft, double initialCost)
        {
            if (path.Count > 1)
                order.Add(new KeyValuePair<int, int>(path.ElementAt(path.Count - 2), path.ElementAt(path.Count - 1)));
            this.path = path;
            this.matrix = matrix;
            this.cost = initialCost + reduce();
            this.citiesLeft = citiesLeft;
        }

        public double reduce()
        {
            double rowReductionCost = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                if (!matrix[i].Contains(0))
                {
                    double min = matrix[i].Min();
                    if (min != Double.PositiveInfinity)
                    {
                        rowReductionCost += min;
                        for (int j = 0; j < matrix.Length; j++)
                        {
                            matrix[i][j] = matrix[i][j] - min;
                        }
                    }
                }
            }
            double columnReductionCost = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                double min = Double.PositiveInfinity;
                for (int j = 0; j < matrix.Length; j++)
                {
                    min = min > matrix[j][i] ? matrix[j][i] : min;
                }
                if (min != Double.PositiveInfinity && min != 0)
                {
                    columnReductionCost += min;
                    for (int j = 0; j < matrix.Length; j++)
                    {
                        matrix[j][i] = matrix[j][i] - min;

                    }
                }
            }
            return rowReductionCost + columnReductionCost;
        }

        public double getCost()
        {
            return cost;
        }

        public bool extend(SortedSet<BBState> states, double bssf)
        {
            if (citiesLeft.Count() != 0)
            {
                foreach (int i in citiesLeft)
                {
                    double initialCost = matrix[path.Last()][i] + cost;
                    if (initialCost < bssf)
                    {
                        double[][] newMatrix = this.matrix.Select(a => a.ToArray()).ToArray();
                        for (int j = 0; j < matrix.Count(); j++)
                        {
                            newMatrix[path.Last()][j] = Double.PositiveInfinity;
                            newMatrix[j][i] = Double.PositiveInfinity;
                        }
                        List<int> newPath = path.Select(a => a).ToList();
                        newPath.Add(i);
                        List<int> newCitiesLeft = citiesLeft.Select(a => a).ToList();
                        newCitiesLeft.Remove(i);
                        BBState state = new BBState(newMatrix, newPath, newCitiesLeft, initialCost);
                        states.Add(state);
                    }
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
        
        public List<int> getPath()
        {
            return path;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            BBState p = obj as BBState;
            if ((System.Object)p == null)
            {
                return false;
            }

            if(cost == p.cost)
            {
                if(this.path == p.path)
                {
                    if(this.matrix == p.matrix)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
