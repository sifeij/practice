using System;

namespace DataStructures
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var queue = new Queue<string>();
            queue.Enqueue("aa");
            queue.Enqueue("bb");
            queue.Enqueue("zz");
            queue.Enqueue("ff");

            while (queue.Count != 0) {
                Console.WriteLine(queue.Dequeue());
            }
        }


    }
}
