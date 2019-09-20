using System;

namespace PID_Tuner
{
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


        public static Source operator +(Source s1, Source s2) => new Source(new Func<int, double>(n => s1.function(n) + s2.function(n)));
        public static Source operator - (Source s1, Source s2) => new Source(new Func<int, double>(n=> s1.function(n) - s2.function(n)));
        public static Source operator *(Source s1, Source s2) => new Source(new Func<int, double>(n => s1.function(n) * s2.function(n)));
        public static Source operator /(Source s1, Source s2) => new Source(new Func<int, double>(n => s1.function(n) / s2.function(n)));


        public static Source operator *(double s2, Source s1) => new Source(new Func<int, double>(n => s1.function(n) * s2));
        public static Source operator *(Source s1, double s2) => new Source(new Func<int, double>(n => s1.function(n) * s2));
        public static Source operator /(double s2, Source s1) => new Source(new Func<int, double>(n => s2 / s1.function(n)));
        public static Source operator /(Source s1, double s2) => new Source(new Func<int, double>(n => s1.function(n) / s2));
        public static Source operator -(double s2, Source s1) => new Source(new Func<int, double>(n => s2 - s1.function(n)));
        public static Source operator -(Source s1, double s2) => new Source(new Func<int, double>(n => s1.function(n) - s2));
        public static Source operator +(double s2, Source s1) => new Source(new Func<int, double>(n => s2 + s1.function(n)));
        public static Source operator +(Source s1, double s2) => new Source(new Func<int, double>(n => s1.function(n) + s2));

        public static implicit operator Source(double d) => new Source(new Func<int, double>(n=>d));



        public static Source Step() => new Source(n => n > 0 ? 1 : 0);
        public static Source Impulse() => new Source(n => n == 0 ? 1 : 0);
        public static Source Ramp() => new Source(n => 1 * n);
        public static Source Sine(double f, double fs) => new Source(n => Math.Sin(2 * Math.PI * f * n / fs));
        public static Source Square(double f, double fs) => new Source(n => (n % (fs/f)) > (fs / (2 * f)) ? 1 : 0);

        public static Source Noise() => new Source(n => r.NextDouble());

        public static Random r = new Random();
    }

    

}
