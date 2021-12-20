using System;
using System.Collections.Generic;
using SimuladorAMedida.src.Enums;
using SimuladorAMedida.src.Interfaces;

namespace SimuladorAMedida.src.Implementations
{
    public class Barbero : IElemento
    {
        public Simulator simulator;
        public int tiempoCorte;
        public Sink sink;
        public int entidadesProcesadas;
        public int maxOcup = 4;
        public StateType state { get; set; }
        public int currentOcup { get; set; }
        public List<List<int>> horario;

        public Barbero(Simulator simulator, int tiempoCorte, List<List<int>> horario)
        {
            this.simulator = simulator;
            this.tiempoCorte = tiempoCorte;
            this.horario = horario;            
        }

        public void SimulationStart(){}

        public void InitBarbero(Sink sink)
        {
            this.sink = sink;
            this.state = StateType.Free;
            entidadesProcesadas = 0;
            currentOcup = 0;
        }

        public void TractarEsdeveniment(Event e)
        {
            if(e.type == EventType.LlamarBarbero)
            {
                if(BarberoDisponible(e))
                    ProcessCortarPelo(e);
                else 
                    ProcessBarberosOcupados(e);
            }
            
        }

        private bool BarberoDisponible(Event e)
        {
            int barberosActuales = 0;
            this.horario.ForEach(x => {
                if(x[0] <= e.time && x[1] >= e.time)
                    barberosActuales++;
            });

            return currentOcup < barberosActuales;
        }

        private void ProcessBarberosOcupados(Event e)
        {
            int time = simulator.GetTimeOfNextDie();
            if(time != -1)
                simulator.AfegirEsdeveniment(new Event(this, time + 1, e.type, e.conexion));
        }

        private void ProcessCortarPelo(Event e)
        {
            int tempsOcupacio = tiempoCorte;
            currentOcup ++;
            entidadesProcesadas ++;
            e.conexion.currentOcup --;
            simulator.AfegirEsdeveniment(new Event(sink, e.time + tempsOcupacio, EventType.Muere, this));
        }
    }
}