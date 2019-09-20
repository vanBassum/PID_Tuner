using System;

namespace PID_Tuner
{
    public class IIR_DF1
    {
        public double[] CooficientsX { get; set; } = new double[] { 1 };
        public double[] CooficientsY { get; set; } = new double[] { };

        private History<double> histX;
        private History<double> histY;

        public void SetCoof(params double[] p)
        {
            if (p.Length % 2 == 0)
                throw new Exception("");

            CooficientsX = new double[p.Length / 2 + 1];
            CooficientsY = new double[p.Length / 2];


            Array.Copy(p, 0, CooficientsX, 0, CooficientsX.Length);
            Array.Copy(p, CooficientsX.Length, CooficientsY, 0, CooficientsY.Length);

        }

        public double Cycle(double input)
        {
            double accum;

            histX.Add(input);

            accum = CooficientsX[0] * histX[0];

            for (int k = 0; k < CooficientsY.Length; k++)
                accum += CooficientsX[k+1] * histX[k+1] + CooficientsY[k] * histY[k];


            
            histY.Add(accum);


            /*
            int l = histX.Count - CooficientsX.Length;
            if (l>0)
                histX.RemoveRange(CooficientsX.Length, l);

            l = histY.Count - CooficientsY.Length;
            if (l > 0)
                histY.RemoveRange(CooficientsY.Length, l);
                */

            return accum;
        }

        public void Reset()
        {
            histX = new History<double>(CooficientsX.Length);
            histY = new History<double>(CooficientsY.Length);
        }


    }

    

}
