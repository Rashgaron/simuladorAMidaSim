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
        private List<Event> events;
        double simulationTime;
        // Source generator;
        // Queue queue;
        // Server server;
        // Sink exit;
        
        public Simulator()
        {
            events = new List<Event>();
            simulationTime = 0;
            InitEvents();
        }

        private void InitEvents()
        {
            AddEvent(new Event(0, EventTypes.SimulationStart));
            AddEvent(new Event(4, EventTypes.NewArrival));
            AddEvent(new Event(0, EventTypes.SimulationEnd));
        }
        public double ActualTime()
        {
            throw new NotImplementedException();
        }

        public void AddEvent(Event e)
        {
            events.Add(e);
        }

        public bool CanSimulate()
        {
            return events.Count > 0;
        }

        public Event FirstEvent()
        {
            return events.First();
        }

        public void Run()
        {
            Event e;

            while(CanSimulate())
            {
                e = FirstEvent();
                ProcessEvent(e); 
                simulationTime += e.Time;
                Console.WriteLine($"on time: {simulationTime}");
                events.Remove(e);
            }

        }
        private void ProcessEvent(Event e)
        {
            switch(e.Type)
            {
                case EventTypes.SimulationStart:
                    SimulationStart();
                    break;
                case EventTypes.SimulationEnd:
                    SimulationEnd();
                    break;
                default:
                    e.Process();
                    break;
            }
        }

        public void SimulationEnd()
        {
            Console.Write($"Ending the simulation on time ... ");
        }

        public void SimulationStart()
        {
            Console.Write($"Starting the simulation ...");
        }

        
    }
}