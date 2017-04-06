using System;

namespace LinearDataStructures
{
    // Common interface for ALL linear data structures

    public interface IContainer<T>
    {
        void MakeEmpty();  // Reset an instance to empty
        bool Empty();      // Test if an instance is empty 
        int Size();        // Return the number of items in an instance
    }

    //-----------------------------------------------------------------------------

    public interface IQueue<T> : IContainer<T>
    {
        void Add(T item);  // Add an item to the back of a queue
        void Remove();     // Remove an item from the front of a queue
        T Front();         // Return the front item of a queue
    }

    //-------------------------------------------------------------------------

    // Queue
    // Implementation:  Singly-linked list

    public class LinkedQueue<T> : IQueue<T>
    {
        protected Node first;    // Reference to the front item
        private Node last;     // Reference to the back item
        protected int numItems;  // Number of items in a queue

        public LinkedQueue()
        {
            MakeEmpty();
        }

        public virtual void Add(T item)
        {
            Node newNode = new Node(item, null);
            if (numItems == 0)
            {
                first = last = newNode;
            }
            else
            {
                last = last.Next = newNode;
            }
            numItems++;
        }

        public  void Remove()
        {
            if (!Empty())
            {
                first = first.Next;
                numItems--;
            }
        }

        public void removeItems(T i)
        {
            if ( 0 !=  numItems )
            {
                Node n = first;
                while ( null != n )
                {
                    if (i.Equals(n.Item))
                    {
                        n = n.Next;
                        numItems--;
                    }//end deleting
                }//end while
            }//end if not empty
                    }//end remove items

        public T Front()
        {
            if (!Empty())
                return first.Item;
            else
                return default(T);
        }

        public T pop()
        {
            T obj = this.Front();
            this.Remove();
            return obj;
        }

        public void MakeEmpty()
        {
            first = last = null;
            numItems = 0;
        }

        public bool Empty()
        {
            return numItems == 0;
        }

        public int Size()
        {
            return numItems;
        }

        public override string ToString()
        {
            string s = "";
            for (Node curr = first; null != curr; curr = curr.Next)
                s += curr.Item.ToString() + ", ";
            return s.TrimEnd();
        }
        
        //---------------------------------------------------------------------

        protected class Node
        {
            private T item;
            private Node next;

            public T Item
            {
                get { return item; }
                set { item = value; }
            }

            public Node Next
            {
                get { return next; }
                set { next = value; }
            }

            public Node(T item, Node next)
            {
                Item = item;
                Next = next;
            }

        }
    }

    //-----------------------------------------------------------------------------

    // Priority Queue
    // Implementation:  Sorted, singly-linked list

    public class LinkedListPriorityQueue<T> : LinkedQueue<T> where T : IComparable
    {
        
        public  override void Add(T item)
        {
            System.Console.WriteLine("adding " + item.GetType().ToString());
            Node current = first;
            Node previous = null;

            while (current != null && current.Item.CompareTo(item) > 0)
            {
                previous = current;
                current = current.Next;
            }

            Node newNode = new Node(item, current);
            if (previous != null)
                previous.Next = newNode;
            else // Empty
                first = newNode;

            numItems++;
        }
                                    }
}
