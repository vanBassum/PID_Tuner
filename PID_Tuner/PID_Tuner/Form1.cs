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
        Series[] ser;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            ser = new Series[3];
            for (int i = 0; i < ser.Length; i++)
            {
                ser[i] = new Series();
                ser[i].ChartType = SeriesChartType.Line;
                chart1.Series.Add(ser[i]);
            }
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            StepResponceEstimate();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            PIDResponceEstimate();
        }


        void PIDResponceEstimate()
        {
            double fs = 2000;

            Transfer H = new Transfer();
            IPID pid = new PID_TypeC(1/fs);

            H.CooficientsX = new double[] { 0.008632 };
            H.CooficientsY = new double[] { 0.990093 };

            pid.KC = 0.003 * (1- 0.008632) / 0.990093;
            pid.TI = 0.5 / fs;
            pid.TD = 0.5 / fs;

            pid.Reset();
            H.Reset();
            double fb = 0;
            ser[0].Points.Clear();


            for (int i = 0; i < fs; i++)
            {
                fb = pid.Cycle(fb, 1);
                fb = H.Cycle(fb);
                ser[0].Points.AddXY(i, fb);
            }


        }





        void StepResponceEstimate()
        {
            double fs = 2000;
            chart1.Series.Clear();
            Source s = Source.Step();//Source.Sine(10, fs) + Source.Sine(30, fs) / 3 + Source.Sine(50, fs) / 5 + Source.Sine(70, fs) / 7 + Source.Sine(90, fs) / 9 + Source.Sine(110, fs) / 11;
            IIR_DF1 iir = new IIR_DF1();
            iir.SetCoof(0.09716823218728182, -0.19368260411232704, 0.09716823218728185, 1.9518876434994206, -0.9526483729825383);

            iir.Reset();
            s.Reset();

            double fb = 0;

            Series[] ser = new Series[3];
            for (int i=0; i<ser.Length; i++)
            {
                ser[i] = new Series();
                ser[i].ChartType = SeriesChartType.Line;
                chart1.Series.Add(ser[i]);
            }

            List<double> hInpSamples = new List<double>();
            List<double> hOutSamples = new List<double>();

            for (int i = 0; i < fs; i++)
            {
                fb = s.Cycle();
                //if(hInpSamples.Count < 80)
                    hInpSamples.Insert(0, fb);
                fb = iir.Cycle(fb);
                //if (hOutSamples.Count < 80)
                    hOutSamples.Insert(0, fb);
                ser[0].Points.AddXY(i, fb);
            }

            Transfer tEstimate = Transfer.GenerateEstimate(hInpSamples.ToArray(), hOutSamples.ToArray(), 1, 1);

            richTextBox1.Text = tEstimate.ToString();
            iir.Reset();
            s.Reset();
            tEstimate.Reset();

            for (int i = 0; i < fs; i++)
            {
                fb = s.Cycle();
                fb = tEstimate.Cycle(fb);
                ser[1].Points.AddXY(i, fb);
            }


        }



        







        private void Chart1_Click(object sender, EventArgs e)
        {
            
        }

        
    }

    

}
