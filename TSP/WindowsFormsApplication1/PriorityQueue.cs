using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    public class PriorityQueue
    {
        private static PriorityQueue _instance;
        public static PriorityQueue getInstance()
        {
            if (_instance == null)
                _instance = new PriorityQueue();
            return _instance;
        }
        private List<double> cost = new List<double>();
        private List<int> pointers = new List<int>();
        private int lastIndex = Int32.MaxValue;
        private List<BBState> states = new List<BBState>();

        public PriorityQueue()
        {
            //some sort of initialization...
            lastIndex = -1;
        }

        //initializes the distances with the given list and sets the lastIndex which
        //is used to track which elements have been checked
        //public PriorityQueue(List<double> costs)
        //{
        //    foreach (double distance in costs)
        //    {
        //        insert(distance);
        //    }
        //    lastIndex = costs.Count - 1;
        //}
        //adds element to the bottom of the tree and if its not the max value then bubble up
        //Insert is O(log(|v|)) because Heapify is O(log(|v|)) and add is O(1)
        public void insert(BBState state)
        {
            states.Add(state);
            double cost = state.getCost();
            this.cost.Insert(++lastIndex,cost);
            pointers.Add(pointers.Count);
            if (cost != Int32.MaxValue)
            {
                HeapifyUp(lastIndex);
            }
        }
        //updates the distance at the index from the pointer list and 
        //bubble up to make sure parents are smaller
        //DecreaseKey is O(log(|v|)) because bubbleup is O(log(|v|))
        public void decreaseKey(int index, double value)
        {
            cost[pointers.IndexOf(index)] = value;
            HeapifyUp(pointers.IndexOf(index));
        }
        //swaps the minimum with the lastIndexed item and bubbles down
        //DeleteMin is O(log(|v|)), because of the reorder
        public BBState deletemin()
        {
            int node = pointers[0];
            BBState state = states.ElementAt(node);
            pointers[0] = pointers[lastIndex];
            pointers[lastIndex] = node;
            cost[0] = cost[lastIndex];
            cost[lastIndex] = -1;
            //this make it O(log(|v|))
            HeapifyDown(0);
            lastIndex--;
            return state;
            //return node;
        }
        //checks if the queue is empty, if the initial element is -1 then that means 
        //the queue is empty because all elements have been checked and set to -1
        //O(1)
        public bool isEmpty()
        {
            return cost[0] == -1;
        }
        //bubble up to ensure that parents are smaller than the child
        //BubbleUp is O(log(|v|)) because the index is divided by two each time we traverse
        private void HeapifyUp(int childIdx)
        {
            if (childIdx > 0)
            {
                //this make it O(log(|v|))
                int parentIdx = (childIdx - 1) / 2;
                if (cost[childIdx] < cost[parentIdx])
                {
                    // swap parent and child
                    int node = pointers[parentIdx];
                    pointers[parentIdx] = pointers[childIdx];
                    pointers[childIdx] = node;

                    double nodedist = cost[parentIdx];
                    cost[parentIdx] = cost[childIdx];
                    cost[childIdx] = nodedist;
                    HeapifyUp(parentIdx);
                }
            }
        }
        //bubble down to ensure that children are larger than parent
        //BubbleDown is O(log(|v|)) because we only traverse log(|v|) levels of the tree by multiplying 
        //the index by 2
        private void HeapifyDown(int parentIdx)
        {
            if (cost[parentIdx] == -1)
                return;
            //this make it O(log(|v|))
            int leftChildIdx = 2 * parentIdx + 1;
            int rightChildIdx = leftChildIdx + 1;
            int largestChildIdx = parentIdx;
            if (leftChildIdx < pointers.Count && cost[leftChildIdx] != -1 &&
                cost[leftChildIdx] < cost[largestChildIdx])
            {
                largestChildIdx = leftChildIdx;
            }
            if (rightChildIdx < pointers.Count && cost[rightChildIdx] != -1 &&
                cost[rightChildIdx] < cost[largestChildIdx])
            {
                largestChildIdx = rightChildIdx;
            }
            if (largestChildIdx != parentIdx)
            {
                int node = pointers[parentIdx];
                pointers[parentIdx] = pointers[largestChildIdx];
                pointers[largestChildIdx] = node;
                double nodedist = cost[parentIdx];
                cost[parentIdx] = cost[largestChildIdx];
                cost[largestChildIdx] = nodedist;
                HeapifyDown(largestChildIdx);
            }
        }

        internal void trim(double bssf)
        {
            for (int i = 0; i < pointers.Count; i++)
            {
                if(states.ElementAt(pointers.ElementAt(i)).getCost() > bssf)
                {
                    int node = pointers[i];
                    pointers[i] = pointers[lastIndex];
                    pointers[lastIndex] = node;
                    cost[i] = cost[lastIndex];
                    cost[lastIndex] = -1;
                    //this make it O(log(|v|))
                    HeapifyDown(i);
                    lastIndex--;
                }
            }
        }
    }
}
