using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimuladorAMedida.src.Enums;
using SimuladorAMedida.src.Interfaces;

namespace SimuladorAMedida.src.Implementations
{
    public class Simulator : ISimulator
    {
        int tempsSimulacio;
        int currentTime;
        int maxTime;
        List<Event> eventList = new List<Event>();
        Creator creator;
        Silla silla;
        Sink sink;

        public StateType state { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int currentOcup { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int PROCESSOR_TIME = 10; 
        public int MAX_OCUP = 4; 
        public int TIME_BETWEEN_ARRIVALS = 2; 

        public Simulator()
        {
            Event simulationStart = new Event(this, 0, EventType.SimulationStart, null);
            eventList.Add(simulationStart);
        } 

        public void Run()
        {
            currentTime = 0;
            maxTime = 100;
            Configurar();
            CrearModel();

            while (currentTime < maxTime)
            {
                eventList.Sort((x, y) => x.time.CompareTo(y.time));
                Event e = eventList.First();
                eventList.Remove(e);
                currentTime = e.time;
                e.@object.TractarEsdeveniment(e);
            }

            Console.WriteLine($"Entidades eliminadas: {this.sink.entitatsEliminades}");
        }

        private void Configurar()
        {
            Console.WriteLine("Introdueix el temps màxim de la simulació:");
        }

        private void CrearModel()
        {
            creator = new Creator(this, TIME_BETWEEN_ARRIVALS);
            silla = new Silla(this, PROCESSOR_TIME, MAX_OCUP);
            sink = new Sink(this);

            silla.InitSilla(sink);
            creator.InitCreator(silla);
        }

        public void TractarEsdeveniment(Event e)
        {
            if(e.type == EventType.SimulationStart)
            {
                creator.SimulationStart();
                silla.SimulationStart();
                sink.SimulationStart();
            }
        }

        public void AfegirEsdeveniment(Event e)
        {
            eventList.Add(e);
        }

        public void SimulationStart()
        {
            throw new NotImplementedException();
        }

        public int GetTimeOfNextDie()
        {
            List<Event> events = eventList.Where(e => e.type == EventType.Muere).ToList();
            int minTime = events.Min(e => e.time);
            return minTime;
        }
    }
}