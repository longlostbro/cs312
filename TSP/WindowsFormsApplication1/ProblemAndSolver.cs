using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace TSP
{

    class ProblemAndSolver
    {

        private class TSPSolution
        {
            /// <summary>
            /// we use the representation [cityB,cityA,cityC] 
            /// to mean that cityB is the first city in the solution, cityA is the second, cityC is the third 
            /// and the edge from cityC to cityB is the final edge in the path.  
            /// You are, of course, free to use a different representation if it would be more convenient or efficient 
            /// for your data structure(s) and search algorithm. 
            /// </summary>
            public ArrayList
                Route;

            /// <summary>
            /// constructor
            /// </summary>
            /// <param name="iroute">a (hopefully) valid tour</param>
            public TSPSolution(ArrayList iroute)
            {
                Route = new ArrayList(iroute);
            }

            /// <summary>
            /// Compute the cost of the current route.  
            /// Note: This does not check that the route is complete.
            /// It assumes that the route passes from the last city back to the first city. 
            /// </summary>
            /// <returns></returns>
            public double costOfRoute()
            {
                // go through each edge in the route and add up the cost. 
                int x;
                City here;
                double cost = 0D;

                for (x = 0; x < Route.Count - 1; x++)
                {
                    here = Route[x] as City;
                    cost += here.costToGetTo(Route[x + 1] as City);
                }

                // go from the last city to the first. 
                here = Route[Route.Count - 1] as City;
                cost += here.costToGetTo(Route[0] as City);
                return cost;
            }
        }

        #region Private members 

        /// <summary>
        /// Default number of cities (unused -- to set defaults, change the values in the GUI form)
        /// </summary>
        // (This is no longer used -- to set default values, edit the form directly.  Open Form1.cs,
        // click on the Problem Size text box, go to the Properties window (lower right corner), 
        // and change the "Text" value.)
        private const int DEFAULT_SIZE = 25;

        /// <summary>
        /// Default time limit (unused -- to set defaults, change the values in the GUI form)
        /// </summary>
        // (This is no longer used -- to set default values, edit the form directly.  Open Form1.cs,
        // click on the Time text box, go to the Properties window (lower right corner), 
        // and change the "Text" value.)
        private const int TIME_LIMIT = 60;        //in seconds

        private const int CITY_ICON_SIZE = 5;


        // For normal and hard modes:
        // hard mode only
        private const double FRACTION_OF_PATHS_TO_REMOVE = 0.20;

        /// <summary>
        /// the cities in the current problem.
        /// </summary>
        private City[] Cities;
        /// <summary>
        /// a route through the current problem, useful as a temporary variable. 
        /// </summary>
        private ArrayList Route;
        /// <summary>
        /// best solution so far. 
        /// </summary>
        private TSPSolution bssf;

        /// <summary>
        /// how to color various things. 
        /// </summary>
        private Brush cityBrushStartStyle;
        private Brush cityBrushStyle;
        private Pen routePenStyle;


        /// <summary>
        /// keep track of the seed value so that the same sequence of problems can be 
        /// regenerated next time the generator is run. 
        /// </summary>
        private int _seed;
        /// <summary>
        /// number of cities to include in a problem. 
        /// </summary>
        private int _size;

        /// <summary>
        /// Difficulty level
        /// </summary>
        private HardMode.Modes _mode;

        /// <summary>
        /// random number generator. 
        /// </summary>
        private Random rnd;

        /// <summary>
        /// time limit in milliseconds for state space search
        /// can be used by any solver method to truncate the search and return the BSSF
        /// </summary>
        private int time_limit;
        #endregion

        #region Public members

        /// <summary>
        /// These three constants are used for convenience/clarity in populating and accessing the results array that is passed back to the calling Form
        /// </summary>
        public const int COST = 0;
        public const int TIME = 1;
        public const int COUNT = 2;

        public int Size
        {
            get { return _size; }
        }

        public int Seed
        {
            get { return _seed; }
        }
        #endregion

        #region Constructors
        public ProblemAndSolver()
        {
            this._seed = 1;
            rnd = new Random(1);
            this._size = DEFAULT_SIZE;
            this.time_limit = TIME_LIMIT * 1000;                  // TIME_LIMIT is in seconds, but timer wants it in milliseconds

            this.resetData();
        }

        public ProblemAndSolver(int seed)
        {
            this._seed = seed;
            rnd = new Random(seed);
            this._size = DEFAULT_SIZE;
            this.time_limit = TIME_LIMIT * 1000;                  // TIME_LIMIT is in seconds, but timer wants it in milliseconds

            this.resetData();
        }

        public ProblemAndSolver(int seed, int size)
        {
            this._seed = seed;
            this._size = size;
            rnd = new Random(seed);
            this.time_limit = TIME_LIMIT * 1000;                        // TIME_LIMIT is in seconds, but timer wants it in milliseconds

            this.resetData();
        }
        public ProblemAndSolver(int seed, int size, int time)
        {
            this._seed = seed;
            this._size = size;
            rnd = new Random(seed);
            this.time_limit = time * 1000;                        // time is entered in the GUI in seconds, but timer wants it in milliseconds

            this.resetData();
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Reset the problem instance.
        /// </summary>
        private void resetData()
        {

            Cities = new City[_size];
            Route = new ArrayList(_size);
            bssf = null;

            if (_mode == HardMode.Modes.Easy)
            {
                for (int i = 0; i < _size; i++)
                    Cities[i] = new City(rnd.NextDouble(), rnd.NextDouble());
            }
            else // Medium and hard
            {
                for (int i = 0; i < _size; i++)
                    Cities[i] = new City(rnd.NextDouble(), rnd.NextDouble(), rnd.NextDouble() * City.MAX_ELEVATION);
            }

            HardMode mm = new HardMode(this._mode, this.rnd, Cities);
            if (_mode == HardMode.Modes.Hard)
            {
                int edgesToRemove = (int)(_size * FRACTION_OF_PATHS_TO_REMOVE);
                mm.removePaths(edgesToRemove);
            }
            City.setModeManager(mm);

            cityBrushStyle = new SolidBrush(Color.Black);
            cityBrushStartStyle = new SolidBrush(Color.Red);
            routePenStyle = new Pen(Color.Blue, 1);
            routePenStyle.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// make a new problem with the given size.
        /// </summary>
        /// <param name="size">number of cities</param>
        public void GenerateProblem(int size, HardMode.Modes mode)
        {
            this._size = size;
            this._mode = mode;
            resetData();
        }

        /// <summary>
        /// make a new problem with the given size, now including timelimit paremeter that was added to form.
        /// </summary>
        /// <param name="size">number of cities</param>
        public void GenerateProblem(int size, HardMode.Modes mode, int timelimit)
        {
            this._size = size;
            this._mode = mode;
            this.time_limit = timelimit * 1000;                                   //convert seconds to milliseconds
            resetData();
        }

        /// <summary>
        /// return a copy of the cities in this problem. 
        /// </summary>
        /// <returns>array of cities</returns>
        public City[] GetCities()
        {
            City[] retCities = new City[Cities.Length];
            Array.Copy(Cities, retCities, Cities.Length);
            return retCities;
        }

        /// <summary>
        /// draw the cities in the problem.  if the bssf member is defined, then
        /// draw that too. 
        /// </summary>
        /// <param name="g">where to draw the stuff</param>
        public void Draw(Graphics g)
        {
            float width = g.VisibleClipBounds.Width - 45F;
            float height = g.VisibleClipBounds.Height - 45F;
            Font labelFont = new Font("Arial", 10);

            // Draw lines
            if (bssf != null)
            {
                // make a list of points. 
                Point[] ps = new Point[bssf.Route.Count];
                int index = 0;
                foreach (City c in bssf.Route)
                {
                    if (index < bssf.Route.Count - 1)
                        g.DrawString(" " + index + "(" + c.costToGetTo(bssf.Route[index + 1] as City) + ")", labelFont, cityBrushStartStyle, new PointF((float)c.X * width + 3F, (float)c.Y * height));
                    else
                        g.DrawString(" " + index + "(" + c.costToGetTo(bssf.Route[0] as City) + ")", labelFont, cityBrushStartStyle, new PointF((float)c.X * width + 3F, (float)c.Y * height));
                    ps[index++] = new Point((int)(c.X * width) + CITY_ICON_SIZE / 2, (int)(c.Y * height) + CITY_ICON_SIZE / 2);
                }

                if (ps.Length > 0)
                {
                    g.DrawLines(routePenStyle, ps);
                    g.FillEllipse(cityBrushStartStyle, (float)Cities[0].X * width - 1, (float)Cities[0].Y * height - 1, CITY_ICON_SIZE + 2, CITY_ICON_SIZE + 2);
                }

                // draw the last line. 
                g.DrawLine(routePenStyle, ps[0], ps[ps.Length - 1]);
            }

            // Draw city dots
            foreach (City c in Cities)
            {
                g.FillEllipse(cityBrushStyle, (float)c.X * width, (float)c.Y * height, CITY_ICON_SIZE, CITY_ICON_SIZE);
            }

        }

        /// <summary>
        ///  return the cost of the best solution so far. 
        /// </summary>
        /// <returns></returns>
        public double costOfBssf()
        {
            if (bssf != null)
                return (bssf.costOfRoute());
            else
                return -1D;
        }

        /// <summary>
        /// This is the entry point for the default solver
        /// which just finds a valid random tour 
        /// </summary>
        /// <returns>results array for GUI that contains three ints: cost of solution, time spent to find solution, number of solutions found during search (not counting initial BSSF estimate)</returns>
        public string[] defaultSolveProblem()
        {
            int i, swap, temp, count = 0;
            string[] results = new string[3];
            int[] perm = new int[Cities.Length];
            Route = new ArrayList();
            Random rnd = new Random();
            Stopwatch timer = new Stopwatch();

            timer.Start();

            do
            {
                for (i = 0; i < perm.Length; i++)                                 // create a random permutation template
                    perm[i] = i;
                for (i = 0; i < perm.Length; i++)
                {
                    swap = i;
                    while (swap == i)
                        swap = rnd.Next(0, Cities.Length);
                    temp = perm[i];
                    perm[i] = perm[swap];
                    perm[swap] = temp;
                }
                Route.Clear();
                for (i = 0; i < Cities.Length; i++)                            // Now build the route using the random permutation 
                {
                    Route.Add(Cities[perm[i]]);
                }
                bssf = new TSPSolution(Route);
                count++;
            } while (costOfBssf() == double.PositiveInfinity);                // until a valid route is found
            timer.Stop();

            results[COST] = costOfBssf().ToString();                          // load results array
            results[TIME] = timer.Elapsed.ToString();
            results[COUNT] = count.ToString();

            return results;
        }

        /// <summary>
        /// performs a Branch and Bound search of the state space of partial tours
        /// stops when time limit expires and uses BSSF as solution
        /// </summary>
        /// <returns>results array for GUI that contains three ints: cost of solution, time spent to find solution, number of solutions found during search (not counting initial BSSF estimate)</returns>
        public string[] bBSolveProblem()
        {
            StreamWriter file = new StreamWriter("report.csv",true);
            file.AutoFlush = true;
            string[] results = new string[3];
            Stopwatch timer = new Stopwatch();
            timer.Start();

            //data structure used as priority queue with IComparer for the priority sort
            BBState.states.Clear();
            double max = 0;
            double secondMax = 0;
            List<double> maxes = new List<double>();

            List<int> citiesLeft = new List<int>();
            //initialization of first matrix O(n^2)
            double[][] matrix = new double[Cities.Length][];
            for (int i = 0; i < Cities.Length; i++)
            {
                citiesLeft.Add(i);
                matrix[i] = new double[Cities.Length];
                for (int j = 0; j < Cities.Length; j++)
                {
                    if (i != j)
                        matrix[i][j] = Cities[i].costToGetTo(Cities[j]);
                    else
                        matrix[i][j] = Double.PositiveInfinity;
                    //calculate the first two max values of each row for use in calculating initial bssf
                    if (matrix[i][j] != Double.PositiveInfinity)
                    {
                        if(max < Cities[i].costToGetTo(Cities[j]))
                        {
                            secondMax = max;
                            max = Cities[i].costToGetTo(Cities[j]);
                        }
                    }

                }
                //calculate the first two max values of each row for use in calculating initial bssf
                maxes.Add(secondMax + max);
            }
            citiesLeft.RemoveAt(0);
            double result = 0;
            //calculate the first two max values of each row for use in calculating initial bssf O(n)
            foreach (int i in maxes)
            {
                result += i;
            }
            //take the average of each set of two maxes and multiply it by two for the bssf, produces results in 3/4-1/2 the time reduction
            result = result / matrix.Length * 2;
            //set the initial bssf
            BBState.bssf = result;
            //Initial state created O(n^2) because it calls reduce
            BBState state = new BBState(matrix, new List<int>() { 0 }, citiesLeft, 0);
            //insertion operation is O(logn)
            BBState.states.Add(state);
            int count = 0;
            //while there are still states in the queue and the time hasn't reached 60 seconds, continue branching
            //insertion operation is O(logn)
            while (BBState.states.Count > 0 && timer.ElapsedMilliseconds < 60000)
            {
                state = BBState.states.Min;
                //deletion operation is O(logn)
                BBState.states.Remove(state);
                //create children states from the remaining cities left to visit, return true if there are no children to extend
                bool done = state.extend();
                BBState.maxStateCount = BBState.maxStateCount < BBState.states.Count ? BBState.states.Count : BBState.maxStateCount;
                if (done)
                {
                    //update the bssf if the state cost is less than the current.
                    if (state.cost < BBState.bssf)
                    {
                        BBState.bssf = state.cost;
                        count++;
                        //remove states from queue if they are more than the bssf O(kn)
                        List<BBState> removable = new List<BBState>();
                        foreach(BBState st in BBState.states)
                        {
                            if (st.cost >= BBState.bssf)
                                removable.Add(st);
                        }
                        foreach(BBState st in removable)
                        {
                            BBState.states.Remove(st);
                            BBState.prunedCount++;
                        }
                    }
                }
            }
            BBState.generatedCount += BBState.states.Count;

            timer.Stop();
            Route = new ArrayList();
            foreach(int index in state.path)
            {
                Route.Add(Cities[index]);
            }
            this.bssf= new TSPSolution(Route);
            results[COST] = String.Format("{0}",BBState.bssf);    // load results into array here, replacing these dummy values
            results[TIME] = String.Format("{0}",timer.ElapsedMilliseconds/1000.00);
            results[COUNT] = String.Format("{0}",count);
            file.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7}", Cities.Length, Seed, timer.ElapsedMilliseconds / 1000.00, bssf, BBState.maxStateCount, count, BBState.generatedCount, BBState.prunedCount));
            file.Close();
            return results;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        // These additional solver methods will be implemented as part of the group project.
        ////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// finds the greedy tour starting from each city and keeps the best (valid) one
        /// </summary>
        /// <returns>results array for GUI that contains three ints: cost of solution, time spent to find solution, number of solutions found during search (not counting initial BSSF estimate)</returns>
        public string[] greedySolveProblem()
        {
            string[] results = new string[3];
            Stopwatch timer = new Stopwatch();
            timer.Start();
            //initialization
            double count = Double.PositiveInfinity;
            map = new Dictionary<City, List<City>>();
            KeyValuePair<List<City>, double> bestPath = new KeyValuePair<List<City>, double>(null, Double.PositiveInfinity);
            //Find greedy path starting from each city O(n^2)
            foreach (City city in Cities)
            {
                List<City> path = new List<City>() { city };
                map.Add(city, path);
                //find shortest for individual city
                double cost = findShortestPath(city, path);
                path.Add(city);
                cost += path[path.Count-2].costToGetTo(city);
                //Calculate Costs of path results check each edge excluding cities visited and current city
                if (cost < bestPath.Value)
                {
                    count++;
                    bestPath = new KeyValuePair<List<City>, double>(path, cost);
                }
            }
            timer.Stop();

            Route = new ArrayList();
            foreach (City city in bestPath.Key)
            {
                Route.Add(city);
            }
            this.bssf = new TSPSolution(Route);
            results[COST] = String.Format("{0}", bestPath.Value); ;    // load results into array here, replacing these dummy values
            results[TIME] = String.Format("{0}",timer.ElapsedMilliseconds/1000.00);
            results[COUNT] = String.Format("{0}",count);

            return results;
        }
        Dictionary<City, List<City>> map;
        //find the edge with the least cost for cities not yet visited O(n)
        private double findShortestPath(City city, List<City> path)
        {
            double bestCost = Double.PositiveInfinity;
            City bestCity = null;
            for (int i = 0; i < Cities.Length; i++)
            {
                if (!path.Contains(Cities[i]))
                {
                    double cost = city.costToGetTo(Cities[i]);
                    if(cost < bestCost)
                    {
                        bestCost = cost;
                        bestCity = Cities[i];
                    }
                }
            }
            path.Add(bestCity);
            if(path.Count < Cities.Length)
                return bestCost + findShortestPath(bestCity, path);
            return bestCost;
        }

        public string[] fancySolveProblem()
        {
            string[] results = new string[3];

            // TODO: Add your implementation for your advanced solver here.

            results[COST] = "not implemented";    // load results into array here, replacing these dummy values
            results[TIME] = "-1";
            results[COUNT] = "-1";

            return results;
        }
        #endregion
    }
    //simple IComparer to order the priority in the SortedSet to work as a priority queue O(1)
    public class BBStateComparer : IComparer<BBState>
    {
        public int Compare(BBState x, BBState y)
        {
            if (x.cost > y.cost)
            {
                return 1;
            }
            else if (x.Equals(y))
            {
                return 0;
            }
            return -1;
        }
    }

}
