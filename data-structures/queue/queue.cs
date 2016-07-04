namespace DataStructures 
{
    public class Queue<T> {
        public Queue() {
            _linkedList = new LinkedList<T>();
        }

        public int Count { 
            get {
                return _linkedList.Count;
            }
        }
        public T Dequeue () {
            return _linkedList.RemoveFirst();
        }

        public void Enqueue (T item) {
            _linkedList.AddLast(item);
        }

        readonly LinkedList<T> _linkedList;
    }
}