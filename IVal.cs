using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialSolver
{
    interface IVal
    {
        string getName();
        double Val(double time);
    }
}
