using SimuladorAMedida.src.Implementations;

namespace SimuladorAMedida.src.Interfaces
{
    public interface IElemento
    {
         void TractarEsdeveniment(Event e);
         void SimulationStart();
    }
}