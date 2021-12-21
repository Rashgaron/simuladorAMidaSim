namespace SimuladorAMedida.src.Interfaces
{
    public interface IDistribution
    {
        void Init(double minValue, double maxValue, int seed = 0);
        double NextData();

    }
}