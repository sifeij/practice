﻿using System;

namespace ConsoleApplication
{
    public class Program
    {
        static void Main(String[] args) {
        /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */
            Int64 T = Int64.Parse(Console.ReadLine());
            while (T-- > 0) {
                Int64 number = Convert.ToInt64(Console.ReadLine());
                
                if (IsPrimeFast(number)) {
                    Console.WriteLine("Prime");
                } else {
                    Console.WriteLine("Not prime");
                }
            }
        }
        
        static bool IsPrimeFast(Int64 n)
        {
            if ( n == 2 ) {
                return true;
            }
            else if ( n == 1 || (n & 1) == 0) {
                return false;
            }

            for (int i = 3; i <= Math.Sqrt(n); i += 2) {
                if( n % i == 0 ){
                    return false;
                }
            }
            return true;
        }

        static bool IsPrime(Int64 n)
        {
            if (n == 1) return false;
            if (n == 2) return true;

            for (int i = 2; i*i <= n; i++) {
                if (n % i == 0) {
                    return false;
                }
            }

            //for (int i = 2; i <= Math.Ceiling(Math.Sqrt(n)); i++) {
            //   if (n % i == 0)  return false;
            //}

            //for (int i = 2; i <= Math.Sqrt(n); i++) {
            //   if (n % i == 0)  return false;
            //}

            //// the method below will give timeout error

            //for (int i = 2; i < n; i++) {
            //    if (n % i == 0) {
            //        return false;
            //    }
            //}

            return true;
        }
    }
}

/* input
3
12
5
2147483647
*/

/* output
Not prime
Prime
Prime
*/