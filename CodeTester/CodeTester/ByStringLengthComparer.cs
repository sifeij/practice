using System;
using System.Collections.Generic;

namespace CodeTester
{
    public class ByStringLengthComparer : IComparer<String>
    {
        public int Compare(string a, string b)
        {
            return a.Length.CompareTo(b.Length);
        }
    }
}