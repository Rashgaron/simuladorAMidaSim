using System;
using SimuladorAMedida.src.Interfaces;

namespace SimuladorAMedida.src.Distributions
{
    public class Exponential : IDistribution
    {
        private Random r;
        private double minValue;
        private double maxValue;
        public Exponential(double minValue, double maxValue, int seed = 0)
        {
            Init(minValue, maxValue, seed);
        }
        public void Init(double minValue, double maxValue, int seed = 0)
        {
            r = new Random(seed);
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public double NextData()
        {
            double lambda = 1.0;
            double result = this.maxValue;
            do
            {
            double u = r.NextDouble();
            double t = -Math.Log(u) / lambda;
            double increment =
                (maxValue - minValue) / 6.0;
            result = minValue + (t * increment);
            } while (result >= this.maxValue);

            return result;
        }
    }
}