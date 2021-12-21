using System;
using SimuladorAMedida.src.Distributions;
using SimuladorAMedida.src.Enums;
using SimuladorAMedida.src.Interfaces;

namespace SimuladorAMedida.src.Implementations
{
    public class Creator : IElemento
    {
        public int entitatsCreades;
        public StateType state = StateType.Service;
        public Simulator simulator;
        public int maxTimeBetweenArrivals;

        public Barbero barbero;
        public int ocupacionMaxima;

        StateType IElemento.state { get; set; }
        public int currentOcup {get; set;} 
        public int noHaPodidoEntrar;
        public IDistribution distribution;  
        public Creator(Simulator simulator, int timeBetweenArrivals)
        {
            this.simulator = simulator;
            this.maxTimeBetweenArrivals = timeBetweenArrivals;
        }

        public void InitCreator(Barbero barbero)
        {
            this.barbero = barbero;
            this.distribution = new Exponential();
            this.distribution.Init(1, this.maxTimeBetweenArrivals); 
        }

        public void SimulationStart()
        {
            entitatsCreades = 0;
            currentOcup = 0;
            ocupacionMaxima = 4;
            noHaPodidoEntrar = 0;
            ProperaArribada(0);
        }

        public void TractarEsdeveniment(Event e)
        {
            if(e.type == EventType.NextArrival)
                ProcessNextArrival(e);
        }

        private void ProperaArribada(int time)
        {
            int tempsEntreArribades = Convert.ToInt32(this.distribution.NextData()); 
            simulator.AfegirEsdeveniment(new Event(this, time + tempsEntreArribades, EventType.NextArrival, null, new Cliente(entitatsCreades, time)));
        }

        private void ProcessNextArrival(Event e)
        {
            entitatsCreades++;
            if(currentOcup < ocupacionMaxima)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"El cliente con id {e.cliente.Id} ha entrado en la peluquería en {e.time}");
                currentOcup ++;
                simulator.AfegirEsdeveniment(new Event(barbero, e.time, EventType.LlamarBarbero, this, e.cliente));
            }else{
                noHaPodidoEntrar ++;
            }
            ProperaArribada(e.time);
        }
    }
}