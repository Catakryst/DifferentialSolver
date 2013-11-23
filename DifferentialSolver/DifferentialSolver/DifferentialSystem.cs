using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialSolver
{
    class DifferentialSystem
    {
        private List<Variable> variables = new List<Variable>();
        private List<StaticVariable> staticVariables = new List<StaticVariable>();
        private Plotter p;

        public double time;
        public DifferentialSystem(string[] DiffEquations, string[] StaticEquations, double[] startingVals, double startTime = 0) //x = y^2/3
        {
            for (int i = 0; i < DiffEquations.Count(); i++)
            {
                DiffEquations[i] = DiffEquations[i].Replace(" ", "");
                int EqualsLocation = 0;
                string var = "";
                while (DiffEquations[i][EqualsLocation] != '=')
                {
                    var += DiffEquations[i][EqualsLocation];
                    EqualsLocation++;
                }
                Equation Equation = new Equation(DiffEquations[i].Substring(EqualsLocation + 1));
                variables.Add(new Variable(var, Equation, startingVals[i], startTime)); ;
            }
            for (int i = 0; i < StaticEquations.Count(); i++)
            {
                StaticEquations[i] = StaticEquations[i].Replace(" ", "");
                int EqualsLocation = 0;
                string var = "";
                while (StaticEquations[i][EqualsLocation] != '=')
                {
                    var += StaticEquations[i][EqualsLocation];
                    EqualsLocation++;
                }
                Equation Equation = new Equation(StaticEquations[i].Substring(EqualsLocation + 1));
                staticVariables.Add(new StaticVariable(var, Equation));
            }
            for (int i = 0; i < variables.Count(); i++)
            {
                variables[i].DiffEquation.addVariables(variables);
                variables[i].DiffEquation.addVariables(staticVariables);
            }
        }

        public void Update(double dt)
        {
            double[] ValChange = new double[variables.Count()];
            for (int i = 0; i < variables.Count(); i++)
			{
                ValChange[i] = variables[i].Slope(time) * dt;
            }
            for (int i = 0; i < variables.Count(); i++)
            {
                variables[i].AddValue(time + dt, variables[i].Val(time) + ValChange[i]);
            }
            time += dt;
        }
        public void Paint(System.Drawing.Graphics g, double startTime, double endTime, double stepTime, int waitTime = 0)
        {
            p = new Plotter(variables[0], variables[1]);
            p.Paint(g, startTime, endTime, stepTime, waitTime);
        }
    }
}
