using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] a = { 89, 76, 45, 92, 67, 12, 99 };

            foreach (int num in a)
            {
                Console.Write($"\t {num}");
            }
            Console.WriteLine("\n");
            PrintSortedArray(a);
        }

        static void PrintSortedArray(int[] a) {
            int n = a.Length;
            int numberOfSwaps = 0;
            int temp = 0;
            
            for (int i = 0; i < n; i++) {

                for (int j = 0; j < n - 1; j++) {
                    if (a[j] > a[j + 1]) {
                        temp = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = temp;
                        numberOfSwaps++;
                    }
                }

                if (numberOfSwaps == 0) {
                    break;
                }
            }

            foreach (int num in a)
            {
                Console.Write($"\t {num}");
            }
            Console.Read();
        }
    }
}
