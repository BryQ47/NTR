using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCalc.Controllers;
using WebCalc.Models;
using System.Web;
using Moq;

namespace WebCalc.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {

        internal class MockHttpSession : HttpSessionStateBase
        {
            Dictionary<string, object> _session = new Dictionary<string, object>();
            public override object this[string name]
            {
                get { return _session[name]; }
                set { _session[name] = value; }
            }
        }

        Mock<ControllerContext> Context;
        MockHttpSession Session;
        HomeController Controller;

        private void SetUpFixture()
        {
            Context = new Mock<ControllerContext>();
            Session = new MockHttpSession();
            Context.Setup(m => m.HttpContext.Session).Returns(Session);
            Session["sign"] = "";
            Session["number"] = "0";
            Session["lastResult"] = 0.0;
            Session["lastOperation"] = Operation.Nop;
            Session["operationPerformed"] = true;
            Controller = new HomeController();
            Controller.ControllerContext = Context.Object;
        }

        [TestMethod]
        public void CalcSimpleTest()
        {
            SetUpFixture();
            ViewResult result = Controller.Index("D1") as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CalcCalcSimpleTest()
        {
            SetUpFixture();
            Controller.Index("D2");
            Controller.Index("O+");
            Controller.Index("D2");
            ViewResult result = Controller.Index("E") as ViewResult;
            Assert.AreEqual("4", result.ViewBag.Result);
        }

        [TestMethod]
        public void CalcSquareRootTest()
        {
            SetUpFixture();
            Controller.Index("D9");
            ViewResult result = Controller.Index("R") as ViewResult;
            Assert.AreEqual("3", result.ViewBag.Result);
        }

        [TestMethod]
        public void CalcPercentTest()
        {
            SetUpFixture();
            Controller.Index("D2");
            Controller.Index("D0");
            Controller.Index("D0");
            Controller.Index("O*");
            Controller.Index("D1");
            Controller.Index("D2");
            ViewResult result = Controller.Index("P") as ViewResult;
            Assert.AreEqual("24", result.ViewBag.Result);
        }

        [TestMethod]
        public void CalcFailTest1()
        {
            SetUpFixture();
            Controller.Index("D1");
            Controller.Index("O/");
            Controller.Index("D0");
            ViewResult result = Controller.Index("E") as ViewResult;
            Assert.IsTrue(result.ViewBag.Error);
        }

        [TestMethod]
        public void CalcFailTest2()
        {
            SetUpFixture();
            Controller.Index("D2");
            Controller.Index("N");
            ViewResult result = Controller.Index("R") as ViewResult;
            Assert.IsTrue(result.ViewBag.Error);
        }

        [TestMethod]
        public void CalcNegationTest()
        {
            Calculator calc = new Calculator(999999999.0);
            calc.setArgumentAndOperation(2.0, Operation.Add);
            calc.getResult(3.0);
            calc.negateLastResut();
            calc.setOperation(Operation.Mul);
            var result = calc.getResult(2.0);
            Assert.AreEqual(-10.0, result);
        }

        [TestMethod]
        public void CalcMultiplicationDivisionTest()
        {
            Calculator calc = new Calculator(999999999.0);
            calc.setArgumentAndOperation(1.0, Operation.Mul);
            calc.setArgumentAndOperation(2.0, Operation.Mul);
            calc.setArgumentAndOperation(3.0, Operation.Mul);
            calc.setArgumentAndOperation(4.0, Operation.Div);
            var result = calc.getResult(6.0);
            Assert.AreEqual(2.0 * 3.0 * 4.0 / 6.0, result);
        }

        [TestMethod]
        public void CalcAdditionSubtractionTest()
        {
            Calculator calc = new Calculator(999999999.0);
            calc.setArgumentAndOperation(1.0, Operation.Add);
            calc.setArgumentAndOperation(5.0, Operation.Sub);
            calc.setArgumentAndOperation(3.0, Operation.Add);
            calc.setArgumentAndOperation(7.0, Operation.Sub);
            var result = calc.getResult(4.0);
            Assert.AreEqual(1.0 + 5.0 - 3.0 + 7.0 - 4.0, result);
        }

        [TestMethod]
        public void CalcComplexTest()
        {
            Calculator calc = new Calculator(999999999.0);
            calc.setArgumentAndOperation(1.0, Operation.Add);
            calc.setArgumentAndOperation(10.0, Operation.Mul);
            calc.setArgumentAndOperation(3.5, Operation.Sub);
            var result = calc.getResult(4.0);
            result = calc.getSquareRoot(result);
            Assert.AreEqual(Math.Sqrt((1.0 + 10.0) * 3.5 - 4.0), result);
        }

        [TestMethod]
        public void CalcResetTest()
        {
            Calculator calc = new Calculator(999999999.0);
            calc.setArgumentAndOperation(1.0, Operation.Add);
            calc.setArgumentAndOperation(10.0, Operation.Mul);
            calc.reset();
            calc.setArgumentAndOperation(0.5, Operation.Add);
            var result = calc.getResult(1.0);
            Assert.AreEqual(1.5, result);
        }
    }
}
