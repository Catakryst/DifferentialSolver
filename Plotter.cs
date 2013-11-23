using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DifferentialSolver
{
    class Plotter
    {
        public IVal xAxis;
        public IVal yAxis;

        public double xMin = -10;
        public double xMax = 10;
        public double yMin = -10;
        public double yMax = 10;
        public float xGraphMin = 0;
        public float xGraphMax = 800;
        public float yGraphMin = 800;
        public float yGraphMax = 0;
        public double xScale;
        public double yScale;
        
        public Plotter(IVal xAxis, IVal yAxis)
        {
            this.xAxis = xAxis;
            this.yAxis = yAxis;
        }
        public void Paint(Graphics g, double startTime, double endTime, double stepTime, int waitTime = 0)
        {
            double previousX = xAxis.Val(startTime);
            double previousY = yAxis.Val(startTime);
            for (int step = 1; step <= (endTime - startTime) / stepTime; step++)
            {
                System.Threading.Thread.Sleep(waitTime);
                double time = startTime + step * stepTime;
                g.DrawLine(Pens.Blue, shiftX(previousX), shiftY(previousY), shiftX(xAxis.Val(time)), shiftY(yAxis.Val(time)));
                previousX = xAxis.Val(time);
                previousY = yAxis.Val(time);
            }
        }
        private float shiftX(double x)
        {
            return (float)(xGraphMin + (xGraphMax - xGraphMin) * (x - xMin) / (xMax - xMin));
        }
        private float shiftY(double y)
        {
            return (float)(yGraphMin + (yGraphMax - yGraphMin) * (y - yMin) / (yMax - yMin));

        }

    }
}
