using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CodeTester
{
    [TestClass]
    public class SortedCollections
    {
        [TestMethod]
        public void SimpleSortedSet()
        {
            var s = new SortedSet<int>();

            s.Add(5);
            s.Add(7);
            s.Add(2);

            // no duplicate4s allowed, no exception thrown
            s.Add(2);
        }

        [TestMethod]
        public void SortedSetWithCustomIComparer()
        {
            var s = new SortedSet<string>(new ByStringLengthComparer());

            s.Add("Sarah");
            s.Add("Gentry");
            s.Add("Amrit"); // won't show because it has same length with "Sarah"
        }

        [TestMethod]
        public void SortedDictionary()
        {
            var s = new SortedDictionary<string, int>();

            s.Add("Sarah", 34);
            s.Add("Gentry", 22);
            s.Add("Amrit", 37);

            // will throw exception due to no duplicates are allowed
            // s.Add("Amrit", 37);
        }

        [TestMethod]
        public void SortedList()
        {
            var s = new SortedList<string, int>();

            s.Add("Sarah", 34);
            s.Add("Gentry", 22);
            s.Add("Amrit", 37);

            // will throw exception due to no duplicates are allowed
            // s.Add("Amrit", 37);
        }
    }
}
