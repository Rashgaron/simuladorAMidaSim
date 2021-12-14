using System;
using SimuladorAMedida.src.Interfaces;

namespace SimuladorAMedida.src.Implementations
{
    public class Sink : IElemento
    {
        public Simulator simulator;
        public int entitatsEliminades;

        public Sink (Simulator simulator)
        {
            this.simulator = simulator;
        }

        public void SimulationStart()
        {
            entitatsEliminades = 0;
        }

        public void TractarEsdeveniment(Event e)
        {
            Console.WriteLine("Sink: " + e.time);
        }
    }
}