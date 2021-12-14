using SimuladorAMedida.src.Enums;
using SimuladorAMedida.src.Interfaces;

namespace SimuladorAMedida.src.Implementations
{
    public class Silla : IElemento
    {
        public Simulator simulator;
        public int cortarPelo;
        public Sink sink;
        public int entitatsProcessades;
        public Silla(Simulator simulator, int cortarPelo)
        {
            this.simulator = simulator;
            this.cortarPelo = cortarPelo;
        }
        public void TractarEsdeveniment(Event e)
        {
            if(e.type == EventType.SillaOcupada)
            {
                ProcessSillaOcupada(e);
            }
        }

        public void InitSilla(Sink sink)
        {
            this.sink = sink;
        }

        private void ProcessSillaOcupada(Event e)
        {
            int tempsOcupacio = cortarPelo;
            simulator.AfegirEsdeveniment(new Event(sink, e.time + tempsOcupacio, EventType.Muere));
        }

        public void SimulationStart()
        {
            entitatsProcessades = 0;
        }
    }
}