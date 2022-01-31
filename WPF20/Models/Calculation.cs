using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF20.Models
{    
    class Calculation
    {
        public static double Operation(double a, double b, string type)
        {
            double result=0;
            switch (type)
            {
                case "+":
                    result = a + b; break;
                case "-":
                    result = a - b; break;
                case "*":
                    result = a * b; break;
                case "/":
                    result = a / b; break;
            }
            return result;
        }        
    }
}
