using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class PriorityQueueHeap
    {
        private List<double> distances = new List<double>();
        private List<int> pointers = new List<int>();
        private int lastIndex = Int32.MaxValue;
        //initializes the distances with the given list and sets the lastIndex which
        //is used to track which elements have been checked
        public PriorityQueueHeap(List<double> distances)
        {
            foreach (double distance in distances)
            {
                insert(distance);
            }
            lastIndex = distances.Count - 1;
        }
        //adds element to the bottom of the tree and if its not the max value then bubble up
        //Insert is O(log(|v|)) because Heapify is O(log(|v|)) and add is O(1)
        public void insert(double distance)
        {
            distances.Add(distance);
            pointers.Add(pointers.Count);
            if (distance != Int32.MaxValue)
            {
                HeapifyUp(distances.Count - 1);
            }
        }
        //updates the distance at the index from the pointer list and 
        //bubble up to make sure parents are smaller
        //DecreaseKey is O(log(|v|)) because bubbleup is O(log(|v|))
        public void decreaseKey(int index, double value)
        {
            distances[pointers.IndexOf(index)] = value;
            HeapifyUp(pointers.IndexOf(index));
        }
        //swaps the minimum with the lastIndexed item and bubbles down
        //DeleteMin is O(log(|v|)), because of the reorder
        public int deletemin()
        {
            int node = pointers[0];
            pointers[0] = pointers[lastIndex];
            pointers[lastIndex] = node;
            distances[0] = distances[lastIndex];
            distances[lastIndex] = -1;
            //this make it O(log(|v|))
            HeapifyDown(0);
            lastIndex--;
            return node;
        }
        //checks if the queue is empty, if the initial element is -1 then that means 
        //the queue is empty because all elements have been checked and set to -1
        //O(1)
        public bool isEmpty()
        {
            return distances[0] == -1;
        }
        //bubble up to ensure that parents are smaller than the child
        //BubbleUp is O(log(|v|)) because the index is divided by two each time we traverse
        private void HeapifyUp(int childIdx)
        {
            if (childIdx > 0)
            {
                //this make it O(log(|v|))
                int parentIdx = (childIdx - 1) / 2;
                if (distances[childIdx] < distances[parentIdx])
                {
                    // swap parent and child
                    int node = pointers[parentIdx];
                    pointers[parentIdx] = pointers[childIdx];
                    pointers[childIdx] = node;

                    double nodedist = distances[parentIdx];
                    distances[parentIdx] = distances[childIdx];
                    distances[childIdx] = nodedist;
                    HeapifyUp(parentIdx);
                }
            }
        }
        //bubble down to ensure that children are larger than parent
        //BubbleDown is O(log(|v|)) because we only traverse log(|v|) levels of the tree by multiplying 
        //the index by 2
        private void HeapifyDown(int parentIdx)
        {
            if (distances[parentIdx] == -1)
                return;
            //this make it O(log(|v|))
            int leftChildIdx = 2 * parentIdx + 1;
            int rightChildIdx = leftChildIdx + 1;
            int largestChildIdx = parentIdx;
            if (leftChildIdx < pointers.Count && distances[leftChildIdx] != -1 &&
                distances[leftChildIdx] < distances[largestChildIdx])
            {
                largestChildIdx = leftChildIdx;
            }
            if (rightChildIdx < pointers.Count && distances[rightChildIdx] != -1 &&
                distances[rightChildIdx] < distances[largestChildIdx])
            {
                largestChildIdx = rightChildIdx;
            }
            if (largestChildIdx != parentIdx)
            {
                int node = pointers[parentIdx];
                pointers[parentIdx] = pointers[largestChildIdx];
                pointers[largestChildIdx] = node;
                double nodedist = distances[parentIdx];
                distances[parentIdx] = distances[largestChildIdx];
                distances[largestChildIdx] = nodedist;
                HeapifyDown(largestChildIdx);
            }
        }
    }
}
