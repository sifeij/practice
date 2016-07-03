using System;

namespace DataStructures
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PrintIntegers();
            PrintStrings();
        }

        static void PrintStrings() {
            var linkedList = new LinkedList<string>();
            linkedList.AddLast("this");
            linkedList.AddLast("is");
            linkedList.AddLast("a");
            linkedList.AddLast("test");
            PrintInOrderAndReverse(linkedList);
        }
        static void PrintIntegers() {
            var linkedList = new LinkedList<int>();
            linkedList.AddLast(1);
            linkedList.AddLast(2);
            linkedList.AddLast(3);
            linkedList.AddLast(4);
            PrintInOrderAndReverse(linkedList);
        }

        static void PrintInOrderAndReverse<T>(LinkedList<T> linkedList) {
            var node = linkedList.Head;
            do {
                Console.WriteLine(node.Value);
                node = node.Next;
            } while (node != null);

            node = linkedList.Tail;
            while (node != null) {
                Console.WriteLine(node.Value);
                node = node.Previous;
            }
        }
    }
}
