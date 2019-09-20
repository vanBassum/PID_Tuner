using MathNet.Numerics.LinearAlgebra;

namespace PID_Tuner
{
    public class DiscreteTransfer
    {
        public Vector<double> Numerators { get; set; }              //First instance is 0st order
        public Vector<double> Denominators { get; set; }

        double[] uHist;
        double[] yHist;



        /// <summary>
        /// Resets all buffers of the transfer
        /// </summary>
        public void Reset()
        {
            uHist = new double[Numerators.Count + 1];               //First instance is actual
            yHist = new double[Denominators.Count];
        }

        /// <summary>
        /// Calculates one step
        /// </summary>
        public double Cycle(double u)
        {
            double y = 0;
            int i;

            uHist[0] = u;

            for (i = 0; i < Numerators.Count; i++)
                y += Numerators[i] * uHist[i + 1];


            for (i = 1; i < Denominators.Count; i++)
                y -= Denominators[i] * yHist[i - 1];


            y /= Denominators[0];


            for (i = yHist.Length - 1; i > 0; i--)
                yHist[i] = yHist[i - 1];

            for (i = uHist.Length - 1; i > 0; i--)
                uHist[i] = uHist[i - 1];

            yHist[0] = y;


            return y;
        }


        public DiscreteTransfer()
        {

        }

        public DiscreteTransfer(Vector<double> u, Vector<double> y, int num, int den)
        {
            int i;
            int samples = num > den ? num : den;
            int rows = u.Count - samples;

            Matrix<double> F = Matrix<double>.Build.Dense(rows, num + den);

            for (int row = 0; row < rows; row++)
            {
                for (i = 0; i < den; i++)
                {
                    int ind = row + den - 1 - i;
                    F[row, i] = -y[ind];
                }

                for (i = 0; i < num; i++)
                {
                    int ind = row + num - 1 - i;
                    F[row, i + den] = u[ind];
                }

            }


            Vector<double> yShort = Vector<double>.Build.Dense(rows);

            y.CopySubVectorTo(yShort, samples, 0, rows);


            Matrix<double> n1 = F.Transpose() * F;
            Matrix<double> n2 = n1.Inverse();
            Vector<double> n3 = F.Transpose() * yShort;


            Vector<double> c = n2 * n3;

            //Vector<double> c = F.Solve(yShort);

            Numerators = Vector<double>.Build.Dense(num);
            Denominators = Vector<double>.Build.Dense(den + 1);

            for (i = 0; i < den; i++)
                Denominators[i + 1] = c[i];

            for (i = 0; i < num; i++)
                Numerators[i] = c[i + den];

            Denominators[0] = 1;

        }




        /*
        public Vector<double> Simulate(Vector<double> u)
        {
            double[] uHist = new double[Numerators.Count + 1];        //First instance is actual
            double[] yHist = new double[Denominators.Count];

            Vector<double> y = Vector<double>.Build.Dense(u.Count);


            for (int k = 0; k < u.Count; k++)
            {
                y[k] = 0;
                int i;


                uHist[0] = u[k];

                for (i = 0; i < Numerators.Count; i++)
                    y[k] += Numerators[i] * uHist[i + 1];


                for (i = 1; i < Denominators.Count; i++)
                    y[k] -= Denominators[i] * yHist[i - 1];


                y[k] /= Denominators[0];


                for (i = yHist.Length - 1; i > 0; i--)
                    yHist[i] = yHist[i - 1];

                for (i = uHist.Length - 1; i > 0; i--)
                    uHist[i] = uHist[i - 1];

                yHist[0] = y[k];

            }


            return y;
        }
        */


    }

    

}
