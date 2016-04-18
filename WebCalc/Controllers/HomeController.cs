using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCalc.Models;

namespace WebCalc.Controllers
{
    public class HomeController : Controller
    {
        private const int maxDigits = 10;

        public string DecimalSeparator { get; set; }

        public HomeController()
        {
            DecimalSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
        }

        [HttpGet]
        public ActionResult Index()
        {
            Session["sign"] = "";
            Session["number"] = "0";
            Session["lastResult"] = 0.0;
            Session["lastOperation"] = Operation.Nop;
            Session["operationPerformed"] = true;
            ViewBag.Result = "0";
            ViewBag.Error = false;
            ViewBag.Separator = DecimalSeparator;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string param)
        {
            try {

                if (String.IsNullOrEmpty(param))
                {
                    ViewBag.Result = "0";
                    ViewBag.Error = false;
                    return View();
                }
                else
                {
                    Calculator calc = new Calculator(999999999.9, (double)Session["lastResult"], (Operation)Session["lastOperation"]);
                    if (param[0] == 'D')
                        putDigit(param[1]);
                    else if (param[0] == 'O')
                        performOperation(param[1], calc);
                    else if (param[0] == 'S')
                        putDot();
                    else if (param[0] == 'E')
                        computeResult(calc);
                    else if (param[0] == 'P')
                        computePercent(calc);
                    else if (param[0] == 'C')
                        clear(calc);
                    else if (param[0] == 'R')
                        squareRoot(calc);
                    else
                        negate(calc);

                    var calcState = calc.GetState();
                    Session["lastResult"] = calcState.Item1;
                    Session["lastOperation"] = calcState.Item2;
                    ViewBag.Result = Session["sign"] as string + Session["number"] as string;
                    ViewBag.Error = false;
                    ViewBag.Separator = DecimalSeparator;
                    return View();
                }  
            }
            catch (Exception ex)
            {
                ViewBag.Error = true;
                ViewBag.ErrorDesc = ex.Message;
                ViewBag.Result = "ERR";
                ViewBag.Separator = DecimalSeparator;
                return View();
            }

        }

        private void putDigit(char c)
        {
            string number = Session["number"] as string;

            if ((bool)Session["operationPerformed"])
            {
                number = "" + c;
                Session["sign"] = "";

                Session["operationPerformed"] = false;

            }
            else if (number.Length < maxDigits)
            {
                if (number.Equals("0"))
                    number = "";
                number += c;
            }
            Session["number"] = number;
        }

        private void putDot()
        {
            string number = Session["number"] as string;

            if (!number.Contains("."))
            {
                if ((bool)Session["operationPerformed"])
                {
                    number = "0" + DecimalSeparator;
                    Session["sign"] = "";
                    Session["operationPerformed"] = false;
                }
                else
                    number += DecimalSeparator;
            }
            Session["number"] = number;
        }

        private void negate(Calculator calc)
        {
            string sign = Session["sign"] as string;

            if (sign.Equals(""))
                sign = "-";
            else
                sign = "";
            if ((bool)Session["operationPerformed"])
                calc.negateLastResut();

            Session["sign"] = sign;
        }

        private void squareRoot(Calculator calc)
        {
            double val = prepareArgument(Session["number"] as string, Session["sign"] as string);
            val = calc.getSquareRoot(val);
            interpretResult(val);
            Session["operationPerformed"] = true;
            calc.reset();
        }

        private void clear(Calculator calc)
        {
            Session["number"] = "0";
            Session["sign"] = "";
            Session["operationPerformed"] = true;
            calc.reset();
        }

        private void computePercent(Calculator calc)
        {
            if (!(bool)Session["operationPerformed"])
            {

                double val = prepareArgument(Session["number"] as string, Session["sign"] as string);
                val = calc.getPercentResult(val);
                interpretResult(val);

            }
            Session["operationPerformed"] = true;
            calc.reset();
        }

        private void computeResult(Calculator calc)
        {
            if (!(bool)Session["operationPerformed"])
            {

                double val = prepareArgument(Session["number"] as string, Session["sign"] as string);
                val = calc.getResult(val);
                interpretResult(val);
            }
            Session["operationPerformed"] = true;
            calc.reset();
        }

        private void performOperation(char c, Calculator calc)
        {
            Operation op = Operation.Nop;
            switch (c)
            {
                case '+': op = Operation.Add; break;
                case '-': op = Operation.Sub; break;
                case '*': op = Operation.Mul; break;
                case '/': op = Operation.Div; break;
            }

            if (!(bool)Session["operationPerformed"])
            {
                double val = prepareArgument(Session["number"] as string, Session["sign"] as string);
                val = calc.setArgumentAndOperation(val, op);
                interpretResult(val);
                Session["operationPerformed"] = true;
            }
            else
            {
                calc.setOperation(op);
            }
        }

        private double prepareArgument(string number, string sign)
        {
            double val = 0.0;
            if (number.EndsWith(DecimalSeparator))
                val = Double.Parse(sign + number + "0", CultureInfo.CurrentCulture);
            else
                val = Double.Parse(sign + number, CultureInfo.CurrentCulture);
            return val;
        }

        private void interpretResult(double val)
        {
            if (val < 0)
                Session["sign"] = "-";
            val = Math.Abs(val);

            string result = null;
            if (val == (long)val)
                result = String.Format("{0:D}", (long)val);
            else
                result = String.Format("{0:F}", val);

            if (result.Length > maxDigits)
                Session["number"] = result.Substring(0, maxDigits);
            else
                Session["number"] = result;
        }
    }
}