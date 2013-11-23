using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialSolver
{
    class StaticVariable : IVal
    {
        private string Name;
        public string getName()
        {
            return Name;
        }
        private Equation Equation;
        public StaticVariable(string Name, Equation Equation)
        {
            this.Name = Name;
            this.Equation = Equation;
        }
        public double Val(double time)
        {
            return Equation.Val(time);
        }
    }
}
