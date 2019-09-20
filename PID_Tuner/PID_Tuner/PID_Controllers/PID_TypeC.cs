namespace PID_Tuner
{
    public class PID_TypeC : IPID
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


        public PID_TypeC(double ts)
        {
            TS = ts;
            Init();
        }

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

        public double Cycle(double feedback, double setPoint)
        {
            pp = KC * (xk_1 - feedback);              // Kc.(x[k-1]-x[k])
            pi = k0 * (setPoint - feedback);              // (Kc.Ts/Ti).e[k]
            pd = k1 * (2.0 * xk_1 - feedback - xk_2); // (Kc.Td/Ts).(2.x[k-1]-x[k]-x[k-2])
            yk += pp + pi + pd;                 // add y[k-1] + P, I & D actions to y[k]

            xk_2 = xk_1;                        // x[k-2] = x[k-1]
            xk_1 = feedback;                          // x[k-1] = x[k]

            
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

    

}
