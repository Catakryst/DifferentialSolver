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
        
        private TimeValueNode head; //most recent time

        public Variable(string Name, Equation DiffEquation, double startingValue, double time)
        {
            this.Name = Name;
            this.DiffEquation = DiffEquation;
            head = new TimeValueNode(time, startingValue);
        }
        public double Slope(double time)
        {
            return DiffEquation.Val(time);
        }
        public double Val(double time)
        {
            if (time >= head.time)
                return head.val;
            else
            {
                TimeValueNode current = head;
                while (current.time > time)
                {
                    if(current.next == null)
                        return current.val;
                    current = current.next;
                }
                double timeDiff = current.time - current.next.time;
                return ((time - current.next.time) / timeDiff * current.next.val + (current.time - time) / timeDiff * current.val);
            }
        }
        public void AddValue(double time, double val)
        {
            TimeValueNode toAdd = new TimeValueNode(time, val);
            toAdd.next = head;
            head = toAdd;
        }
    }
    class TimeValueNode
    {
        public double time;
        public double val;
        public TimeValueNode next;

        public TimeValueNode(double time, double val)
        {
            this.time = time;
            this.val = val;
        }
    }
}
