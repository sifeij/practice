using System;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Divide(10, 0); 
        }

        static int Divide(int amount, int divideBy)
        {
            try 
            {
                return amount / divideBy;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.FullMessage());
                return 0;
            }
        }
    }
}
