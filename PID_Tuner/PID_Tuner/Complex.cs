using MasterLibrary.MathX;
using System;

namespace PID_Tuner
{
    public class Complex
    {
        public Complex()
        {
            Re = 0;
            Im = 0;
        }
        public Complex(double re, double im)
        {
            Re = re;
            Im = im;
        }

        public double Re { get; set; }
        public double Im { get; set; }
        public double Phase { get { return Math.Atan2(Im, Re); } set { double l = Mag; Re = l * Math.Cos(value); Im = l * Math.Sin(value); } }
        public double Mag { get { return Math.Sqrt(Im * Im + Re * Re); } set { double a = Phase; Re = value * Math.Cos(a); Im = value * Math.Sin(a); } }

        public Complex Conjugate()
        {
            return new Complex(Re, -Im);
        }

        public static Complex operator +(Complex a, Complex b) => new Complex(a.Re + b.Re, a.Im + b.Im);
        public static Complex operator -(Complex a, Complex b) => new Complex(a.Re - b.Re, a.Im - b.Im);
        public static Complex operator *(Complex a, Complex b) => new Complex(a.Re * b.Re - a.Im * b.Im, a.Re * b.Im + a.Im * b.Re);
        public static Complex operator *(Complex a, double b) => new Complex(a.Re * b, a.Im * b);
        public static Complex operator *(double a, Complex b) => b * a;
        public static Complex FromPolar(double mag, double phase) => new Complex(mag * Math.Cos(phase), mag * Math.Sin(phase));


        public static implicit operator Complex(V2D v) => new Complex(v.X, v.Y);
        public static implicit operator V2D(Complex v) => new V2D(v.Re, v.Im);



        public override string ToString()
        {
            return Re.ToString("0.000") + (Im < 0 ? "" : "+") + Im.ToString("0.000") + "i";
        }
    }

    

}
