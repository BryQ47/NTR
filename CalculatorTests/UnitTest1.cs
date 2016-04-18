using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfCalc.Model;

namespace CalculatorTests
{
    [TestClass]
    public class UnitTest1
    {
        Calculator calc = new Calculator(999999999.0);

        [TestMethod]
        public void SimpleTest()
        {
            calc.reset();
            calc.setArgumentAndOperation(2.0, Operation.Add);
            var result = calc.getResult(2.0);
            Assert.AreEqual(4.0, result);
        }

        [TestMethod]
        public void SquareRootTest()
        {
            calc.reset();
            var result = calc.getSquareRoot(9.0);
            Assert.AreEqual(3.0, result);
        }

        [TestMethod]
        public void PercentTest()
        {
            calc.reset();
            calc.setArgumentAndOperation(200.0, Operation.Mul);
            var result = calc.getPercentResult(12.0);
            Assert.AreEqual(24.0, result);
        }

        [TestMethod]
        public void FailTest1()
        {
            calc.reset();
            try
            {
                calc.setArgumentAndOperation(1.0, Operation.Div);
                var result = calc.getResult(0.0);
                Assert.IsTrue(false);
            }
            catch(Exception) {}
        }

        [TestMethod]
        public void FailTest2()
        {
            calc.reset();
            try
            {
                var result = calc.getSquareRoot(-1.0);
                Assert.IsTrue(false);
            }
            catch (Exception) {}
        }

        [TestMethod]
        public void NegationTest()
        {
            calc.reset();
            calc.setArgumentAndOperation(2.0, Operation.Add);
            calc.getResult(3.0);
            calc.negateLastResut();
            calc.setOperation(Operation.Mul);
            var result = calc.getResult(2.0);
            Assert.AreEqual(-10.0, result);
        }

        [TestMethod]
        public void MultiplicationDivisionTest()
        {
            calc.reset();
            calc.setArgumentAndOperation(1.0, Operation.Mul);
            calc.setArgumentAndOperation(2.0, Operation.Mul);
            calc.setArgumentAndOperation(3.0, Operation.Mul);
            calc.setArgumentAndOperation(4.0, Operation.Div);
            var result = calc.getResult(6.0);
            Assert.AreEqual(2.0 * 3.0 * 4.0 / 6.0, result);
        }

        [TestMethod]
        public void AdditionSubtractionTest()
        {
            calc.reset();
            calc.setArgumentAndOperation(1.0, Operation.Add);
            calc.setArgumentAndOperation(5.0, Operation.Sub);
            calc.setArgumentAndOperation(3.0, Operation.Add);
            calc.setArgumentAndOperation(7.0, Operation.Sub);
            var result = calc.getResult(4.0);
            Assert.AreEqual(1.0+5.0-3.0+7.0-4.0, result);
        }

        [TestMethod]
        public void ComplexTest()
        {
            calc.reset();
            calc.setArgumentAndOperation(1.0, Operation.Add);
            calc.setArgumentAndOperation(10.0, Operation.Mul);
            calc.setArgumentAndOperation(3.5, Operation.Sub);
            var result = calc.getResult(4.0);
            result = calc.getSquareRoot(result);
            Assert.AreEqual(Math.Sqrt((1.0+10.0)*3.5-4.0), result);
        }

        [TestMethod]
        public void ResetTest()
        {
            calc.reset();
            calc.setArgumentAndOperation(1.0, Operation.Add);
            calc.setArgumentAndOperation(10.0, Operation.Mul);
            calc.reset();
            calc.setArgumentAndOperation(0.5, Operation.Add);
            var result = calc.getResult(1.0);
            Assert.AreEqual(1.5, result);
        }

    }
}
