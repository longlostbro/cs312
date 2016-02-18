using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkRouting
{
    public abstract class IPriorityQueue
    {
        public abstract void decreaseKey();
        public abstract void deletemin();
    }

    public class PriorityQueueArray : IPriorityQueue
    {
        private List<int> key = new List<int>();
        private List<int> value = new List<int>();
        public override void decreaseKey()
        {
            
        }

        public override void deletemin()
        {
            throw new NotImplementedException();
        }
    }

    public class PriorityQueueHeap : IPriorityQueue
    {
        public override void decreaseKey()
        {
            throw new NotImplementedException();
        }

        public override void deletemin()
        {
            throw new NotImplementedException();
        }
    }
}
