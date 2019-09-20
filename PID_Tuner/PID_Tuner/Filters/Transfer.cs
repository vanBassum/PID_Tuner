using MathNet.Numerics.LinearAlgebra;
using System;

namespace PID_Tuner
{
    /*
    public class Transfer
    {
        public double[] CooficientsX { get; set; } = new double[] { 1 };
        public double[] CooficientsY { get; set; } = new double[] { };

        private History<double> histX;
        private History<double> histY;


        public double Cycle(double input)
        {
            double accum = 0;

            //k=0 => -1 in time
            for (int k = 0; k < CooficientsY.Length; k++)
                accum += CooficientsY[k] * histY[k];

            for (int k = 0; k < CooficientsX.Length; k++)
                accum += CooficientsX[k] * histX[k];


            histX.Add(input);
            histY.Add(accum);

            return accum;
        }

        public void Reset()
        {
            histX = new History<double>(CooficientsX.Length);
            histY = new History<double>(CooficientsY.Length);
        }


        public static Transfer GenerateEstimate(double[] hInSamples, double[] hOutSamples, int numerators, int denumerators)
        {
            int num = numerators;
            int den = denumerators;


            int samples = 6;
            samples = hInSamples.Length - Math.Max(num, den);

            Matrix<double> F = Matrix<double>.Build.Dense(samples, num + den);
            //first sample is most actual sample.

            Vector<double> yShort = Vector<double>.Build.Dense(samples);

            for (int y = 0; y < F.RowCount; y++)
            {
                for (int x = 0; x < F.ColumnCount; x++)
                {
                    int ind = F.RowCount - y + x - (x < num ? 0 : num);
                    F[y, x] = (x < num ? hOutSamples[ind] : hInSamples[ind]);
                }
                yShort[y] = hOutSamples[samples - y - 1];
            }



            Matrix<double> Ft = F.Transpose();
            Matrix<double> F1 = Ft * F;
            Matrix<double> F2 = F1.PseudoInverse();
            Vector<double> V1 = Ft * yShort;
            Vector<double> c = F2 * V1;

            Transfer t = new Transfer();
            t.CooficientsY = c.SubVector(0, num).ToArray();
            t.CooficientsX = c.SubVector(num, den).ToArray();

            return t;
        }

        public override string ToString()
        {
            string s = "a=[";


            foreach(double c in CooficientsY)
                s += c.ToString("0.000000") + ", ";
            s = s.Substring(0, s.Length -2) + "]\r\nb=[";

            foreach (double c in CooficientsX)
                s += c.ToString("0.000000") + ", ";
            s = s.Substring(0, s.Length - 2) + "]\r\n";

            return s;
        }
    }

    */
}
