using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DifferentialSolver
{
    public partial class Form1 : Form
    {
        DifferentialSystem d = new DifferentialSystem(new string[] { "x = 2y", "y = 1" }, new string[] { "z = x + y" }, new double[] { 0, 1 });
        double time = 0;
        public Form1()
        {
            InitializeComponent();
            while(time <= 10)
            {
                d.Update(.01);
                time += .01;
            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            d.Paint(e.Graphics, 0, 10, .1, 0);
        }
    }
}
