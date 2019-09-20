using System;

namespace PID_Tuner
{
    public static class Fourier
    {
        public static Complex[] DFT(double[] x, int fs = 0, bool periodiek = false)
        {
            fs = fs == 0 ? x.Length : fs;
            Complex[] spectrum = new Complex[fs];

            for (int k = 0; k < fs; k++)
            {
                Complex ak = new Complex(0, 0);
                if (fs == 0)
                    fs = x.Length;

                for (int n = 0; n < fs; n++)
                {
                    if (periodiek)
                        ak += Complex.FromPolar(x[n % x.Length], 2 * Math.PI * k * n / fs);
                    else
                        ak += Complex.FromPolar(n<x.Length?x[n % x.Length]:0, 2 * Math.PI * k * n / fs);
                }

                spectrum[k] = ak;
            }
            return spectrum;
        }

        public static double[] IDFT(Complex[] x, int len = 0)
        {
            len = len == 0 ? (x.Length - 1) * 2 : len;
            double[] y = new double[len];


            int p = x.Length;
            if (p > len / 2)
                p = len / 2;

            for (int n = 0; n <= p; n++)
            {
                for (int i = 0; i < len; i++)
                {
                    y[i] += Math.Cos(2 * Math.PI * n / len * i) * x[n].Re;
                    y[i] += Math.Sin(2 * Math.PI * n / len * i) * x[n].Im;
                }
            }

            return y;
        }

    }

    

}
