using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialSolver
{
    class Variable : IVal
    {
        private string Name;
        public string getName()
        {
            return Name;
        }
        public Equation DiffEquation;
        
        private TimeValueNode front; //most recent time
        private TimeValueNode back; 

        public Variable(string Name, Equation DiffEquation, double startingValue, double time = 0)
        {
            this.Name = Name;
            this.DiffEquation = DiffEquation;
            back = new TimeValueNode(time, startingValue);
        }
        public double Slope(double time)
        {
            return DiffEquation.Val(time);
        }
        public double Val(double time)
        {
            if (front == null)
            {
                if (time == back.time)
                    return back.val;
                else
                    throw new InvalidTimeException();
            }
            if (front.time == time)
                return front.val;
            TimeValueNode current = back;
            while (current.foward.time < time)
            {
                current = current.foward;
                if (current.foward == null)
                    throw new InvalidTimeException();
            }
            double timeDiff = current.foward.time - current.time;
            return ((time- current.time) / timeDiff * current.val + (current.foward.time - time) / timeDiff * current.foward.val);

        }
        public IEnumerator<double> Val(IEnumerator<double> times) //times in increasing order
        {
            bool doContinue = true;
            if (front == null)
            {
                doContinue = false;
                if(times.MoveNext())
                {
                    if(times.Current == back.time)
                    {
                        yield return back.val;
                        if (times.MoveNext())
                            throw new InvalidTimeException();
                    }
                }
            }
            if (doContinue)
            {
                TimeValueNode current = back;
                while (times.MoveNext())
                {
                    while (current.foward.time < times.Current)
                    {
                        current = current.foward;
                        if (current.foward == null)
                            throw new InvalidTimeException();
                    }
                    double timeDiff = current.foward.time - current.time;
                    yield return ((times.Current - current.time) / timeDiff * current.val + (current.foward.time - times.Current) / timeDiff * current.foward.val);
                }
            }
        }
        public void AddValue(double time, double val)
        {
            TimeValueNode toAdd = new TimeValueNode(time, val);
            if (front == null)
            {
                front = toAdd;
                back.foward = front;
                front.backward = back;
                return;
            }
            toAdd.backward = front;
            front.foward = toAdd;
            front = toAdd;  
        }
    }
    class TimeValueNode
    {
        public double time;
        public double val;
        public TimeValueNode foward;
        public TimeValueNode backward;

        public TimeValueNode(double time, double val)
        {
            this.time = time;
            this.val = val;
        }
    }
}
