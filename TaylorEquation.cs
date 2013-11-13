using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialSolver
{
    class TaylorEquation
    {   
        public double[] coefs; // [2, 3, 5] --> 2 + 3x + 5x^2
        public TaylorEquation(double[] coefs)
        {
            this.coefs = coefs;
        }
        public double Val(double x)
        {
            double toReturn = 0;
            for (int i = 0; i < coefs.Count(); i++)
            {
                toReturn += coefs[i] * Power(x, i);
            }
            return toReturn;
        }
        private double Power(double b, int exponent) //return b ^ exponent
        {
            if (exponent == 0)
                return 1;
            if (exponent == 1)
                return b;
            if(exponent % 2 == 0)
            {
                double temp = Power(b, (exponent / 2));
                return temp * temp;
            }
            else
            {
                double temp = Power(b, (exponent / 2));
                return temp * temp * b;
            }
        }
    }
}
