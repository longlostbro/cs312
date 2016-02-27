using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NetworkRouting
{
    public abstract class IPriorityQueue
    {
        public abstract void decreaseKey(int index, double value);
        public abstract int deletemin();

        public abstract bool isEmpty();
    }

    public class PriorityQueueArray : IPriorityQueue
    {
        List<double> queue;

        public PriorityQueueArray(List<double> dist)
        {
            queue = new List<double>(dist);
        }
        public override void decreaseKey(int index, double value)
        {
            queue[index] = value;
        }

        public override int deletemin()
        {
            double value = queue.Where(x => x != -1).Min();
            int index = queue.IndexOf(value);
            queue[index] = -1;
            return index;
        }

        public override bool isEmpty()
        {
            return queue.Where(x => x != -1).Count() == 0;
        }
    }

    public class PriorityQueueHeap : IPriorityQueue
    {
        private List<double> distances = new List<double>();
        private List<int> pointers = new List<int>();
        private int lastIndex=Int32.MaxValue;

        public PriorityQueueHeap(List<double> distances)
        {
            foreach(double distance in distances)
            {
                insert(distance);
            }
            lastIndex = distances.Count - 1;
        }

        public void insert(double distance)
        {
            distances.Add(distance);
            pointers.Add(pointers.Count);
            if(distance != Int32.MaxValue)
            {
                HeapifyUp(distances.Count - 1);
            }
        }

        public override void decreaseKey(int index, double value)
        {
            distances[pointers.IndexOf(index)] = value;
            HeapifyUp(pointers.IndexOf(index));
        }

        public override int deletemin()
        {
            int node = pointers[0];
            pointers[0] = pointers[lastIndex];
            pointers[lastIndex] = node;

            distances[0] = distances[lastIndex];
            distances[lastIndex] = -1;

            HeapifyDown(0);
            lastIndex--;
            return node;
        }

        public override bool isEmpty()
        {
            return distances.Where(x => x != -1).Count() == 0;
        }
        private void HeapifyUp(int childIdx)
        {
            if (childIdx > 0)
            {
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

        private void HeapifyDown(int parentIdx)
        {
            if (distances[parentIdx] == -1)
                return;
            int leftChildIdx = 2 * parentIdx + 1;
            int rightChildIdx = leftChildIdx + 1;
            int largestChildIdx = parentIdx;
            if (leftChildIdx < pointers.Count && distances[leftChildIdx] != -1 && distances[leftChildIdx] < distances[largestChildIdx])
            {
                largestChildIdx = leftChildIdx;
            }
            if (rightChildIdx < pointers.Count && distances[rightChildIdx] != -1 && distances[rightChildIdx] < distances[largestChildIdx])
            {
                largestChildIdx = rightChildIdx;
            }
            if (largestChildIdx != parentIdx)
            {
                int node = pointers[parentIdx];
                pointers[parentIdx] = pointers[largestChildIdx];
                pointers[largestChildIdx] =  node;

                double nodedist = distances[parentIdx];
                distances[parentIdx] = distances[largestChildIdx];
                distances[largestChildIdx] = nodedist;

                HeapifyDown(largestChildIdx);
            }
        }
    }
    public class PriorityQueueFactory
    {
        public enum QueueType { Array, Heap}
        public static IPriorityQueue makeQueue(QueueType type, List<double> distances)
        {
            switch (type) {
                case QueueType.Array:
                    return new PriorityQueueArray(distances);
                case QueueType.Heap:
                    return new PriorityQueueHeap(distances);
                default:
                    return null;
            }
        }
    }
}
