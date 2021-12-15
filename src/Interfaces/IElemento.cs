using SimuladorAMedida.src.Enums;
using SimuladorAMedida.src.Implementations;

namespace SimuladorAMedida.src.Interfaces
{
    public interface IElemento
    {
        StateType state { get; set; }
        int currentOcup { get; set; }

        void TractarEsdeveniment(Event e);
         void SimulationStart();
    }
}