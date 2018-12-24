using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Application.Models
{
    public class Calculator
    {
        private int firstNumber;
        private string sign;
        private int lastNumber;

        public Calculator(string firstNumber, string sign, string lastNumber)
        {
            this.firstNumber = int.Parse(firstNumber);
            this.sign = sign;
            this.lastNumber = int.Parse(lastNumber);
        }

        public double Calculate()
        {
            if (sign == "+")
            {
                return this.firstNumber + this.lastNumber;
            }
            else if (sign == "-")
            {
                return firstNumber + lastNumber;
            }

            else if (sign == "*")
            {
                return firstNumber * lastNumber;
            }

            else if (sign == "/")
            {
                return firstNumber / lastNumber;
            }

            else
            {
                throw new Exception("Sign must be - +,-,*,/");
            }
        }
    }
}
