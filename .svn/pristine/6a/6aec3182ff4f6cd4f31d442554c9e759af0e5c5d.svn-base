using System;

namespace WpfCalc.Model
{
    public enum Operation { Mul, Div, Add, Sub, Nop }

    public class Calculator
    {
        private double lastResult = 0.0;
        private Operation lastOperation = Operation.Nop;
        private double maxResult;

        public Calculator(double maxValue)
        {
            maxResult = maxValue;
        }

        public void reset()
        {
            lastOperation = Operation.Nop;
        }

        public void negateLastResut()
        {
            lastResult = -lastResult;
        }


        public double setArgumentAndOperation(double argument, Operation op)
        {
            lastResult = computeResult(argument);
            lastOperation = op;
            return lastResult;
        }

        public void setOperation(Operation op)
        {
            lastOperation = op;
        }

        public double getResult(double argument)
        {
            lastResult = computeResult(argument);
            return lastResult;
        }

        public double getPercentResult(double argument)
        {
            double result = 0.0;
            switch (lastOperation)
            {
                case Operation.Mul: result = lastResult * (argument / 100.0); break;
                case Operation.Div: result = lastResult / (argument / 100.0); break;
                case Operation.Add: result = lastResult * (1.0 + argument / 100.0); break;
                case Operation.Sub: result = lastResult * (1.0 - argument / 100.0); break;
                case Operation.Nop: result = argument; break;
            }
            checkResult(result);
            lastResult = result;
            return lastResult;
        }

        public double getSquareRoot(double argument)
        {
            double result = Math.Sqrt(argument);
            checkResult(result);
            lastResult = result;
            return result;
        }

        private double computeResult(double arg)
        {
            double result = 0.0;
            switch (lastOperation)
            {
                case Operation.Mul: result = lastResult * arg; break;
                case Operation.Div: result = lastResult / arg; break;
                case Operation.Add: result = lastResult + arg; break;
                case Operation.Sub: result = lastResult - arg; break;
                case Operation.Nop: result = arg; break;
            }
            checkResult(result);
            return result;
        }

        void checkResult(double result)
        {
            bool resultOK = true;
            string msg = "";

            if (Double.IsInfinity(result))
            {
                resultOK = false;
                msg = "Infinity";
            }
            else if (Double.IsNaN(result))
            {
                resultOK = false;
                msg = "NaN";
            }
            else if (maxResult < result)
            {
                resultOK = false;
                msg = "Result exceeds max value";
            }

            if (!resultOK)
                throw new Exception(msg);
        }
    }
}
