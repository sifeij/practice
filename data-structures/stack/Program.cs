using System;

namespace DataStructures
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var stack = new Stack<int>();
            stack.Push(1);
            stack.Push(10);
            stack.Push(100);
            stack.Push(1000);

            while (!stack.IsEmpty) {
                Console.WriteLine(stack.Pop());
            }
        }
    }
}
