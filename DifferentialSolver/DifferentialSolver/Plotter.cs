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

        public double xMin = -30;
        public double xMax = 30;
        public double yMin = -30;
        public double yMax = 30;
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
            double[] timesToCheck = new double[(int)((endTime - startTime) / stepTime) + 1];
            for (int step = 0; step < (int)((endTime - startTime) / stepTime); step++)
            {
                timesToCheck[step] = startTime + step * stepTime;
            }
            IEnumerator<double> xEnum = xAxis.Val(timesToCheck.Cast<double>().GetEnumerator());
            IEnumerator<double> yEnum = yAxis.Val(timesToCheck.Cast<double>().GetEnumerator());
            xEnum.MoveNext(); 
            yEnum.MoveNext();
            double previousX = xEnum.Current;
            double previousY = yEnum.Current;
            while(xEnum.MoveNext() && yEnum.MoveNext())
            {
                System.Threading.Thread.Sleep(waitTime);
                if (xEnum.Current > xMax || xEnum.Current < xMin || yEnum.Current > yMax || yEnum.Current < yMin)
                    continue;
                g.DrawLine(Pens.Blue, shiftX(previousX), shiftY(previousY), shiftX(xEnum.Current), shiftY(yEnum.Current));
                previousX = xEnum.Current;
                previousY = yEnum.Current;
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
