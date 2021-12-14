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

        public Simulator()
        {
            Event simulationStart = new Event(this, 0, EventType.SimulationStart);
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
                Event e = eventList.First();
                eventList.Remove(e);
                currentTime = e.time;
                e.@object.TractarEsdeveniment(e);
            }
        }

        private void Configurar()
        {
            Console.WriteLine("Introdueix el temps màxim de la simulació:");

        }

        private void CrearModel()
        {
            creator = new Creator(this, 10);
            silla = new Silla(this, 20);
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
    }
}