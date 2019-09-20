using System;
using System.Collections.Generic;

namespace PID_Tuner
{
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

            int to = Math.Min(Cooficients.Length, hist.Count);

            for (int k = 0; k < to; k++)
                accum += Cooficients[k] * hist[k];

            

            return accum;
        }

        public void Reset()
        {
            hist.Clear();
        }
    }

    

}
