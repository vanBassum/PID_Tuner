using MasterLibrary.MathX;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PID_Tuner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Transfer H = new Transfer();

            H.Poles.Add(new Complex(0.5, 0.5));
            H.Poles.Add(new Complex(0.5, -0.5));

            H.Zeroes.Add(new Complex(0, 0)); 
            H.Zeroes.Add(new Complex(-1, 0));

            //0.377014140004109, 0, 0, 1, -0.5, // b0, b1, b2, a1, a2
            //0.5, 0.5, 0, 0, 0                 // b0, b1, b2, a1, a2

            //Zie DSP WK6 5e sheet...


            pN_Viewer1.Transfer = H;

            int N = 1000;
            chart1.Series[0].Points.Clear();

            for(int i=0; i<N; i++)
                chart1.Series[0].Points.AddXY((double)i/N ,( H.S(Math.PI * i / N)));
            




        } 
    }

    public class Transfer
    {
        public List<Complex> Poles = new List<Complex> { };
        public List<Complex> Zeroes = new List<Complex> { };


        public double S(double p)
        {
            double h = 1;

            foreach (Complex c in Zeroes)
                h *= (Complex.FromPolar(1, p) - c).Mag;

            foreach (Complex c in Poles)
                h /= (Complex.FromPolar(1, p) - c).Mag;

            return h;
        }


    }
    

}
