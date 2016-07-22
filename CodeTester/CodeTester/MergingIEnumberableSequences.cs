using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;

namespace CodeTester
{
    [TestClass]
    public class MergingIEnumberableSequences
    {
        [TestMethod]
        public void EqualLengthSequences()
        {
            var names = new[] { "Sarah", "Gentry", "Amrit" };
            var ages = new[] { 20, 22, 36 };
            var namesAndAges = names.Zip(ages, CombineNameAndAge).ToList();
        }

        private string CombineNameAndAge(String name, int age)
        {
            return name + ": " + age;
        }

        [TestMethod]
        public void DifferentLengthSequences()
        {
            var names = new[] { "Sarah", "Gentry", "Amrit", "Mark" };
            var ages = new[] { 20, 22, 36 };
            var namesAndAges = names.Zip(ages, (name, age) => $"{name} - {age}").ToList();
        }

        [TestMethod]
        public void CreatingObjects()
        {
            var names = new[] { "Sarah", "Gentry", "Amrit" };
            var ages = new[] { 20, 22, 36 };
            var namesAndAges = names.Zip(ages, (name, age) => Tuple.Create(name, age));

            //or using method group for a simplied version
            namesAndAges = names.Zip(ages, Tuple.Create);
        }
    }
}
