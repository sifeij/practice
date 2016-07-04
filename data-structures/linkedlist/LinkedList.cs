using System;

namespace DataStructures
{
    public class Node<T> {
        public Node (T value) {
            Value = value;
        }
        internal T Value {get; set;}
        internal Node<T> Next {get; set;}
        internal Node<T> Previous { get; set; }
    }
    public class LinkedList<T> {
        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }

        public int Count {
            get {
                var i = 0;
                var node = Head;
                while (node != null) {
                    i += 1;
                    node = node.Next;
                }
                return i;
            }
        }
        internal void AddLast(Node<T> node) {
            if (node == null) {
               throw new ArgumentNullException(nameof(node));
            }

            if(Tail == null) { // First node added
                Tail = node;
                Head = node;
            }
            else {
                Tail.Next = node;
                node.Previous = Tail;
                Tail = node;
            }
        }

        public void AddLast(T obj) {
            var node = new Node<T>(obj);
            AddLast(node);
        }

        public T RemoveLast() {
            if (Tail == null) {
               throw new Exception("empty list");
            }
            var result = Tail.Value;
            Tail = Tail.Previous;
            if(Tail != null) {
                Tail.Next = null;
            } else {
                Head = null;
            }
            return result;
        }

        public T RemoveFirst() {
            if (Head == null) {
               throw new Exception("empty list");
            }
            var result = Head.Value;
            Head = Head.Next;
            if(Head != null) {
                Head.Previous = null;
            } else {
                Tail = null;
            }
            return result;
        }
    }
}
