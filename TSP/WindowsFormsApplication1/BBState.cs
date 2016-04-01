using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    //this class is used to represent a branch and bound tsp state
    public class BBState
    {
        public List<int> citiesLeft;
        public double cost;
        public double[][] matrix;
        public List<int> path;
        public static int prunedCount=0;
        public static int generatedCount=0;
        public static int maxStateCount = 0;

        public BBState(double[][] matrix, List<int> path, List<int> citiesLeft, double initialCost)
        {
            this.path = path;
            this.matrix = matrix;
            //set cost to the initial cost plus the reduction costs for both row and column reduction
            this.cost = initialCost + reduce();
            this.citiesLeft = citiesLeft;
        }
        //iterates through the rows and columns to reduce and returns the reduction cost
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

        //This method generates children states for the remaining cities left and adds each to the queue if its cost is < bssf
        public bool extend(SortedSet<BBState> states, double bssf)
        {
            if (citiesLeft.Count() != 0)
            {
                foreach (int i in citiesLeft)
                {
                    generatedCount++;
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
                        if(state.cost > bssf)
                        {
                            prunedCount++;
                        }
                        else
                        {
                            states.Add(state);
                        }
                    }
                    else
                    {
                        prunedCount++;
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
        //simple equals operator
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
