using System;
using System.Collections.Generic;

namespace RPNCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = "5 6 7 * + 1 -";
            var tokensArray = input.Split(' ');
            
            var items = new Stack<int>();
            foreach (string token in tokensArray)
            {
                int item;
                if(int.TryParse(token, out item))
                {
                    items.Push(item);
                }
                else
                {
                    int last1 = items.Pop();
                    int last2 = items.Pop();

                    switch (token)
                    {
                        case "+":
                            items.Push(last2 + last1);
                            break;
                        case "-":
                            items.Push(last2 - last1);
                            break;
                        case "*":
                            items.Push(last2 * last1);
                            break;
                        case "/":
                            items.Push(last2 / last1);
                            break;
                        case "%":
                            items.Push(last2 % last1);
                            break;
                        default:
                            throw new ArgumentException(
                                $"Unrecognized argument: {token}");
                    }
                }
            }

            if(items.Count > 0) {
                Console.WriteLine($"{input} = {items.Pop()}");
            }
            else {
                Console.WriteLine("stack is empty");
            }
        }
    }
}
