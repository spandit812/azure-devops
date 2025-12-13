using Microsoft.VisualStudio.TestTools.UnitTesting;
using PipelineApp;

namespace TestProject1
{
    [TestClass]
    public class ReturnOneTests
    {
        [TestMethod]
        public void ReturnOne_ReturnsOne()
        {
            var calc = new Calculator();
            Assert.AreEqual(1, calc.ReturnOne());
        }

        [TestMethod]
        public void ReturnTwo_ReturnsTwo()
        {
            int result = ReturnTwo();
            Assert.AreEqual(2, result);
        }

        private static int ReturnOne() => 1;

        private static int ReturnTwo() => 2;
    }
}