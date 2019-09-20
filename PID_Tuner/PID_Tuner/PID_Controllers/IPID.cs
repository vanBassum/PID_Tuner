namespace PID_Tuner
{
    public interface IPID
    {
        double KC { get; set; } // Controller gain
        double TI { get; set; } // Time-constant for I action
        double TD { get; set; } // Time-constant for D action
        double TS { get; set; } // Sample time [sec.]
        double GMA_HLIM { get; set; }
        double GMA_LLIM { get; set; }


        void Init();
        double Cycle(double feedback, double setPoint);
        void Reset();
    }

    

}
