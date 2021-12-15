using System;
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
        public int maxOcup = 4;

        public StateType state { get; set; }
        public int currentOcup { get; set; }

        public Silla(Simulator simulator, int cortarPelo, int maxOcup)
        {
            this.simulator = simulator;
            this.cortarPelo = cortarPelo;
            this.maxOcup = maxOcup;
        }
        public void TractarEsdeveniment(Event e)
        {
            if(state == StateType.Free)
            {
                ProcessSentarseEnSilla(e);
            }else if(state == StateType.Busy)
            {
                ProcessSillaOcupada(e);
            }
        }

        private void ProcessSillaOcupada(Event e)
        {
            int time = simulator.GetTimeOfNextDie();
            simulator.AfegirEsdeveniment(new Event(this, time + 1, e.type, null));
        }

        public void InitSilla(Sink sink)
        {
            this.sink = sink;
            this.currentOcup = 0;
            this.state = StateType.Free;
        }

        private void ProcessSentarseEnSilla(Event e)
        {
            int tempsOcupacio = cortarPelo;
            currentOcup ++;
            state = currentOcup == maxOcup ? StateType.Busy : StateType.Free; 
            entitatsProcessades ++;
            simulator.AfegirEsdeveniment(new Event(sink, e.time + tempsOcupacio, EventType.Muere, this));
        }

        public void SimulationStart()
        {
            entitatsProcessades = 0;
        }
    }
}