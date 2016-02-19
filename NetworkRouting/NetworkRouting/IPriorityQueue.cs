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
            throw new NotImplementedException();
        }

        public override bool isEmpty()
        {
            throw new NotImplementedException();
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
