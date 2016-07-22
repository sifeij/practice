using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CodeTester
{
    [TestClass]
    public class SetOperationsOnIEnumerable
    {
        [TestMethod]
        public void Concat()
        {
            var oneToFive = Enumerable.Range(1, 5);
            var fiveToTen = Enumerable.Range(5, 6);

            // duplicated items are allowed
            var result = oneToFive.Concat(fiveToTen);
        }

        [TestMethod]
        public void Union()
        {
            var oneToFive = Enumerable.Range(1, 5);
            var fiveToTen = Enumerable.Range(5, 6);

            // eliminate any duplicats
            var result = oneToFive.Union(fiveToTen);
        }

        [TestMethod]
        public void Intersect()
        {
            var oneToFive = Enumerable.Range(1, 5);
            var fiveToTen = Enumerable.Range(5, 6);

            // get only common items
            var result = oneToFive.Intersect(fiveToTen);
        }

        [TestMethod]
        public void Except()
        {
            var oneToFive = Enumerable.Range(1, 5);
            var fiveToTen = Enumerable.Range(5, 6);

            // get items from 1st sequence that do not exist in the 2nd sequence
            var result = oneToFive.Except(fiveToTen);

            var names = new[] { "Sarah", "Gentry", "Amrit" };
            var namesResult = names.Except(new[] { "Sarah", "Amrit", "Mark" });
        }
    }
}
