using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string s = Console.ReadLine();
            //string s = "racecar";

            var obj = new Solution();

            foreach (char c in s) {
                obj.pushCharacter(c);
                obj.enqueueCharacter(c);
            }
            
            bool isPalindrome = true;
        
            for (int i = 0; i < s.Length / 2; i++) {
                if (obj.popCharacter() != obj.dequeueCharacter()) {
                    isPalindrome = false;
                    
                    break;
                }
            }
            
            if (isPalindrome) {
                Write("The word, {0}, is a palindrome.", s);
            } else {
                Write("The word, {0}, is not a palindrome.", s);
            }
        }
    }

    public class Solution {

        public Solution() {
            _t = new Stack<char>();
            _q = new Queue<char>();
        }
        
        public void pushCharacter(char ch) {
            _t.Push(ch);
        }
        
        public void enqueueCharacter(char ch) {
            _q.Enqueue(ch);
        }
            
        public char popCharacter() {
            return _t.Pop();
        }
        
        public char dequeueCharacter() {
            return _q.Dequeue();
        }

        Stack<char> _t;
        Queue<char> _q;
    }
}
