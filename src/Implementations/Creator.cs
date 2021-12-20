using System;
using SimuladorAMedida.src.Enums;
using SimuladorAMedida.src.Interfaces;

namespace SimuladorAMedida.src.Implementations
{
    public class Creator : IElemento
    {
        public int entitatsCreades;
        public StateType state = StateType.Service;
        public Simulator simulator;
        public int parameter;

        public Barbero barbero;
        public int ocupacionMaxima;

        StateType IElemento.state { get; set; }
        public int currentOcup {get; set;} 
        public Creator(Simulator simulator, int parameter)
        {
            this.simulator = simulator;
            this.parameter = parameter;
        }

        public void InitCreator(Barbero barbero)
        {
            this.barbero = barbero;
        }

        public void SimulationStart()
        {
            entitatsCreades = 0;
            currentOcup = 0;
            ocupacionMaxima = 4;
            ProperaArribada(0);
        }

        public void TractarEsdeveniment(Event e)
        {
            if(e.type == EventType.NextArrival)
                ProcessNextArrival(e);
        }

        private void ProperaArribada(int time)
        {
            var rand = new Random();
            int tempsEntreArribades = rand.Next(1, parameter);
            simulator.AfegirEsdeveniment(new Event(this, time + tempsEntreArribades, EventType.NextArrival, null));
        }

        private void ProcessNextArrival(Event e)
        {
            ProperaArribada(e.time);
            entitatsCreades++;
            if(currentOcup < ocupacionMaxima)
            {
                currentOcup ++;
                simulator.AfegirEsdeveniment(new Event(barbero, e.time, EventType.LlamarBarbero, this));
            }
        }
    }
}