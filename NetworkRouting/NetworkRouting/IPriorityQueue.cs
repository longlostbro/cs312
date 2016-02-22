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
        private List<double> distances;
        private List<int> queue;

        public PriorityQueueHeap(List<double> distances)
        {
            this.distances = distances;
        }

        public override void decreaseKey(int index, double value)
        {
            throw new NotImplementedException();
        }

        public override int deletemin()
        {
            int node = queue[0];
            queue[0] = queue[queue.Count];
            HeapifyDown(0);
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
                if (queue[childIdx] < queue[parentIdx])
                {
                    // swap parent and child
                    int node = queue[parentIdx];
                    queue[parentIdx] = queue[childIdx];
                    queue[childIdx] = node;
                    HeapifyUp(parentIdx);
                }
            }
        }

        private void HeapifyDown(int parentIdx)
        {
            int leftChildIdx = 2 * parentIdx + 1;
            int rightChildIdx = leftChildIdx + 1;
            int largestChildIdx = parentIdx;
            if (leftChildIdx < queue.Count && queue[leftChildIdx] < queue[largestChildIdx])
            {
                largestChildIdx = leftChildIdx;
            }
            if (rightChildIdx < queue.Count && queue[rightChildIdx]< queue[largestChildIdx])
            {
                largestChildIdx = rightChildIdx;
            }
            if (largestChildIdx != parentIdx)
            {
                int node = queue[parentIdx];
                queue[parentIdx] = queue[largestChildIdx];
                queue[largestChildIdx] =  node;
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
