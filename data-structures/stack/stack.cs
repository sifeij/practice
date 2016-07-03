
namespace DataStructures
{
    class Stack<T> {

        public Stack() {
            _linkedList = new LinkedList<T>();
        }

        public bool IsEmpty { 
            get {
                return _linkedList.Head == null;
            }
        }

        public void Push(T item) {
            _linkedList.AddLast(item);
        }

        public T Pop() {
            return _linkedList.RemoveLast();
        }
        
        readonly LinkedList<T> _linkedList;
    }
}