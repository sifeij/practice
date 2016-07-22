using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeTester
{
    [TestClass]
    public class UsingReservedKeywords
    {
        [TestMethod]
        public void AsVariableNames()
        {
            var @namespace = "hello";
            @namespace += " world";
        }
    }
}
