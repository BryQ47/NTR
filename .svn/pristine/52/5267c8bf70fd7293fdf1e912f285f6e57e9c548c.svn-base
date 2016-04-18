using System;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Input;
using WpfCalc.Helpers;
using WpfCalc.Model;
using log4net;

namespace WpfCalc.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _display = "0";
        public string Display
        {
            get
            {
                return _display;
            }
            set
            {
                _display = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Display"));
            }
        }

        private bool _buttonsEnabled = true;
        public bool ButtonsEnabled
        {
            get
            {
                return _buttonsEnabled;
            }
            set
            {
                _buttonsEnabled = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ButtonsEnabled"));
            }
        }

        private string _decimalSeparator;
        public string DecimalSeparator
        {
            get
            {
                return _decimalSeparator;
            }
            set
            {
                _decimalSeparator = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("DecimalSeparator"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private RelayCommand btnCmd;
        public ICommand ButtonCommand { get { return btnCmd; } }

        private const int maxDigits = 10;
        private string number = "0";
        private string sign = "";

        private bool dot = false;
        private Calculator calc = new Calculator(9999999999.0);
        private bool operationPerformed = true;

        private ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MainWindowViewModel()
        {
            DecimalSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
            btnCmd = new RelayCommand(HandleBtnClick);
        }

        private void HandleBtnClick(object parameter)
        {
            string opcode = parameter.ToString();

            try
            {
                if (opcode[0] == 'D')
                    putDigit(opcode[1]);
                else if (opcode[0] == 'O')
                    performOperation(opcode[1]);
                else if (opcode[0] == 'S')
                    putDot();
                else if (opcode[0] == 'E')
                    computeResult();
                else if (opcode[0] == 'P')
                    computePercent();
                else if (opcode[0] == 'C')
                    clear();
                else if (opcode[0] == 'R')
                    squareRoot();
                else
                    negate();

                Display = sign + number;
            }
            catch(Exception ex)
            {
                HandleException(ex.Message);
            }
        }

        private void negate()
        {
            if (sign.Equals(""))
                sign = "-";
            else
                sign = "";
            if (operationPerformed)
                calc.negateLastResut();
            log.Info("Negate");
        }

        private void squareRoot()
        {
            double val = prepareArgument();
            log.Info("Square root of " + val);
            val = calc.getSquareRoot(val);
            interpretResult(val);
            operationPerformed = true;
            dot = false;
            calc.reset();
            log.Info("Result = " + val);
        }

        private void clear()
        {
            number = "0";
            sign = "";
            dot = false;
            operationPerformed = true;
            if (ButtonsEnabled == false)
                ButtonsEnabled = true;
            log.Info("Clear");
            calc.reset();
        }

        private void computePercent()
        {
            if (!operationPerformed)
            {

                double val = prepareArgument();
                log.Info(val + " %");
                val = calc.getPercentResult(val);
                interpretResult(val);
                log.Info("Result = " + val);

            }
            operationPerformed = true;
            dot = false;
            calc.reset();
        }

        private void computeResult()
        {
            if (!operationPerformed)
            {

                double val = prepareArgument();
                log.Info(val + " =");
                val = calc.getResult(val);
                interpretResult(val);
                log.Info("Result = " + val);

            }
            operationPerformed = true;
            dot = false;
            calc.reset();
        }

        private void putDot()
        {
            if (!dot)
            {
                if (operationPerformed)
                {
                    number = "0" + DecimalSeparator;
                    sign = "";
                    operationPerformed = false;
                }
                else
                    number += DecimalSeparator;
                dot = true;
            }
        }

        private void performOperation(char c)
        {
            Operation op = Operation.Nop;
            switch (c)
            {
                case '+': op = Operation.Add; break;
                case '-': op = Operation.Sub; break;
                case '*': op = Operation.Mul; break;
                case '/': op = Operation.Div; break;
            }

            if (!operationPerformed)
            {
                double val = prepareArgument();
                log.Info(val + " " + c);
                val = calc.setArgumentAndOperation(val, op);
                interpretResult(val);
                log.Info("Result = " + val);
                operationPerformed = true;
                dot = false;
            }
            else
            {
                calc.setOperation(op);
                log.Info(c);
            }
        }

        private void putDigit(char c)
        {
            if (operationPerformed)
            {
                number = "" + c;
                sign = "";

                operationPerformed = false;

            }
            else if (number.Length < maxDigits)
            {
                if (number.Equals("0"))
                    number = "";
                number += c;
            }
        }

        private double prepareArgument()
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
                sign = "-";
            val = Math.Abs(val);

            string result = null;
            if (val == (long)val)
                result = String.Format("{0:D}", (long)val);
            else
                result = String.Format("{0:F}", val);

            if (result.Length > maxDigits)
                number = result.Substring(0, maxDigits);
            else
                number = result;
        }

        private void HandleException(string msg)
        {
            Display = "ERR";
            ButtonsEnabled = false;
            log.Error(msg);
        }
    }
}
