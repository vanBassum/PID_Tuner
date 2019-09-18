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
            double fs = 100;
            Source s = Source.Sine(1, fs);
            FIR fir = new FIR();
            PID pid = new PID();
            Series ser = new Series();
            ser.ChartType = SeriesChartType.Point;


            pid.TS = 1 / fs;

            fir.Reset();
            s.Reset();
            pid.Reset();
            

            

            double fb = 0;

            for(int i=0; i< fs; i++)
            {

                fb = fir.Cycle(pid.Cycle(fb, 2));
                
                ser.Points.AddXY(i, fb);
            }
            chart1.Series.Add(ser);
        }
    }


    public class PID
    {
        public double KC { get; set; } = 0.075; // Controller gain
        public double TI { get; set; } = 0.001; // Time-constant for I action
        public double TD { get; set; } = 0; // Time-constant for D action
        public double TS { get; set; } = 0; // Sample time [sec.]

        public double GMA_HLIM { get; set; } = 100;
        public double GMA_LLIM { get; set; } = 0;


        double xk_1;  // PV[k-1] = Thlt[k-1]
        double xk_2;  // PV[k-2] = Thlt[k-1]
        double k0; // k0 value for PID controller
        double k1; // k1 value for PID controller
        double k2; // k2 value for PID controller
        double k3; // k3 value for PID controller
        double pp; // debug
        double pi; // debug
        double pd; // debug
        double yk;


        public void Init()
        {
            if (TI == 0.0)
            {
                k0 = 0.0;
            }
            else
            {
                k0 = (KC * TS) / TI;
            } // else

            k1 = (KC * TD) / TS;
        }


        public double Cycle(double xk, double tset)
        {
            pp = KC * (xk_1 - xk);              // Kc.(x[k-1]-x[k])
            pi = k0 * (tset - xk);              // (Kc.Ts/Ti).e[k]
            pd = k1 * (2.0 * xk_1 - xk - xk_2); // (Kc.Td/Ts).(2.x[k-1]-x[k]-x[k-2])
            yk += pp + pi + pd;                 // add y[k-1] + P, I & D actions to y[k]

            xk_2 = xk_1;                        // x[k-2] = x[k-1]
            xk_1 = xk;                          // x[k-1] = x[k]

            
            // limit y[k] to GMA_HLIM and GMA_LLIM
            if (yk > GMA_HLIM)
            {
                yk = GMA_HLIM;
            }
            else if (yk < GMA_LLIM)
            {
                yk = GMA_LLIM;
            }
            

            return yk;
        }

        public void Reset()
        {
            Init();
        }



    }




    public class FIR
    {       
        public double[] Cooficients { get; set; } = new double[] {1};

        private List<double> hist = new List<double>();

        public double Cycle(double input)
        {
            double accum = 0;

            hist.Insert(0, input);
            if (hist.Count > Cooficients.Length)
                hist.RemoveRange(Cooficients.Length, hist.Count - Cooficients.Length);

            int to = Math.Min(Cooficients.Length, hist.Count());

            for (int k = 0; k < to; k++)
                accum += Cooficients[k] * hist[k];

            

            return accum;
        }

        public void Reset()
        {
            hist.Clear();
        }
    }




    public class Source
    {
        int actualStep = 0;
        Func<int, double> function;

        public Source(Func<int, double> func)
        {
            function = func;
        }

        public double Cycle()
        {
            return function(actualStep++);
        }

        public void Reset()
        {
            actualStep = 0;
        }



        public static Source Step(double ampl = 1) => new Source(n => n > 0 ? ampl : 0);
        public static Source Impulse(double ampl = 1) => new Source(n => n == 0 ? ampl : 0);
        public static Source Ramp(double ampl = 1) => new Source(n => ampl * n);
        public static Source Sine(double f, double fs, double ampl = 1) => new Source(n => ampl * Math.Sin(2 * Math.PI * f * n / fs));

    }


}
