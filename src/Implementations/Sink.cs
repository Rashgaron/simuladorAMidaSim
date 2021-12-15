using System;
using SimuladorAMedida.src.Enums;
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

        public StateType state { get; set; }
        public int currentOcup { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void SimulationStart()
        {
            entitatsEliminades = 0;
        }

        public void TractarEsdeveniment(Event e)
        {
            if(e.type == EventType.Muere)
            {
                entitatsEliminades ++;
                e.conexion.state = StateType.Free;
                e.conexion.currentOcup --;
                Console.WriteLine($"Muerte del evento {e.time}");
            }
        }
    }
}